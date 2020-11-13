using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float healthAmount;
    private Animator animator;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        healthAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        AttackIfPlayerNearby();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            healthAmount -= 0.5f;
            
            if(healthAmount > 0)
            {
                animator.Play("Standing React Large Gut");
            }
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
        gameObject.GetComponent<FollowPlayer>().enabled = false;
        animator.Play("Dying");
    }

    public void EndAttack()
    {
        var dist = Vector3.Distance(player.transform.position, gameObject.transform.position);

        if(dist <= 1)
        {
            player.GetComponent<Player>().CalculateDamage(0.5f);
        }
    }

    private void AttackIfPlayerNearby()
    {
        var dist = Vector3.Distance(gameObject.transform.position, player.transform.position);
        if(dist <= 1 && player.GetComponent<Player>().alive == true)
        {
            animator.Play("Standing Melee Attack Downward");
        }
    }

    private void StopAttack()
    {
        var dist = Vector3.Distance(gameObject.transform.position, player.transform.position);
        if (dist > 1 || player.GetComponent<Player>().alive == false)
        {
            animator.Rebind();
        }
    }


}
