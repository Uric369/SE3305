using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("基本属性")]
    public float maxHealth;
    public float currentHealth;
    public float HealingHealth;

    [Header("受伤无敌")]
    public float invulnerableDuration;
    private float invulnerableCounter;
    public bool invulnerable;

    public UnityEvent<Character> onBloodChange;
    public UnityEvent<Transform> onTakeDamage;
    public UnityEvent onDie;
    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = maxHealth;
        onBloodChange.Invoke(this);
    }
    public void Restart()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (invulnerable)
        {
            invulnerableCounter -= Time.deltaTime;
            if (invulnerableCounter <= 0)
            {
                invulnerable = false;
            }
        }
    }
    public void TakeDamage(Attack attacker)
    {
        // Debug.Log(attacker.damage);
        if (invulnerable) return;
        if (currentHealth > attacker.damage)
        {
            currentHealth -= attacker.damage;
            // 受伤事件触发
            onTakeDamage?.Invoke(attacker.transform);
        }
        else
        {
            if (currentHealth > 0) onDie?.Invoke();
            currentHealth = 0;
            
        }
        TriggerInvulnerable();
        onBloodChange?.Invoke(this);
    }

    private void TriggerInvulnerable()
    {
        if (!invulnerable)
        {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
    }

    public void Heal()
    {
        if (currentHealth + HealingHealth > maxHealth) currentHealth = maxHealth;
        else currentHealth += HealingHealth;
    }
}
