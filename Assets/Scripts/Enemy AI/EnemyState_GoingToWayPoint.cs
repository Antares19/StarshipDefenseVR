using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_GoingToWayPoint : EnemyState
{
    public override void Tick(EnemyData enemy, EnemyAI enemyAI)
    {
        Debug.Log("tick");
        //ДВИГАЕМ МОБА
        enemyAI.Mover.Move(enemy.Transform, enemy.CurrentTarget.transform, enemyAI.EnemySpeedFree * enemyAI.DeltaTime);

        //ЕСЛИ ДОШЛИ ДО НОДА
        if (!(Vector3.Distance(enemy.Transform.position, enemy.CurrentTarget.transform.position) <= enemyAI.DistanceToTargetToCountAsReached))
            return;

        //ВЫБИРАЕМ СЛЕДУЮЩУЮ ТОЧКУ НАЗНАЧЕНИЯ.
        enemyAI.FindNewTargetNodeForEnemy(enemy);
        enemyAI.TurnEnemyToFaceTarget(enemy);

        //ЕСЛИ ЭТО ДОМ, то
        //enemyAI.State_GoingToBuilding.OnStateEnter(enemy, enemyAI);

    }

    public override void OnStateEnter(EnemyData enemy, EnemyAI enemyAI)
    {
        enemyAI.TurnEnemyToFaceTarget(enemy);

        enemy.Animator.SetBool("Walk", true);
        enemy.Animator.SetBool("Grab", false);

        base.OnStateEnter(enemy, enemyAI);
    }
}
