using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour
{
    private Button exitButton;
    private void Start()
    {
        exitButton = GetComponent<Button>();
        exitButton.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        Debug.Log("Exit button pressed, bye");
        UnityEditor.EditorApplication.isPlaying = false; // Delete after test
        Application.Quit();
    }
}
