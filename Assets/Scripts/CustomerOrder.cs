using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerOrder : MonoBehaviour
{
    public delegate void Order();
    public static Order OrderComplete;

    public delegate void Movement(List<string> order);
    public static Movement EnteredStore;

    [SerializeField] List<string> order = new List<string>();
    [SerializeField] string[] products = new string[5];

    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            order.Add(products[UnityEngine.Random.Range(0, products.Length)]);
        }
    }

    private void OnEnable()
    {
        ProductPickUp.TalkedTo += CheckOrder;
    }

    private void OnDisable()
    {
        ProductPickUp.TalkedTo -= CheckOrder;
    }

    private void CheckOrder(GameObject playerInventory)
    {
        for(int i = 0; i < playerInventory.transform.childCount; i++)
        {
            if (order.Contains(playerInventory.transform.GetChild(i).name))
            {
                order.Remove(playerInventory.transform.GetChild(i).name);
                Destroy(playerInventory.transform.GetChild(i).gameObject);      
                
                if (order.Count == 0)
                {
                    Debug.Log("Pleasure doing business");
                    OrderComplete?.Invoke();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Shop")
        {
            EnteredStore?.Invoke(order);
        }
    }
}
