using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour, IHealth
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] float meleeCooldown = 0.5f;
    [SerializeField] float meleeMaxDistance = 2f;
    [SerializeField] int meleeDamage = 50;

    [SerializeField] KeyCode shootButton = KeyCode.Mouse0;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform spawnFrom;
    [SerializeField] float shootCooldown = 0.5f;

    int currentHealth;
    float lastMeleeHit;
    float lastShot;

    private void Awake()
    {
        currentHealth = maxHealth;
        lastMeleeHit = Time.time;
    }

    private void Update()
    {
        if (Input.GetKeyDown(shootButton))
        {
            Shoot();
        }
    }

    public void MeleeAttack()
    {
        if (Time.time > lastMeleeHit + meleeCooldown)
        {
            lastMeleeHit = Time.time;

            //play melee animation

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity))
            {
                IHealth _healthSystem = hit.transform.gameObject.GetComponent<IHealth>();

                if (_healthSystem != null)
                {
                    if (hit.distance < meleeMaxDistance)
                    {
                        _healthSystem.DoDamage(meleeDamage);
                    }
                }
            }
        }
    }

    public void Shoot()
    {
        if (Time.time > lastShot + shootCooldown)
        {
            lastShot = Time.time;

            Instantiate(bulletPrefab, spawnFrom.position, spawnFrom.rotation);
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
