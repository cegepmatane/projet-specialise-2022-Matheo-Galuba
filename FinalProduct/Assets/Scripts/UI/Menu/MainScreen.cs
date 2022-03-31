using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreen : MonoBehaviour
{
    public void OnPlayButton()
    {
        GameManager.Instance.LoadGame();
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
