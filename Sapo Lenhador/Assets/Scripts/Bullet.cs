using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    //Detecta se colide com algo.
    void OnCollisionEnter2D(Collision2D col)
    {
        //Se colidir com o jogador, reseta a cena.
        if(col.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //É destruído depois que colide com algo.
        Destroy(gameObject, 0.01f);
    }
}
