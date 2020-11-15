using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private float healthAmount;
    private float maxHealth;
    private Animator animator;
    private GameObject player;
    private Player playerScript;
    private GameObject cam;

    public GameObject healthBarUI;
    public Slider slider;
    private bool alive;
    private bool hitReact;
    private FollowPlayer followPlayer;
    private RandomMovement randomMovement;
    private EnemyController enemyController;
    private NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        followPlayer = gameObject.GetComponent<FollowPlayer>();
        randomMovement = gameObject.GetComponent<RandomMovement>();
        enemyController = gameObject.GetComponent<EnemyController>();
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        alive = true;
        animator = gameObject.GetComponent<Animator>();
        maxHealth = 1;
        healthAmount = maxHealth;
        slider.value = healthAmount;
        hitReact = false;

    }

    // Update is called once per frame
    void Update()
    {
        AttackIfPlayerNearby();
        SetSliderRotation();
        CheckPlayerDistance();

    }

    private void SetSliderRotation()
    {
        healthBarUI.gameObject.transform.rotation = cam.transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            CalculateHealth(0.5f);
            
            if(healthAmount > 0)
            {
                hitReact = true;
                navMeshAgent.enabled = false;
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
        DeactivateControlScripts();
        animator.Play("Dying");
    }

    private void DeactivateControlScripts()
    {
        navMeshAgent.enabled = false;
        randomMovement.enabled = false;
        followPlayer.enabled = false;
        enemyController.enabled = false;
    }

    public void EndAttack()
    {
        var dist = Vector3.Distance(player.transform.position, gameObject.transform.position);

        if(dist <= 1)
        {
            playerScript.CalculateHealth(0.5f);
        }
    }

    private void AttackIfPlayerNearby()
    {
        var dist = Vector3.Distance(gameObject.transform.position, player.transform.position);
        var playerAlive = playerScript.alive;

        if (dist <= 1 && playerAlive == true && hitReact == false)
        {
            navMeshAgent.enabled = false;
            animator.Play("Standing Melee Attack Downward");
        }
    }

    private void StopAttack()
    {
        var dist = Vector3.Distance(gameObject.transform.position, player.transform.position);
        var playerAlive = playerScript.alive;
        navMeshAgent.enabled = true;

        if (dist > 1 || playerAlive == false)
        {
            animator.Rebind();
        }
    }

    private void EndReact()
    {
        animator.Rebind();
        navMeshAgent.enabled = true;
        hitReact = false;
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
        bool playerAlive = playerScript.alive;
        var dist = Vector3.Distance(gameObject.transform.position, player.transform.position);

        if(dist <= 10 && playerAlive)
        {
            followPlayer.enabled = true;
            randomMovement.enabled = false;
        }
        else
        { 
            if (followPlayer.enabled == true)
            {
                followPlayer.enabled = false;
            }
            if(randomMovement.enabled == false)
            {
                randomMovement.enabled = true;
            }
        }
    }

}
