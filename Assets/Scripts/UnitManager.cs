using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//1. получение координат ресурсов +
//2. выбор свобдного юнита
//3. отправка свободного юнита
//4. возвращение свободного юнитана базу
public class UnitManager : MonoBehaviour
{
    [SerializeField] private Unit[] _units;
    [SerializeField] private BaseScaner _scaner;

    private Transform _target;

    private void GetResourse()
    {

    }

    //private Unit GetFree()
    //{
    //    foreach(Unit unit in _units)
    //    {
    //        if(unit.IsBusy == false)
    //            return unit;
    //    }
    //}
}
