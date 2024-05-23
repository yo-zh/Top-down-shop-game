using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class Timer : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField] float bonusTime;
    [SerializeField] Canvas gameOverCanvas;

    public delegate void GameTimer();
    public static GameTimer RanOutOfTime;

    private void OnEnable()
    {
        CustomerOrder.OrderComplete += AddTime;
    }

    private void OnDisable()
    {
        CustomerOrder.OrderComplete -= AddTime;
    }

    private void Start()
    {
        gameOverCanvas.transform.GetChild(0).gameObject.SetActive(false);
    }

    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;

            if (time < 0) time = 0;
        }

        if (time <= 0)
        {
            gameOverCanvas.transform.GetChild(0).gameObject.SetActive(true);
            RanOutOfTime?.Invoke();
            //time = delay;
        }
    }

    private void AddTime()
    {
        time += 30;
    }
}
