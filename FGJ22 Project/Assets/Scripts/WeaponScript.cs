using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public Transform firePoint;
    public GameObject positiveBullet;
    public GameObject negativeBullet;
    public float bulletForce = 50f;
    public float fireRate = 2;
    public float nextShotPossible;

    private void Update()
    {
        // Shoot positive charge by pressing left mouse button
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > nextShotPossible)
        {
            nextShotPossible = Time.time + 1 / fireRate;
            ShootPositive();
        }

        // Shoot negative charge by pressing right mouse button
        if (Input.GetKeyDown(KeyCode.Mouse1) && Time.time > nextShotPossible)
        {
            nextShotPossible = Time.time + 1 / fireRate;
            ShootNegative();
        }
    }
    
    // Instantiate positive bullets and set speed
    void ShootPositive()
    {
        GameObject bulletInst = Instantiate(positiveBullet, firePoint.position, transform.rotation);
        bulletInst.GetComponent<Rigidbody>().velocity = transform.up * bulletForce;
    }

    // Instantiate negative bullets and set speed
    void ShootNegative()
    {
        GameObject bulletInst2 = Instantiate(negativeBullet, firePoint.position, transform.rotation);
        bulletInst2.GetComponent<Rigidbody>().velocity = transform.up * bulletForce;
    }
}
