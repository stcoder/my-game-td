﻿using UnityEngine;

class Enemy : MonoBehaviour
{
    private GameControll.EnemiesType type;

    // Установить тип врага.
    public void setEnemyType(GameControll.EnemiesType type)
    {
        this.type = type;
    }

    // Получить тип врага.
    public GameControll.EnemiesType getEnemyType()
    {
        return type;
    }
}