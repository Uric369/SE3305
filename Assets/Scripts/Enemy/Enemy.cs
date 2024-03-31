using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    [HideInInspector] public Animator animator;
    [HideInInspector] public PhysicsCheck physicsCheck;
    public GameObject enemyUI;


    [Header("基本参数")]
    // public float scale;
    public float normalSpeed;
    public float chaseSpeed;
    public float currentSpeed;
    public Vector3 faceDir;
    public float directionChangeTimer;
    public bool canChangeDirection = true;
    public Transform attacker;
    public float hurtForce;


    [Header("范围检测通用参数")]
    public Vector2 centerOffset;
    public Vector2 checkSize;
    public LayerMask attackLayer;

    [Header("追击距离范围")]
    public float chaseDistance;

    [Header("攻击距离范围")]
    public float attackDistance;

    [Header("计时器")]
    public float waitTime;
    public float waitTimeCounter;
    public bool isWaiting;

    [Header("状态")]
    public bool isHurt;
    public bool isDead;

    protected BaseState patrolState;
    protected BaseState chaseState;
    protected BaseState attackState;
    private BaseState currentState;
 

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();

        currentSpeed = normalSpeed;
    }

    private void OnEnable()
    {
        currentState = patrolState;
        currentState.onEnter(this);
    }

    private void Update()
    {
        // if (canChangeDirection)
        // {
        currentState.LogicUpdate();
        TimeCounter();
        // }
    }

    private void FixedUpdate()
    {
        if (!isHurt && !isDead)
        Move();
        currentState.PhysicsUpdate();
    }

    private void OnDisable()
    {
        currentState = patrolState;
        currentState.onEnter(this);
    }

    public virtual void Move()
    {
        if (isWaiting) {
            rb.velocity = Vector3.zero;
        }
        else
        {
            rb.velocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime, rb.velocity.y);
        }
    }

    public void TimeCounter()
    {
        if (isWaiting)
        {
            waitTimeCounter -= Time.deltaTime;
            if (waitTimeCounter <= 0)
            {
                isWaiting = false;
                waitTimeCounter = waitTime;
                if (transform.localScale.x >= 0)
                    faceDir = new Vector3(1, 0, 0);
                else if (transform.localScale.x < 0)
                    faceDir = new Vector3(-1, 0, 0);

                transform.localScale = new Vector3(-faceDir.x * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }

    public bool isChasePlayer()
    {
        return Physics2D.BoxCast(transform.position + (Vector3)centerOffset, checkSize, 0, faceDir, chaseDistance, attackLayer);
    }

    public bool isAttackPlayer()
    {
        return Physics2D.BoxCast(transform.position + (Vector3)centerOffset, checkSize, 0, faceDir, attackDistance, attackLayer);
    }

    public void SwitchState(NPCState state)
    {
        var newState = state switch
        {
            NPCState.Patrol => patrolState,
            NPCState.Chase => chaseState,
            NPCState.Attack => attackState,
            _ => null
        };

        currentState.onExit();
        currentState = newState;
        currentState.onEnter(this);
    }


    #region 事件执行函数
    public void onTakeDamage(Transform attackerTrans)
    {
        attacker = attackerTrans;

        // 转身
        if (attackerTrans.position.x - transform.position.x > 0)
        {
            transform.localScale = new Vector3(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            faceDir = new Vector3(1, 0, 0);
        }
        if (attackerTrans.position.x - transform.position.x < 0) {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            faceDir = new Vector3(-1, 0, 0);
        }

        isHurt = true;

        Vector2 dir = new Vector2(transform.localScale.x - attackerTrans.position.x, 0).normalized;

        StartCoroutine(onHurt(dir));
    }


    private IEnumerator onHurt(Vector2 dir)
    {
        rb.velocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime, rb.velocity.y);
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        isHurt = false;
    }

    public void OnDie()
    {
        gameObject.layer = 2;
        animator.SetBool("Dead", true);
        isDead = true;
    }

    public void DestroyAfterDeath()
    {
        Destroy(this.gameObject);
    }

    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + (Vector3)centerOffset, 0.2f);
    }
}
