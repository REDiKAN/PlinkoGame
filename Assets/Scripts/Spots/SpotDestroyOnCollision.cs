using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class SpotDestroyOnCollision : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";

    public UnityEvent OnPlayerCollision;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == PLAYER_TAG)
        {
            OnPlayerCollision?.Invoke();
            Destroy(collision.gameObject);
        }
    }
}
