using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//1. ��������� ��������� �������� +
//2. ����� ��������� �����
//3. �������� ���������� �����
//4. ����������� ���������� ������� ����
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
