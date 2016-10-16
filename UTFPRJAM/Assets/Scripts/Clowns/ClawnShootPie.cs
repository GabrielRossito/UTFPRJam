using UnityEngine;
using System.Collections;

public class ClawnShootPie : MonoBehaviour
{
    public float ReleadTime = 1;

    private ClawnManager _manager;
    private Pie _piePrefab { get { return Resources.Load<Pie>("PiePrefab"); } }

    private float _countReload;

    public void SetManager(ClawnManager manager)
    {
        _manager = manager;
    }

    private void Update()
    {
        if (_manager.BatmanOnSight && _countReload < 0)
            ShootPie();
        else
            _countReload -= Time.deltaTime;
    }

    private void ShootPie()
    {
        _countReload = ReleadTime;
        Pie pie = Instantiate(_piePrefab, transform.position, Quaternion.identity) as Pie;
        pie.Shoot(_manager.BatmanPosition);
    }
}
