using UnityEngine;
using System.Collections;

public class BatmanManager : MonoBehaviour
{

    public BatmanLife Life { get { return GetComponent<BatmanLife>(); } }
    public BatmanAnimation Animation { get { return GetComponent<BatmanAnimation>(); } }
    public BatmanPunch Punch { get { return GetComponent<BatmanPunch>(); } }
    public BatmanMovement Movement { get { return GetComponent<BatmanMovement>(); } }

    private GameManager _gameManager;

    public void Initialize(GameManager manager)
    {
        _gameManager = manager;
        Punch.Initialize(this);
        Movement.Initialize(this);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Bullet")
        {
            Pie pie = collider.gameObject.GetComponent<Pie>();
            Life.TakeDamage(pie.Damage);
        }
    }

    private void Update()
    {
        if (Life.Dead)
        {
            Debug.Log("DEAD"); 
            Destroy(this.gameObject);
        }
    }

}
