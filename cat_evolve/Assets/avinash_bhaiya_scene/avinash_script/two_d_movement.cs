using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class two_d_movement : MonoBehaviour
{
    [Header("movement componenet")]
    public float moveSpeed ;
    public float movespeed_normal_time;
    public float movespeed_while_slowmotion;

    [Header("jump component")]
    public float jumpForce ;
    public float jumpforce_normal_time;
    public float jumpforce_while_slowmotion;
    public LayerMask groundLayer;
    [SerializeField] private bool isGrounded;


    public float groundCheckRadius ;

    [Header("righidbody component")]
    private Rigidbody2D rb;
    public float gravitional_scale_while_slowmotion;
    public float gravitional_scale_normal_time;


    [Header("slowmotion component")]
    public bool isSlowMotion = false;
    public float slowMotionFactor;


    public health_manager health;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health= GetComponent<health_manager>();
    }

    void Update()
    {
        // Toggle Slow Motion
        if (Input.GetKeyDown(KeyCode.E) && !isSlowMotion)
        {
            isSlowMotion = true;
            Time.timeScale = slowMotionFactor;
            Time.fixedDeltaTime = 0.02f * Time.timeScale; // Adjust physics simulation speed
            making_the_time_slower();
        }
        else if (Input.GetKeyDown(KeyCode.F) && isSlowMotion)
        {
            isSlowMotion = false;
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f; // Reset physics speed
            making_the_time_at_normal_pase();
        }

        // Smooth Horizontal Movement
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, moveInput * moveSpeed, 0.1f), rb.velocity.y);

        // Ground Check
        isGrounded = Physics2D.OverlapCircle(transform.position, groundCheckRadius, groundLayer);

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

    }

    void making_the_time_slower()
    {
        //Time.timeScale = 0.2f;

        //float unscaledDeltaTime = Time.unscaledDeltaTime;
        moveSpeed = movespeed_while_slowmotion;
        jumpForce = jumpforce_while_slowmotion;
        rb.gravityScale = gravitional_scale_while_slowmotion;
        health.time_slow_health_decreaser(true);

    }
    void making_the_time_at_normal_pase()
    {
        //Time.timeScale = 1f;

        moveSpeed = movespeed_normal_time;
        jumpForce = jumpforce_normal_time;
        rb.gravityScale = gravitional_scale_normal_time;
        health.time_slow_health_decreaser(false);
    }



}
