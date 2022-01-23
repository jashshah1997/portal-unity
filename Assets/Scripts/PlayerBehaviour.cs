using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Rigidbody rigidBody;

    [Header("Movement Properties")] 
    public float maxSpeed = 10.0f;
    public float gravity = -30.0f;
    public float jumpHeight = 3.0f;
    public Vector3 velocity;

    [Header("Ground Detection Properties")]
    public Transform groundCheck;
    public float groundRadius = 0.5f;
    public LayerMask groundMask;
    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        if (isGrounded)
        {
            rigidBody.velocity = new Vector3(move.x * 10, rigidBody.velocity.y, move.z * 10);
        }

        if (Input.GetButton("Jump") && isGrounded)
        {
            rigidBody.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}
