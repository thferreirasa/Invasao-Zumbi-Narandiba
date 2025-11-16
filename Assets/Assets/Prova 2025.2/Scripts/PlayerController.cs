using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // variaveis
    public float velocidade = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // dados de posicao do jogador
        float posX = Input.GetAxisRaw("Horizontal");
        float posY = Input.GetAxisRaw("Vertical");

        // velocidade nova calculada
        Vector2 velocidadeNova = new Vector2(posX, posY).normalized * velocidade;

        // aplica a velocidade ao Rigidbody
        rb.velocity = velocidadeNova;

        // virar o sprite pra esquerda/direita
        if (posX != 0)
        {
            // esquerda
            if (posX < 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            // direita
            else
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }
}