using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class FlagSetter : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler 
{
    [SerializeField] private FlagGenerator _generator;
    [SerializeField] private GetMousePosition _mousePosition;
    [SerializeField] private Text _tooltipText;
    [SerializeField] private Text _setBaseText;

    private Vector3 _target;

    private void Start()
    {
        _tooltipText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _target = _mousePosition.Point;

            _target.y = transform.position.y;
            _generator.CreateFlag(_target);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _tooltipText.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tooltipText.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Time.timeScale = 0.5f;
        _tooltipText.gameObject.SetActive(false);
        _setBaseText.text = "Выберите куда будете ставить флаг";
    }
}
