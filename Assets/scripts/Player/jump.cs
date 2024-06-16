using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{
    public Rigidbody2D RB;
    public float maxjump; //нужно будет подключать к данным персонажа
    
    [SerializeField] private bool isGrounded;
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask groundLayerMask;

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.Raycast(RB.position, Vector3.down, rayDistance, groundLayerMask);
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        RB.AddForce(new Vector2(0, maxjump),ForceMode2D.Impulse);
        Debug.DrawRay(RB.position, Vector3.down* rayDistance,Color.green);
    }
}
