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
    private void Start()
    {
        targetTransform = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        Physics2D.queriesHitTriggers = false;
    }

    void MoveTowardsPlayer()
    {
        if (transform.position.x < targetTransform.position.x)
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
        if (hit.transform.tag == "Player")
        {
            Debug.DrawRay(transform.position, targetTransform.position - transform.position, Color.red);
            MoveTowardsPlayer();
        }
        else
        {
            Patrol();
        }


    }
}
