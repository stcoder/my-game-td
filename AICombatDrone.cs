using UnityEngine;
using System.Collections;
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
    public List<Transform> targets = new List<Transform>();

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

    // Список базового класса оружия для каждого активного оружия.
    public List<Weapon> weaponsBase = new List<Weapon>();

    // Слой врагов.
    public int enemyLayer = 20;

    // Маска врагов, нужна для поиска врагов в радиусе видимости сенсора.
    private int enemyMask;

    private void Start()
    {
        enemyMask = 1 << 20;
        basePointPosition = currentPointPosition = transform.position;
        basePointRotate = currentPointRotate = transform.rotation;

        foreach (Transform weapon in weapons)
        {
            weaponsBase.Add(weapon.GetComponent<Weapon>());
        }

        // Запускаем поиск целей.
        StartCoroutine(searchTargetsInSensorRadius());
    }

    /**
     * Тут будут выполняться простые операции.
     * 
     * Например перемещение.
     */
    private void Update()
    {
        //print("update");
    }

    /**
     * Ищем цели в радиусе сенсора.
     */
    private IEnumerator searchTargetsInSensorRadius()
    {
        while(true)
        {
            if (targets.Count <= 0 || targets.Count < maxTargets)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, sensorRadius, enemyMask);
                foreach (Collider collider in colliders)
                {
                    if (targets.IndexOf(collider.transform) == -1)
                    {
                        targets.Add(collider.transform);
                    }
                }
            }


            // цикл не пройдет если нет целей.
            // не понятно как это работает, но работает, ибо просто уже не соображаю %)
            for (int i = 0; i < targets.Count; i++)
            {
                Transform target = targets[i];
                float distance = Vector3.Distance(transform.position, target.position);
                print(Time.deltaTime);
                print(target.name + " -> " + distance);
                if (distance > sensorRadius)
                {
                    targets.Remove(target);
                }
            }

            // Функция будет срабатывать каждый игровой тик?
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sensorRadius);
    }
}