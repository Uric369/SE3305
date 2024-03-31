using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    private PlayerController playerController;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        playerController = GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimation();
    }

    private void SetAnimation()
    {
        animator.SetFloat("SpeedX", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("SpeedY", rb.velocity.y);
        animator.SetBool("isGround", physicsCheck.isGround);
        animator.SetBool("isDead", playerController.isDead);
    }

    public void getHurt()
    {
        Debug.Log("anim hurt");
        // spriteRenderer.enabled = true;
        // spriteRenderer.color = new Color(1, 1, 1, 0);
        animator.SetTrigger("Hurt");
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public void Disable()
    {
        animator.enabled = false;
    }

    public void Enable()
    {
        animator.enabled = true;
    }

    public void Restart()
    {
        animator.Play("Idle", -1, 0);
    }
}
