using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBox : MonoBehaviour
{
    public int quantCura = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // verifica colisao - tag Player
        if (other.CompareTag("Player"))
        {
            // pega dados do script de saude
            Health playerHealth = other.GetComponent<Health>();

            if (playerHealth != null)
            {
                // cura
                playerHealth.Curar(quantCura);

                // destroi o objeto para cura
                Destroy(gameObject);
            }
        }
    }
}