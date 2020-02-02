using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_GoingToBuilding : EnemyState
{

    public override void Tick(EnemyData enemy, EnemyAI enemyAI)
    {
        //ДВИГАЕМ ВРАГА ПО НАПРАВЛЕНИЮ К ЕГО CurrentTarget

        //Если дошел до дома
        //, то ChangeToState(enemy, enemyAI.State_WaitingAtBuilding, enemyAI);

    }

    public override void OnStateEnter(EnemyData enemy, EnemyAI enemyAI)
    {
        
    }

    public override void ChangeToState(EnemyData enemy, EnemyState targetState, EnemyAI enemyAI)
    {
 
    }




}
