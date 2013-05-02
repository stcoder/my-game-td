using UnityEngine;

class LowCreep : Enemy
{
    private void Awake()
    {
        setEnemyType(GameControll.EnemiesType.land);
    }
}