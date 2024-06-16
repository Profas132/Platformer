using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{
    public Rigidbody2D RB;
    public float maxjump; //нужно будет подключать к данным персонажа
    public float dashForce;

    private bool isGrounded;
    private bool jumpBust;
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask groundLayerMask;
    

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.Raycast(RB.position, Vector3.down, rayDistance, groundLayerMask);
        Debug.DrawRay(RB.position, Vector3.down * rayDistance, Color.green);

        if (Input.GetKeyDown(KeyCode.W) && isGrounded) Jumping(maxjump);

        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("dash");
            RB.AddForce(gameObject.transform.right * dashForce, ForceMode2D.Impulse);
        }
    }

    public void Jumping(float distance)
    {
        RB.AddForce(new Vector2(0, distance), ForceMode2D.Impulse);
    }
    
}
