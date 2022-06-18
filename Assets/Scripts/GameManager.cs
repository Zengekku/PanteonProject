using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] Transform[] runners;
    [SerializeField] Transform startPos;
    [SerializeField] GameObject drawWall;
    [SerializeField] TextMeshProUGUI rankingText;
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
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in runners)
            {
                stringBuilder.AppendLine(item.name);
            }
            rankingText.SetText(stringBuilder.ToString());
            yield return gap;
        }
    }
    public void ActivateDrawWall()
    {
        drawWall.SetActive(true);
        rankingText.gameObject.SetActive(false);
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
