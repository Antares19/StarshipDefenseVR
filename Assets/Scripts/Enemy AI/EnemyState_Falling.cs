using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Falling : EnemyState
{
    public override void Tick(EnemyData enemy, EnemyAI enemyAI)
    {
        // ЕСЛИ УПАЛ НА ЗЕМЛЮ, ТО
        //enemy.RigidBody.isKinematic = true;
        //enemyAI.State_IdleOnStateEnter(enemy, enemyAI);
    }

    public override void OnStateEnter(EnemyData enemy, EnemyAI enemyAI)
    {
        enemy.RigidBody.isKinematic = false;
        base.OnStateEnter(enemy, enemyAI);
    }

    //private bool IsTouchingGround(EnemyData enemy)
    //{
    //    velocity.y >= 0 ?
    // raycast вниз до слоя ground?
    // oncollisionenter на enemy и ивентом?
    //}
}
