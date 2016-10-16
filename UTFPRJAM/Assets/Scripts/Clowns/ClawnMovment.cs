using UnityEngine;
using System.Collections;

public class ClawnMovment : MonoBehaviour
{
    [SerializeField]
    private float
        _speed = 1,
        _distance = 2,
        _idleMinTime = 1,
        _idleMaxTime = 5;

    private Animator _animator { get {return GetComponent<Animator>(); } }

    private Vector2 _goTo;
    private bool _walking;
    private Vector3 _intialScale;

    private ClawnManager _clawManager;

    public void Initialize(ClawnManager manager)
    {
        _clawManager = manager;
        _intialScale = transform.localScale;
        RandomMovement();
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    // Se colidir, procurar outra posição
    //    if (collision.gameObject.tag == "Wall")
    //    {
    //        StopAllCoroutines();
    //        RandomMovement();
    //    }
    //}

    private void RandomMovement()
    {
        Vector2 randomPos = (Random.insideUnitCircle * _distance) + (Vector2)transform.position;
        MoveTo(randomPos);
    }

    private void MoveTo(Vector2 randomPos)
    {
        StopAllCoroutines();
        StartCoroutine(MoveToRoutine(randomPos));
    }

    private IEnumerator MoveToRoutine(Vector2 pos)
    {
        float timer = 0;
        Vector2 initialPos = transform.position;
        Vector2 deltaMov = initialPos - pos;

        if (deltaMov.x > 0) transform.localScale = new Vector3(-_clawManager.Size.x, _clawManager.Size.y, _clawManager.Size.z);
        if (deltaMov.x < 0) transform.localScale = new Vector3(_clawManager.Size.x, _clawManager.Size.y, _clawManager.Size.z);

        _walking = true;
        _animator.SetBool("Walk", true);

        while (Vector2.Distance(transform.position, pos) > 0.1f)
        {
            transform.position = Vector2.Lerp(initialPos, pos, timer);
            timer += Time.deltaTime * _speed;
            yield return null;
        }

        _walking = false;
        _animator.SetBool("Walk", false);

        transform.position = pos;
        timer = Random.Range(_idleMinTime, _idleMaxTime);

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        RandomMovement();
    }
}
