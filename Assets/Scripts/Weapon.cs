using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    
    public Transform firePoint;
    public GameObject bulletPrefab;
    public int maxAmmo = 30;
    public static int currentAmmo;
    public float reloadTime = 5f;
    public static bool isReloading = false;
    private float timeBtwShots;
    public float startTimeBtwShots;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if(isReloading)
            return;

        if(currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        if(timeBtwShots <= 0)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                timeBtwShots = startTimeBtwShots;
                Shoot();
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void Shoot()
    {
        //shooting logic
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        currentAmmo--;
    }
}
