using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class move : MonoBehaviour
{
    public Rigidbody2D RB;
    public float maxSpeed; //нужно будет подключать к данным персонажа

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {   
        float horizontal = Input.GetAxisRaw("Horizontal");
        Vector2 movement = new Vector2 (horizontal, 0);
        RB.AddForce(movement * maxSpeed * 200);

        if (horizontal > 0)
        {
            FlipPerson(0);
        }
        else if (horizontal < 0)
        {
            FlipPerson(-180);
        }
    }

    private void FlipPerson(int y)
    {
        transform.eulerAngles = new Vector3(0, y, 0);
    }
}