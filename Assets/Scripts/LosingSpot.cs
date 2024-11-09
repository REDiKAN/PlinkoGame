using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LosingSpot: MonoBehaviour
{
    private const string PLAYER_TAG = "Player";

    public int Coast;

    public UnityEvent<int> OnPlayerCollision;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == PLAYER_TAG) OnPlayerCollision.Invoke(Coast);
    }
}
