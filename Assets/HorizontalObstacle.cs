using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalObstacle : MonoBehaviour
{
    [SerializeField] MeshRenderer groundMesh;
    [SerializeField] float horizontalSpeed = 1;
    [SerializeField] float pushForce = 50f;
    float length;
    Vector3 startPos;
    float time;
    bool start;
    private IEnumerator Start()
    {
        
        startPos = transform.position;
        length = groundMesh.bounds.size.x / 2;
        yield return new WaitForSeconds(Random.Range(1, 10));
        start = true;
    }
    private void Update()
    {
        if (!start) return;
        var currentPos = startPos;
        currentPos.x += length * Mathf.Sin(time * horizontalSpeed);
        time += Time.deltaTime;
        transform.position = currentPos;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //other.gameObject.GetComponent<EnemyAI>().StopForce();
           // other.gameObject.GetComponent<Rigidbody>().AddForce((other.transform.position - other.contacts[0].point) * pushForce, ForceMode.VelocityChange);
        }

    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
            other.gameObject.GetComponent<EnemyAI>().Continue();
    }
}
