using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    private float healthAmount;
    private float maxHealth;
    private Animator animator;
    public GameObject player;

    public GameObject healthBarUI;
    public Slider slider;
    private bool alive;

    // Start is called before the first frame update
    void Start()
    {
        alive = true;
        animator = gameObject.GetComponent<Animator>();
        maxHealth = 1;
        healthAmount = maxHealth;
        slider.value = healthAmount;

    }

    // Update is called once per frame
    void Update()
    {
        AttackIfPlayerNearby();
        healthBarUI.gameObject.transform.rotation = player.transform.rotation;
        CheckPlayerDistance();

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            CalculateHealth(0.5f);
            
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

    public void CalculateHealth(float amount)
    {
        healthAmount -= amount;
        CheckHealth();
        StartCoroutine("AnimateSlider");
        
    }

    public void Die()
    {
        alive = false;
        //healthBarUI.SetActive(false);
        DisableFollowPlayer();
        DisableRandomMovement();
        animator.Play("Dying");
    }

    private void DisableFollowPlayer()
    {
        gameObject.GetComponent<FollowPlayer>().enabled = false;
    }

    private void DisableRandomMovement()
    {
        gameObject.GetComponent<RandomMovement>().enabled = false;
    }

    public void EndAttack()
    {
        var dist = Vector3.Distance(player.transform.position, gameObject.transform.position);

        if(dist <= 1)
        {
            player.GetComponent<Player>().CalculateHealth(0.5f);
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

    private void EndReact()
    {
        animator.Rebind();
    }

    IEnumerator AnimateSlider()
    {
        slider.value = healthAmount;
        if (healthAmount < maxHealth)
        {
            healthBarUI.SetActive(true);
        }

        if(alive == true)
        {
            yield return new WaitForSeconds(2f);
        }
        else
        {
            yield return new WaitForSeconds(1f);
        }
        
        healthBarUI.SetActive(false);
    }

    private void CheckPlayerDistance()
    {
        var dist = Vector3.Distance(gameObject.transform.position, player.transform.position);
        if(dist <= 10)
        {
            gameObject.GetComponent<FollowPlayer>().enabled = true;
            gameObject.GetComponent<RandomMovement>().enabled = false;
        }
        else
        {
            if (gameObject.GetComponent<FollowPlayer>().enabled == true)
            {
                gameObject.GetComponent<FollowPlayer>().enabled = false;
            }
            if(gameObject.GetComponent<RandomMovement>().enabled == false)
            {
                gameObject.GetComponent<RandomMovement>().enabled = true;
            }
        }
    }


}
