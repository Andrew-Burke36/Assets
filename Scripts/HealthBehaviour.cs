using UnityEngine;

public class HealthBehaviour
{
    // Fields 
    int currentHealth;
    int maxHealth;

    // Properties
    public int Health
    {
        get
        {
            return currentHealth;
        }

        set
        {
            currentHealth = value;
        }
    }

    public int MaxHealth
    {
        get
        {
            return maxHealth;
        }
        set
        {
            maxHealth = value;
        }
    }

    public HealthBehaviour(int health, int MaxHealth)
    {
        currentHealth = health;
        maxHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            if ( currentHealth < 0 )
            {
                currentHealth = 0; // Ensure health does not go below zero
            }
        }
    }

    public void Heal(int healAmount)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += healAmount;
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // Ensure health does not exceed max health
        }
    }
}

