using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHP : MonoBehaviour
{
    [SerializeField] private float maxHP;
    [SerializeField] private bool canDie;
    private float currentHP;

    private void Awake()
    {
        currentHP = maxHP;
    }

    public void takeAHit(float damage)
    {
        currentHP -= damage;
        if (currentHP < 0 && canDie) Die();
        Debug.Log(currentHP);
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
