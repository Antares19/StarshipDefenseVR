using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_GoingToWayPoint : EnemyState
{
    public override void Tick(EnemyData enemy, EnemyAI enemyAI)
    {
        //ДВИГАЕМ МОБА
        enemyAI.Mover.Move(enemy.Transform, enemy.TargetWaypoint.transform, enemyAI.EnemySpeedFree * enemyAI.DeltaTime);

        //ЕСЛИ ДОШЛИ ДО НОДА
        if (!(Vector3.Distance(enemy.Transform.position, enemy.TargetWaypoint.transform.position) <= enemyAI.DistanceToTargetToCountAsReached))
            return;

        //ВЫБИРАЕМ СЛЕДУЮЩУЮ ТОЧКУ НАЗНАЧЕНИЯ.
        if (Random.Range(0,2) == 0)
        {
            //building
            enemy.TargetBuilding = enemy.TargetWaypoint.GetComponent<Node>().getRandomBuildingPath();
            enemyAI.State_GoingToBuilding.OnStateEnter(enemy, enemyAI);
        }
        else
        {
            enemyAI.FindNewTargetNodeForEnemy(enemy);
            enemyAI.TurnEnemyToFaceTarget(enemy);
        }
        
    }

    public override void OnStateEnter(EnemyData enemy, EnemyAI enemyAI)
    {
        Debug.Log("пошел к вейпоинту");
        enemyAI.TurnEnemyToFaceTarget(enemy);

        enemy.Animator.SetBool("Walk", true);
        enemy.Animator.SetBool("Grab", false);

        base.OnStateEnter(enemy, enemyAI);
    }
}
