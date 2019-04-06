﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    public ShootProfile shootProfile;
    public GameObject bulletPrefabs;
    public Transform firePoint;

    private float totalSpread;
    private WaitForSeconds rate, interval;
  
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            interval = new WaitForSeconds(shootProfile.interval);
            rate = new WaitForSeconds(shootProfile.fireRate);

            if (firePoint == null)
                firePoint = transform;

            totalSpread = shootProfile.spread * shootProfile.amount;

            StartCoroutine(ShootingSequence());
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
            StopAllCoroutines();
    }
   
    IEnumerator ShootingSequence()
    {
        yield return rate;

        while (true)
        {
            float angle = 0f;

                for (int i = 0; i < shootProfile.amount; i++)
                {
                    angle = totalSpread * (i / (float)shootProfile.amount);
                    angle -= (totalSpread / 2f) - (shootProfile.spread / shootProfile.amount);

                    Shoot(angle);

                    if (shootProfile.fireRate > 0f)
                        yield return rate;
                }
        
            yield return interval;
        }
    }

    void Shoot(float angle)
    {
        GameObject temp = PoolingManager.instance.UseObject(bulletPrefabs, firePoint.position, firePoint.rotation);
        temp.name = shootProfile.damage.ToString();
        temp.transform.Rotate(Vector3.up, angle);
        temp.GetComponent<BulletMove>().speed = shootProfile.speed;
    }
}