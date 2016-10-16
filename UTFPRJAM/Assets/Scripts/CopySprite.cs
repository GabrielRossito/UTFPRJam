using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class CopySprite : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private float _timesBigger = 2.2f;
    [SerializeField]
    private SpriteRenderer _copyFrom;

    private SpriteRenderer _spriteRenderer { get { return GetComponent<SpriteRenderer>(); } }

    private void Start()
    {
        _spriteRenderer.enabled = false;
        transform.localScale = Vector3.one * _timesBigger;
    }

    private void Update()
    {
        _spriteRenderer.sprite = _copyFrom.sprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _spriteRenderer.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _spriteRenderer.enabled = false;
    }
}
