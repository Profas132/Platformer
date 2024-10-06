using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class enemyBehaviour : MonoBehaviour
{

    [SerializeField][Range(0, 10)] private float detectRange;
    [SerializeField] private float moveSpeed;
    [SerializeField][Range(0, 1)] private float groundCheckDist;
    //[SerializeField] private Transform leftBorder;
    //[SerializeField] private Transform rightBorder;
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private LayerMask groundLayerMask;

    //private Transform targetTransform;
    //private Vector3 targetPos;

    //private void Start()
    //{
    //    //targetTransform = GameObject.FindGameObjectsWithTag("Player")[0].transform;
    //    //targetPos = targetTransform.position;
    //}

    private void MoveTowardsPlayer(Transform playerPosition)
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2((targetPos - transform.position).x, 0f).normalized, detectRange);

        //if (hit.transform.tag == "Ground")
        //{
        //    // ���� �������� �� ����
        //}

        if (Physics2D.Raycast(transform.position, Vector3.down, groundCheckDist, groundLayerMask))//�������� �� �����
        {
            if (transform.position.x < playerPosition.position.x)
                enemyMove(moveSpeed, 180);
            else enemyMove(-moveSpeed, 0);
        }
    }

    private void enemyMove(float moveSpeed, int y)
    {
        transform.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
        transform.eulerAngles = new Vector3(0, y, 0);
    }

    //void Patrol()
    //{
    //    if (Physics2D.Raycast(rightBorder.position, Vector3.down, 1f, groundLayerMask)) // ������
    //    {
    //        transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
    //    }
    //    if (Physics2D.Raycast(leftBorder.position, Vector3.down, 1f, groundLayerMask)) // �����
    //    {
    //        transform.position += new Vector3(-speed * Time.deltaTime, 0f, 0f);
    //    }
    //}

    void FixedUpdate()
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, targetTransform.position - transform.position, detectRange, defaultLayerMask);
        Collider2D[] playerPosition = Physics2D.OverlapCircleAll(transform.position, detectRange, playerLayerMask); //����� ������

        foreach (var item in playerPosition)//������� ���� ��������� �������
        {
            //UnityEngine.Debug.Log("opaopa");
            MoveTowardsPlayer(item.transform);
        }

        Debug.DrawRay(transform.position, Vector3.down * groundCheckDist, Color.green);//��� ��� ������ � �����

        //if (hit)
        //{
        //    targetPos = targetTransform.position;
        //    Debug.DrawRay(transform.position, targetPos - transform.position, Color.red);
        //    MoveTowardsPlayer();
        //}
        //else
        //{
        //    MoveTowardsPlayer(); // ��� ���� ������������� lastpos
        //}
    }
    private void OnDrawGizmosSelected()//������������ ������� �����������
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}
