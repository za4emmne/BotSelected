using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//сканирование карты в заданном радиусе + 
//возвращение листа отсканированных ресурсов +
public class BaseScaner : MonoBehaviour
{
    [SerializeField] private float _scanRadius;
    [SerializeField] private LayerMask _scannLayerMask;
    [SerializeField] private float _delayOnScan;

    public List<Resourse> Resourses => _resourses;
    public event Action OnScanComplete;

    private List<Resourse> _resourses;

    private void Start()
    {
        _resourses = new List<Resourse>();
    }

    private void Update()
    {
        DrawScanZone(100, Color.blue);
    }

    public List<Resourse> Scan()
    {
        Collider[] resiurseCollider = Physics.OverlapSphere(transform.position, _scanRadius, _scannLayerMask);

        foreach (var collider in resiurseCollider)
        {
            if (collider.TryGetComponent(out Resourse resourse))
            {
                _resourses.Add(resourse);
                OnScanComplete?.Invoke();
            }
        }

        return _resourses;
    }

    public Transform GetResoursePosition()
    {
        Resourse resourse = _resourses[0];
        return resourse.transform;
    }

    private void DrawScanZone(int pointsCount, Color color)
    {
        List<Vector3> circlePoints = new List<Vector3>();
        float degreesInCircle = 360.0f;
        float angleStep = degreesInCircle / pointsCount * Mathf.Deg2Rad;
        Vector3 center = transform.position;

        for (int i = 0; i < pointsCount; i++)
        {
            float angle = angleStep * i;
            Vector2 point = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * _scanRadius;
            circlePoints.Add(new Vector3(center.x + point.x, center.y, center.z + point.y));
        }

        for (int i = 0; i < circlePoints.Count - 1; i++)
            Debug.DrawLine(circlePoints[i], circlePoints[i + 1], color);

        Debug.DrawLine(circlePoints[0], circlePoints[circlePoints.Count - 1], color);
    }
}
