using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMousePosition : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private Vector3 _point;

    public Vector3 Point => _point;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos2D = Input.mousePosition;
            //float screenToCameraDistance = _camera.nearClipPlane;
            float screenToCameraDistance = 0.5f;
            Vector3 mousePosNearClipPlane = new Vector3(mousePos2D.x, mousePos2D.y, screenToCameraDistance);
            _point = Camera.main.ScreenToWorldPoint(mousePosNearClipPlane);
            //_point = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            Debug.Log(Input.mousePosition);
        }
    }
}
