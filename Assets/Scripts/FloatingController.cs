using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingController : MonoBehaviour
{
    public float speed;
    Vector3 speedVec3;
    Animator animator;
    GameObject topLine;

    void Start()
    {
        animator = GetComponent<Animator>();
        speedVec3.y = speed;
        topLine = GameObject.Find("TopLine");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("hit");
        }
    }

    void Update()
    {
        Move();
    }

    // Update is called once per frame
    void Move()
    {
        transform.position += speedVec3 * Time.deltaTime;
        if (transform.position.y > topLine.transform.position.y)
            Destroy(gameObject);
    }
}
