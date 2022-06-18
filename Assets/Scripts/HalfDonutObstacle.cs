using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfDonutObstacle : MonoBehaviour
{
    [SerializeField] Transform handle;
    [SerializeField] Animator animator;
    Transform caughtUnit;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<IRunner>().StopMoving();
            caughtUnit = other.transform;
            animator.SetTrigger("Catch");
        }
    }
    public void Catch()
    {
        caughtUnit.parent = handle;
        caughtUnit.transform.localPosition = Vector3.zero;
    }
    public void LetGo()
    {
        caughtUnit.parent = null;
        var runner = caughtUnit.GetComponent<IRunner>();
        runner.ContinueMoving();
        runner.Push(Vector3.down * 200);
        caughtUnit = null;
    }

#if UNITY_EDITOR    
    void OnValidate()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }
#endif
}
