using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Opponent"))
        {
            other.gameObject.GetComponent<Unit>().RestartUnit();
        }
    }
}