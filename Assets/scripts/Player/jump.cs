using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : Sounds
{
    public Rigidbody2D RB;
    public float maxjump; //нужно будет подключать к данным персонажа
    public float dashForce;
    [SerializeField] private float jumpCooldown = 0.2f;
    private float time = 0.2f;
    private bool isGrounded;
    private bool jumpBust;
    [SerializeField] private float rayDistanceCheckGround;
    [SerializeField] private LayerMask groundLayerMask;
    
    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.Raycast(RB.position, Vector3.down, rayDistanceCheckGround, groundLayerMask);
        Debug.DrawRay(RB.position, Vector3.down * rayDistanceCheckGround, Color.green);

        if ((/*Input.GetKey(KeyCode.W) ||*/ Input.GetKey(KeyCode.Space)) && isGrounded) Jumping(maxjump);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("dash");
            RB.AddForce(gameObject.transform.right * dashForce, ForceMode2D.Impulse);
            PlaySound(sounds[1], p1: 0.5f, p2: 0.8f);
        }
        if (time<=jumpCooldown) time += Time.deltaTime;
    }

    public void Jumping(float distance)
    {
        if (time > jumpCooldown)
        {
            RB.AddForce(new Vector2(0, distance), ForceMode2D.Impulse);
            time = 0;
            PlaySound(sounds[0], p1: 2f, p2: 4f);
        }
    }
    
}
