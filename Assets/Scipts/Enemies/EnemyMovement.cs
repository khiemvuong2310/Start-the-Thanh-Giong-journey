using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        ChooseDirection();
    }

    private void Update()
    {
        if (rb.velocity.x > 0)
            spriteRenderer.flipX = false; // Quay mặt sang phải
        else if (rb.velocity.x < 0)
            spriteRenderer.flipX = true;  // Quay mặt sang trái
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDirection * moveSpeed;
    }

    private void ChooseDirection()
    {
        // Chọn hướng trái hoặc phải
        moveDirection = (Random.value > 0.5f) ? Vector2.right : Vector2.left;
    }
}
