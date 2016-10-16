using UnityEngine;
using System.Collections;
using System;

public class Batarangue : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5, _rotationSpeed = 15;
    [SerializeField]
    private int _damage = 10;

    public int Damage { get { return _damage; } }

    private Transform _batmanTransform;
    private Vector2 _positionTo;
    private Action _callback;
    private float _rotateAngle;

    private void Update()
    {
        transform.Rotate(Vector3.forward, _rotationSpeed);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag != "Batman")
            Die();
    }

    public void Shoot(Transform batman, float distance, Action callback)
    {
        _positionTo = transform.position + Vector3.right * distance;
        _callback = callback;
        _batmanTransform = batman;
        StartCoroutine(ShootRoutine());
    }

    private void Die()
    {
        _callback();
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

        timer = 0;
        do
        {
            timer += Time.deltaTime * _speed;
            transform.position = Vector2.Lerp(_positionTo, _batmanTransform.position, timer);
            yield return null;
        } while (Vector2.Distance(transform.position, _batmanTransform.position) > 0.1f);

        Die();
    }
}
