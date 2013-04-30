using UnityEngine;
using System.Collections.Generic;

/**
 * Базовый класс искуственного интеллекта боевеого дрона.
 */
class AICombatDrone : MonoBehaviour
{
    // Количество соправаждаемых целей.
    private int maxTargets = 2;

    // Список сопровождаемых целей типа Transform.
    private List<Transform> targets = new List<Transform>();

    // Радиус сенсоров для определения целей.
    private float sensorRadius = 5.0f;

    // Угол обзора.
    private float viewAngle = 70.0f;
}