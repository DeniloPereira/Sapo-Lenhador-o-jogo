using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score = 0; //Pontuação do jogo.
    public int goal; //Número de frutas que se deve pegar para vencer.
    public GameObject congratCanvas;

    void Start()
    {
        //Desabilita a tela de vitória.
        congratCanvas.SetActive(false);
    }

    void Update()
    {
        //Checa se o número de pontos atingiu o número de frutas
        //na fase. Se sim, pausa o jogo e ativa a tela de vitória.
        if(score == goal)
        {
            Time.timeScale = 0;
            congratCanvas.SetActive(true);
        }

        //Reseta o jogo caso o botão R seja pressionado.
        if(Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1;
        }
    }
}
