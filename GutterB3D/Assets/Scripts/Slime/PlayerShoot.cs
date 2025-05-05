using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform fireBase;
    public Transform firePoint;

    [Header("Projectile Prefabs")]
    public GameObject slimeballPrefab;
    public GameObject fireballPrefab;

    public float projectileSpeed = 10f;
    public float attackRate = 2f;

    [Header("Switch Between Slimeballs and Fireballs")]
    public bool useFireball = false;

    private float nextAttackTime = 0f;

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetAxis("Attack") > 0)
            {
                playerFire();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void playerFire()
    {
        Vector3 fwd = (firePoint.position - fireBase.position).normalized;

        GameObject prefabToUse = useFireball ? fireballPrefab : slimeballPrefab;

        if (prefabToUse == null)
        {
            Debug.LogWarning("Projectile prefab not assigned!");
            return;
        }

        GameObject projectile = Instantiate(prefabToUse, firePoint.position, transform.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(fwd * projectileSpeed, ForceMode.Impulse);
        }
    }
}
