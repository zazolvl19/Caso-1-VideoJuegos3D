using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
    public Transform target; // Referencia al transform del jugador
    public Vector3 offset; // Offset de la c�mara con respecto al jugador

    void LateUpdate()
    {
        if (target != null)
        {
            // Actualiza la posici�n de la c�mara para que siga al jugador con el offset dado
            transform.position = target.position + offset;

            // Mantiene la orientaci�n de la c�mara mirando hacia el jugador
            transform.LookAt(target);
        }
    }
}
