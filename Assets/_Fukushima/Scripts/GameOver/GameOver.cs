using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameOver : MonoBehaviour
{
    public UnityEvent onShockDeath;
    public UnityEvent onDrownDeath;
    public UnityEvent onSpottedDeath;

    public GameObject gameOverText;
    public GameObject gameOverFade;
    public float timeToReload;
    public void EndGame(string causeOfDeath)
    {

        if(causeOfDeath == "Drown")
        {
            onDrownDeath.Invoke();
        }
        if(causeOfDeath == "Shock")
        {
            onShockDeath.Invoke();
        }
        if(causeOfDeath == "Spotted")
        {
            onSpottedDeath.Invoke();
        }
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
