using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] Transform[] runners;
    [SerializeField] Transform startPos;
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
            runners = runners.OrderByDescending(t => t.transform.position.z).ToArray();
            yield return gap;
        }
    }

    public void PassedFinishLine()
    {
        Debug.Log("Player win");
    }
}
