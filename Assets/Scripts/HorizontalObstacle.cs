using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalObstacle : Obstacle
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
        yield return new WaitForSeconds(Random.Range(1f, 5f));
        start = true;
    }
    void Update()
    {
        if (!start) return;
        LoopMovement();
    }

    private void LoopMovement()
    {
        var currentPos = startPos;
        currentPos.x += length * Mathf.Sin(time * horizontalSpeed);
        time += Time.deltaTime;
        transform.position = currentPos;
    }
    /* void OnCollisionEnter(Collision other)  Push back mechanic
{
    if (other.gameObject.CompareTag("Player"))
    {
        var runner = other.gameObject.GetComponent<IRunner>();
        runner.StopMoving();
        runner.Push(Vector3.back * pushForce);
        runner.ContinueMoving();
    }
}*/

}
