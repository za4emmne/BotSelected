using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
//1. получение координат ресурсов 
//2. выбор свобдного юнита
//3. отправка свободного юнита
//4. возвращение свободного юнитана базу
[RequireComponent(typeof(UnitGenerator))]

public class UnitManager : MonoBehaviour
{
    [SerializeField] private Base _base;

    private UnitGenerator _unitGenerator;
    private Resourse _target;
    private Coroutine _coroutine;
    private List<Unit> _activeUnits;

    private void Awake()
    {
        _activeUnits = new List<Unit>();
        _unitGenerator = GetComponent<UnitGenerator>();
    }

    private void OnEnable()
    {
        _base.SendFreeBot += BotMove;
    }

    private void OnDisable()
    {
        _base.SendFreeBot -= BotMove;
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
        while (true)
        {
            _target = _base.GetNearResourse();
           
            if (_target != null)
            {
                Unit unit = GetFree();

                if (unit != null)
                {
                    unit.TakeResourse(_target);
                }
            }

            yield return null;
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
