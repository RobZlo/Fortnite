using Invector.CharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Transform hand;
    public GameObject cubePrefab;
    public GameObject ballPrefab;
    public float throwForce;
    public Camera cam;
    public LayerMask interactionMask;
    public float maxRange;
    public GameObject bombPrefab;

    private GameObject objInHand;

    public static GameObject instance;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public int bulletVelocity;
    private float healthAmount;
    private Animator animator;
    public bool alive;
    public Slider slider;
    private vThirdPersonController vThirdPersonController;
    // Start is called before the first frame update


    void Awake()
    {
        vThirdPersonController = gameObject.GetComponent<vThirdPersonController>();
        alive = true;
        healthAmount = 1f;
        Player.instance = gameObject;
        animator = gameObject.GetComponent<Animator>();
        slider.value = healthAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            Instantiate(cubePrefab, hand.position, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameObject ball = Instantiate(ballPrefab, hand.position, Quaternion.identity);
            ball.GetComponent<Rigidbody>().AddForce(cam.transform.forward * throwForce);

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject bomb = Instantiate(bombPrefab, hand.position, Quaternion.identity);
            bomb.GetComponent<Rigidbody>().AddForce(cam.transform.forward * throwForce);

        }
              
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (Time.timeScale == 1f)
            {
                Time.timeScale = 0.33f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
          
            Fire();
        }

    }

    public void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        bullet.GetComponent<AudioSource>().Play();

        gameObject.GetComponent<BuildingController>().bulletsExists += 1;

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletVelocity;

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);

        Invoke("destroyBullet", 2.0f);
    }

    private void destroyBullet()
    {
        gameObject.GetComponent<BuildingController>().bulletsExists -= 1;
    }

    private void CheckHealth()
    {
        if (healthAmount <= 0)
        {
            Die();
        }
    }

    public void CalculateHealth(float amount)
    {
        healthAmount -= amount;
        slider.value = healthAmount;

        if (healthAmount > 0)
        {
            vThirdPersonController.enabled = false;
            animator.Play("Standing React Large Gut");
        }
        CheckHealth();
    }

    public void Die()
    {
        healthAmount = 0f;
        alive = false;
        gameObject.GetComponent<Player>().enabled = false;
        vThirdPersonController.enabled = false;
        animator.Play("Dying");
    }

    void EndReact()
    {
        vThirdPersonController.enabled = true;
        animator.Rebind();
    }
}
