using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerAttack : MonoBehaviour
{
    // danos e morte
    public int danoForma1 = 1;
    public int danoForma2 = 3;
    public int mortesParaForma2 = 5;

    // sprites forma 1 e 2
    public Sprite spriteForma1;
    public Sprite spriteForma2;

    // ataque
    public float raioDeAtaque = 1.5f;
    public LayerMask layerInimigo;

    [HideInInspector] public bool emForma2 = false;
    private int numeroMortes = 0;

    private SpriteRenderer sr;
    private Animator anim;

    public TMPro.TextMeshProUGUI killCountText;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        // inicia o jogo com a forma 1
        if (sr != null)
        {
            sr.sprite = spriteForma1;
        }
    }

    void Update()
    {
        // tecla E ataca
        if (Input.GetKeyDown(KeyCode.E))
        {
            Atacar();
        }
    }

    void Atacar()
    {
        // define o dano de acordo com a forma do player
        int danoAtual = emForma2 ? danoForma2 : danoForma1;

        // usa o raio de ataque para encontrar inimigos
        Collider2D[] inimigosAtingidos = Physics2D.OverlapCircleAll(transform.position, raioDeAtaque, layerInimigo);

        // aplica o dano
        foreach (Collider2D inimigo in inimigosAtingidos)
        {
            Health inimigoHealth = inimigo.GetComponent<Health>();

            if (inimigoHealth != null)
            {
                inimigoHealth.ReceberDano(danoAtual);
            }
        }
    }

    public void ContarMorte()
    {
        if (emForma2) return;

        numeroMortes++;

        if (killCountText != null)
        {
            killCountText.text = numeroMortes.ToString();
        }

        if (numeroMortes >= mortesParaForma2)
        {
            emForma2 = true;

            if (sr != null && spriteForma2 != null)
            {
                sr.sprite = spriteForma2;
            }

            if (anim != null)
            {
                anim.SetBool("IsForm2", true);
            }
        }
    }

    // Gizmo no editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, raioDeAtaque);
    }
}