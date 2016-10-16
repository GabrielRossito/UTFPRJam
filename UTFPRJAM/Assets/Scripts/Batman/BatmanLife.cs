using UnityEngine;
using System.Collections;

public class BatmanLife : MonoBehaviour
{
    [SerializeField]
    private TextMesh _textMesh;

    [SerializeField]
    private float _life = 200;
    public float Life
    {
        get { return _life; }
        private set { _life = Mathf.Clamp(value, 0, 200); }
    }

    public bool Dead { get { return Life <= 0; } }

    public void TakeDamage(int quantity)
    {
        Life -= quantity;
    }

    private void Update()
    {
        _textMesh.text = "Vida: " + Life;
    }
}
