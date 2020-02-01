using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Idle : EnemyState
{
    public override void Tick(EnemyData enemy, EnemyAI enemyAI)
    {
        enemyAI.FindNewTargetNodeForEnemy(enemy);
        enemyAI.State_GoingToWaypoint.OnStateEnter(enemy, enemyAI);
    }

    public override void OnStateEnter(EnemyData enemy, EnemyAI enemyAI)
    {
        enemy.Animator.SetBool("Walk", false);
        enemy.Animator.SetBool("Grab", false);
        base.OnStateEnter(enemy, enemyAI);
    }
}
