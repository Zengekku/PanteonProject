using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateZAxis : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 50;
    [SerializeField] float forceToUnits = 10;
    private void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.forward);
    }
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<EnemyAI>().GetEffected(Vector3.left * forceToUnits);
        }
    }
}
