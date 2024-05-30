using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.RenderGraphModule;

public class PickUp : MonoBehaviour
{
    public Canvas proximityPromptCanvas;
    public Material prefabMaterial;

    private void OnEnable()
    {
        ProductPickUp.PickedUp += PickUpTheBox;
    }

    private void OnDisable()
    {
        ProductPickUp.PickedUp -= PickUpTheBox;
    }
    private void Start()
    {
        proximityPromptCanvas = FindObjectOfType<Canvas>();
    }

    private void PickUpTheBox(GameObject playerInventory)
    {
        if (gameObject.tag == "Interactable")
        {
            proximityPromptCanvas.enabled = false;
            gameObject.tag = "Untagged";
            gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            transform.SetParent(playerInventory.transform, false); //change 2nd argument if gameObject is blocking movement
        }
    }
}
