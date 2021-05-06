using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseScript : MonoBehaviour
{
    [SerializeField] private Transform pulse;
    [SerializeField] private float rangeMax = 100;
    [SerializeField] private float pulseSpeed;
    [SerializeField] private float minSize;

    //private RippleEffect rippleEffect;

    private float currentRange;

    private void Awake()
    {
        //rippleEffect = Camera.main.GetComponent<RippleEffect>();
        currentRange = rangeMax;
    }

    private void Update()
    {
        if (currentRange <= rangeMax)
        {
            currentRange += pulseSpeed * Time.deltaTime;
            pulse.localScale = new Vector3(currentRange, currentRange);

            //RaycastHit2D[] raycastHit2DArray = Physics2D.CircleCastAll(transform.position, currentRange, Vector2.zero);
            //foreach(RaycastHit2D raycastHit2D in raycastHit2DArray)
            //{
            //    if(raycastHit2D.collider != null)
            //    {
            //        hitEntities.Add(raycastHit2D.collider);
            //        ClearEnemy(raycastHit2D.transform.gameObject);
            //        Debug.Log(raycastHit2D.transform.gameObject.name);
            //    }
            //}
        }
        else
        {
            pulse.localScale = new Vector3(minSize, minSize);
        }
    }

    public void ClearEnemy(GameObject bullet)
    {
        if (bullet.CompareTag("Bullet"))
        {
            bullet.SetActive(false);
        }
    }

    public void DoPulse()
    {
        currentRange = minSize;
    }
}
