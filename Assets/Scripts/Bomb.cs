using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public float delay;
    public float explosionForce;
    public float explosionRadius;
    public float upModifier;
    public float closeAreaEffect = 1.5f;
    public float mediumAreaEffect = 3f;
    public float farAreaEffect = 5f;
    public LayerMask interactionMask;
    public GameObject explosionEffectPrefab;
    private GameObject explosionEffectInstance;


    // Use this for initialization
    void Start()
    {
        Invoke("explode", delay);
    }


    void explode()
    {

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider c in hitColliders)
        {
            // It is possible to destroy objects with either the Building Block or the Tree tag
            if (c.gameObject.tag == "BuildingObject" || c.gameObject.tag == "Tree")
            {
                //Calculate distance between bomb and collider
                float distance = Vector3.Distance(c.transform.position, transform.position);
                var health = c.GetComponent<Health>();
                if (health != null)
                {
                    // collider takes damage depending on the distance
                    if (distance <= closeAreaEffect)
                    {
                        health.CalculateDamage(1);
                    }
                    else if (distance <= mediumAreaEffect)
                    {
                        health.CalculateDamage(0.5f);
                    }
                    else if (distance <= farAreaEffect)
                    {
                        health.CalculateDamage(0.2f);
                    }
                }
            }
            else if (c.gameObject.tag == "Enemy")
            {
                float distance = Vector3.Distance(c.transform.position, transform.position);
                var health = c.GetComponent<HealthEnemy>();
                
                    // collider takes damage depending on the distance
                    if (distance <= closeAreaEffect)
                    {
                        health.CalculateDamage(1);
                    }
                    else if (distance <= mediumAreaEffect)
                    {
                        health.CalculateDamage(0.5f);
                    }
                    else if (distance <= farAreaEffect)
                    {
                        health.CalculateDamage(0.2f);
                    }
                
            }
            else if (c.GetComponent<Rigidbody>() != null)
            {
                Rigidbody r = c.GetComponent<Rigidbody>();
                r.AddExplosionForce(explosionForce, transform.position, explosionRadius, upModifier);
            }
        }

        explosionEffectInstance = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        gameObject.GetComponent<AudioSource>().Play();
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        Invoke("kill", 2f);
    }

    void kill()
    {
        Destroy(explosionEffectInstance);
        Destroy(gameObject);
    }

}
