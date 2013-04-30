using UnityEngine;
using System.Collections.Generic;

/**
 * Базовый класс искуственного интеллекта боевеого дрона.
 * 
 * Должен уметь защищать солдат.
 * Если дрон атакует врага C, а враг B атакует солдата А и он в радиусе области сенсора то атакавать врага B.
 */
class AICombatDrone : MonoBehaviour
{
    // Базовая скорость передвижения.
    private float basicSpeedMove = 1.0f;

    // Скорость передвижения во время турбо.
    private float turboSpeedMove = 2.5f;

    // Базовая скорость разворота.
    private float baseSpeedRotate = 7.0f;

    // Количество соправаждаемых целей.
    private int maxTargets = 2;

    // Список сопровождаемых целей типа Transform.
    private List<Transform> targets = new List<Transform>();

    // Радиус сенсоров для определения целей.
    private float sensorRadius = 5.0f;

    // Угол обзора для атаки.
    private float viewAngle = 70.0f;

    // Базовая точка позиции.
    private Vector3 basePointPosition;

    // Базовая точка поворота.
    private Quaternion basePointRotate;

    // Текущая точка позиции.
    private Vector3 currentPointPosition;

    // Текущая точка поворта.
    private Quaternion currentPointRotate;

    // Список оружий типа Transform.
    // Можно будет менять оружия.
    public List<Transform> weapons = new List<Transform>();

    private void Start()
    {
        basePointPosition = currentPointPosition = transform.position;
        basePointRotate = currentPointRotate = transform.rotation;

        if (weapons == null || weapons.Count <= 0)
        {
            // попытаться найти оружие.
        }
    }
}