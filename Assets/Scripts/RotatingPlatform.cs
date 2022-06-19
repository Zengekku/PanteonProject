using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 50;
    [SerializeField] float forceToUnits = 10;

    private void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.forward);
    }
    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.TryGetComponent<IRunner>(out var runner))
            runner.AddHorizontalForce(forceToUnits);
    }
}
