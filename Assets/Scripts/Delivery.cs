using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Delivery : MonoBehaviour
{
    [SerializeField] GameObject product;

    private void OnMouseDown()
    {
        for (int i = 0; i < 3; i++)
        {
            float offset = i * 0.5f;
            Vector3 position = transform.position + Vector3.forward * offset;
            Instantiate(product, position, transform.rotation);
        }
    }
}
