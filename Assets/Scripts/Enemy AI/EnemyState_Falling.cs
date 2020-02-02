using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Falling : EnemyState
{
    public override void Tick(EnemyData enemy, EnemyAI enemyAI)
    {
        // ЕСЛИ УПАЛ НА ЗЕМЛЮ, ТО
        //ChangeToState(enemy, enemyAI.State_GoingToWaypoint, enemyAI);
    }
}
