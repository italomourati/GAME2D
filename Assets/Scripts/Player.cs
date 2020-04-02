using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Movimentacao do Player
    public float moveSpeed = 5f;

    // Pulo do Player
    public float jumpForce = 5f;
    public bool jump = true;

    private new Rigidbody2D rigidbody2D;

    // Moeda
    public static int COUNT_COIN;

    // Animacao
    public Animator animator;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        rigidbody2D.freezeRotation = true;

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(horizontal, 0f, 0f);

        transform.position += movement * Time.fixedDeltaTime * moveSpeed;

        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal") * moveSpeed));

        Flip(horizontal);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && jump)
        {
            rigidbody2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jump = true;
        }
    }

    void Flip(float horizontalDirection)
    {
        if (horizontalDirection > 0)
        {
            spriteRenderer.flipX = false;
        }
        if (horizontalDirection < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            jump = true;
        }
        else if (collision.collider.tag == "Coin")
        {
            COUNT_COIN++;
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            jump = false;
        }
    }
}
