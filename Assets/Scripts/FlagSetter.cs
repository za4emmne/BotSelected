using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class FlagSetter : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private FlagGenerator _generator;
    [SerializeField] private Text _tooltipText;
    [SerializeField] private Text _setBaseText;

    private Flag _flag;
    private Vector3 _target;
    private bool _canBuild;
    private bool _isCreated;

    public bool IsCreated => _isCreated;

    public event Action BuildNewBase;

    private void Start()
    {
        _flag = GetComponent<Flag>();
        _canBuild = false;
        _isCreated = false;
        _setBaseText.gameObject.SetActive(false);
        _tooltipText.gameObject.SetActive(false);
    }

    private void Update()
    {
        SetFlag();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerCurrentRaycast.gameObject;

        if (obj.GetComponent<Base>())
        {
            _tooltipText.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tooltipText.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerCurrentRaycast.gameObject;

        if (obj.GetComponent<Base>())
        {
            Time.timeScale = 0.5f;
            _canBuild = true;
            _tooltipText.gameObject.SetActive(false);

            if (_isCreated)
            {
                _setBaseText.text = "�������� ���� ������ ��������� ����";
            }
            else
            {
                _setBaseText.text = "�������� ���� ������ ������� ����";
            }
        }

        _setBaseText.gameObject.SetActive(true);
    }

    public Transform Position()
    {
        if (_flag != null)
        {
            return _flag.transform;
        }

        return null;
    }

    public void ChangeCretedStatus()
    {
        _isCreated = false;
    }

    public void Realese()
    {
        _flag.gameObject.SetActive(false);
    }

    private void SetFlag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (_canBuild && _isCreated == false && _flag == null)
                {
                    _target = hit.point;
                    _flag = _generator.CreateFlag(_target);
                    _isCreated = true;
                }
                else if (_isCreated && _canBuild)
                {
                    if (_flag.gameObject.activeSelf)
                    {
                        _target = hit.point;
                        _generator.Replace(_flag, _target);
                    }
                    else
                    {
                        _flag.gameObject.SetActive(true);
                        _target = hit.point;
                        _generator.Replace(_flag, _target);
                    }
                }

                _setBaseText.gameObject.SetActive(false);
                Time.timeScale = 1;
                _canBuild = false;
            }
        }
    }
}
