using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public static GameOverMenu instance;

    [HideInInspector]
    public bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public string levelName = SAME_SCENE;

    public const string SAME_SCENE = "0";

    public string firstLevel;

    public IsEnabled enabledScript;

    public GameObject selectorLabel;

    public GameObject Destroy1;
    public GameObject Destroy2;
    public GameObject Destroy3;
    public GameObject Destroy4;
    public GameObject Destroy5;
    public GameObject Destroy6;
    public GameObject Destroy7;

    public Vector2 lastCheckPointPos;

    public GameObject player;

    public Image cursor;

    void Update()
    {
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        cursor = GameObject.FindGameObjectWithTag("Cursor").GetComponent<Image>();
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        cursor.enabled = false;
        selectorLabel.SetActive(true);
        GameIsPaused = false;
        enabledScript.enabled = false;
    }

    public void GameOver()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        cursor.enabled = true;
        Cursor.lockState = CursorLockMode.None;
        selectorLabel.SetActive(false);
        GameIsPaused = true;
    }

    public void LoadMenu()

    {
        Resume();
        Time.timeScale = 1f;
        Destroy(Destroy1);
        Destroy(Destroy2);
        Destroy(Destroy3);
        Destroy(Destroy4);
        Destroy(Destroy5);
        Destroy(Destroy6);
        Destroy(Destroy7);
        SceneManager.LoadScene("Menu");
        enabledScript.enabled = false;
        StopAllCoroutines();
        Physics2D.IgnoreLayerCollision(8, 11, false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLastLevel()

    {
        Resume();
        Time.timeScale = 1f;

        Scene currentScene = SceneManager.GetActiveScene();
        int buildIndex = currentScene.buildIndex;

        if (buildIndex == 0)
        {
            Destroy(Destroy1);
            Destroy(Destroy2);
            Destroy(Destroy3);
            Destroy(Destroy4);
            Destroy(Destroy5);
            Destroy(Destroy6);
            Destroy(Destroy7);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }
        
    }

    public void Restart()

    {
        Resume();
        Time.timeScale = 1f;
        Destroy(Destroy1);
        Destroy(Destroy2);
        Destroy(Destroy3);
        Destroy(Destroy4);
        Destroy(Destroy5);
        Destroy(Destroy6);
        Destroy(Destroy7);
        SceneManager.LoadScene(firstLevel, LoadSceneMode.Single);
        enabledScript.enabled = false;
        StopAllCoroutines();
        Physics2D.IgnoreLayerCollision(8, 11, false);
    }

    public void Respawn()
    {
        Resume();
        Time.timeScale = 1f;
        Destroy(Destroy1);
        Destroy(Destroy2);
        Destroy(Destroy3);
        Destroy(Destroy4);
        Destroy(Destroy5);
        Destroy(Destroy6);
        Destroy(Destroy7);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        enabledScript.enabled = false;
        StopAllCoroutines();
        Physics2D.IgnoreLayerCollision(8, 11, false);
    }
}