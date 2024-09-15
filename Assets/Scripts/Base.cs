using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//получение ресурсов +
//определение координат ресурсов
//рекью компонент +
//корутина сканирования карты +
[RequireComponent(typeof(BaseScaner))]
public class Base : MonoBehaviour
{
    [SerializeField] private float _scanDelay;

    private BaseScaner _scaner;
    private List<Resourse> _resourses;

    private void Awake()
    {
        _scaner = GetComponent<BaseScaner>();
    }

    private void Start()
    {
        StartCoroutine(GetResourses());
    }

    public Transform GetPositionResourse(int number)
    {
        return _resourses[number].transform;
    }

    private IEnumerator GetResourses()
    {
        WaitForSeconds waitSpawn = new WaitForSeconds(_scanDelay);

        while (true)
        {
            _resourses = _scaner.Scan();
            Debug.Log(_resourses.Count);   
            yield return waitSpawn;
        }
    }
}
