using UnityEngine;
using System.Collections;

public class ClawnLife : MonoBehaviour
{
    [SerializeField]
    private float _resistencePowerDuration = 4;
    //[SerializeField]
    //private int _resistencePower = 50;
    [SerializeField]
    private TextMesh _textMesh;
    [SerializeField]
    private SpriteRenderer _shieldSprite;

    private int _life = 100;
    public int Life
    {
        get { return _life; }
        private set { _life = value; }
    }

    private int _shild;
    public int Shield
    {
        get { return _shild; }
        private set { _shild = Mathf.Clamp(value, 0, 100); }
    }

    public bool Dead { get { return Life <= 0; } }

    private float _timerColdown;

    private void Update()
    {
        _shieldSprite.enabled = Shield > 0;
        _textMesh.text = Life.ToString();
    }

    public void TakeDamage(int damage)
    {
        if (_shild > 0)
            _shild -= damage;
        else
            Life -= damage;
    }

    public void Cure(int quantity)
    {
        Life = Mathf.Clamp(Life + quantity, 0, 100);
    }

    public void RaiseResistence(int quantity)
    {
        _shild = quantity;
        StartCoroutine(ResistencePowerActivate());
    }

    private IEnumerator ResistencePowerActivate()
    {
        float timer = _resistencePowerDuration;
        do
        {
            timer -= Time.deltaTime;
            yield return null;
        } while (timer > 0);

        _shild = 0;
    }
}
