using UnityEngine;
using System.Collections;

public class ClawnManager : MonoBehaviour
{
    public float DistanceOfSight = 2;
    public float Power = 10;

    public GameManager GameManager { get; private set; }

    private ClawnLife _life { get { return GetComponent<ClawnLife>(); } }
    private ClawnMovment _movement { get { return GetComponent<ClawnMovment>(); } }

    public Vector2 BatmanPosition { get; set; }
    public bool BatmanOnSight
    {
        get
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, BatmanPosition);
            if (hit.collider == null)
                return false;
            return Vector2.Distance(transform.position, BatmanPosition) <= DistanceOfSight;
        }
    }

    private ClawnShootPie _shootPie { get { return GetComponent<ClawnShootPie>(); } }

    private void Update()
    {
        if(_life.Dead)
            Die();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Batarangue")
        {
            Batarangue batarangue = collider.gameObject.GetComponent<Batarangue>();
            _life.TakeDamage(batarangue.Damage);
        }
    }

    public void Initialize(GameManager manager)
    {
        GameManager = manager;
        _movement.Initialize(this);
        if (_shootPie != null)
            _shootPie.SetManager(this);
    }

    public void GainLife(int quantity)
    {
        _life.Cure(quantity);
    }

    public void RaiseResistence()
    {
        _life.RaiseResistence();
    }

    public void TakeDamage(int quantity)
    {
        _life.TakeDamage(quantity);
    }

    private void Die()
    {
        GameManager.ClawDied(this);
        Destroy(gameObject);
    }
}
