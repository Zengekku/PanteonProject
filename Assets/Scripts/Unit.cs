using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] protected Rigidbody rgb;
    [SerializeField] protected Animator animator;
    [SerializeField] protected float speed = 50f;
    [SerializeField] protected float maxHorizontalSpeed;
    [SerializeField] protected float maxForwardSpeed;
    [SerializeField] protected Vector3 force = Vector3.zero;
    protected bool unitStopped = false;
    float startSpeed = 200;
    void FixedUpdate()
    {
        if (BelowThreshold(-10))//game over
        {
            RestartUnit();
            return;
        }
        else if
            (unitStopped) return;

        SetVelocity();

        if (BelowThreshold(-0.75f))//falling
            force.y = -800f;
        else
            force.y = -startSpeed;
    }
    public void RestartUnit()
    {
        if (gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            rgb.velocity = Vector3.zero;
            transform.position = GameManager.instance.StartPos;
            force.x = startSpeed;
        }
    }
    void SetVelocity()
    {
        force.z = Mathf.Clamp(force.z + (speed * Time.fixedDeltaTime), 0, maxForwardSpeed);
        rgb.velocity = force * Time.fixedDeltaTime;
    }
    bool BelowThreshold(float y) => transform.position.y < y;
    void LateUpdate() => transform.rotation = Quaternion.Euler(Vector3.zero);
}