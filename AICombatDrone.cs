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
    public Transform target = null;

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
            Collider[] colliders = Physics.OverlapSphere(transform.position, sensorRadius, enemyMask);

            if (colliders.Length > 0)
            {
                foreach (Collider collider in colliders)
                {
                    Enemy enemy = collider.gameObject.GetComponent<Enemy>();
                    if (enemy.getEnemyType() == GameControll.EnemiesType.land)
                    {
                        if (target == null)
                        {
                            target = collider.transform;
                            continue;
                        }
                    }
                    else if (enemy.getEnemyType() == GameControll.EnemiesType.flying)
                    {
                        if (target != null)
                        {
                            Enemy enemyTarget = target.GetComponent<Enemy>();
                            if (enemyTarget.getEnemyType() != GameControll.EnemiesType.flying)
                            {
                                target = collider.transform;
                            }
                        }
                        else
                        {
                            target = collider.transform;
                        }
                        break;
                    }
                }
            }


            if (target != null)
            {
                // удаляем цель если она вышла за пределы видимости.
                float distance = Vector3.Distance(transform.position, target.position);
                if (distance > sensorRadius)
                {
                    target = null;
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