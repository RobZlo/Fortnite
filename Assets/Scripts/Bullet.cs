using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        var hit = collision.gameObject;
        if (hit.CompareTag("Enemy"))
        {
            Debug.Log("Hit Tag");

            var enemyController = hit.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.CalculateHealth(0.2f);
            }
        }
        else
        {
            var health = hit.GetComponent<Health>();
            if (health != null)
            {
                // it takes 5 bullets to destroy an object
                health.CalculateDamage(0.5f);
            }
        }

        gameObject.GetComponent<SphereCollider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
       
        

    }
}
