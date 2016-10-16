using UnityEngine;
using System.Collections;

public class Pie : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5;
    [SerializeField]
    private int _damage = 10;
    [SerializeField]
    private Sprite _pieAlmost, _pieDie;
    [SerializeField]
    private Sprite[] _splashes;

    private SpriteRenderer _spriteRenderer { get { return GetComponent<SpriteRenderer>(); } }
    public int Damage { get { return _damage; } }

    private Vector2 _positionTo;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag != "Clawn")
            Die();
    }

    public void Shoot(Vector2 position, int aditionalDamage = 0)
    {
        _positionTo = position;

        _damage += aditionalDamage;

        Vector3 delta = (Vector3)_positionTo - transform.position;
        delta.Normalize();
        float rot = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
        transform.rotation = (Quaternion.Euler(0, 0, rot));

        StartCoroutine(ShootRoutine());
    }

    private void Die()
    {
        GameObject splash = new GameObject();
        splash.transform.position = transform.position + Vector3.forward;
        splash.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        SpriteRenderer splashRenderer = splash.AddComponent<SpriteRenderer>();
        splashRenderer.sprite = _splashes[Random.Range(0, _splashes.Length)];
        scriptSoundMaster.Instance.PlaySound("torta");
        Destroy(gameObject);
    }

    private IEnumerator ShootRoutine()
    {
        Vector2 pos = transform.position;
        float timer = 0;
        float maxDist = Vector2.Distance(_positionTo, transform.position);
        do
        {
            timer += Time.deltaTime * _speed;
            transform.position = Vector2.Lerp(pos, _positionTo, timer);
            if (Vector2.Distance(_positionTo, transform.position) <= maxDist / 2)
                _spriteRenderer.sprite = _pieAlmost;
            if (Vector2.Distance(_positionTo, transform.position) <= maxDist / 3)
            {
                _spriteRenderer.sprite = _pieDie;
            }
            yield return null;
        } while (Vector2.Distance(_positionTo, transform.position) > 0.1f);

        Die();
    }
}
