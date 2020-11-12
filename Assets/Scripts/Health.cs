using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public float healthAmount;

    void Start()
    {
        healthAmount = 1;
    }

    public void CalculateDamage(float amount)
    {
        healthAmount -= amount;
        if (healthAmount <= 0)
        {
            Destroy(gameObject);
        }
    }
}
