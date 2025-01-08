using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Base _botBase;
    [SerializeField] private Transform _position;
    [SerializeField] private int _unitCount;

    private void Start()
    {
        Base botBase = Instantiate(_botBase, _position);
        botBase.Initialize(_unitCount);
    }
}
