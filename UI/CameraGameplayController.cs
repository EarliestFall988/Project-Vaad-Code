using System.Collections;
using System.Collections.Generic;
using CMF;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class CameraGameplayController : MonoBehaviour
{

    public float zoomIn = 60;
    public float zoomOut = 90;
    public float zoomSpeed = 0.5f;
    public Camera Cam;
    public Cinemachine.CinemachineVirtualCamera vCam;
    public GameObject GameCameraRig;
    public GameObject GameUI;
    public GameObject MenuUI;
    public GameObject MenuParent;

    public List<string> ignoreTags = new List<string>();

    public CMF.TurnTowardTransformDirection turnTowardTransformDirection;
    public CMF.TurnTowardControllerVelocity turnTowardControllerVelocity;

    public float aimSpeed = 4f;
    public float walkSpeed = 5f;
    public float runSpeed = 7f;

    public CMF.AdvancedWalkerController mover;

    public RectTransform ReticleCenterLocation;
    public Transform BulletSpawnLocation;

    public CharacterAudioEvents AudioEvents;

    public AudioMixerSnapshot GameSnapshot;
    public AudioMixerSnapshot MenuSnapshot;

    public CharacterKeyboardInput KeyboardInput;

    public Transform SourceTarget;

    public Animator anim;
    public WeaponsManager weaponsManager;

    public UnityEvent<bool> OnAiming = new UnityEvent<bool>();
    private bool onAimingTrigger = false;

    public GameState CurrentGameState = GameState.Cutscene;
    public UnityEvent<GameState> OnPlayerStateChange = new UnityEvent<GameState>();

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        KeyboardInput.canMove = CurrentGameState == GameState.Game;
        GameCameraRig.SetActive(CurrentGameState == GameState.Game);
        GameUI.SetActive(CurrentGameState == GameState.Game);

        // MenuUI.SetActive(CurrentGameState == GameState.Menu);
        // MenuParent.SetActive(CurrentGameState == GameState.Menu);
        weaponsManager.gameObject.SetActive(CurrentGameState == GameState.Game);

        if (CurrentGameState == GameState.Game)
        {

            if ((Input.GetMouseButton(1) || Input.GetMouseButton(0)) && !weaponsManager.isSprinting && weaponsManager.CurrentActiveGun != null && !weaponsManager.CurrentActiveGun.isReloading && weaponsManager.CurrentActiveGun.weaponReady)
            {
                SetCameraController(true);

                if (Input.GetMouseButton(1))
                {
                    vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView, weaponsManager.ZoomCameraFOV, Time.deltaTime * zoomSpeed);
                    mover.movementSpeed = aimSpeed;

                    if (!onAimingTrigger)
                    {
                        OnAiming.Invoke(true);
                        onAimingTrigger = true;
                    }
                }
            }
            else if ((!Input.GetMouseButton(1) && !Input.GetMouseButton(0)) || weaponsManager.CurrentActiveGun != null && (weaponsManager.CurrentActiveGun.isReloading || !weaponsManager.CurrentActiveGun.weaponReady))
            {
                vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView, zoomOut, Time.deltaTime * zoomSpeed);
                SetCameraController(false);

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    mover.movementSpeed = runSpeed;
                    anim.SetBool("Sprinting", true);
                    weaponsManager.isSprinting = true;
                }
                else
                {
                    mover.movementSpeed = walkSpeed;
                    anim.SetBool("Sprinting", false);
                    weaponsManager.isSprinting = false;
                }
            }

            if (!Input.GetMouseButton(1))
            {
                if (onAimingTrigger)
                {
                    OnAiming.Invoke(false);
                    onAimingTrigger = false;
                }
            }



            Vector3 loc = GetReticalAimPoint();
            BulletSpawnLocation.LookAt(loc);
            SourceTarget.position = loc;

            AudioEvents.StrafeSpeed = mover.movementSpeed;


        }

        if (CurrentGameState == GameState.Menu || CurrentGameState == GameState.Game)
        {
            if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Escape))
            {
                SetPlayerState(CurrentGameState == GameState.Menu ? GameState.Game : GameState.Menu);
            }
        }
    }

    public void SetPlayerState(GameState State)
    {
        CurrentGameState = State;

        if (State != GameState.Game)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GameSnapshot.TransitionTo(0.5f);
        }

        if (State == GameState.Menu)
        {
            MenuSnapshot.TransitionTo(0.5f);
        }

        OnPlayerStateChange?.Invoke(State);
    }

    public void StartGame()
    {
        SetPlayerState(GameState.Game);
    }

    void SetCameraController(bool turnTowardDirection)
    {
        turnTowardTransformDirection.enabled = turnTowardDirection;
        turnTowardControllerVelocity.enabled = !turnTowardDirection;
    }

    public Vector3 GetReticalAimPoint()
    {
        Ray ray = Cam.ScreenPointToRay(ReticleCenterLocation.position);
        RaycastHit hit;

        // LayerMask masks = LayerMask.GetMask(ignoreTags.ToArray()); <- create a library of items that we are allowed to hit, plug it in here and then the raycast...

        if (Physics.Raycast(ray, out hit, 1000))
        {
            return hit.point;
        }
        else
        {
            return Cam.transform.forward * 1000;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(GetReticalAimPoint(), 0.1f);

        Gizmos.DrawRay(new Ray(BulletSpawnLocation.position, BulletSpawnLocation.forward * 1000));
        Gizmos.DrawSphere(BulletSpawnLocation.position, 0.1f);
        Gizmos.DrawSphere(SourceTarget.transform.position, 0.1f);
        Gizmos.DrawLine(transform.position, SourceTarget.transform.position);
    }
}

public enum GameState
{
    Game,
    Cutscene,
    Menu
}
