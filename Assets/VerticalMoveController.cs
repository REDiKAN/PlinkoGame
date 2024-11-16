using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using Unity.VisualScripting;
using UnityEngine;

public class VerticalMoveController : MonoBehaviour
{
    [SerializeField] private float _minLevelDelta = 0;
    [SerializeField] private float _maxLevelDelta = 0;
                     private CompositeDisposable _disposable = new CompositeDisposable();
                     private Vector3 _startPos;

    private void Awake()
    {
        _startPos = gameObject.transform.position;
    }
    
    private void Start() {
        gameObject.OnMouseDragAsObservable().Subscribe((x) =>
        {
            gameObject.transform.position = new Vector3(_startPos.x,
            Mathf.Clamp(
                Camera.main.ScreenToWorldPoint(Input.mousePosition).y,
                _startPos.y + _minLevelDelta,
                _startPos.y + _maxLevelDelta), 0);
        }).AddTo(_disposable);
    }

    private void OnDestroy()
    {
        _disposable.Dispose();
    }
}
