using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    [SerializeField] private float _maxSpeedMoving;
    [SerializeField] private float _minSpeedMoving;
    [SerializeField] private float _speedMoving;
    [SerializeField] private Transform[] _movePoints;

    private int _currentMovePointIndex = 0;

    private void Start()
    {
        RandomizeSpeed();
    }

    private void FixedUpdate()
    {
        MoveToNextPoint();
    }


    private void MoveToNextPoint()
    {
        if (_currentMovePointIndex < _movePoints.Length)
        {
            Vector3 targetPosition = _movePoints[_currentMovePointIndex].position;

            // ����������� Vector3.right ��� Vector3.up � ����������� �� ����������� ��������
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speedMoving * Time.fixedDeltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                _currentMovePointIndex++;
                if (_currentMovePointIndex == _movePoints.Length)
                {
                    _currentMovePointIndex = 0; // ��������� � ������ �����, ���� ���������� ���������
                }
            }
        }
    }

    private void RandomizeSpeed()
    {
        _speedMoving = Random.Range(_minSpeedMoving, _maxSpeedMoving);
    }

}
