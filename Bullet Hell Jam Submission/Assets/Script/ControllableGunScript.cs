using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllableGunScript : GunScript
{
    [SerializeField] private Slider power;

    [SerializeField] private float powerToBuildUp;

    private bool isShooting;
    private float powerAmount;
    protected override void Start()
    {
        base.Start();

        power.maxValue = powerToBuildUp;
    }
    protected override void Update()
    {
        if (Input.GetMouseButton(0))
        {
            base.Update();
            isShooting = true;
        }
        else
            isShooting = false;

        BuildUpPower();

        if (Input.GetMouseButtonDown(1)) { SpecialAttack(); }
    }
    private void BuildUpPower()
    {
        if (isShooting) { return; }

        powerAmount += Time.deltaTime;
        power.value = Mathf.Min(powerAmount, powerToBuildUp);

    }
    private void SpecialAttack()
    {
        if (powerAmount < powerToBuildUp) { return; }

        Debug.Log("Woah");
        GameObject[] _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] _bullets = GameObject.FindGameObjectsWithTag("Bullet");

        foreach (GameObject _enemy in _enemies)
        {
            _enemy.GetComponent<ITakeDamage>().GetDamaged(1000, gameObject);
        }

        foreach (GameObject _bullet in _bullets)
        {
            _bullet.SetActive(false);
        }

        powerAmount = 0;
    }
}
