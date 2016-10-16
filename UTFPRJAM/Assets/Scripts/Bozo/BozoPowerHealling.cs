using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class BozoPowerHealling : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private float _reloadTime = 5, _effectRadius = 2;
    [SerializeField]
    private int _power = 50;
    [SerializeField]
    private TextMesh _textMesh;

    private SpriteRenderer _sprite { get { return GetComponent<SpriteRenderer>(); } }

    private BozoManager _bozoManager;
    private Vector2 _initialPosition;
    private float _reloadTimerCount;

    private void Update()
    {
        if (_reloadTimerCount == 0)
            _textMesh.text = "Cura";
        else
            _textMesh.text = _reloadTimerCount.ToString();
    }

    public void Initialize(BozoManager manager)
    {
        _bozoManager = manager;
        _initialPosition = transform.position;
    }

    private void UsePower(Vector3 pos)
    {
        foreach (ClawnManager claw in _bozoManager.GameManager.Claws)
        {
            if (Vector3.Distance(pos, claw.transform.position) < _effectRadius)
                claw.GainLife(_power);
        }
        scriptSoundMaster.Instance.PlaySound("magiaCura");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_reloadTimerCount != 0) return;
        transform.position = eventData.pointerCurrentRaycast.worldPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        UsePower(eventData.pointerCurrentRaycast.worldPosition);
        transform.position = _initialPosition;
        StartCoroutine(Reloading());
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
