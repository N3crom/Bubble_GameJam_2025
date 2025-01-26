using UnityEngine;
using UnityEngine.SceneManagement;

public class S_UIManager : MonoBehaviour
{
    [Header("RSE")]
    [SerializeField] private RSE_CallPause callPause;
    [SerializeField] private RSE_UnCallPause unCallPause;

    [SerializeField] private RSE_CallRestart callRestart;
    [SerializeField] private RSE_CallMenu callMenu;
    [SerializeField] private RSE_CallQuit callQuit;

    [SerializeField] private RSE_OnGameLost onGameLost;

    [Header("References")]
    [SerializeField] private GameObject panelPause;
    [SerializeField] private GameObject panelGameOver;

    private void OnEnable()
    {
        callPause.action += ShowPause;
        unCallPause.action += UnShowPause;

        callRestart.action += Restart;
        callMenu.action += Menu;
        callQuit.action += Quit;

        onGameLost.action += GameOver;
    }

    private void OnDisable()
    {
        callPause.action -= ShowPause;
        unCallPause.action -= UnShowPause;

        callRestart.action -= Restart;
        callMenu.action -= Menu;
        callQuit.action -= Quit;

        onGameLost.action -= GameOver;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(panelPause.activeInHierarchy)
            {
                UnShowPause();
            }
            else
            {
                ShowPause();
            }
        }
    }

    private void ShowPause()
    {
        panelPause.SetActive(true);

        Time.timeScale = 0;
    }

    private void UnShowPause()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        panelPause.SetActive(false);

        Time.timeScale = 1;
    }

    private void Restart()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        int currentSceneIndex = currentScene.buildIndex;

        SceneManager.LoadScene(currentSceneIndex);
    }

    private void Menu()
    {
        SceneManager.LoadScene("Scene_MainMenu");
    }

    private void Quit()
    {
        Application.Quit();
    }

    private void GameOver()
    {
        panelGameOver.SetActive(true);

        Time.timeScale = 0;
    }
}
