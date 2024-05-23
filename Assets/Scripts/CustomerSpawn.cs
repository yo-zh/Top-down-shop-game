using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomerSpawn : MonoBehaviour
{
    [SerializeField] GameObject customerPrefab;

    private void OnEnable()
    {
        CustomerMovement.CustomerExit += SpawnCustomer;
    }

    private void OnDisable()
    {
        CustomerMovement.CustomerExit -= SpawnCustomer;
    }

    private void SpawnCustomer()
    {
        GameObject newCustomer = Instantiate(customerPrefab, transform.position + Vector3.up, transform.rotation);
        newCustomer.name = customerPrefab.name;

        Debug.Log("A new customer has entered", newCustomer);
    }
}
