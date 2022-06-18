using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] Transform[] runners;
    [SerializeField] Transform startPos;
    [SerializeField] GameObject drawWall;
    List<Transform> ranks = new List<Transform>();
    public Vector3 StartPos { get { return startPos.position; } }
    WaitForSeconds gap;

    void Awake()
    {
        instance = this;
    }
    IEnumerator Start()
    {
        gap = new WaitForSeconds(1);
        while (gameObject.activeSelf)
        {
            if (runners.Length == 0) yield break;
            OrderUnitRanks();
            yield return gap;
        }
    }
    public void ActivateDrawWall()
    {
        drawWall.SetActive(true);
        foreach (var runner in runners)
        {
            runner.GetComponent<IRunner>().StopMoving();
        }
    }

    public void PassedFinishLine(Transform runner)
    {
        ranks.Add(runner);
    }
    void OrderUnitRanks() => runners = runners.OrderByDescending(t => t.transform.position.z).ToArray();
}
