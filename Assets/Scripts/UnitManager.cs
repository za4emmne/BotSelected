using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//1. получение координат ресурсов 
//2. выбор свобдного юнита
//3. отправка свободного юнита
//4. возвращение свободного юнитана базу
[RequireComponent(typeof(UnitGenerator))]

public class UnitManager : MonoBehaviour
{
    [SerializeField] private Base _base;

    private UnitGenerator _unitGenerator;
    private Transform _target;
    private List<Unit> _units;

    private void Awake()
    {
        _unitGenerator = GetComponent<UnitGenerator>();
    }

    private void Start()
    {
        _units = new List<Unit>();
    }

    private List<Unit> GetUnits()
    {
        return _units = _unitGenerator.GetList();
    }

    private IEnumerator MoveToResourses()
    {
        WaitForSeconds wait = new WaitForSeconds(1);
        _target = _base.GetPositionResourse(1);

        while (true)
        {
            if (GetFree() != null)
            {
                _target = _base.GetPositionResourse(0);
                //GetFree().transform.position = MoveTowards(_target.position, );
            }
            yield return wait;
        }
    }

    private Unit GetFree()
    {
        Unit unit;

        if (_units[0].IsBusy == false)
        {
            unit = _units[0];
            _units.RemoveAt(0);
            return unit;
        }
        else
            return null;        
    }
}
