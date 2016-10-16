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
            _clawns.Add(AddClawn(Vector3.zero));
        }
    }

    private void LateUpdate()
    {
        if (_batmanPosition != null)
        {
            for (int i = 0; i < _clawns.Count; i++)
                _clawns[i].BatmanPosition = _batmanPosition.position;
        }

    }

    public ClawnManager AddClawn(Vector3 position)
    {
        ClawnManager clawn = Instantiate(_clawPrefab, position, Quaternion.identity) as ClawnManager;
        clawn.Initialize(this);
        return clawn;
    }

    public void ClawDied(ClawnManager clawn)
    {
        _clawns.Remove(clawn);
    }
}
