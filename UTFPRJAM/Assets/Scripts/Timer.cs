using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.Events;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float _gameDuration = 120;
    [SerializeField]
    private TextMesh _textMesh;

    [Serializable]
    public class TimerEvent : UnityEvent<float> { }
    public TimerEvent onStart = new TimerEvent();
    public TimerEvent onTick = new TimerEvent();
    public TimerEvent onEnd = new TimerEvent();

    private float _timerCounter;
    private bool _timerStarted;

    public void StartCounting()
    {
        _timerStarted = true;
        _timerCounter = _gameDuration;
        onTick.Invoke(_gameDuration);
    }

    public void Stop()
    {
        _timerStarted = false;
    }

    private void LateUpdate()
    {
        if(_timerStarted)
        {
            _timerCounter -= Time.deltaTime;
            _textMesh.text = ((int)_timerCounter).ToString();
            if (_timerCounter <= 0)
            {
                _timerStarted = false;
                onEnd.Invoke(0);
            }
            else
                onTick.Invoke(_timerCounter);
        }
    }
}
