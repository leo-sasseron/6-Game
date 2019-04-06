﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth = 10f;
    public GameObject hitEffect, healthBar;
    public bool isEnemy = true;
    public int minScore = 25, maxScore = 50;
    public float currentHealth;

    private string tagName = "Bullet";    
    private DeathSystem deathScript;
    private bool dead;
   
    void OnEnable ()
    {
        if (isEnemy)
        {
            tagName = "Bullet";
        }
        else
        {
            tagName = "EnemyBullet";
            maxHealth = StatsManager.instance.GetStatsValue("Health", StatsManager.instance.healthUpgradeList);
        }
        
        currentHealth = maxHealth;
	}

    private void Start()
    {
        LevelManager.instance.RegisterEnemy();
        deathScript = GetComponent<DeathSystem>();        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagName))
        {
            if (!isEnemy)
                LevelManager.instance.PlayerHit();

            Vector3 triggerPosition = other.ClosestPointOnBounds(transform.position);
            Vector3 direction = triggerPosition - transform.position;

            GameObject fx = PoolingManager.instance.UseObject(hitEffect, triggerPosition, Quaternion.LookRotation(direction));

            PoolingManager.instance.ReturnObject(fx, 1f);

            //damage 
            float damage = float.Parse(other.name);
            TakeDamage(damage);

            PoolingManager.instance.ReturnObject(other.gameObject);
        }

        if (other.CompareTag("Coin"))
        {
            currentHealth++;
            UpdateUI();
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        CheckHealth();
        UpdateUI();
    }

    void CheckHealth()
    {
        if (currentHealth <= 0f)
        {
            if (healthBar != null)
                healthBar.transform.parent.gameObject.SetActive(false);

            //die
            if (deathScript != null)
                deathScript.Death();

            //if its enemy, add points

            if (isEnemy && !dead)
            {
                dead = true;
                gameObject.tag = "Untagged";
                LevelManager.instance.AddEnemyKill(Random.Range(minScore, maxScore));
            }            
        }
    }

    void UpdateUI()
    {
        if (healthBar != null)
        {
            Vector3 scale = Vector3.one;
            float value = currentHealth / maxHealth;
            scale.x = value;
            healthBar.transform.localScale = scale;
        }    
    }
}