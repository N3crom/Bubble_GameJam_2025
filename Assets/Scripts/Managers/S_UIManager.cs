using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Dan.Main;

public class S_UIManager : MonoBehaviour
{
    [Header("RSE")]
    [SerializeField] private RSE_CallPause callPause;
    [SerializeField] private RSE_UnCallPause unCallPause;

    [SerializeField] private RSE_CallRestart callRestart;
    [SerializeField] private RSE_CallMenu callMenu;
    [SerializeField] private RSE_CallQuit callQuit;

    [SerializeField] private RSE_OnGameLost onGameLost;

    [Header("RSO")]
    [SerializeField] private RSO_Score score;

    [Header("References")]
    [SerializeField] private GameObject panelPause;
    [SerializeField] private GameObject panelGameOver;
    [SerializeField] private TextMeshProUGUI textScore;

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

        Time.timeScale = 1;

        SceneManager.LoadScene(currentSceneIndex);
    }

    private void Menu()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("Scene_MainMenu");
    }

    private void Quit()
    {
        Application.Quit();
    }

    private void GameOver()
    {
        panelGameOver.SetActive(true);
        textScore.text = score.Score.ToString();

        Leaderboards.Score.UploadNewEntry("Paul", 50);
        Leaderboards.Score.ResetPlayer();

        Time.timeScale = 0;
    }
}
