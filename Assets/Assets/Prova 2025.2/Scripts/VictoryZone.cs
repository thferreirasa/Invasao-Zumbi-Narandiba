using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryZone : MonoBehaviour
{
    public GameObject winCanvas;
    public GameObject player;
    public float tempoParaVoltar = 3f;

    private bool isWin = false;
    private float timerToReturn; // contagem regressiva

    void Start()
    {
        timerToReturn = tempoParaVoltar;
    }

    void Update()
    {
        if (isWin)
        {
            // contagem regressiva
            timerToReturn -= Time.deltaTime;

            if (timerToReturn <= 0)
            {
                // volta pro menu inicial
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isWin = true;

            // ativa o canvas de vitoria
            if (winCanvas != null)
            {
                winCanvas.SetActive(true);
            }

            // impede movimento do jogador
            if (player != null)
            {
                PlayerController controller = player.GetComponent<PlayerController>();
                if (controller != null)
                {
                    controller.enabled = false;
                }
            }

            // desativa a area de win
            gameObject.SetActive(false);
        }
    }
}