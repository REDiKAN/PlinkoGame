using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class MoneyInterfase : MonoBehaviour
{
    public TextMeshProUGUI text;
    public int countFPS = 30;
    public float duration = 1f;
    public string numberFormat = "N0";
    private int _valueCoins;
    public int ValueCoins
    {
        get { return _valueCoins; }
        set { UpdateText(value); _valueCoins = value;}
    }
    private Coroutine CountingCoroutine;

    private void UpdateText(int newValue)
    {
        if (CountingCoroutine != null) { StopCoroutine(CountingCoroutine); }

        UpdateScale();

        CountingCoroutine = StartCoroutine(CountText(newValue));
    }

    private void UpdateScale()
    {
        DOTween.Sequence()
            .Append(text.gameObject.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.3f))
            .Append(text.gameObject.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f));

    }

    private IEnumerator CountText(int newValue)
    {
        WaitForSeconds Wait = new WaitForSeconds(1f / countFPS);
        int previousValue = _valueCoins;
        int stepAmount;

        if (newValue - previousValue < 0)
            stepAmount = Mathf.FloorToInt((newValue - previousValue) / (countFPS * duration));
        else
            stepAmount = Mathf.CeilToInt((newValue - previousValue) / (countFPS * duration));

        if (previousValue < newValue)
        {
            while (previousValue < newValue)
            {
                previousValue += stepAmount;
                if (previousValue > newValue)
                    previousValue = newValue;

                text.SetText(previousValue.ToString(numberFormat));

                yield return Wait;
            }
        }
        else
        {
            while (previousValue > newValue)
            {
                previousValue += stepAmount;
                if (previousValue < newValue)
                    previousValue = newValue;

                text.SetText(previousValue.ToString(numberFormat));

                yield return Wait;
            }
        }
    }
}
