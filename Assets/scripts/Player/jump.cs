using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{
    public Rigidbody2D RB;
    public float maxjump; //нужно будет подключать к данным персонажа
    public float dashForce;
    private float jumpCooldown = 0.2f;
    private float time = 0.2f;
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
        time += Time.deltaTime;

        isGrounded = Physics2D.Raycast(RB.position, Vector3.down, rayDistance, groundLayerMask);
        Debug.DrawRay(RB.position, Vector3.down * rayDistance, Color.green);

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)) && isGrounded) Jumping(maxjump);

        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("dash");
            RB.AddForce(gameObject.transform.right * dashForce, ForceMode2D.Impulse);
        }
    }

    public void Jumping(float distance)
    {
        if (time > jumpCooldown)
        {
            RB.AddForce(new Vector2(0, distance), ForceMode2D.Impulse);
            time = 0;
        }
    }
    
}
