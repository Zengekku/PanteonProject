using UnityEngine;

public class EnemyAI : Unit, IRunner
{
    [SerializeField] int rayCount;
    [SerializeField] float rayAngle;
    [SerializeField] float rayMaxDistance = 1f;
    [SerializeField] LayerMask layerMask;
    [SerializeField] MeshRenderer ground;
    bool raycasting;

    private void Start()
    {
        //maxForwardSpeed = Random.Range(maxForwardSpeed * 0.5f, maxForwardSpeed);
        maxForwardSpeed = 300;
        maxHorizontalSpeed = 400;
    }
    void Update()
    {
        if (stopped) return;
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

            }
        }

        if (hitCount == 0)
        {
            if (transform.position.x < -2f || transform.position.x > 2f)
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

        force.x = Mathf.Clamp(forceDir * speed + force.x, -maxHorizontalSpeed, maxHorizontalSpeed);
        //if (transform.position.x > 4.2 && force.x > 0 || transform.position.x < -4.2f && force.x < 0)
        //force.x = -force.x; if you want border detection
    }

    Vector3 DirFromAngle(float angle, bool isGlobal = true)
    {
        if (!isGlobal)
            angle += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }
    public void AddHorizontalForce(float _force) => force.x = Mathf.Clamp(force.x - _force, -maxHorizontalSpeed, maxHorizontalSpeed);
    public void AddVerticalForce(float _force) => force.y += _force;
    public void AddForwardForce(float _force) => force.z += _force;
    public void StopMoving()
    {
        stopped = true;
        rgb.velocity = Vector3.down * 25;
        force.z *= 0.5f;
        force.x = 0;
        /*stopped = true;  
        rgb.isKinematic = true;
        rgb.velocity = Vector3.down * 25;
        force.x = 0;
        force.z = 0;*/
    }
    public void ContinueMoving() => stopped = false;
    public void Push(Vector3 dir) => rgb.AddForce(dir, ForceMode.VelocityChange);
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            StopMoving();
            rgb.isKinematic = true;
            GetComponent<Collider>().enabled = false;
        }
    }
}