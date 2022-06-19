using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    bool gameOver;
    void Awake()
    {
        instance = this;
        
    }
    IEnumerator Start()
    {
        gap = new WaitForSeconds(1);
        while (!gameOver)
        {
            if (runners.Length == 0) yield break;
            OrderUnitRanks();
            StringBuilder stringBuilder = new StringBuilder();
            SetRankList(stringBuilder);
            yield return gap;
        }
    }

    private void SetRankList(StringBuilder stringBuilder)
    {
        foreach (var item in runners)
        {
            stringBuilder.AppendLine(item.name);
        }
        rankingText.SetText(stringBuilder.ToString());
    }

    public void ActivateDrawWall()
    {
        drawWall.SetActive(true);
        rankingText.fontSize = 40;
        gameOver = true;
        foreach (var runner in runners)
        {
            runner.GetComponent<IRunner>().StopMoving();
        }
    }
    public void RestartGame() => SceneManager.LoadScene(0);
    public void PassedFinishLine(Transform runner) => ranks.Add(runner);
    void OrderUnitRanks() => runners = runners.OrderByDescending(t => t.transform.position.z).ToArray();
}
