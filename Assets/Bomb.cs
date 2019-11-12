using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float delay;
    public float explosionForce;
    public float explosionRadius;
    public float upModifier;
    public LayerMask interactionMask;
    public GameObject particlePrefab;
    private GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("explode", delay);
    }

    void explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius, interactionMask);
        foreach(Collider c in hitColliders)
        {
            Rigidbody r = c.GetComponent<Rigidbody>();
            r.AddExplosionForce(explosionForce, transform.position, explosionRadius, upModifier);
        }
        gameObject.GetComponent<AudioSource>().Play();
        explosion = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        Invoke("kill", 3f);

    }

    void kill()
    {
        Destroy(gameObject);
    }
}
