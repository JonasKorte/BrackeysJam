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

    private void Awake()
    {
        currentHealth = maxHealth;
        lastMeleeHit = Time.time;
    }

    public void MeleeAttack()
    {
        if (Time.time > lastMeleeHit + meleeCooldown)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, meleeMaxDistance);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].transform.gameObject.CompareTag("Player"))
                {
                    if (Vector3.Distance(transform.position, colliders[i].transform.position) < meleeMaxDistance)
                    {
                        IHealth _healthSystem = colliders[i].transform.gameObject.GetComponent<IHealth>();

                        if (_healthSystem != null)
                        {
                            _healthSystem.DoDamage(meleeDamage);
                        }
                        else
                        {
                            Debug.LogError("Expected player to have a health system but didn't find one", this);
                        }
                    }

                    break;
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
            Destroy(this.gameObject);
        }
    }
}
