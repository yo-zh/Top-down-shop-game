using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopScreen : MonoBehaviour
{
    private TextMeshPro screenText;

    private void Start()
    {
        screenText = GetComponent<TextMeshPro>();
    }

    private void OnEnable()
    {
        CustomerOrder.EnteredStore += PrintOrder;
        CustomerOrder.OrderComplete += ClearScreen;
    }

    private void OnDisable()
    {
        CustomerOrder.EnteredStore -= PrintOrder;
        CustomerOrder.OrderComplete -= ClearScreen;
    }

    private void PrintOrder(List<string> order)
    {
        Debug.Log("Customer entered, printing order");
        screenText.text = string.Empty;
        for (int i = 0; i < order.Count; i++)
        {
            screenText.text += order[i] + "\n";
        }
    }
        
    private void ClearScreen()
    {
        Debug.Log("Customer is leaving, deleting order");
        screenText.text = "Awaiting orders";
    }
}
