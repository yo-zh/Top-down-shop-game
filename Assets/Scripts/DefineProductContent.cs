using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefineProductContent : MonoBehaviour
{
    private string[] products = {"Tea", "Bread", "Milk", "Sugar", "Butter"};

    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = products[Random.Range(0, products.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
