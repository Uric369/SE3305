using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringController : MonoBehaviour
{
    Animator animator;
    public float e;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("bounce");

            Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                Vector2 newVelocity = new Vector2(playerRigidbody.velocity.x, 5.0f);
                playerRigidbody.velocity = newVelocity; // 调整竖直方向速度
            }
        }
    }
}

