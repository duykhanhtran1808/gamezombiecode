using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPSCamera;
    [SerializeField] float range = 100f; //Tam cua sung
    [SerializeField] float attackSpeed = 0.2f; //Toc do ban bao nhieu lau 1 vien, cang thap ban cang nhanh
    [SerializeField] float damage = 20f; //damage cua sung
    [SerializeField] ParticleSystem muzzleFlash; //Lua o dau sung
    [SerializeField] GameObject hitEffect; //Lua o vi tri bi ban
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] TextMeshProUGUI ammoDescText;

    private bool isShooting = false;
    private bool allowShoot = true;
    Coroutine oneShot = null;
    AudioSource gunShotSFX;
    private void OnEnable()
    {
        oneShot = null;
        StopAllCoroutines();
    }
    private void Start()
    {
        gunShotSFX = GetComponent<AudioSource>();
    }
    void Update()
    {
        DisplayAmmo();
        if (allowShoot)
        {
            if (Input.GetButton("Fire1"))
            {
                isShooting = true;
            }

            if (Input.GetButtonUp("Fire1"))
            {
                isShooting = false;
            }

            if (isShooting && oneShot == null)
            {
                if(ammoSlot.GetCurrentAmmo(ammoType) > 1)
                {
                    ammoSlot.ReduceCurrentAmmo(ammoType);
                    oneShot = StartCoroutine(Shoot());
                }
            }
        }
        
    }

    private void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        string gunName = this.gameObject.name;
        string descText = ammoType.ToString() == "PistolBullets" ? "0,4 giây/viên - 40 Damage" :
                          (ammoType.ToString() == "ShotgunShells" ? "1 giây/viên - 100 Damage" :
                                                                    "0,1 giây/viên - 20 Damage");
        
        ammoText.text = gunName + ": " + currentAmmo;
        ammoDescText.text = descText;
    }

    public void LockShoot()
    {
        allowShoot = false;
    }

    public void UnlockShoot()
    {
        allowShoot = true;
    }
    IEnumerator Shoot()
    {
        if(gunShotSFX != null)
        {
            gunShotSFX.Play();
        }
        PlayMuzzleFlash();
        ProcessRaycast();
        yield return new WaitForSeconds(attackSpeed);
        oneShot = null;
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPSCamera.transform.position, FPSCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            EnemyHealth targetHealth = hit.transform.GetComponent<EnemyHealth>();
            if (targetHealth == null) return;
            targetHealth.TakeDamage(damage);
        }
        else return;
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal)); //Huong quay doi dien voi mat phang
        Destroy(impact, 1f);
    }

    private void OnDisable()
    {
        if(gunShotSFX != null)
            gunShotSFX.Stop();
    }
}
