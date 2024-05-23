using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowPrompt : MonoBehaviour
{
    private Canvas proximityPromptCanvas;
    private Material prefabMaterial;
    private Transform floatingText;
    private void Start()
    {
        proximityPromptCanvas = FindObjectOfType<Canvas>();
        prefabMaterial = gameObject.transform.parent.GetComponent<MeshRenderer>().materials[1];
        prefabMaterial.color = Color.red;
        floatingText = transform.GetChild(0);
        floatingText.GetComponent<TextMeshPro>().text = transform.parent.name;
        floatingText.GetComponent<TextMeshPro>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            proximityPromptCanvas.enabled = true;
            floatingText.GetComponent<TextMeshPro>().enabled = true;
            prefabMaterial.color = Color.green;
            transform.parent.gameObject.tag = "Interactable";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            proximityPromptCanvas.enabled = false;
            floatingText.GetComponent<TextMeshPro>().enabled = false;
            prefabMaterial.color = Color.red;
            transform.parent.gameObject.tag = "Untagged";
        }
    }
}
