using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class BozoPowerStrenght : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private float _reloadTime = 7.5f;
    [SerializeField]
    private int _power = 20;
    [SerializeField]
    private TextMesh _textMesh;

    private SpriteRenderer _sprite { get { return GetComponent<SpriteRenderer>(); } }

    private BozoManager _bozoManager;
    private Vector2 _initialPosition;
    private float _reloadTimerCount;

    private void Update()
    {
        if (_reloadTimerCount == 0)
            _textMesh.text = "Força";
        else
            _textMesh.text = _reloadTimerCount.ToString();
    }

    public void Initialize(BozoManager manager)
    {
        _bozoManager = manager;
        _initialPosition = transform.position;
    }

    private void UsePower(ClawnManager clawn)
    {
        clawn.RaiseDamage(_power);
        StartCoroutine(Reloading());
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_reloadTimerCount != 0) return;
        Vector3 pos = eventData.pointerCurrentRaycast.worldPosition;
        pos.z = 0.5f;
        transform.position = pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.tag == "Clawn")
        {
            UsePower(eventData.pointerCurrentRaycast.gameObject.GetComponent<ClawnManager>());
        }
        transform.position = _initialPosition;
    }

    private IEnumerator Reloading()
    {
        _reloadTimerCount = _reloadTime;
        _sprite.color = Color.black;
        do
        {
            _reloadTimerCount -= Time.deltaTime;
            yield return null;
        } while (_reloadTimerCount > 0);
        _reloadTimerCount = 0;
        _sprite.color = Color.white;
    }
}
