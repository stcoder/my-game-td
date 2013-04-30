using UnityEngine;
using System.Collections.Generic;

/**
 * ������� ����� ������������� ���������� �������� �����.
 */
class AICombatDrone : MonoBehaviour
{
    // ���������� �������������� �����.
    private int maxTargets = 2;

    // ������ �������������� ����� ���� Transform.
    private List<Transform> targets = new List<Transform>();

    // ������ �������� ��� ����������� �����.
    private float sensorRadius = 5.0f;

    // ���� ������.
    private float viewAngle = 70.0f;
}