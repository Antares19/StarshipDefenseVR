using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_AttachedToBuildingAndCarrying : EnemyState
{
    public override void OnStateEnter(EnemyData enemy, EnemyAI enemyAI)
    {
        enemy.Animator.SetBool("Walk", true);
        enemy.Animator.SetBool("Grab", true);
        base.OnStateEnter(enemy, enemyAI);
    }
}
