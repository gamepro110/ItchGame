using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button StartB;
    [SerializeField] private Button OptionsB;
    [SerializeField] private Button QuitB;

    void Start()
    {
        StartB.onClick.AddListener(GoToMultiplayer);
        OptionsB.onClick.AddListener(GoToOptions);
        QuitB.onClick.AddListener(QuitTheGame);
    }

    void GoToMultiplayer()
    {
        SceneManager.LoadScene(1);
    }

    void GoToOptions()
    {

    }

    void QuitTheGame()
    {
        Application.Quit();
    }
}