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

    [Header("References")]
    [SerializeField] private GameObject panelPause;

    private void OnEnable()
    {
        callPause.action += ShowPause;
        unCallPause.action += UnShowPause;

        callRestart.action += Restart;
        callMenu.action += Menu;
        callQuit.action += Quit;
    }

    private void OnDisable()
    {
        callPause.action -= ShowPause;
        unCallPause.action += UnShowPause;

        callRestart.action -= Restart;
        callMenu.action -= Menu;
        callQuit.action -= Quit;
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
}
