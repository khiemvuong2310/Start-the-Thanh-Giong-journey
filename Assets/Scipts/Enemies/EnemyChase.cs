using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public float moveSpeed = 3f; // Tốc độ di chuyển
    public float detectionRange = 5f; // Phạm vi phát hiện
    public Transform player; // Tham chiếu đến Player
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            moveDirection = (player.position - transform.position).normalized; // Hướng đến Player
        }
        else
        {
            moveDirection = Vector2.zero; // Dừng di chuyển khi mất dấu
        }

        // Lật mặt quái theo hướng di chuyển
        if (moveDirection.x > 0)
            spriteRenderer.flipX = false; // Quay phải
        else if (moveDirection.x < 0)
            spriteRenderer.flipX = true; // Quay trái
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDirection * moveSpeed;
    }
}
