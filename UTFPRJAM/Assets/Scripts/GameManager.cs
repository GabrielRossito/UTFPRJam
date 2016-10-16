using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform _batmanPosition;

    [SerializeField]
    private int _clawnQuantity = 5;
    [SerializeField]
    private ClawnManager _clawPrefab;

    [SerializeField]
    private BozoManager _bozoManager;

    [SerializeField]
    private BatmanManager _batmanManager;

    private List<ClawnManager> _clawns = new List<ClawnManager>();
    public List<ClawnManager> Claws { get { return _clawns; } }

    private void Start()
    {
        _bozoManager.Initialize(this);
        _batmanManager.Initialize(this);
        for (int i = 0; i < _clawnQuantity; i++)
        { 
            _clawns.Add(Instantiate(_clawPrefab));
            _clawns[i].Initialize(this);
        }
    }

    private void Update()
    {
        if (_batmanPosition != null)
            for (int i = 0; i < _clawnQuantity; i++)
                _clawns[i].BatmanPosition = _batmanPosition.position;
    }
}
