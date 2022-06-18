using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObstacle : MonoBehaviour
{
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //other.gameObject.GetComponent<IRunner>().AddForwardForce(-200 * Time.deltaTime);
        }

    }
    /* private void OnCollisionEnter(Collision other)
     {
         if (other.gameObject.CompareTag("Player"))
         {
             other.gameObject.GetComponent<IRunner>().AddForwardForce(-500);
         }
     }*/
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<IRunner>().AddForwardForce(-100);
        }
    }
}
