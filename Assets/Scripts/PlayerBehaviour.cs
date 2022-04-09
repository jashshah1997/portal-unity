using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Rigidbody rigidBody;
    public Transform startPosition;

    [Header("Movement Properties")] 
    public float MovementMultiplier = 10.0f;
    public float JumpMultiplier = 10f;

    [Header("Ground Detection Properties")]
    public Transform groundCheck;
    public float groundRadius = 0.5f;
    public LayerMask groundMask;
    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        startPosition = GameObject.Find("StartPosition").transform;
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
            rigidBody.velocity = new Vector3(move.x * MovementMultiplier, rigidBody.velocity.y, move.z * MovementMultiplier);
        }

        if (Input.GetButton("Jump") && isGrounded)
        {
            rigidBody.AddForce(Vector3.up * JumpMultiplier, ForceMode.Impulse);
        }

        Quaternion q = Quaternion.FromToRotation(transform.up, Vector3.up) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 3.5f);

        if (gameObject.transform.position.y < -40f) { gameObject.transform.position = startPosition.transform.position; }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hiho" + other.gameObject.tag);
        if (other.gameObject.CompareTag("FinishLine"))
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().SetLevelFinished();
        }
    }
}
