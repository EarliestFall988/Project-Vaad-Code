using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Health))]
public class SharpShooter : MonoBehaviour
{

    public float damage = 10f;
    public float speed = 5f;
    public float retreatSpeed = 5f;

    private NavMeshAgent agent;
    private Health health;

    public Animator Anim;

    public List<Transform> CoverPoints = new List<Transform>();
    public List<Transform> VantagePoints = new List<Transform>();

    public List<Health> Targets = new List<Health>();

    public float hideWhenHealthIsThisLow = 25;
    public float firingRange = 50;

    public Vector2 goToNewVantagePointTime = new Vector2(1, 5);

    private bool idle = true;

    public Transform WeaponAimPoint;
    public Transform WeaponFireTransform;

    public RigBuilder builder;

    private Vector3 lastKnownPosition = Vector3.zero;

    public Vector2 AimSway = new Vector2(0.1f, 0.5f);

    public Vector2 fireRate = new Vector2(0.25f, 1);
    private float fireRateStore = 0;

    public float ammoAmount = 5;
    private float ammoAmountStore = 0;

    public float reloadTime = 5;
    private float reloadTimeStore = 0;

    public LineRenderer Renderer;

    public List<AudioClip> WeaponFireAudioClips = new List<AudioClip>();
    public AudioSource WeaponAudioSource;
    public AudioSource VoiceAudioSource;

    public List<AudioClip> HurtAudioClips = new List<AudioClip>();

    private Vector3 lastPosition = Vector3.zero;

    private GameObject _nearestTarget = null;

    public GameObject Gun;

    private float LastPlayedHurtTimer = 0;
    private float LastPlayedHurtTimerStore = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        health = GetComponent<Health>();
        PickRandomVantagePoint();

        lastKnownPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {


        LastPlayedHurtTimer += Time.deltaTime;

        if (health.HealthPoints <= 0)
        {
            agent.isStopped = true;
            return;
        }

        var moveDirection = transform.position - lastPosition;
        var direction = Vector3.Normalize(moveDirection);

        Anim.SetFloat("x", direction.x);
        Anim.SetFloat("z", direction.z);


        lastPosition = this.transform.position;


        float nearestTarget = GetDistanceFromNearestTarget();

        if ((health.HealthPoints < hideWhenHealthIsThisLow && health.ShieldPoints <= 25 && GetAverageHealthFromTargets() >= hideWhenHealthIsThisLow) || nearestTarget < 5) // hide when the odds are great
        {

            // Debug.Log("hiding: " + health.HealthPoints + " enemy " + GetAverageHealthFromTargets() + " distance " + GetDistanceFromNearestTarget());

            var hideLocation = GetNearestCoverPoint();
            agent.isStopped = false;
            agent.updateRotation = true;
            agent.SetDestination(hideLocation);

            Gun.gameObject.SetActive(false);
            SetIKRig(false);
            agent.speed = retreatSpeed;
            Anim.speed = 2;

            return;
        }
        else
        {
            Gun.gameObject.SetActive(true);
            SetIKRig(true);
            Anim.speed = 1;
        }


        agent.speed = speed;


        // Debug.Log("ready to fire: " + health.HealthPoints + " enemy " + GetAverageHealthFromTargets() + " distance " + GetDistanceFromNearestTarget());

        // agent.updateRotation = Targets.Count <= 0 || GetDistanceFromNearestTarget() >= firingRange; // FIX THIS
        LookAtTarget();


        if (nearestTarget <= firingRange && lastKnownPosition != Vector3.zero)
        {
            FireAtLastKnownPosition();
        }


        if (agent.remainingDistance <= 1 && !idle)
        {
            StartCoroutine(WaitToPickNewVantagePoint(UnityEngine.Random.Range(goToNewVantagePointTime.x, goToNewVantagePointTime.y)));
            idle = true;
        }
    }

    Vector3 GetNearestCoverPoint()
    {

        if (CoverPoints.Count == 0)
            return this.transform.position;

        float dist = 0;
        Transform nearestCoverPoint = CoverPoints[0];

        foreach (var x in CoverPoints)
        {

            float distance = Vector3.Distance(transform.position, x.position);

            if (distance < dist)
            {
                dist = distance;
                nearestCoverPoint = x;
            }
        }

        return nearestCoverPoint.position;
    }


    IEnumerator WaitToPickNewVantagePoint(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("picking new vantage point");
        PickRandomVantagePoint();
    }


    public void Hit()
    {
        if (LastPlayedHurtTimer >= LastPlayedHurtTimerStore)
        {
            Anim.SetTrigger("hit");
            PlayRandomHurtAudioClips();
            LastPlayedHurtTimer = 0;

            if (GetDistanceFromNearestTarget() > firingRange)
            {
                agent.SetDestination(lastKnownPosition);
            }
        }
    }

    public void Death()
    {
        Anim.SetTrigger("death");
        Anim.SetLayerWeight(1, 0);
        Anim.SetLayerWeight(2, 0);
        Gun.SetActive(false);

        agent.isStopped = true;

        Anim.speed = 1;

        SetIKRig(false);
    }

    void SetIKRig(bool enabled)
    {
        foreach (var x in builder.layers)
        {
            x.active = enabled;
        }

        builder.enabled = enabled;
    }

    void PickRandomVantagePoint()
    {
        if (VantagePoints.Count == 0)
            return;

        int randomIndex = UnityEngine.Random.Range(0, VantagePoints.Count);

        agent.SetDestination(VantagePoints[randomIndex].position);
        idle = false;
    }

    float GetDistanceFromNearestTarget()
    {

        if (Targets.Count <= 0)
            return 1000;

        float dist = -1;

        foreach (var x in Targets)
        {

            float distance = Vector3.Distance(transform.position, x.transform.position);

            if (distance < dist || dist == -1)
            {
                dist = distance;
            }
        }

        return dist;
    }

    float GetAverageHealthFromTargets()
    {

        if (Targets.Count <= 0)
            return 0;

        float average = 0;

        foreach (var x in Targets)
        {
            average = average + x.HealthPoints;
        }

        return average / Targets.Count;
    }

    void LookAtTarget()
    {

        if (health.HealthPoints <= hideWhenHealthIsThisLow) // might want to check for a state instead??
            return;


        if (Targets.Count <= 0 || GetDistanceFromNearestTarget() > firingRange)
        {
            agent.updateRotation = true;
            return;
        }
        else
        {
            agent.updateRotation = false;
        }

        float dist = -1;
        Health nearestTarget = Targets[0];

        foreach (var x in Targets)
        {

            float distance = Vector3.Distance(transform.position, x.transform.position);

            if (distance < dist || dist == -1)
            {
                dist = distance;
                nearestTarget = x;
            }
        }



        var ray = new Ray(WeaponAimPoint.transform.position, WeaponAimPoint.transform.forward);

        Debug.DrawRay(WeaponAimPoint.position, WeaponAimPoint.transform.forward * firingRange, Color.red);

        var results = Physics.SphereCastAll(ray, 1f, firingRange);
        List<RaycastHit> hits = new List<RaycastHit>();
        hits.AddRange(results);

        hits.Sort((x, y) =>
        {
            if (x.distance < y.distance)
                return -1;
            else if (x.distance > y.distance)
                return 1;
            else
                return 0;
        });


        for (int i = hits.Count - 1; i >= 0; i--)
        {

            var x = hits[i];

            if (x.collider.gameObject == this.gameObject)
                hits.RemoveAt(i);
            else
                Debug.Log(i + " " + x.transform.gameObject.name);
        }

        // if (nearestTarget == hits[0].collider.gameObject)
        // {
        WeaponAimPoint.LookAt(nearestTarget.transform.position + new Vector3(0, 1, 0));
        lastKnownPosition = nearestTarget.transform.position;

        _nearestTarget = nearestTarget.gameObject;

        // }
        // else if (lastKnownPosition != Vector3.zero)
        // {
        //     WeaponFirePoint.LookAt(lastKnownPosition);
        // }
        // else
        // {
        //     WeaponFirePoint.LookAt(nearestTarget.transform);
        //     lastKnownPosition = nearestTarget.transform.position; //wanna set this precident???
        // }


        Vector3 correctedNearestTarget = new Vector3(lastKnownPosition.x, transform.position.y, lastKnownPosition.z);
        transform.LookAt(correctedNearestTarget);
    }

    private void FireAtLastKnownPosition()
    {



        if (ammoAmount <= 0)
        {

            if (Anim.GetBool("reloading") == false)
            {
                Anim.SetBool("reloading", true);
            }

            if (reloadTimeStore <= 0)
            {
                ammoAmount = 5;
                reloadTimeStore = reloadTime;
            }
            else
            {
                reloadTimeStore -= Time.deltaTime;
            }
            return;
        }

        if (Anim.GetBool("reloading") == true)
        {
            Anim.SetBool("reloading", false);
        }




        if (fireRateStore <= 0)
        {

            RaycastHit hit;

            Vector3 randomSway = new Vector3(UnityEngine.Random.Range(-AimSway.x, AimSway.x), UnityEngine.Random.Range(-AimSway.y, AimSway.y), 0);

            var ray = new Ray(WeaponFireTransform.position, WeaponFireTransform.transform.forward + randomSway);

            if (Physics.Raycast(ray, out hit, firingRange))
            {
                if (hit.collider.gameObject == _nearestTarget)
                {
                    lastKnownPosition = hit.point;

                    var health = hit.collider.gameObject.GetComponent<Health>();

                    if (health)
                        health.TakeDamage(damage);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }

            Debug.DrawRay(WeaponFireTransform.position, WeaponFireTransform.transform.forward + randomSway * firingRange, Color.blue, 1f);

            fireRateStore = UnityEngine.Random.Range(fireRate.x, fireRate.y);
            ammoAmount--;
            Renderer.gameObject.SetActive(true);
            Renderer.SetPositions(new Vector3[] { WeaponAimPoint.position, lastKnownPosition });
            PlayRandomFireWeaponAudioClip();
            StartCoroutine(DisableRenderer());
        }
        else
        {
            fireRateStore -= Time.deltaTime;
        }

    }

    public void PlayRandomFireWeaponAudioClip()
    {
        if (WeaponFireAudioClips.Count == 0)
            return;

        int randomIndex = UnityEngine.Random.Range(0, WeaponFireAudioClips.Count);
        WeaponAudioSource.PlayOneShot(WeaponFireAudioClips[randomIndex]);
    }


    public void PlayRandomHurtAudioClips()
    {
        if (HurtAudioClips.Count == 0)
            return;

        int randomIndex = UnityEngine.Random.Range(0, HurtAudioClips.Count);
        VoiceAudioSource.PlayOneShot(HurtAudioClips[randomIndex]);
    }


    IEnumerator DisableRenderer()
    {
        yield return new WaitForSeconds(0.2f);
        Renderer.gameObject.SetActive(false);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        foreach (var x in VantagePoints)
        {
            Gizmos.DrawWireSphere(x.position, 0.5f);
            Gizmos.DrawLine(transform.position, x.position);
        }

        for (int i = 1; i < VantagePoints.Count; i++)
        {
            Gizmos.DrawLine(VantagePoints[i - 1].position, VantagePoints[i].position);
        }


        Gizmos.color = Color.red;

        foreach (var x in CoverPoints)
        {
            Gizmos.DrawWireCube(x.position, Vector3.one * 2);
            Gizmos.DrawLine(transform.position, x.position);
        }

        for (int i = 1; i < CoverPoints.Count; i++)
        {
            Gizmos.DrawLine(CoverPoints[i - 1].position, CoverPoints[i].position);
        }
    }
}
