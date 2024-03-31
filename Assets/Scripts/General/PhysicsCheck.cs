using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    private CapsuleCollider2D capsuleCollider;
    [Header("检测参数")]
    public bool isManual;
    public float checkRadius;
    public LayerMask ground;
    public Vector2 bottomOffset;
    public Vector2 leftOffset;
    public Vector2 rightOffset;
    public Vector2 aheadOffset;
    public Vector2 behindOffset;

    [Header("状态")]
    public bool isGround;
    public bool touchLeftWall;
    public bool touchRightWall;
    public bool isGroundAhead;
    public bool isGroundBehind;
    // Start is called before the first frame update
    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        if (!isManual)
        {
            rightOffset = new Vector2((capsuleCollider.bounds.size.x + capsuleCollider.offset.x) / 2, capsuleCollider.bounds.size.y / 2);
            leftOffset = new Vector2(-rightOffset.x, rightOffset.y);
        }
    }
    public void Update()
    {
        Check();
    }

    public void Check()
    {
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRadius, ground);
        // 墙体检测
        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRadius, ground);
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRadius, ground);

        // 前方有路检测
        isGroundAhead = Physics2D.OverlapCircle((Vector2)transform.position + aheadOffset, checkRadius, ground);
        isGroundBehind = Physics2D.OverlapCircle((Vector2)transform.position + behindOffset, checkRadius, ground);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRadius);

        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRadius);

        Gizmos.DrawWireSphere((Vector2)transform.position + aheadOffset, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + behindOffset, checkRadius);
    }
}
