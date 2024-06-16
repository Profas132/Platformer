using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHP : MonoBehaviour
{
    [SerializeField] private float maxHP;
    public float currentHP;

    private void Awake()
    {
        currentHP = maxHP;
    }

    public void takeAHit(float damage)
    {
        currentHP -= damage;

        Debug.Log(currentHP);
    }
}
