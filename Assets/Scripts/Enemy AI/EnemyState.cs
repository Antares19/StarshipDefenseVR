using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    public virtual void Tick(EnemyData enemy, EnemyAI enemyAI)
    { 
    }
}
