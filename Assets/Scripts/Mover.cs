using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс для перемещения Transform-а на заданное расстояние.
/// Используется для передвижения врагов.
/// Можно усложнить, добавить нормали для движения по сложным поверхностям и т.п.
/// </summary>
public class Mover
{
    public void Move(Transform movingObject, Transform targetToMoveTowards, float moveDistance)
    {
        movingObject.position = Vector3.MoveTowards(movingObject.position, targetToMoveTowards.position, moveDistance);
    }
}
