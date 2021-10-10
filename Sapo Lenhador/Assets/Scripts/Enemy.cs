using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float shootSpeed; //Velocidade do tiro.
    public float timeBtwShots; //Tempo que o inimigo deve esperar para tornar a atirar.
    public bool canShoot; //Booleana que autoriza o inimigo a atirar.
    public float direction; //Direção em que a bala irá. -1 é esquerda, 1 é direita.
    public bool alive; //Fica falso quando o inimigo é pisado pelo jogador.

    public GameObject bullet; //Prefab da bala que será instanciada.

    public Animator anim; //Pega o componente Animação do inimigo.
    public Transform tran; //Pega o componente Transformação do inimigo.
    public Transform shootPos; //Pega onde aparecerá o tiro inimigo.
    public Transform shootRange; //Pega o limite da visão do inimigo.

    public Transform player; //Pega o componente Transformação do player.

    public BoxCollider2D boxCol;
    public Rigidbody2D rig;

    void Start()
    {
        anim = GetComponent<Animator>();
        tran = GetComponent<Transform>();
        canShoot = true;

        boxCol = GetComponent<BoxCollider2D>();
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(seeThePlayer() && canShoot)
        {
            canShoot = false;
            StartCoroutine(Shoot());
        }
    }

    bool seeThePlayer()
    {
        bool val = false;
        Vector2 visionRange = shootRange.position;
        RaycastHit2D hit = Physics2D.Linecast(shootPos.position, visionRange);

        if(hit.collider != null)
        {
            if(hit.collider.gameObject.CompareTag("Player"))
            {
                val = true;
            }
        }

        return val;
    }

    IEnumerator Shoot()
    {
        anim.SetTrigger("shoot");
        GameObject newBullet = Instantiate(bullet, shootPos.position, tran.rotation);
        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * 1000 * Time.deltaTime * direction, 0f);
        yield return new WaitForSeconds (timeBtwShots);
        canShoot = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 20, ForceMode2D.Impulse);
            boxCol.enabled = false;
            rig.bodyType = RigidbodyType2D.Kinematic;
            alive = false;
            anim.SetTrigger("death");
            Destroy(gameObject, 0.5f);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player" && alive)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
