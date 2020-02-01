using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_GoingToWayPoint : EnemyState
{
    public override void Tick(EnemyData enemy, EnemyAI enemyAI)
    {
        //ДВИГАЕМ МОБА
        //ЕСЛИ ДОШЛИ ДО НОДА,
        //ВЫБИРАЕМ СЛЕДУЮЩУЮ ТОЧКУ НАЗНАЧЕНИЯ.
        //ЕСЛИ ЭТО ДОМ, то
        //enemyAI.State_GoingToBuilding.OnStateEnter(enemy, enemyAI);
    }

    public override void OnStateEnter(EnemyData enemy, EnemyAI enemyAI)
    {
        enemy.Animator.SetBool("Walk", true);
        enemy.Animator.SetBool("Grab", false);
    }
}
