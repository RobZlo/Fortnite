using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        var hit = collision.gameObject;
        var health = hit.GetComponent<Health>();
        if (health != null)
        {
            // it takes 5 bullets to destroy an object
            health.CalculateDamage(20);
        }
        Destroy(gameObject);
    }
}
