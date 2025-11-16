using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxSaude = 3;

    // inimigo ou player
    public bool isEnemy = false;

    // coisas da HUD
    public Slider healthSlider;

    [HideInInspector] public int saudeAtual;

    public GameObject gameOverCanvas;

    void Start()
    {
        saudeAtual = maxSaude;

        // slider de saude
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxSaude;
            healthSlider.value = saudeAtual;
        }
    }

    public void ReceberDano(int dano)
    {
        saudeAtual -= dano;

        // atualiza slider com a saude
        if (healthSlider != null)
        {
            healthSlider.value = saudeAtual;
        }

        // morte
        if (saudeAtual <= 0)
        {
            Morrer();
        }
    }

    void Morrer()
    {
        // morte para inimigos
        if (isEnemy)
        {
            // encontra jogador
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                // script de ataque do jogador
                PlayerAttack playerAttack = player.GetComponent<PlayerAttack>();

                if (playerAttack != null)
                {
                    // ativa contador de mortes
                    playerAttack.ContarMorte();
                }
            }

            // destroi o inimigo
            Destroy(gameObject, 0.1f);
            return;
        }

        // morte do jogador
        // ativa tela de game over
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }

        // desativa movimento do jogador
        PlayerController controller = GetComponent<PlayerController>();
        if (controller != null) controller.enabled = false;

        PlayerAttack attack = GetComponent<PlayerAttack>();
        if (attack != null) attack.enabled = false;

        // destroi objeto
        Destroy(gameObject, 0.1f);
    }

    public void Curar(int quantidade)
    {
        // aumenta a saude, mas nao pode passar da saude maxima
        saudeAtual = Mathf.Min(saudeAtual + quantidade, maxSaude);

        // atualiza slider
        if (healthSlider != null)
        {
            healthSlider.value = saudeAtual;
        }
    }
}