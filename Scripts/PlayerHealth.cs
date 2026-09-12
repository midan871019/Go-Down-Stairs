using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;

    public float currentHealth;

    public Action<float, float> OnHealthChanged;
    public Action OnDamaged;
    public Action OnHealed;

    void Start()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log("受傷：" + currentHealth);

        OnDamaged?.Invoke();
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;

        currentHealth =
            Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log("回血：" + currentHealth);

        OnHealed?.Invoke();
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    void Die()
    {
        Debug.Log("玩家死亡");

        GameManager.Instance.roundManager.GameOver();
    }
}