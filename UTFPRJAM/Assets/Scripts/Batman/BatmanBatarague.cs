using UnityEngine;
using System.Collections;

public class BatmanBatarague : MonoBehaviour
{
    [SerializeField]
    private float _distanceTravel = 2, _reloadTime = 1;

    [SerializeField]
    private TextMesh _textMesh;

    private BatmanManager _manager;
    private Batarangue _bataranguePrefab { get { return Resources.Load<Batarangue>("BataranguePrefab"); } }

    private bool Reloading { get { return _countReload > 0; } }

    private float _countReload;
    private bool _shoot;

    public void SetManager(BatmanManager manager)
    {
        _manager = manager;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
            Shoot();
        _textMesh.text = "Tempo de batarangue: " + _countReload;
    }

    private void Shoot()
    {
        if (_shoot) return;
        if (Reloading) return;
        _shoot = true;
        Batarangue pie = Instantiate(_bataranguePrefab, transform.position, Quaternion.identity) as Batarangue;
        pie.Shoot(transform, transform.localScale.x < 0 ? -_distanceTravel : _distanceTravel, Reload);
    }

    private void Reload()
    {
        _shoot = false;
        StartCoroutine(ReloadingRoutine());
    }

    private IEnumerator ReloadingRoutine()
    {
        _countReload = _reloadTime;
        do
        {
            _countReload -= Time.deltaTime;
            yield return null;
        } while (_countReload > 0);
        _countReload = 0;
    }

}
