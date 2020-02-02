using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Hanging : EnemyState
{
    public override void Tick(EnemyData enemy, EnemyAI enemyAI)
    {
        enemy.HangTimer -= enemyAI.DeltaTime;

        if (enemy.HangTimer < 0)
        {
            enemy.HangTimer = 0;
            enemyAI.State_Falling.OnStateEnter(enemy, enemyAI);
        }
    }

    public override void OnStateEnter(EnemyData enemy, EnemyAI enemyAI)
    {
        enemy.HangTimer = enemyAI.EnemyHangTimeWhenBuildingIsGrabbed;
        base.OnStateEnter(enemy, enemyAI);
    }
}
