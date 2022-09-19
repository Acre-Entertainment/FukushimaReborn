using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private string levelName;
    public GameObject loadingText;
    public bool activateText;
    public void Play()
    {
        if (activateText) loadingText.SetActive(true);
        SceneManager.LoadScene(levelName);
    }
}
