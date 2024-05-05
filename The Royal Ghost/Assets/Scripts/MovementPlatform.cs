using System.Collections.Generic;
using UnityEngine;

public class MovementPlatform : MonoBehaviour
{
    public float speed = 2f;
    public bool isActivated = false;
    public GameObject leverON;
    public GameObject leverOFF;

    public List<Transform> turningPoints; // ������ ����� ��������

    private int currentTargetIndex = 0; // ������ ������� ������� �����
    private Vector2 targetPosition;

    void Start()
    {
        if (turningPoints.Count > 0)
        {
            targetPosition = turningPoints[currentTargetIndex].position;
        }
    }

    void Update()
    {
        if (leverON.activeInHierarchy)
        {
            isActivated = true;
        }
        else if(leverOFF.activeInHierarchy)
        {
            isActivated = false;
        }

        if (isActivated && turningPoints.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentTargetIndex = (currentTargetIndex + 1) % turningPoints.Count; // ����������� ������������ ����� �������
                targetPosition = turningPoints[currentTargetIndex].position;
            }
        }
    }

    // (�����������) ������������ ����� ��������
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        foreach (Transform point in turningPoints)
        {
            Gizmos.DrawSphere(point.position, 0.2f);
        }
    }
}