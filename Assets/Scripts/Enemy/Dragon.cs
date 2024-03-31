using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Enemy
{
    public override void Move()
    {
        base.Move();
        animator.SetBool("Walk", true);
    }

    protected override void Awake()
    {
        base.Awake();
        patrolState = new DragonPatrolState();
        chaseState = new DragonChaseState();
        attackState = new DragonAttackState();
    }
}
