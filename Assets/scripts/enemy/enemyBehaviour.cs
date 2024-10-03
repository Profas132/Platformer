using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehaviour : MonoBehaviour
{

    [SerializeField] private float detectRange;
    [SerializeField] private float speed;
    [SerializeField] private Transform leftBorder;
    [SerializeField] private Transform rightBorder;
    [SerializeField] private LayerMask defaultLayerMask;
    [SerializeField] private LayerMask groundLayerMask;

    
    private Transform targetTransform;
    private Vector3 targetPos;

    private void Start()
    {
        targetTransform = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        Physics2D.queriesHitTriggers = false;
        targetPos = targetTransform.position;
    }

    void MoveTowardsPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2((targetPos - transform.position).x, 0f).normalized, detectRange);

        if (hit.transform.tag == "Ground")
        {
            // надо прыгнуть по идее
        }
        

        if (transform.position.x < targetPos.x)
        {
            if (Physics2D.Raycast(rightBorder.position, Vector3.down, 1f, groundLayerMask)) // вправо
            {
                transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
            }
        }
        else
        {
            if (Physics2D.Raycast(leftBorder.position, Vector3.down, 1f, groundLayerMask)) // влево
            {
                transform.position += new Vector3(-speed * Time.deltaTime, 0f, 0f);
            }
        }
    }

    void Patrol()
    {
        /*if (Physics2D.Raycast(rightBorder.position, Vector3.down, 1f, groundLayerMask)) // вправо
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
        }
        if (Physics2D.Raycast(leftBorder.position, Vector3.down, 1f, groundLayerMask)) // влево
        {
            transform.position += new Vector3(-speed * Time.deltaTime, 0f, 0f);
        }*/
    }


    void Update()
    {
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetTransform.position - transform.position, detectRange, defaultLayerMask);
        
        if (hit)
        {
            if (hit.transform.tag == "Player")
            targetPos = targetTransform.position;
            Debug.DrawRay(transform.position, targetPos - transform.position, Color.red);
            MoveTowardsPlayer();
        }
        else
        {
            MoveTowardsPlayer(); // тут надо патрулировать lastpos
        }


    }
}
