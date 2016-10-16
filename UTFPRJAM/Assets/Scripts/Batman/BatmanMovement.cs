using UnityEngine;
using System.Collections;

public class BatmanMovement : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed = 0.1f;

    private BatmanManager _batmanManager;
    private Vector3 _localScale;

    public void Initialize(BatmanManager manager)
    {
        _batmanManager = manager;
        _localScale = transform.localScale;
    }

    private void Update()
    {
        if (_batmanManager.Dead) return;

        if (Input.GetAxis("Vertical") > 0)
        {
            transform.position += Vector3.up * _movementSpeed;
            _batmanManager.Animation.Run();
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            transform.position -= Vector3.up * _movementSpeed;
            _batmanManager.Animation.Run();
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.localScale = new Vector3(_localScale.x, _localScale.y, _localScale.z);
            transform.position += Vector3.right * _movementSpeed;
            _batmanManager.Animation.Run();
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.localScale = new Vector3(_localScale.x * -1, _localScale.y, _localScale.z);
            transform.position -= Vector3.right * _movementSpeed;
            _batmanManager.Animation.Run();
        }

        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
            _batmanManager.Animation.Idle();
    }

}
