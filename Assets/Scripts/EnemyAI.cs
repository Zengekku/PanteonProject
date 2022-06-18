using UnityEngine;

public class EnemyAI : Unit, IRunner
{
    [SerializeField] int rayCount;
    [SerializeField] float rayAngle;
    [SerializeField] float rayMaxDistance = 1f;
    [SerializeField] LayerMask layerMask;
    [SerializeField] MeshRenderer ground;

    private void Start()
    {
        //if you want different AI
        /*maxForwardSpeed = Random.Range(maxForwardSpeed -100, maxForwardSpeed);
        maxHorizontalSpeed = maxForwardSpeed;*/
    }
    void Update()
    {
        if (unitStopped) return;
        float distPerRay = rayAngle / rayCount;
        float desiredDirection = 0;
        int hitCount = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 dir = GetDirection(distPerRay, i);

#if UNITY_EDITOR
            Debug.DrawRay(transform.position + Vector3.up, dir * rayMaxDistance, Color.red);
#endif

            RaycastHit hit;
            if (CheckObstacles(dir, out hit))
            {
                hitCount++;
                desiredDirection += -hit.transform.position.x;
            }
        }
        if (hitCount == 0)//if no obstacles found align to middle
        {
            if (transform.position.x < -2 || transform.position.x > 2)
            {
                desiredDirection = -transform.position.x;
            }
            else
            {
                desiredDirection = 0;
                force.x = 0;
            }
        }
        else if (desiredDirection == 0)//if no direction found go right
            desiredDirection = 1;

        SetDesiradeForce(desiredDirection);
        //if (transform.position.x > 4.2 && force.x > 0 || transform.position.x < -4.2f && force.x < 0)
        //force.x = -force.x; if you want border detection
    }
    public void AddHorizontalForce(float _force) => force.x = Mathf.Clamp(force.x - _force, -maxHorizontalSpeed, maxHorizontalSpeed);
    public void AddVerticalForce(float _force) => force.y += _force;
    public void AddForwardForce(float _force) => force.z += _force;
    public void StopMoving()
    {
        unitStopped = true;
        rgb.velocity = Vector3.down * 25;
        force.z *= 0.5f;
        force.x = 0;
    }
    public void ContinueMoving() => unitStopped = false;
    public void Push(Vector3 dir) => rgb.AddForce(dir, ForceMode.VelocityChange);
    void SetDesiradeForce(float forceDir) => force.x = Mathf.Clamp(forceDir * speed + force.x, -maxHorizontalSpeed, maxHorizontalSpeed);
    bool CheckObstacles(Vector3 dir, out RaycastHit hit) => Physics.Raycast(transform.position + Vector3.up, dir, out hit, rayMaxDistance, layerMask);
    Vector3 GetDirection(float distPerRay, int i)
    {
        float angle = transform.eulerAngles.y - rayAngle * 0.5f + distPerRay * i;
        var dir = Vector3.forward + DirFromAngle(angle);
        return dir;
    }
    Vector3 DirFromAngle(float angle) => new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            GameManager.instance.PassedFinishLine(transform);
            StopMoving();
            rgb.isKinematic = true;
            GetComponent<Collider>().enabled = false;
        }
    }
}