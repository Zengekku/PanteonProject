using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] protected Rigidbody rgb;
    [SerializeField] protected float speed = 50f;
    [SerializeField] protected float maxHorizontalSpeed;
    [SerializeField] protected float maxForwardSpeed;
    [SerializeField] protected Vector3 force = Vector3.zero;
    protected bool stopped = false;

    void FixedUpdate()
    {
        if (transform.position.y < -10 )
        {
            rgb.velocity = Vector3.zero;
            transform.position = GameManager.instance.StartPos;
            force.x = 200;

            return;
        }
        else if (stopped) return;


        force.z = Mathf.Clamp(force.z + (speed * Time.fixedDeltaTime), 0, maxForwardSpeed);
        rgb.velocity = force * Time.fixedDeltaTime;

        if (transform.position.y < -0.75f)
            force.y = -800f;
        else
            force.y = -200f;
    }

}