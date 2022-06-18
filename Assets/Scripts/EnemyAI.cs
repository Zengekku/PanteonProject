using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] Rigidbody rgb;
    [SerializeField] float speed = 50f;
    [SerializeField] float gravity = 100f;
    [SerializeField] Vector3 force = Vector3.zero;
    [SerializeField] Vector3 externalForce = Vector3.zero;
    [SerializeField] int rayCount;
    [SerializeField] float rayAngle;
    [SerializeField] float rayMaxDistance = 1f;
    [SerializeField] LayerMask layerMask;
    [SerializeField] MeshRenderer ground;

    bool stop = false;

    public void GetEffected(Vector3 dir)
    {
        externalForce.x = Mathf.Clamp(externalForce.x + dir.x, -300, 0);
    }
    private void Update()
    {
        if (stop) return;
        float distPerRay = rayAngle / rayCount;
        float forceDir = 0;
        int hitCount = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            float angle = transform.eulerAngles.y - rayAngle * 0.5f + distPerRay * i;
            var dir = Vector3.forward + DirFromAngle(angle);
            Debug.DrawRay(transform.position + Vector3.up, dir * rayMaxDistance, Color.red);


            if (Physics.Raycast(transform.position + Vector3.up, dir, out var hit, rayMaxDistance, layerMask))
            {
                hitCount++;
                forceDir += -hit.transform.position.x;
                break;
            }
        }

        if (hitCount == 0)
        {
            if (transform.position.x < -0.25f || transform.position.x > 0.25f)
            {
                forceDir = -transform.position.x;
              
            }
            else
            {
                forceDir = 0;
                force.x = 0;
            }

        }
        else if (forceDir == 0)
            forceDir = 1;

        force.x = Mathf.Clamp(forceDir * speed + force.x, -400, 400);
        if (transform.position.x > 4.2 && force.x > 0 || transform.position.x < -4.2f && force.x < 0)
            force.x = -force.x;
    }

    Vector3 DirFromAngle(float angle, bool isGlobal = true)
    {
        if (!isGlobal)
            angle += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    public void StopForce()
    {
        stop = true;
        rgb.velocity = Vector3.zero;
        force.z = -25;
        //force.y = -250f;

    }
    public void Continue()
    {
        stop = false;
        force.y = -gravity;
    }
    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        float distPerRay = rayAngle / rayCount;
        for (int i = 0; i <= rayCount; i++)
        {
            float angle = transform.eulerAngles.y - rayAngle * 0.5f + distPerRay * i;
            Gizmos.DrawRay(transform.position + Vector3.up, Vector3.forward + DirFromAngle(angle) * rayMaxDistance); ;

        }
    }*/
    private void FixedUpdate()
    {
        if (stop) return;

        force.z = Mathf.Clamp(force.z + (speed * Time.fixedDeltaTime), 0, 400);
        rgb.velocity = (force + externalForce) * Time.fixedDeltaTime;
    }
    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            //force.y = -250;
            externalForce.x = 0;
            //StopForce();
            Debug.Log("off ground");
        }

    }
    private void OnCollisionEnter(Collision other)
    {
        //if (other.gameObject.CompareTag("Ground"))
        Continue();
    }

}