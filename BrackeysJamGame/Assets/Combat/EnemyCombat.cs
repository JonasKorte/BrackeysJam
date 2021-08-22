using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour, IHealth
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] float meleeCooldown = 0.5f;
    [SerializeField] float meleeMaxDistance = 2f;
    [SerializeField] int meleeDamage = 20;

    int currentHealth;
    float lastMeleeHit;

    //later will replace it with a static reference to player instance, once there's a main player script or something
    Transform playerPos;

    private void Awake()
    {
        currentHealth = maxHealth;
        lastMeleeHit = Time.time;
    }

    public void MeleeAttack()
    {
        if (Time.time > lastMeleeHit + meleeCooldown)
        {
            if (Vector3.Distance(transform.position, playerPos.position) < meleeMaxDistance)
            {
                IHealth _healthSystem = playerPos.transform.gameObject.GetComponent<IHealth>();

                if (_healthSystem != null)
                {
                    _healthSystem.DoDamage(meleeDamage);
                }
                else
                {
                    Debug.LogError("Expected player to have a health system but didn't find one", this);
                }
            }
        }
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public void DoDamage(int _amount)
    {
        currentHealth -= _amount;

        if (currentHealth <= 0)
        {
            //die
        }
    }
}
