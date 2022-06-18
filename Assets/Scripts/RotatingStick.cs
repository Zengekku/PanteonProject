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
            PushUnitBack(other);
        }
    }

    private void PushUnitBack(Collision other)
    {
        var runner = other.gameObject.GetComponent<IRunner>();
        runner.AddVerticalForce(-800);
        runner.AddForwardForce(-400);
        var dir = other.transform.position;
        dir.y = 0;
        dir.z = 0;
        runner.Push(dir.normalized * pushForce);
    }
}
//(other.transform.position - other.contacts[0].point)