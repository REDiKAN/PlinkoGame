using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using TMPro;

public class WinSpot : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";

    public int RewardCoins = 0;

    public UnityEvent<int> OnPlayerCollision;

    [SerializeField] private TMP_Text moneyText;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == PLAYER_TAG) OnPlayerCollision?.Invoke(RewardCoins); Display—ollisionCount(5);
    }

    private void Display—ollisionCount(int _money)
    {
        DOTween.Sequence()
            .Append(moneyText.GetComponent<CanvasGroup>().DOFade(1f, 0.2f))
            .Append(moneyText.transform.DOMoveY(moneyText.transform.position.y + 5, 0.5f))
            .Append(moneyText.GetComponent<CanvasGroup>().DOFade(0f, 0.2f));
    }
}
