using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingStick : MonoBehaviour
{
    [SerializeField] float pushForce = 100;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().enabled = false;
            other.gameObject.GetComponent<Rigidbody>().AddForce((other.transform.position - other.contacts[0].point) * pushForce,ForceMode.Impulse);
        }
    }
}
