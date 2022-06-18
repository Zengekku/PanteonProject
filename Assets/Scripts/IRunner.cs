using UnityEngine;

public interface IRunner
{
    public void AddHorizontalForce(float _force);
    public void AddVerticalForce(float _force);
    public void AddForwardForce(float _force);
    public void Push(Vector3 dir);
    public void StopMoving();
    public void ContinueMoving();
    
}