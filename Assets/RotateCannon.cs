using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RotateCannon : MonoBehaviour
{
    [SerializeField] private float _minLevelDelta = 0;
    [SerializeField] private float _maxLevelDelta = 0;
                     private CompositeDisposable _disposable = new CompositeDisposable();
                     private Transform _cannon;

    private void Awake()
    {
        _cannon = transform.parent;
    }

    private void Start()
    {
        gameObject.OnMouseDragAsObservable().Subscribe((x) =>
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _cannon.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.left, (_cannon.position-mousePos)));
        }).AddTo(_disposable);
    }

    private void OnDestroy()
    {
        _disposable.Describe();
    }
}