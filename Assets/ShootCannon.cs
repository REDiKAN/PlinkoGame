using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;


public class ShootCannon : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _crosshair;
                     private bool CanShoot = true;
                     private CompositeDisposable _disposablesAddJump = new CompositeDisposable();
                     private CompositeDisposable _disposables = new CompositeDisposable();
    [SerializeField] private GameCycleController _gmc;
                     private IntReactiveProperty CountInGameBalls = new IntReactiveProperty(0);


    private void Start()
    {
        CountInGameBalls.Where(x => x == 0 && _gmc.CheckBallsIsEmpty()).Subscribe(_ => {
            _gmc.LostLastBall();
        });
        Observable.EveryGameObjectUpdate()
            .Where(_ => Input.GetKeyDown(KeyCode.P) && CanShoot && !_gmc.CheckBallsIsEmpty())
            .Subscribe(_ => {
                GameObject gmObject = Instantiate(_bullet);
                gmObject.transform.position = _crosshair.transform.position;
                Vector2 dir = (_crosshair.transform.position - transform.position).normalized;
                gmObject.GetComponent<Rigidbody2D>().AddForce(dir*10, ForceMode2D.Impulse);
                var rot = Mathf.Atan2(dir.y, dir.x);
                gmObject.transform.rotation = Quaternion.EulerRotation(new Vector3(0, 0, rot * Mathf.Deg2Rad));
                CanShoot = false;
                _gmc.Balls--;
                CountInGameBalls.Value++;
                
                Observable.Timer(System.TimeSpan.FromSeconds(1)).Subscribe(_ =>
                {
                    CanShoot = true;
                });

                gmObject.OnDestroyAsObservable().Subscribe(_ => {
                    CountInGameBalls.Value--;
                });
            }).AddTo(_disposables);
    }

    private void OnDestroy()
    {
        _disposables.Dispose();
    }
}
