using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAttackState : BaseState
{
    public override void onEnter(Enemy enemy)
    {
        enemy.enemyUI.SetActive(true);
        currentEnemy = enemy;
        Debug.Log("Attack");
        currentEnemy.currentSpeed = 0;
        currentEnemy.animator.SetBool("Attack", true);
    }

    public override void LogicUpdate()
    {
        if (!currentEnemy.isAttackPlayer() && currentEnemy.isChasePlayer())
        {
            currentEnemy.SwitchState(NPCState.Chase);
        }

        if (!currentEnemy.isChasePlayer())
        {
            currentEnemy.SwitchState(NPCState.Patrol);
        }
    }

    public override void PhysicsUpdate()
    {
        
    }


    public override void onExit()
    {
        currentEnemy.animator.SetBool("Attack", false);
    }

}
