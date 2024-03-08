using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
    public Transform target; // Referencia al transform del jugador
    public Vector3 offset; // Offset de la cámara con respecto al jugador

    void LateUpdate()
    {
        if (target != null)
        {
            // Actualiza la posición de la cámara para que siga al jugador con el offset dado
            transform.position = target.position + offset;

            // Mantiene la orientación de la cámara mirando hacia el jugador
            transform.LookAt(target);
        }
    }
}
