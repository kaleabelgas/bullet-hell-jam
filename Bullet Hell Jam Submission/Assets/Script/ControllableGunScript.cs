using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllableGunScript : GunScript
{
    [SerializeField] private Slider power;

    [SerializeField] private float powerToBuildUp;

    private float powerAmount;

    private float delayTimer;
    private const float delayTimerDefault = .55f;

    ObjectPooler objectPooler;

    private bool shootToggle = false;
    protected override void Start()
    {
        base.Start();

        objectPooler = ObjectPooler.Instance;
        power.maxValue = powerToBuildUp;
    }
    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightAlt)) { shootToggle = !shootToggle; }

        if (Input.GetMouseButton(0) || shootToggle)
        {
            base.Update();
            delayTimer = delayTimerDefault;
        }
        else
            delayTimer -= Time.deltaTime;

        BuildUpPower();

        if (Input.GetMouseButtonDown(1)) { SpecialAttack(); }
    }
    private void BuildUpPower()
    {
        power.value = Mathf.Min(powerAmount, powerToBuildUp);
        if (delayTimer > 0) { return; }

        powerAmount += Time.deltaTime;

    }
    private void SpecialAttack()
    {
        if (powerAmount < powerToBuildUp) { return; }

        //Debug.Log("Woah");
        GameObject[] _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] _bullets = GameObject.FindGameObjectsWithTag("Bullet");

        foreach (GameObject _enemy in _enemies)
        {
            _enemy.GetComponent<ITakeDamage>().GetDamaged(120);
            objectPooler.SpawnFromPool("hit effect", _enemy.transform.position, _enemy.transform.rotation);
            AudioManager.instance.Play("enemy ded");
        }

        foreach (GameObject _bullet in _bullets)
        {
            _bullet.SetActive(false);
        }

        powerAmount = 0;
        delayTimer = delayTimerDefault;
    }
}
