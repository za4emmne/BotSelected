using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
//1. ��������� ��������� �������� 
//2. ����� ��������� �����
//3. �������� ���������� �����
//4. ����������� ���������� ������� ����
[RequireComponent(typeof(UnitGenerator))]

public class UnitManager : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private BaseScaner _scaner;

    private UnitGenerator _unitGenerator;
    private Transform _target;
    private Coroutine _coroutine;
    private List<Unit> _activeUnits;

    private void Awake()
    {
        _activeUnits = new List<Unit>();
        _scaner = GetComponent<BaseScaner>();
        _unitGenerator = GetComponent<UnitGenerator>();
    }

    private void OnEnable()
    {
        _scaner.OnScanComplete += BotMove;
    }

    private void OnDisable()
    {
        _scaner.OnScanComplete -= BotMove;
    }

    private void BotMove()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(MoveToResourses());
        }
    }

    private IEnumerator MoveToResourses()
    {
        WaitForSeconds wait = new WaitForSeconds(1);

        while (true)
        {
            Unit unit = GetFree();

            if (unit != null)
            {
                _target = _base.GetNearPositionResourse();

                if (_target != null)
                {
                    unit.TakeResoursePosition(_target);
                }
            }

            yield return wait;
        }

    }

    private Unit GetFree()
    {
        _activeUnits = _unitGenerator.ActiveObject;

        foreach (var unit in _activeUnits)
        {
            if (unit.IsBusy == false)
            {
                unit.ChangeStatus();
                return unit;
            }
        }

        return null;
    }
}
