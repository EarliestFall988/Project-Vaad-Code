using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public sealed class Health : MonoBehaviour
{

    [SerializeField]
    private float _healthPoints = 100;

    public float HealthPoints
    {
        get
        {
            return _healthPoints;
        }
    }

    public float MaxHealth { get; private set; } = 100;

    private float _shieldPoints = 0;
    public float ShieldPoints
    {
        get
        {
            return _shieldPoints;
        }
    }

    public float MaxShields { get; private set; } = 100;

    public float ShieldRechargeDelay { get; private set; } = 5;

    public float ShieldRechargeRate { get; private set; } = 5;

    public bool IsDead { get; private set; } = false;

    public UnityEvent OnDeath;
    public UnityEvent<float> OnDamage;
    public UnityEvent<float> OnDamageShields;

    public UnityEvent<float> OnShieldRecharge;

    public UnityEvent<float> OnStart;

    public UnityEvent<float> OnHeal;

    public UnityEvent OnRevive;

    public HealthSettings Settings;

    public Volume normalVolume;
    public Volume damagedVolume;

    [Header("Debug")]
    public bool reset = false;

    private bool setVolumeToDamaged = false;

    void Start()
    {
        ResetHealth();
    }

    void ResetHealth()
    {
        _healthPoints = Settings.HealthPoints;
        MaxHealth = Settings.MaxHealth;
        _shieldPoints = Settings.ShieldPoints;

        MaxShields = Settings.MaxShields;
        ShieldRechargeDelay = Settings.ShieldRechargeDelay;

        if (OnStart != null)
            OnStart.Invoke(HealthPoints / MaxHealth);

        ManageVolumes();

        StartCoroutine(ShieldRechargeDelayRoutine());
    }

    void Update()
    {
        if (reset)
        {
            reset = false;
            ResetHealth();
        }

    }

    void ManageVolumes()
    {
        if (normalVolume != null && damagedVolume != null)
        {

            if (_healthPoints <= 10 && !IsDead && setVolumeToDamaged == false)
            {
                // damagedVolume.weight = 1;
                // normalVolume.weight = 0;

                LeanTween.value(gameObject, (v) =>
                {
                    damagedVolume.weight = v;
                }, damagedVolume.weight, 1, 1f).setEase(LeanTweenType.easeInOutSine).setOnComplete(() =>
                {
                    damagedVolume.weight = 1;
                    normalVolume.weight = 0;
                });

                setVolumeToDamaged = true;
            }
            else if (setVolumeToDamaged == true && _healthPoints > 10)
            {
                LeanTween.value(gameObject, (v) =>
                {
                    damagedVolume.weight = v;
                }, damagedVolume.weight, 0, 1f).setEase(LeanTweenType.easeInOutSine).setOnComplete(() =>
                {
                    damagedVolume.weight = 0;
                    normalVolume.weight = 1;
                });

                setVolumeToDamaged = false;
            }
        }
    }


    public bool Damaged
    {
        get
        {
            return HealthPoints < MaxHealth;
        }
    }


    public bool FullHealth
    {
        get
        {
            return HealthPoints == MaxHealth;
        }
    }

    public void TakeDamage(float damage)
    {

        if (IsDead)
            return;

        if (damage > 0)
        {

            _shieldPoints -= damage;

            if (_shieldPoints < 0)
            {
                _healthPoints += _shieldPoints;
                _shieldPoints = 0;
            }

            if (HealthPoints <= 0)
            {
                Kill();
            }

            if (OnDamage != null)
                OnDamage.Invoke(HealthPoints / MaxHealth);

            OnDamageShields?.Invoke(_shieldPoints / MaxShields);

            ManageVolumes();

            //reset the shield recharge
            if (gameObject.activeInHierarchy && !IsDead)
            {
                StopCoroutine(ShieldRechargeDelayRoutine());
                StopCoroutine(ShieldRechargeRoutine());

                StartCoroutine(ShieldRechargeDelayRoutine());
            }
        }
    }

    IEnumerator ShieldRechargeDelayRoutine()
    {
        yield return new WaitForSeconds(ShieldRechargeDelay);
        StartCoroutine(ShieldRechargeRoutine());
    }

    IEnumerator ShieldRechargeRoutine()
    {
        while (_shieldPoints < MaxShields)
        {
            _shieldPoints += ShieldRechargeRate * Time.deltaTime;
            OnShieldRecharge.Invoke(_shieldPoints / MaxShields);
            yield return null;
        }
    }

    public void Kill()
    {
        if (IsDead)
            return;

        Debug.Log("Killed" + gameObject.name);
        IsDead = true;

        _healthPoints = 0;

        if (OnDeath != null)
        {
            OnDeath.Invoke();
        }

        ManageVolumes();
    }

    public void Heal(float amount)
    {
        if (IsDead)
            return;

        if (amount > 0)
        {
            _healthPoints += amount;

            if (HealthPoints > MaxHealth)
            {
                _healthPoints = MaxHealth;
            }

            if (OnHeal != null)
                OnHeal.Invoke(HealthPoints / MaxHealth);

            ManageVolumes();
        }
    }

    public void Revive()
    {
        if (!IsDead)
            return;

        Debug.Log("Revived" + gameObject.name);


        _healthPoints = MaxHealth / 2;

        if (OnRevive != null)
        {
            OnRevive.Invoke();
        }

        ManageVolumes();

        StartCoroutine(ShieldRechargeRoutine());
    }

    public void SetMaxHealth()
    {

        if (IsDead)
            return;

        _healthPoints = MaxHealth;

        if (HealthPoints < MaxHealth)
        {
            if (OnHeal != null)
                OnHeal.Invoke(HealthPoints / MaxHealth);
        }

        ManageVolumes();
    }

    public float GetHealth()
    {

        if (IsDead)
            return 0;

        return HealthPoints;
    }
}
