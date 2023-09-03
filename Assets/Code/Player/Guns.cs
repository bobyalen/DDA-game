using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Guns : MonoBehaviour
{
    public float damage = 2;
    public float shotDelay; 
    public float reloadTime;
    public float range;
    public int magazineSize;
    public int ammo;
    bool shooting;
    bool canShoot = true;
    bool reloading;
    [SerializeField]
    PlayerModel playerModel;
    public Camera cam;
    public Transform shotDirection;
    [SerializeField] Animator animation;
    public TMP_Text ammoCounter;

    // Start is called before the first frame update


    public void selAWM()
    {

        GameObject player = GameObject.Find("AWM");
        player.SetActive(true);
        animation = player.GetComponent<Animator>();
        range= 250;
        magazineSize= 10;
        ammo= 10;
        shotDelay= 1;
        reloadTime= 1;
        damage= 95;

    }
    
    public void selM82()
    {
        GameObject player = GameObject.Find("BarrettM82");
        player.SetActive(true);
        animation = player.GetComponent<Animator>();
        range = 315;
        magazineSize = 5;
        ammo = 5;
        shotDelay = 1.5f;
        reloadTime = 2.2f;
        damage = 2;
    }
    
    public void selMRAD()
    {
        GameObject player = GameObject.Find("BarrettMRAD");
        player.SetActive(true);
        animation = player.GetComponent<Animator>();
        range = 200;
        magazineSize = 15;
        ammo = 15;
        shotDelay = 0.5f;
        reloadTime = 1;
        damage = 70;
    }
    void inputs()
    {
        shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (shooting && !reloading && ammo > 0 && canShoot) 
        {
            Shoot();
            Debug.Log("Shots Fired: " + playerModel.getShots());
            Debug.Log("Shots Hit: " + playerModel.getShotsHit());
        }

        if (Input.GetKeyDown(KeyCode.R) && !reloading && ammo < magazineSize)
        {
            Reload();
        }
    }
    void Shoot()
    {
        RaycastHit hit;
        canShoot = false;
        bool shotHit = false;
        animation.SetTrigger("Shot");
        ammo--;
       if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            string name = hit.transform.name;
            if(hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<DamageHandler>().Damage(damage);
                float health = hit.collider.GetComponent<DamageHandler>().getHealth();
                if (health <= 0)
                {
                    hit.collider.GetComponent<Bear>().die();
                }
                shotHit = true;
                hit.collider.GetComponent<Bear>().hit();
            }
        }
       playerModel.updateShots(shotHit);
        //Invoke("ResetShot", shotDelay);
    }

    public void ResetShot()
    {
        canShoot=true;
    }

    public void Reload()
    {
        canShoot = false;
        reloading = true;
        animation.SetTrigger("Reload");
        //resetReload();
    }

    public void resetReload()
    {
        ammo= magazineSize;
        reloading = false;
        canShoot= true;
        //animation.SetTrigger("Reload");
    }

    public int ammoCount()
    {
        return ammo;
    }

    // Update is called once per frame
    void Update()
    {
        inputs();
        ammoCounter.text = ammo.ToString() + "/" + magazineSize.ToString();
    }
}
