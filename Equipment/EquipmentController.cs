using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentController : MonoBehaviour
{

    [Header("Character Progression")]
    public TMPro.TMP_Text PlayerNameText;
    public TMPro.TMP_Text PlayerLevelAndExperienceText;

    [Header("Character Health and Shields")]
    public float HealthNormalized;
    public HealthBarUIController EquipmentMenuUIController;

    private Health playerHealth;

    public float ShieldsNormalized;
    public HealthBarUIController ShieldsUIController;


    [Header("Guns")]
    public Gun CurrentPrimaryGun;
    public List<GunData> PrimaryGuns = new List<GunData>();
    public Gun CurrentSpecialGun;
    public List<GunData> SpecialGuns = new List<GunData>();

    [Space(5)]
    public Image PrimaryGunImage;
    public Image SpecialGunImage;

    private int _availableObiCanisterSlots = 3;


    public delegate GunData[] GetGuns(string location);
    public static event GetGuns OnGetGuns;

    public int AvailableObiCanisterSlots
    {
        get { return _availableObiCanisterSlots; }
        set
        {
            _availableObiCanisterSlots = value;
            if (_availableObiCanisterSlots < 0)
            {
                _availableObiCanisterSlots = 0;
            }

            if (_availableObiCanisterSlots > ObiCanisterSlotButtons.Count)
            {
                _availableObiCanisterSlots = ObiCanisterSlotButtons.Count;
            }

            for (int i = 0; i < ObiCanisterSlotButtons.Count; i++)
            {
                if (i < _availableObiCanisterSlots)
                {
                    ObiCanisterSlotButtons[i].interactable = true;
                }
                else
                {
                    ObiCanisterSlotButtons[i].interactable = false;
                }
            }
        }
    }

    public List<Button> ObiCanisterSlotButtons = new List<Button>();

    void Start()
    {



        if (CurrentPrimaryGun != null)
        {
            PrimaryGunImage.sprite = CurrentPrimaryGun.settings.WeaponSilhouette;
        }

        if (CurrentSpecialGun != null)
        {
            SpecialGunImage.sprite = CurrentSpecialGun.settings.WeaponSilhouette;
        }

        AvailableObiCanisterSlots = _availableObiCanisterSlots;
        CollectWeapons();

        Debug.Log("collecting guns");

        if (CharacterEventBus.main == null)
            return;

        if (CharacterEventBus.main.health == null)
            return;

        playerHealth = CharacterEventBus.main.health;
    }
    #region health
    public void SetHealthNormalized(float health)
    {
        HealthNormalized = health;
    }

    public void SetShieldsNormalized(float shields)
    {
        ShieldsNormalized = shields;
    }

    #endregion

    public void SetPrimaryGun(Gun gun)
    {
        CurrentPrimaryGun = gun;
        PrimaryGunImage.sprite = gun.settings.WeaponSilhouette;
    }

    public void SetSpecialGun(Gun gun)
    {
        CurrentSpecialGun = gun;
        SpecialGunImage.sprite = gun.settings.WeaponSilhouette;
    }


    public void Update()
    {

        if (playerHealth == null || playerHealth.IsDead)
            return;

        SetHealthNormalized(playerHealth.HealthPoints);
        SetShieldsNormalized(playerHealth.ShieldPoints);

        EquipmentMenuUIController.SetHealth(HealthNormalized);
        ShieldsUIController.SetHealth(ShieldsNormalized);

        CurrentPrimaryGun = CharacterEventBus.main.weaponsManager.PrimaryGun;
        PrimaryGunImage.sprite = CurrentPrimaryGun.settings.WeaponSilhouette;

        CurrentSpecialGun = CharacterEventBus.main.weaponsManager.SpecialGun;
        SpecialGunImage.sprite = CurrentSpecialGun.settings.WeaponSilhouette;
    }

    private void CollectWeapons()
    {
        if (OnGetGuns != null)
        {
            GunData[] guns = OnGetGuns("primary");
            PrimaryGuns.Clear();
            PrimaryGuns.AddRange(guns);

            foreach (var x in guns)
            {
                if (x != null)
                {
                    Debug.Log(x.Gun.name);
                    if (CurrentPrimaryGun != null && CurrentPrimaryGun == x.Gun)
                    {
                        SetPrimaryGun(x.Gun);
                        PrimaryGuns.Remove(x);
                    }
                }
            }

            guns = OnGetGuns("special");
            SpecialGuns.Clear();
            SpecialGuns.AddRange(guns);

            foreach (var x in guns)
            {
                if (x != null)
                {
                    Debug.Log(x.Gun.name);
                    if (CurrentSpecialGun != null && CurrentSpecialGun == x.Gun)
                    {
                        SetSpecialGun(x.Gun);
                        SpecialGuns.Remove(x);
                    }
                }
            }
        }
    }

    void OnEnable()
    {
        PlayerNameText.text = PlayerProgression.main.PlayerName;
        PlayerLevelAndExperienceText.text = "Level " + PlayerProgression.main.Level + " (" + PlayerProgression.main.Experience + "/" + PlayerProgression.main.ExperienceToNextLevel + ")";
    }
}
