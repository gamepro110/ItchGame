using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button StartB;
    [SerializeField] private Button ControlsB;
    [SerializeField] private Button OptionsB;
    [SerializeField] private Button QuitB;

    [SerializeField, Range(0, 10)] private int m_startMultiplayerSceneIndex = 0;
    [SerializeField, Range(0, 10)] private int m_controlsSceneIndex = 0;
    [SerializeField, Range(0, 10)] private int m_optionsSceneIndex = 0;

    private void Start()
    {
        StartB.onClick.AddListener(GoToMultiplayer);
        ControlsB.onClick.AddListener(GoToControls);
        OptionsB.onClick.AddListener(GoToOptions);
        QuitB.onClick.AddListener(QuitTheGame);
    }

    private void GoToMultiplayer() => SceneManager.LoadScene(m_startMultiplayerSceneIndex);

    private void GoToControls() => SceneManager.LoadScene(m_controlsSceneIndex);

    private void GoToOptions() => SceneManager.LoadScene(m_optionsSceneIndex);

#if !UNITY_EDITOR
    // quits the game in build
    private void QuitTheGame() => Application.Quit();
#endif

#if UNITY_EDITOR

    // stops the editor playing the game
    private void QuitTheGame() => UnityEditor.EditorApplication.isPlaying = false;

#endif
}