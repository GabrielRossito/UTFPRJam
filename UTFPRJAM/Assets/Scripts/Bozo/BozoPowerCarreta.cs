using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class BozoPowerCarreta : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private float _reloadTime = 15;
    [SerializeField]
    private int _power = 5;
    [SerializeField]
    private TextMesh _textMesh;

    private SpriteRenderer _sprite { get { return GetComponent<SpriteRenderer>(); } }

    private BozoManager _bozoManager;
    private Vector2 _initialPosition;
    private float _reloadTimerCount;

    private void Update()
    {
        if (_reloadTimerCount == 0)
            _textMesh.text = "Carreta\nFuracao";
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
        for (int i = 0; i < _power; i++)
            _bozoManager.GameManager.AddClawn(pos);
        scriptSoundMaster.Instance.PlaySound("risadaPlateia");
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
