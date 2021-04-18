using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOffscreen : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log(other);
        other.gameObject.SetActive(false);
    }
}
