using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class FlagSetter : MonoBehaviour
{
    [SerializeField] private FlagGenerator _generator;
    [SerializeField] private Text _tooltipText;

    private Vector3 _target;

    private void Start()
    {
        _tooltipText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _target.y = transform.position.y;
            _generator.CreateFlag(_target);
        }
    }

    public void OnMouseOver()
    {
        _tooltipText.gameObject.SetActive(true);
    }

    public void OnMouseExit()
    {
        _tooltipText.gameObject.SetActive(false);
    }
}
