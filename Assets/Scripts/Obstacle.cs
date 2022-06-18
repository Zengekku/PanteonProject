using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<Unit>(out var unit))
        {
            unit.RestartUnit();
        }
    }
}