using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    public GameObject assaultRifle;
    public GameObject shotgun;

    public GameObject playerCam;
    public ParticleSystem muzzleFlash;

    public float range = 200f;
    public float damage = 25f;
    public float fireRate = 15f;
    public Animator playerAnimator;
    Vector3 dest = new Vector3();
    public LineRenderer lr;

    public int maxAmmo = 30;
    public int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    public Text ammoText;
    public GameObject reloadText;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
        LineRenderer lr = GetComponent<LineRenderer>();
    }

    private float nextTimeToFire = 0f;

    private void OnEnable()
    {
        isReloading = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
            return;

        if(currentAmmo == 0 || Input.GetButtonDown("Reload"))
        {
            StartCoroutine(Reload());
            return;
        }

        if (playerAnimator.GetBool("isShooting"))
        {
            playerAnimator.SetBool("isShooting", false);
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            lr.SetPosition(1, transform.position);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            playerAnimator.SetBool("isAiming", true);
        }
        if (Input.GetButtonUp("Fire2"))
        {
            playerAnimator.SetBool("isAiming", false);
        }

        if (Input.GetButtonDown("Weapon 1"))
        {
            assaultRifle.SetActive(true);
            shotgun.SetActive(false);
        }

        if (Input.GetButtonDown("Weapon 2"))
        {
            assaultRifle.SetActive(false);
            shotgun.SetActive(true);
        }

       

    }

    void Shoot()
    {
        muzzleFlash.Play();

        playerAnimator.SetBool("isShooting", true);

        RaycastHit hit;

        currentAmmo--;
        ammoText.text = currentAmmo.ToString() + "/30 AMMO";
        lr.SetPosition(0, transform.position);

        
        if (Physics.Raycast(playerCam.transform.position, transform.forward, out hit, range))
        {
            dest = hit.point;
            lr.SetPosition(1, dest);

            EnemyManager enemyManager = hit.transform.GetComponent<EnemyManager>();
            if(enemyManager != null)
            {
                enemyManager.Hit(damage);
            }
        }

    }

  

    IEnumerator Reload()
    {
        Debug.Log("Reloading...");
        isReloading = true;
        reloadText.SetActive(true);
        playerAnimator.SetBool("isReloading", true);
        
        yield return new WaitForSeconds(reloadTime - 0.25f);
        playerAnimator.SetBool("isReloading", false);
        yield return new WaitForSeconds(0.25f);
        reloadText.SetActive(false);
        isReloading = false;
        currentAmmo = maxAmmo;
    }

    
}
