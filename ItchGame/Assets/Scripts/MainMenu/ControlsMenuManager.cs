using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlsMenuManager : MonoBehaviour
{
    [SerializeField] private Button m_returnB = null;
    [SerializeField, Range(0, 10)] private int m_sceneIndex = 0;

    private void Start() => m_returnB.onClick.AddListener(() => SceneManager.LoadScene(m_sceneIndex));
}