using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ShootCannon : MonoBehaviour
{
    [SerializeField] private GameObject Bullet;
    [SerializeField] private GameObject Crosshair;
    private bool CanShoot = true;

    private void Start()
    {
        Observable.EveryGameObjectUpdate()
            .Where(_ => Input.GetKeyDown(KeyCode.P) && CanShoot)
            .Subscribe(_ => { 
                GameObject gmObject = Instantiate(Bullet);
                gmObject.transform.position = Crosshair.transform.position;
                Vector2 dir = (Crosshair.transform.position - transform.position).normalized;
                gmObject.GetComponent<Rigidbody2D>().AddForce(dir*10, ForceMode2D.Impulse);
                var rot = Mathf.Atan2(dir.y, dir.x);
                gmObject.transform.rotation = Quaternion.EulerRotation(new Vector3(0, 0, rot * Mathf.Deg2Rad));
                CanShoot = false;
                Observable.Timer(System.TimeSpan.FromSeconds(1)).Subscribe(_ =>
                {
                    CanShoot = true;
                });
            });
    }
}
