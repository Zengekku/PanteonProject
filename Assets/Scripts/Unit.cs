using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] protected Rigidbody rgb;
    [SerializeField] protected float speed = 50f;
    [SerializeField] protected float maxHorizontalSpeed;
    [SerializeField] protected float maxForwardSpeed;
    [SerializeField] protected Vector3 force = Vector3.zero;
    protected bool unitStopped = false;

    void FixedUpdate()
    {
        if (IfBelow(-10))
        {
            RestartUnit();
            return;
        }
        else if
            (unitStopped) return;
        SetVelocity();

        if (IfBelow(-0.75f))
            force.y = -800f;
        else
            force.y = -200f;
    }
    public void RestartUnit()
    {
        rgb.velocity = Vector3.zero;
        transform.position = GameManager.instance.StartPos;
        force.x = 200;
    }
    void SetVelocity()
    {
        force.z = Mathf.Clamp(force.z + (speed * Time.fixedDeltaTime), 0, maxForwardSpeed);
        rgb.velocity = force * Time.fixedDeltaTime;
    }
    bool IfBelow(float y)
    {
        return transform.position.y < y;
    }
    void LateUpdate() => transform.rotation = Quaternion.Euler(Vector3.zero);
}