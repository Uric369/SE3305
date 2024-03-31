using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage; // ÉËº¦Öµ
    public float attackRange; // ¹¥»÷·¶Î§
    public float attackRate; // ¹¥»÷ÆµÂÊ
    // Start is called before the first frame update

    private void Start()
    {
        Debug.Log("start");
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(" OnTriggerStay2D");
        Debug.Log(collision.name);
        collision.GetComponent<Character>()?.TakeDamage(this);
    }
}
