using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_GoingToBuilding : EnemyState
{

    public override void Tick(EnemyData enemy, EnemyAI enemyAI)
    {
        //ДВИГАЕМ ВРАГА ПО НАПРАВЛЕНИЮ К ЕГО CurrentTarget
        enemyAI.Mover.Move(enemy.Transform, enemy.TargetBuilding.transform, enemyAI.EnemySpeedFree * enemyAI.DeltaTime);

        //Если дом уже горит, ставим вейпоинт
        if (enemy.TargetBuilding.isOnFire() || enemy.TargetBuilding.isPicked())
        {
            enemyAI.FindNewTargetNodeForEnemy(enemy);
            enemyAI.TurnEnemyToFaceTarget(enemy);
            

            enemy.TargetBuilding = null;

            enemyAI.State_GoingToWaypoint.OnStateEnter(enemy, enemyAI);
        }

        //Если дошел до дома, то меняем стейт на enemyAI.State_WaitingAtBuilding
        if (Vector3.Distance(enemy.Transform.position, enemy.TargetBuilding.transform.position) <= enemyAI.DistanceToTargetToCountAsReached)
        {
            enemyAI.State_WaitingAtBuilding.OnStateEnter(enemy, enemyAI);
        }
    }

    public override void OnStateEnter(EnemyData enemy, EnemyAI enemyAI)
    {
        Debug.Log(enemy + " пошел к дому " + enemy.TargetBuilding);
        enemyAI.TurnEnemyToFaceTarget(enemy);

        base.OnStateEnter(enemy, enemyAI);
    }




}
