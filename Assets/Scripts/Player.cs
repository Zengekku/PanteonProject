using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit, IRunner
{
    void Update()
    {
        if (stopped) return;

        if (Input.GetKey(KeyCode.D))
            force.x = Mathf.Clamp(force.x + speed, 0, maxHorizontalSpeed);
        else if (Input.GetKey(KeyCode.A))
            force.x = Mathf.Clamp(force.x - speed, -maxHorizontalSpeed, 0);
        else if (Input.GetKeyUp(KeyCode.D))
            force.x = force.x > 0 ? 0 : force.x;
        else if (Input.GetKeyUp(KeyCode.A))
            force.x = force.x < 0 ? 0 : force.x;
    }
    void LateUpdate() => transform.rotation = Quaternion.Euler(Vector3.zero);
    public void AddHorizontalForce(float _force) => force.x = Mathf.Clamp(force.x - _force, -maxHorizontalSpeed, maxHorizontalSpeed);
    public void AddVerticalForce(float _force) => force.y += _force;
    public void AddForwardForce(float _force) => force.z = Mathf.Clamp(force.z + _force, 0, maxForwardSpeed);
    public void StopMoving()
    {
        stopped = true;
        rgb.velocity = Vector3.down * 25;
        force.z *= 0.5f;
        force.x = 0;
    }
    public void ContinueMoving() => stopped = false;
    public void Push(Vector3 dir) => rgb.AddForce(dir, ForceMode.VelocityChange);
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            GameManager.instance.PassedFinishLine();
        }
    }
}
