using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeController : MonoBehaviour
{
    public int velocity = 0, velSalto = 5, salto = 2, veloCorrer=8;
    public GameObject Kunai;

    [HideInInspector]
    public bool onLadder = false;
    public float climbSpeed = 3;
    public float exitHop = 3;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    Collider2D cl;
    AudioSource audioSource;

    [HideInInspector]
    public bool isGrounded = true;

    [HideInInspector]
    public bool usingLadder = false;

    const int Ani_parado = 0;
    const int Ani_caminar = 1;
    const int Ani_correr = 2;
    const int Ani_correr_espada = 3;
    const int Ani_correr_tirar = 4;
    const int Ani_salto_incio = 5;
    const int Ani_salto = 6;
    const int Ani_caida = 7;
    const int Ani_patada = 8;
    const int Ani_espada = 9;
    const int Ani_espada_aire = 10;
    const int Ani_rodar = 11;
    const int Ani_tirar = 12;
    const int Ani_tirar_aire = 13;
    const int Ani_dano = 14;
    const int Ani_muerte = 15;
    const int Ani_quieto = 16;
    int cont;
    //float dir = 1.2f;
    //float tiempoataque = 0.5f, time = 0;
    //float gravedadInicial;
    //bool darGolpe = false;
    //bool cambio = false;
    float elapsedTime = 0.0f;
    float delay = 0.5f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        cl = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        rb.velocity = new Vector2(-velocity, rb.velocity.y);
        Movimientos();
        if(Input.GetKeyDown("x")){
            
        }
    }
    private void Movimientos()
    {
        if (Input.GetKey(KeyCode.RightArrow)) {
            sr.flipX = false;
            //dir = 1.2f;

            if (Input.GetKey("x"))
            {
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    ChangeAnimation(Ani_rodar);
                    rb.velocity = new Vector2(veloCorrer, rb.velocity.y);
                }
                else
                {
                    ChangeAnimation(Ani_correr);
                    rb.velocity = new Vector2(veloCorrer, rb.velocity.y);
                }
            }
            else 
            {
                ChangeAnimation(Ani_caminar);
                rb.velocity = new Vector2(velocity, rb.velocity.y);
            }

        }
        else if (Input.GetKey(KeyCode.LeftArrow)) {
            sr.flipX = true;
            //dir = -1.2f;

            if (Input.GetKey("x"))
            {
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    ChangeAnimation(Ani_rodar);
                    rb.velocity = new Vector2(-veloCorrer, rb.velocity.y);
                }
                else
                {
                    ChangeAnimation(Ani_correr);
                    rb.velocity = new Vector2(-veloCorrer, rb.velocity.y);
                }
            }
            else 
            {
                ChangeAnimation(Ani_caminar);
                rb.velocity = new Vector2(-velocity, rb.velocity.y);
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            if(isGrounded){
                ChangeAnimation(Ani_parado);
            }
            
            
        }
        if (Input.GetKeyDown(KeyCode.Space) && cont > 0)
        {
            //audioSource.PlayOneShot(jumpClip);
            rb.AddForce(new Vector2(0, velSalto), ForceMode2D.Impulse);
            ChangeAnimation(Ani_salto_incio);
            
            cont--;

        }
        if (Input.GetKeyDown("z"))
        {
            ChangeAnimation(Ani_patada);
            //darGolpe = true;
        }
        if(cont < 2){
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= delay) {
                ChangeAnimation(Ani_caida);
                elapsedTime = 0.0f;
            }
            
        
        }
            
    }
    void OnCollisionEnter2D(Collision2D other){
        cont=salto;
        if(other.gameObject.name == "Llave"){
            Debug.Log("ctmre");
            //gameManager.CogerLlave();
            //gameManager.Escribir();
            Destroy(other.gameObject);
        }
        
    } 
    void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.name == "Suelo") {
            isGrounded = true;
            elapsedTime = 0.0f;
        }

    }
    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.name == "Suelo") isGrounded = false;
    }
    
    void OnTriggerEnter2D(Collider2D other)//para reconocer el checkponit(transparente)
    {
        
    }
    private void ChangeAnimation(int a){
        animator.SetInteger("Estado", a);
    }
}
