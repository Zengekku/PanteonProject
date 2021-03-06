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

            PushUnitBack(other, runner);
        }
    }

    void PushUnitBack(Collision other, IRunner runner)
    {
        runner.AddVerticalForce(-800);
        var dir =  -other.transform.position;
        dir.y = 0;
        dir.z = 0;
        runner.Push(dir * pushForce);
    }
}
//(other.transform.position - other.contacts[0].point)