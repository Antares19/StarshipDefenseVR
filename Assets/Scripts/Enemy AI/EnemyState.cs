using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    public virtual void Tick(EnemyData enemy, EnemyAI enemyAI)
    {
        
    }

    //ВМЕСТО CHANGE TO STATE ДОЛЖЕН БЫТЬ МЕТОД, СРАБАТЫВАЮЩИЙ НА ВХОДЕ,
    //И МЕТОД, СРАБАТЫВАЮЩИЙ НА ВЫХОДЕ.
    //Т.е. при выполнении условия входа в новое состояние мы
    //В СТЕЙТЕ ИЗ КОТОРОГО ВЫХОДИМ, вызываем OnStateExit,
    //В ЦЕЛЕВОМ стейте вызываем OnStateEnter
    public virtual void ChangeToState(EnemyData enemy, EnemyState targetState, EnemyAI enemyAI)
    {
        enemy.CurrentState = targetState;
    }

    public virtual void OnStateEnter(EnemyData enemy, EnemyAI enemyAI)
    {
        enemy.CurrentState = this;
    }

    public virtual void OnStateExit(EnemyData enemy, EnemyAI enemyAI)
    { }

}
