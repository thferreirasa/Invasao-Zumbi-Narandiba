using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // jogador
    public Transform player;

    // profundidade Z da câmera
    private float profundidadeCamera;

    void Start()
    {
        // manter profundidade original da camera
        profundidadeCamera = transform.position.z;
    }

    // LateUpdate - a camera só se move depois que o jogador se mover - evitar tremer
    void LateUpdate()
    {
        if (player != null)
        {
            // cria uma nova posição no X e Y do alvo
            Vector3 novaPosicao = new Vector3(player.position.x, player.position.y, profundidadeCamera);

            // aplica a nova posição na camera
            transform.position = novaPosicao;
        }
    }
}
