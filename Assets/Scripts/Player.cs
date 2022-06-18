using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody rgb;
    [SerializeField] float speed = 50f;
    [SerializeField] float gravity = 100f;
    [SerializeField] Vector3 force = Vector3.zero;
    bool stop = false;
    public void GetEffected(Vector3 dir)
    {
        force.x = Mathf.Clamp(force.x - dir.x, -300, 0);
    }
    private void Update()
    {
        if (stop) return;
        if (Input.GetKey(KeyCode.D))
            force.x = Mathf.Clamp(force.x + speed, 0, 400);
        else if (Input.GetKey(KeyCode.A))
            force.x = Mathf.Clamp(force.x - speed, -400, 0);
        else if (Input.GetKeyUp(KeyCode.D))
            force.x = force.x > 0 ? 0 : force.x;
        else if (Input.GetKeyUp(KeyCode.A))
            force.x = force.x < 0 ? 0 : force.x;

    }
    public void StopForce()
    {
        stop = true;
        rgb.velocity = Vector3.zero;
        force.y = -250f;

    }
    public void Continue()
    {
        stop = false;
        force.y = -gravity;
    }
    private void FixedUpdate()
    {
        rgb.velocity = force * Time.fixedDeltaTime;
    }
    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("RotatingPlatform"))
            StopForce();
    }
    private void OnCollisionEnter(Collision other)
    {
        //if (other.gameObject.CompareTag("Ground"))
        Continue();
    }
}
