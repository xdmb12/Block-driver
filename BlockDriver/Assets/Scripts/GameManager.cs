using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject startButton;
    
    void Start()
    {
        Time.timeScale = 0;
    }

    public void Play()
    {
        Time.timeScale = 1f;
        startButton.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
