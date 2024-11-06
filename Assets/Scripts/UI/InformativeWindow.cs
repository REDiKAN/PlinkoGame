using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class InformativeWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text numberBalls_text;
    [SerializeField] public int maxCountBalls;

    [SerializeField] private Image barBalls;

    /// <summary>
    /// Changes the number of balls
    /// </summary>
    /// <param name="_value">The new meaning of the balls</param>
    public void NumberBalls(int _value)
    {
        numberBalls_text.text = "Number of balls: " + _value;
        DOTween.Sequence(barBalls.DOFillAmount((float)_value / 100, 0.2f));
    }
    public void NumberMaxBalls(int _value) => maxCountBalls = _value;
}
