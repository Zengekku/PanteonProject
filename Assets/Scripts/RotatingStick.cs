using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingStick : MonoBehaviour
{
    [SerializeField] float pushForce = 100;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<IRunner>(out var runner))
        {
            PushUnitBack(other.transform, runner);
        }
    }

    void PushUnitBack(Transform other, IRunner runner)
    {
        runner.AddVerticalForce(-800);
        runner.AddForwardForce(-400);
        var dir = other.position;
        dir.y = 0;
        dir.z = 0;
        runner.Push(dir.normalized * pushForce);
    }
}
//(other.transform.position - other.contacts[0].point)