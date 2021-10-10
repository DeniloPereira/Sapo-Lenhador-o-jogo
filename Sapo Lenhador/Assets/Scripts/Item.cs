using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private SpriteRenderer sRen;
    private CircleCollider2D circ;
    GameManager gManager;
    bool hasCaught = true;

    public GameObject Collected;

    void Start()
    {
        sRen = GetComponent<SpriteRenderer>();
        circ = GetComponent<CircleCollider2D>();
        gManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

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
