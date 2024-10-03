using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class attack : MonoBehaviour
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
        if (Input.GetButton("Fire1")) animator.SetTrigger("isAttack");
        if (Input.GetButton("Fire2")) animator.SetTrigger("forceAttack");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(weaponCollider2D.transform.position, AttackRange);
    }

    private void hit(float damage) //���� ����� �������� � �������� ��� ������� ��� �������
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(weaponCollider2D.transform.position, AttackRange, DamageableLayerMask);
        int i = 0;
        foreach (var item in enemies)
        {
            //�������� ���� �� �����
            enemies[i].GetComponent<enemyHP>().takeAHit(damage);
            i++;
        }

        //UnityEngine.Debug.Log(damage);
    }

}
