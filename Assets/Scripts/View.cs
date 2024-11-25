using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private Text _resourseCountText;

    private int _resourseCount;

    private void Start()
    {
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
        _resourseCount = _base.ResourseCount;
        _resourseCountText.text = "Ресурсов на базе: " + _resourseCount;
    }

}
