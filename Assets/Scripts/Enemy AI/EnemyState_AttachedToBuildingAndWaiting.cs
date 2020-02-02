using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_AttachedToBuildingAndWaiting : EnemyState
{
    public override void Tick(EnemyData enemy, EnemyAI enemyAI)
    {
        var building = enemy.BuildingAttachedTo;
        if (enemyAI.Buildings[building].DependentEnemies.Count >= building.NumberOfEnemiesNeededToCarry)
        {
            //определяем точку назначения и несём
            //enemyAI.Buildings[building].StartCarryingToTarget
            enemyAI.State_CarryingBuilding.OnStateEnter(enemy, enemyAI);            
        }
    }

    public override void OnStateEnter(EnemyData enemy, EnemyAI enemyAI)
    {
        var building = enemy.TargetWaypoint.GetComponent<Building>();
        AttachToBuilding(enemy, building);

        enemy.Animator.SetBool("Grab", true);
        enemy.Animator.SetBool("Walk", false);
        base.OnStateEnter(enemy, enemyAI);
    }

    private static void AttachToBuilding(EnemyData enemy, Building building)
    {
        enemy.BuildingAttachedTo = building;
        enemy.Transform.parent = building.transform;
        enemy.TargetWaypoint = null;
    }


}
