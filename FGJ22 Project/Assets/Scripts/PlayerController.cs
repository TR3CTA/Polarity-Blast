using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    private GameManager gameManager;

    // Moving variables
    public float speed = 15;
    public float gravity = -19.81f;
    public float jumpHeight = 3f;
    Vector3 velocity;

    // Ground measuring variables
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
       
    // Health variables
    public int currentHealth;
    public int maxHealth = 100;
    public 

    // Boundary variables
    float boundaryRange = 50f;

    private void Start()
    {
        currentHealth = maxHealth;
         
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("Healthbar").GetComponent<HealthBar>().SetHealth(currentHealth);

        // Check if player is grounded and set y velocity to -2
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        

        // Make player move and turn directions
        float forwardInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        Vector3 moveDir = transform.right * horizontalInput + transform.forward * forwardInput;
        controller.Move(moveDir * speed * Time.deltaTime);


        // Jump on space bar press
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


        // Keep the player in bounds
        KeepInBounds();
    }

    // Take damage from enemy melee hits and destroy player when health reaches 0
    public void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;

        if (currentHealth <= 0)
        {
            Time.timeScale = 0;
        }
    }

    private void KeepInBounds()
    {
        // Western boundary
        if (transform.position.x < -boundaryRange)
        {
            transform.position = new Vector3(-boundaryRange, transform.position.y, transform.position.z);
        }
        // Eastern boundary
        if (transform.position.x > boundaryRange)
        {
            transform.position = new Vector3(boundaryRange, transform.position.y, transform.position.z);
        }
        // Southern boundary
        if (transform.position.z < -boundaryRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -boundaryRange);
        }
        // Northern boundary
        if (transform.position.z > boundaryRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, boundaryRange);
        }
    }
}
