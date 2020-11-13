using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float healthAmount;

    // Start is called before the first frame update
    void Start()
    {
        healthAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            healthAmount -= 0.5f;
            CheckHealth();
        }


    }

    private void CheckHealth()
    {
        if(healthAmount <= 0)
        {
            Die();
        }
    }

    public void CalculateDamage(float amount)
    {
        healthAmount -= amount;
        CheckHealth();
    }

    public void Die()
    {
        Animator anim = gameObject.GetComponent<Animator>();
        gameObject.GetComponent<FollowPlayer>().enabled = false;
        anim.Play("Dying");
    }


}
