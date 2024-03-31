using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class DragonPatrolState : BaseState
{
    public override void onEnter(Enemy enemy)
    {
        enemy?.enemyUI?.SetActive(false);
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
    }
    public override void LogicUpdate()
    {

        if (currentEnemy.isAttackPlayer())
        {
            currentEnemy.SwitchState(NPCState.Attack);
        }

        if (currentEnemy.isChasePlayer() && !currentEnemy.isAttackPlayer())
        {
            currentEnemy.SwitchState(NPCState.Chase);
        }

        if ((currentEnemy.physicsCheck.transform.localScale.x < 0 && !currentEnemy.physicsCheck.isGroundAhead) || (currentEnemy.physicsCheck.transform.localScale.x > 0 && !currentEnemy.physicsCheck.isGroundBehind))
        {
            currentEnemy.isWaiting = true;
            currentEnemy.animator.SetBool("Walk", false);
            // StartCoroutine(DisableDirectionChange());
        }
    }

    public override void PhysicsUpdate()
    {
        
    }

    public override void onExit()
    {
        
    }
}
