using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class attack : Sounds
{
    [Header("Setting")]
    [SerializeField] private LayerMask DamageableLayerMask;
    [SerializeField] private Collider2D weaponCollider2D;
    [SerializeField][Range(0f, 10f)] private float AttackRange;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            animator.SetTrigger("isAttack");
        }
        if (Input.GetButton("Fire2")) 
        {
            animator.SetTrigger("forceAttack");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(weaponCollider2D.transform.position, AttackRange);
    }

    private void hit(float damage) //урон меням прямиком в анимации или вызывая эту функцию
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(weaponCollider2D.transform.position, AttackRange, DamageableLayerMask);

        foreach (var item in enemies)
        {
            //добавить урон по врагу
            item.GetComponent<enemyHP>().takeAHit(damage);

            PlaySound(sounds[0], p1: 0.5f, p2: 0.8f);
        }
        //UnityEngine.Debug.Log(damage);
    }

}
