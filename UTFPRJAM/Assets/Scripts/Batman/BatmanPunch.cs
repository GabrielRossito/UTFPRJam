using UnityEngine;
using System.Collections;

public class BatmanPunch : MonoBehaviour
{
    [SerializeField]
    private float
        _distance = 1,
        _delay = 3;
    [SerializeField]
    private int _power = 50;
    [SerializeField]
    private TextMesh _textMesh;

    private bool Recharging { get { return _rechargTime != 0; } }

    private BatmanManager _batmanManager;
    private float _rechargTime;

    public void Initialize(BatmanManager manager)
    {
        _batmanManager = manager;
    }

    public void Punch()
    {
        if (_batmanManager.Dead) return;

        if (Recharging) return;
        StartCoroutine(Recharg());
        Vector3 direction = transform.localScale.x > 0 ? transform.right : -transform.right;
        RaycastHit2D hit = Physics2D.Raycast(transform.position + direction * 0.5f, direction, _distance);
        if (hit.collider != null)
        {
            if(hit.collider.gameObject.tag == "Clawn")
            {
                hit.collider.gameObject.GetComponent<ClawnManager>().TakeDamage(_power);
            }
        }

        _batmanManager.Animation.Punch();
    }

    private void Update()
    {
        _textMesh.text = "Tempo do Soco: " + _rechargTime.ToString();

        if (Input.GetKeyDown(KeyCode.Space))
            Punch();
    }

    private IEnumerator Recharg()
    {
        _rechargTime = _delay;
        do
        {
            _rechargTime -= Time.deltaTime;
            yield return null;
        } while (_rechargTime > 0);

        _rechargTime = 0;
    }

}
