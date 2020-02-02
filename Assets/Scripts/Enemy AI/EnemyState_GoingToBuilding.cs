using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_GoingToBuilding : EnemyState
{

    public override void Tick(EnemyData enemy, EnemyAI enemyAI)
    {
        //ДВИГАЕМ ВРАГА ПО НАПРАВЛЕНИЮ К ЕГО CurrentTarget
        enemyAI.Mover.Move(enemy.Transform, enemy.TargetBuilding.transform, enemyAI.EnemySpeedFree * enemyAI.DeltaTime);

        //Если дошел до дома, то меняем стейт на enemyAI.State_WaitingAtBuilding
        if (Vector3.Distance(enemy.Transform.position, enemy.TargetBuilding.transform.position) <= enemyAI.DistanceToTargetToCountAsReached)
        {
            enemyAI.State_WaitingAtBuilding.OnStateEnter(enemy, enemyAI);
        }
    }

    public override void OnStateEnter(EnemyData enemy, EnemyAI enemyAI)
    {
        Debug.Log(enemy + " идет к дому " + enemy.TargetBuilding);
        enemyAI.TurnEnemyToFaceTarget(enemy);
    }




}
