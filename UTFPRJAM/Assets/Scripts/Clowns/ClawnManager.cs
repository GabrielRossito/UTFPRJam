using UnityEngine;
using System.Collections;

public class ClawnManager : MonoBehaviour
{
    public float DistanceOfSight = 2;

    public GameManager GameManager { get; private set; }

    private ClawnLife _life { get { return GetComponent<ClawnLife>(); } }
    private ClawnMovment _movement { get { return GetComponent<ClawnMovment>(); } }
    private ClawnShootPie _shooter { get { return GetComponent<ClawnShootPie>(); } }

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
    public Vector3 Size { get { return _initiazlSize + ((Vector3.one * 0.01f) * _shooter.PowerStrenght); } }

    private Vector3 _initiazlSize;

    private void Update()
    {
        if (_life.Dead)
            Die();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Batarangue")
        {
            Batarangue batarangue = collider.gameObject.GetComponent<Batarangue>();
            //_life.TakeDamage(batarangue.Damage);
            TakeDamage(batarangue.Damage);
        }
    }

    public void Initialize(GameManager manager)
    {
        _initiazlSize = transform.localScale;
        GameManager = manager;
        _movement.Initialize(this);
        if (_shootPie != null)
            _shootPie.SetManager(this);
    }

    public void GainLife(int quantity)
    {
        _life.Cure(quantity);
    }

    public void RaiseDamage(int quantity)
    {
        _shooter.RaiseStrenght(quantity);
    }

    public void RaiseResistence(int quantity)
    {
        _life.RaiseResistence(quantity);
    }

    public void TakeDamage(int quantity)
    {
        scriptSoundMaster.Instance.PlaySound("golpeHit");
        _life.TakeDamage(quantity);
    }

    private void Die()
    {
        scriptSoundMaster.Instance.PlaySound("aye");
        GameManager.ClawDied(this);
        Destroy(gameObject);
    }
}
