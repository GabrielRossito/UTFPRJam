using UnityEngine;
using System.Collections;

public class Pie : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5;
    [SerializeField]
    private int _damage = 10;

    public int Damage { get { return _damage; } }

    private Vector2 _positionTo;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag != "Clawn")
            Die();
    }

    public void Shoot(Vector2 position)
    {
        _positionTo = position;
        StartCoroutine(ShootRoutine());
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private IEnumerator ShootRoutine()
    {
        Vector2 pos = transform.position;
        float timer = 0;
        do
        {
            timer += Time.deltaTime * _speed;
            transform.position = Vector2.Lerp(pos, _positionTo, timer);
            yield return null;
        } while (Vector2.Distance(_positionTo, transform.position) > 0.1f);

        Die();
    }
}
