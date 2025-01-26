using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_MainMenuUI : MonoBehaviour
{
    [Header("RSO")]
    [SerializeField] private RSO_PlayerName playerName;

    [Header("RSE")]
    [SerializeField] private RSE_CallStart callStart;
    [SerializeField] private RSE_CallCredits callCredits;
    [SerializeField] private RSE_CallQuit callQuit;

    [Header("References")]
    [SerializeField] private GameObject panelCredits;
    [SerializeField] private TMP_InputField inputFieldName;

    private void OnEnable()
    {
        callStart.action += StartGame;
        callCredits.action += Credits;
        callQuit.action += Quit;
    }

    private void OnDisable()
    {
        callStart.action -= StartGame;
        callCredits.action -= Credits;
        callQuit.action -= Quit;
    }

    private void Start()
    {
        inputFieldName.text = playerName.PlayerName;
    }

    private void StartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        int currentSceneIndex = currentScene.buildIndex;

        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    private void Credits()
    {
        panelCredits.SetActive(true);
    }

    private void Quit()
    {
        Application.Quit();
    }
}
