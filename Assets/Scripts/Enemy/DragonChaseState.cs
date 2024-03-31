using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonChaseState : BaseState
{
    public override void onEnter(Enemy enemy)
    {
        enemy.enemyUI.SetActive(true);
        currentEnemy = enemy;
        Debug.Log("Chase");
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        currentEnemy.animator.SetBool("Run", true);
    }

    public override void LogicUpdate()
    {
        if (currentEnemy.isAttackPlayer())
        {
            currentEnemy.SwitchState(NPCState.Attack);
        }

        if (!currentEnemy.isChasePlayer())
        {
            currentEnemy.SwitchState(NPCState.Patrol);
        }

        if ((currentEnemy.physicsCheck.transform.localScale.x < 0 && !currentEnemy.physicsCheck.isGroundAhead) || (currentEnemy.physicsCheck.transform.localScale.x > 0 && !currentEnemy.physicsCheck.isGroundBehind))
        {
            currentEnemy.transform.localScale = new Vector3(currentEnemy.faceDir.x * 1.5f, currentEnemy.transform.localScale.y, currentEnemy.transform.localScale.z);
            currentEnemy.faceDir = -currentEnemy.faceDir;
        }
    }

    public override void PhysicsUpdate()
    {
        
    }

    public override void onExit()
    {
        currentEnemy.animator.SetBool("Run", false);
    }
}
