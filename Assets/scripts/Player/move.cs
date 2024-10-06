using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class move : Sounds
{
    public Rigidbody2D RB;
    private Animator Anim;
    public float maxSpeed; //нужно будет подключать к данным персонажа

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        Anim = transform.GetComponent<Animator>();
    }

    void FixedUpdate()
    {   
        float horizontal = Input.GetAxisRaw("Horizontal");
        Vector2 movement = new Vector2 (horizontal, 0);
        RB.AddForce(movement * maxSpeed * 200);

        if (horizontal != 0) 
        {
            Anim.SetBool("isRunning", true);        
        } 
        else Anim.SetBool("isRunning", false); ;

        if (horizontal > 0)
        {
            FlipPerson(0);
        }
        else if (horizontal < 0)    
        {
            FlipPerson(-180);
        }
    }

    private void StepSounds()
    {
        if (gameObject.GetComponent<jump>().isGrounded)
        {
            PlaySound(sounds[1], p1: 0.5f, p2: 0.8f);
        }
    }

    private void FlipPerson(int y)
    {
        transform.eulerAngles = new Vector3(0, y, 0);
    }
}