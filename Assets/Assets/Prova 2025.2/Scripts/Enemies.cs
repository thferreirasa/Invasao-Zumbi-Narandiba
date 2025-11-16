using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    // variaveis
    public float velocidade = 3f;
    public int dano = 1;
    public Transform alvoPlayer;
    public float deteccaoPlayer = 5f;
    public float distanciaPercorrida = 5f;

    // variaveis privadas
    private Rigidbody2D rb;
    private Health health; // vem do script de vida
    private Vector2 posicaoLimiteA;
    private Vector2 posicaoLimiteB;
    private Vector2 direcaoCaminhada = Vector2.right;
    private float posicaoY;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();

        // player como alvo
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null )
        {
            alvoPlayer = playerObject.transform;
        }

        // guarda posicao inicial Y do inimigo
        posicaoY = transform.position.y;

        // define limites da caminhada/patrulha
        float metadeDistancia = distanciaPercorrida / 2f;
        posicaoLimiteA = new Vector2(transform.position.x - metadeDistancia, posicaoY);
        posicaoLimiteB = new Vector2(transform.position.x + metadeDistancia, posicaoY);
    }

    void FixedUpdate()
    {
        // perseguição do jogador
        if (alvoPlayer != null)
        {
            float distancia = Vector2.Distance(rb.position, alvoPlayer.position);

            if (distancia <= deteccaoPlayer)
            {
                direcaoCaminhada = ((Vector2)alvoPlayer.position - rb.position).normalized;
                rb.velocity = direcaoCaminhada * velocidade;
                return;
            }
        }

        // patrulha

        // inverter posicao quando chegar no final
        if (direcaoCaminhada == Vector2.right && rb.position.x >= posicaoLimiteB.x)
        {
            direcaoCaminhada = Vector2.left;
        }
        else if (direcaoCaminhada == Vector2.left && rb.position.x <= posicaoLimiteA.x)
        {
            direcaoCaminhada = Vector2.right;
        }

        // aplica velocidade no eixo X
        rb.velocity = new Vector2(direcaoCaminhada.x * velocidade, 0);
        rb.position = new Vector2(rb.position.x, posicaoY);
    }

    // colisao e dano
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health jogadorHealth = collision.gameObject.GetComponent<Health>(); // script de saude

            if (jogadorHealth != null)
            {
                // dano
                jogadorHealth.ReceberDano(dano);
            }
        }
    }
}