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
    private bool alive = true; //Fica falso quando o inimigo é pisado pelo jogador.

    public GameObject bullet; //Prefab da bala que será instanciada.

    public Animator anim; //Componente Animação do inimigo.
    public Transform tran; //Componente Transformação do inimigo.
    public Transform shootPos; //Posição onde aparecerá o tiro inimigo.
    public Transform shootRange; //Limite da visão do inimigo.

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
        //Atira caso veja o jogador e esteja autorizado a atirar.
        if(seeThePlayer() && canShoot)
        {
            //Desabilita a possibilidade de tiro, para evitar que
            //haja vários tiros de uma vez.
            canShoot = false;

            //Dá o tiro, finalmente.
            StartCoroutine(Shoot());
        }
    }

    //VISÃO DO INIMIGO
    bool seeThePlayer()
    {
        //Booleana para saber se está vendo o inimigo.
        bool val = false;

        //Ajusta o limite de visão do inimigo.
        Vector2 visionRange = shootRange.position;
        RaycastHit2D hit = Physics2D.Linecast(shootPos.position, visionRange);

        //Booleana vira verdadeiro, caso enxergue o jogador.
        if(hit.collider != null)
        {
            if(hit.collider.gameObject.CompareTag("Player"))
            {
                val = true;
            }
        }

        return val;
    }

    //TIRO
    IEnumerator Shoot()
    {
        //Ativa a animação de atirar e espera chegar no frame de disparo.
        anim.SetTrigger("shoot");
        yield return new WaitForSeconds (0.25f);

        //Instancia o tiro.
        GameObject newBullet = Instantiate(bullet, shootPos.position, tran.rotation);
        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * 1000 * Time.deltaTime * direction, 0f);

        //Espera alguns segundos, usando a variável timeBtwShoots, antes de autorizar um novo tiro.
        yield return new WaitForSeconds (timeBtwShots);

        //Autoriza um novo tiro.
        canShoot = true;
    }

    //MORTE
    void OnTriggerEnter2D(Collider2D col)
    {
        //Detecta se os pés do jogador tocam no inimigo.
        if(col.gameObject.tag == "Player")
        {
            //Impulsiona o jogador para cima, dando uma impressão de trampolim.
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 20, ForceMode2D.Impulse);

            //Desativa o colisor e a gravidade
            boxCol.enabled = false;
            rig.bodyType = RigidbodyType2D.Kinematic;

            //Desativa a possibilidade de matar o jogador caso
            //toque outra parte do seu corpo que não os pés, evitando
            //que o jogador morra durante a animação de morte do inimigo.
            alive = false;

            //Ativa a animação de morte e destrói o inimigo meio segundo depois.
            anim.SetTrigger("death");
            Destroy(gameObject, 0.5f);
        }
    }

    //MATA O JOGADOR AO TOCAR
    void OnCollisionEnter2D(Collision2D col)
    {
        //Tenta detectar uma colisão com o jogador e se está vivo.
        if(col.gameObject.tag == "Player" && alive)
        {
            //Reseta a cena.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
