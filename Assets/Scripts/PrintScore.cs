using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PrintScore : MonoBehaviour
{
    private int orderScore = 0;
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] GameObject scoreText;

    private void OnEnable()
    {
        CustomerOrder.OrderComplete += IncreaseScore;
        Timer.RanOutOfTime += PrintScoreToCanvas;
    }
    private void OnDisable()
    {
        CustomerOrder.OrderComplete -= IncreaseScore;
        Timer.RanOutOfTime -= PrintScoreToCanvas;
    }

    private void IncreaseScore()
    {
        Debug.Log("Increasing score");
        orderScore += 1;
        Debug.Log("Current score: " + orderScore);
    }

    private void PrintScoreToCanvas()
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = orderScore.ToString() + " order(s)";
    }
}
