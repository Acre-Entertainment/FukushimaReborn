using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverText;
    public GameObject gameOverFade;
    public float timeToReload;
    public void EndGame()
    {
        gameOverText.SetActive(true);
        gameOverFade.SetActive(true);
        StartCoroutine(reloadLevelAfterTime());
    }

    IEnumerator reloadLevelAfterTime()
    {
        yield return new WaitForSeconds(timeToReload);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
