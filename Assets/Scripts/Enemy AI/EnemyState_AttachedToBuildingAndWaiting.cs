using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_AttachedToBuildingAndWaiting : EnemyState
{
    public override void Tick(EnemyData enemy, EnemyAI enemyAI)
    {
        var building = enemy.BuildingAttachedTo;

        if (building.isOnFire())
        {
            GoToNextWaypoint(enemy, enemyAI, building);
        }

        else if (enemyAI.Buildings[building].DependentEnemies.Count >= building.NumberOfEnemiesNeededToCarry)
        {
            //определяем точку назначения и несём
            //enemyAI.Buildings[building].StartCarryingToTarget
            //enemyAI.State_CarryingBuilding.OnStateEnter(enemy, enemyAI);            

            //FIREEE!
            if (!enemy.TargetBuilding.isOnFire())
            {
                Debug.Log("FIREEEEEEEEEEEEEEee");
                enemy.TargetBuilding.SetOnFire();
            }

            Debug.Log(enemy.TargetWaypoint);
            GoToNextWaypoint(enemy, enemyAI, building);
        }


    }

    private static void GoToNextWaypoint(EnemyData enemy, EnemyAI enemyAI, Building building)
    {
        enemyAI.Buildings[building].DependentEnemies.Remove(enemy.EnemyMono);
        enemy.BuildingAttachedTo = null;
        enemy.TargetBuilding = null;
        enemyAI.FindNewTargetNodeForEnemy(enemy);
        enemyAI.TurnEnemyToFaceTarget(enemy);
        enemyAI.State_GoingToWaypoint.OnStateEnter(enemy, enemyAI);
    }

    public override void OnStateEnter(EnemyData enemy, EnemyAI enemyAI)
    {
        Debug.Log("подошел к дому и ждет");
        

        var building = enemy.TargetBuilding;
        enemyAI.Buildings[building].DependentEnemies.Add(enemy.EnemyMono);
        Debug.Log(enemyAI.Buildings[building].DependentEnemies.Count);
        AttachToBuilding(enemy, building);

        enemy.Animator.SetBool("Grab", true);
        enemy.Animator.SetBool("Walk", false);
        base.OnStateEnter(enemy, enemyAI);
    }

    private static void AttachToBuilding(EnemyData enemy, Building building)
    {
        enemy.BuildingAttachedTo = building;        
        enemy.Transform.parent = building.transform;
    }
    


}
