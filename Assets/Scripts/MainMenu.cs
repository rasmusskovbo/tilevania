using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<PlayerMovement>().DisableControls();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
