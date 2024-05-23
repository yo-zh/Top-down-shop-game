using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProductPickUp : MonoBehaviour
{
    public delegate void Interact (GameObject interactableObject);
    public static Interact PickedUp;
    public static Interact TalkedTo;

    [SerializeField] GameObject playerInventory;

    // Start is called before the first frame update
    void Start()
    {
        playerInventory = transform.Find("Inventory").gameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInventory.transform.childCount < 3)
        {
            PickedUp?.Invoke(playerInventory);
            Debug.Log("Attempting pick up");
        }
        else if (Input.GetKeyDown(KeyCode.E) && playerInventory.transform.childCount >= 3)
        {
            Debug.Log("You're carrying too much");
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 2, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if (Input.GetKeyDown(KeyCode.E) && hit.transform.tag == "Talkative")
            {
                TalkedTo.Invoke(playerInventory);
            }
        }
    }
}
