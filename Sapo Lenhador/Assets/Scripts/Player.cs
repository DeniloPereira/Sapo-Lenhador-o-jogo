using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed; //Variável base para a velocidade de movimentação.
    public float jumpForce; //Variável base para a força do pulo.

    public bool notJumping; //Variável base para verificar se o personagem está pulando.

    private Rigidbody2D rig;
    private Animator anim;

    public BoxCollider2D footCol; //Colisor dos pés.

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move(); //Movimento do personagem.
        Jump(); //Pulo do personagem.
    }

    //MOVIMENTO DO PERSONAGEM
    void Move()
    {
        //Move o personagem usando como base de velocidade a variável speed.
        float movement = Input.GetAxis("Horizontal");
        rig.velocity = new Vector2(movement * speed, rig.velocity.y);

        //Ativa a animação de corrida e vira o personagem
        //para a DIREITA caso esteja andando para a direita. 
        if (movement > 0)
        {
            anim.SetBool("running", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        //Ativa a animação de corrida e deixa o personagem virado
        //para a ESQUERDA caso esteja andando para a esquerda. 
        if (movement < 0)
        {
            anim.SetBool("running", true);
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        //Desativa a animação de corrida ao parar de mover o personagem.
        if (movement == 0)
        {
            anim.SetBool("running", false);
        }
    }

    //PULO DO PERSONAGEM
    void Jump()
    {
        if(Input.GetButtonDown("Jump") && notJumping)
        {
            //Adiciona uma força no eixo y no valor da variável jumpForce.
            rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }
    
    //Verifica se os pés estão tocando algo para permitir o pulo.
    void OnTriggerStay2D(Collider2D col)
    {
        if(col != null)
        {
            notJumping = true;
        }
    }

    //Verifica se os pés deixaram de tocar algo para esperar
    //um tempinho e desativar a possibilidade de pulo.
    void OnTriggerExit2D(Collider2D col)
    {
        StartCoroutine(NoJumpSoon());
    }

    IEnumerator NoJumpSoon()
    {
        yield return new WaitForSeconds (0.1f);
        notJumping = false;
    }
}
