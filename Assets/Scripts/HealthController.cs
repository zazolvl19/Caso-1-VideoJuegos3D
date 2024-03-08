using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    float maxHealth = 100.0f;
    float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= Mathf.Abs(damage);
        if (currentHealth <= 0.0f)
        {
            // Reiniciar la escena
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void Heal(float repair)
    {
        currentHealth += Mathf.Abs(repair);
    }
}
