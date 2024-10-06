using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class enemyBehaviour : MonoBehaviour
{

    [SerializeField][Range(0, 10)] private float detectRange;
    [SerializeField] private float moveSpeed;
    [SerializeField][Range(0, 1)] private float groundCheckDist;

    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private LayerMask groundLayerMask;


    private void Start()
    {
        Physics2D.queriesHitTriggers = false;
    }


    private void MoveTowardsPlayer(Transform playerPosition)
    {
        bool onGround = Physics2D.Raycast(transform.position, Vector3.down, groundCheckDist, groundLayerMask);
        
        // если рядом стенка можно попробовать ее перепрыгнуть 
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2((playerPosition.position - transform.position).x, 0f).normalized, detectRange);
        //Debug.DrawRay(transform.position, new Vector2((playerPosition.position - transform.position).x, 0f), Color.red);
        if (hit != playerPosition.gameObject && onGround)
        {
            enemyJump();
        }


        hit = Physics2D.Raycast(transform.position, playerPosition.position - transform.position, detectRange);
        Debug.DrawRay(transform.position, playerPosition.position - transform.position, Color.red);
        if (hit.transform == transform && onGround) //а нужна ли тут проверка на землю? 
        {
            Debug.Log(hit.transform);
            if (transform.position.x < playerPosition.position.x)
            {
                enemyMove(moveSpeed, 180);
            }
            else
            {
                enemyMove(-moveSpeed, 0);
            }
        }
    }

    private void enemyMove(float moveSpeed, int y)
    {
        transform.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
        transform.eulerAngles = new Vector3(0, y, 0);
    }

    private void enemyJump()
    {
        //jump
    }

    void FixedUpdate()
    {

        Collider2D[] playerPosition = Physics2D.OverlapCircleAll(transform.position, detectRange, playerLayerMask); //поиск игрока

<<<<<<< HEAD
        foreach (var item in playerPosition)//перебор всех вариантов играков
        {
            //UnityEngine.Debug.Log("opaopa");
            MoveTowardsPlayer(item.transform);
=======
        foreach (var target in playerPosition)//перебор всех вариантов играков
        {
            MoveTowardsPlayer(target.transform);
>>>>>>> 9b6ef5bf3e65651d251f18720d84fa69caafa7a9
        }

        Debug.DrawRay(transform.position, Vector3.down * groundCheckDist, Color.green);//луч для сверки с землёй

    }
    private void OnDrawGizmosSelected()//визуализация радиуса обнаружения
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}
