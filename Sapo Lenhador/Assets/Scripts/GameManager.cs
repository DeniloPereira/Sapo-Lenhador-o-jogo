using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public int goal;
    public GameObject congratCanvas;

    void Start()
    {
        congratCanvas.SetActive(false);
    }

    void Update()
    {
        if(score == goal)
        {
            Time.timeScale = 0;
            congratCanvas.SetActive(true);
        }

        if(Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1;
        }
    }
}
