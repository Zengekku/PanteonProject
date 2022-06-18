using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit, IRunner
{
    void Update()
    {
        if (unitStopped) return;
        if (Input.GetButton("Horizontal"))
        {
            var x = Input.GetAxisRaw("Horizontal");

            force.x = Mathf.Clamp(force.x + speed * x, -maxHorizontalSpeed, maxHorizontalSpeed);
        }
        else if (Input.GetButtonUp("Horizontal"))
            force.x = 0;

    }
    public void AddHorizontalForce(float _force) => force.x = Mathf.Clamp(force.x - _force, -maxHorizontalSpeed, maxHorizontalSpeed);
    public void AddVerticalForce(float _force) => force.y += _force;
    public void AddForwardForce(float _force) => force.z = Mathf.Clamp(force.z + _force, 0, maxForwardSpeed);
    public void StopMoving()
    {
        unitStopped = true;
        rgb.velocity = Vector3.down * 25;
        force.z *= 0.5f;
        force.x = 0;
    }
    public void ContinueMoving() => unitStopped = false;
    public void Push(Vector3 dir) => rgb.AddForce(dir, ForceMode.VelocityChange);
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            GameManager.instance.PassedFinishLine(transform);
            GameManager.instance.ActivateDrawWall();
            StopMoving();
            PlayerToLastLocation();
            animator.Play("Cheer");
        }
    }
    void PlayerToLastLocation()
    {
        var pos = transform.position;
        pos.z += 5f;
        pos.x = 0;
        transform.position = pos;
    }
}
