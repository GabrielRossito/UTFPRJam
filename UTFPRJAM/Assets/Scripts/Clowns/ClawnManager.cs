using UnityEngine;
using System.Collections;

public class ClawnManager : MonoBehaviour
{
    public float DistanceOfSight = 2;
    public float Power = 10;

    private ClawnLife _life { get { return GetComponent<ClawnLife>(); } }

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

    private void Start()
    {
        if (_shootPie != null)
            _shootPie.SetManager(this);
    }

    private void Update()
    {
        if(_life.Dead)
            Die();
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
        Destroy(gameObject);
    }
}
