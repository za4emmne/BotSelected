using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private BaseResourseGarage _garage;
    [SerializeField] private Text _resourseCountText;

    private int _resourseCount;

    private void Awake()
    {
        _base = GetComponent<Base>();
    }

    private void Start()
    {
        transform.position = _base.transform.position;
        ChangeResourseView();
    }

    private void OnEnable()
    {
        _base.ChangedResourseCount += ChangeResourseView;
    }

    private void OnDisable()
    {
        _base.ChangedResourseCount -= ChangeResourseView;
    }

    private void ChangeResourseView()
    {
        _resourseCount = _garage.GetResourseCount();
        _resourseCountText.text = "Ресурсов: " + _resourseCount;
    }
}
