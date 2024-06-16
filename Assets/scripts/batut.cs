using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batut : MonoBehaviour
{
    [SerializeField] private float maxImpulse;
    [SerializeField] private Collider2D jumpPodCollider2D;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        if (collision.gameObject.GetComponent<jump>())
        {
            Debug.Log("jump");
            if (collision.gameObject.GetComponent<jump>())
            {
                collision.rigidbody.AddForce(new Vector2(0, maxImpulse), ForceMode2D.Impulse);
            }
        }
    }
}