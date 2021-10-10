using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private SpriteRenderer sRen;
    private CircleCollider2D circ;
    GameManager gManager;
    bool hasCaught = true; //Variável que diz se a fruta já foi tocada (para evitar que contabilize mais de 1 ponto)

    public GameObject Collected;

    void Start()
    {
        sRen = GetComponent<SpriteRenderer>();
        circ = GetComponent<CircleCollider2D>();
        gManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    //Verifica se o jogador tocou na fruta para contabilizar 1 ponto
    //no GameManager e destruir a fruta.
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" && hasCaught)
        {
            hasCaught = false;
            sRen.enabled = false;
            circ.enabled = false;
            Collected.SetActive(true);
            gManager.score++;

            Destroy(gameObject, 0.3f);
        }
    }
}
