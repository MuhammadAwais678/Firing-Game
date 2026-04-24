using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponController : MonoBehaviour
{
    public enum FireType
    {
        SemiAuto,
        Auto
    }

    public FireType fireType = FireType.SemiAuto;
    public Camera playerCamer;
    public float fireRate = 0.2f;
    public int magazineSize = 15;
    public int maxBullets = 45;
    public Text bulletUI;
    public Text totalBullets;
    public ParticleSystem muzzleFlash;
    public GameObject bulletHole;
    public AudioClip fireSound;
    public AudioClip reloadSound;
    public float reloadTime = 0.5f;


    private AudioSource source;
    private int currentBullets;
    private float nextTimeToFire = 0f;
    private bool isReloading = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentBullets = magazineSize;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isReloading)
        {
            return;
        }
        if(fireType == FireType.SemiAuto)
        {
            if(Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
            {
                Shoot();
                nextTimeToFire = Time.time + fireRate;
               
            }
        }
        else if(fireType == FireType.Auto)
        {
            if(Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                Shoot();
                nextTimeToFire = Time.time + fireRate;
                
            }
        }

        if(Input.GetKeyDown(KeyCode.R) && !isReloading && currentBullets < magazineSize && maxBullets > 0)
        {
            StartCoroutine(Reload());
            source.PlayOneShot(reloadSound);
            return;
        }

        if(currentBullets <= 0 && maxBullets > 0)
        {
            StartCoroutine(Reload());
            source.PlayOneShot(reloadSound);
            return;
        }



        bulletUI.text = "Bullets: " + currentBullets.ToString();
        totalBullets.text = "Total: " + maxBullets.ToString();
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);
        int bulletsNeeded = magazineSize - currentBullets;
        int bulletsToReload = Mathf.Min(bulletsNeeded,maxBullets);
        currentBullets += bulletsToReload;
        maxBullets -= bulletsToReload;
        isReloading = false;
    }

    void Shoot()
    {

        if(currentBullets <= 0)
        {
            Debug.Log("Out of bullets!");
            return;
        }

        muzzleFlash.Play(true);
        source.PlayOneShot(fireSound);
        currentBullets--;

        Ray ray = new Ray(playerCamer.transform.position, playerCamer.transform.forward);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit,100f))
        {
            Debug.Log("Hit: " + hit.transform.name);
            GameObject hole = Instantiate(bulletHole, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(hole, 10);
        }
    }
}
