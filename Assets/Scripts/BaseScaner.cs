using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BaseScaner : MonoBehaviour
{
    [SerializeField] private float _scanRadius;
    [SerializeField] private LayerMask _scannLayerMask;
    [SerializeField] private float _delayOnScan;
    [SerializeField] private Base _base;

    private int points = 100;
    private LineRenderer _lineRenderer;
    private List<Resourse> _resourses;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();

        _lineRenderer.positionCount = points;
        _lineRenderer.loop = true;
        DrawCircleShape();
    }

    private void Update()
    {
        DrawScanZone(100, Color.blue);
    }

    public List<Resourse> Scan()
    {
        _resourses = new();
        Collider[] resiurseCollider = Physics.OverlapSphere(transform.position, _scanRadius, _scannLayerMask);

        foreach (var collider in resiurseCollider)
        {
            if (collider.TryGetComponent(out Resourse resourse))
            {
                SelectedResourse(resourse);
            }
        }

        return _resourses;
    }

    private void SelectedResourse(Resourse resourse)
    {
        if (_resourses.Count > 0)
        {
            if (_resourses.Contains(resourse) == false)
                _resourses.Add(resourse);
        }
        else
        {
            _resourses.Add(resourse);
        }
    }

    private void DrawScanZone(int pointsCount, Color color)
    {
        List<Vector3> circlePoints = new();
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

    private void DrawCircleShape()
    {
        float angleStep = 360f / points;  // Шаг угла между точками

        for (int i = 0; i < points; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            float x = Mathf.Cos(angle) * _scanRadius;
            float y = Mathf.Sin(angle) * _scanRadius;

            _lineRenderer.SetPosition(i, new Vector3(x, 0.1f, y)); // Рисуем точки в 2D
        }
    }
}