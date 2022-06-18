using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfDonutObstacle : MonoBehaviour
{
    [SerializeField] Transform handle;
    Transform caughtUnit;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            caughtUnit = other.transform;
            caughtUnit.transform.position = handle.position;
            other.GetComponent<Rigidbody>().isKinematic = true;
            //other.GetComponent<Player>().StopForce();
            GetComponent<Animator>().SetTrigger("Catch");
        }
    }
    public void Catch()
    {
        caughtUnit.parent = handle;
    }
    public void LetGo()
    {
        caughtUnit.parent = null;
        caughtUnit.GetComponent<Rigidbody>().isKinematic = false;
    }
}
