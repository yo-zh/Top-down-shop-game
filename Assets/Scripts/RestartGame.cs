using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResrartGame : MonoBehaviour
{
    private Button restartButton;
    private void Start()
    {
        restartButton = GetComponent<Button>();
        restartButton.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        Debug.Log("Resrart button pressed, let's go");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
