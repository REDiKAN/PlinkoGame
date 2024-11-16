using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UniRx;
using Assets.Scripts.Utilits;
using System;

public class GameplayUIView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numberBalls_text;

    [SerializeField] private TextMeshProUGUI CoinsTMP;

    private CompositeDisposable _disposables = new();

    #region Reactive Property

    public ReactiveProperty<int> Coins;
    public ReactiveProperty<int> Balls;

    #endregion

    #region Commands

    public ReactiveCommand<int> SetCoinsCommand = new ReactiveCommand<int>();
    public void OnSetCoinCommand(int newCoins)
    {
        Coins.Value = newCoins;
    }

    public ReactiveCommand<int> SetBallsCommand = new ReactiveCommand<int>();
    public void OnSetBallsCommand(int newBalls)
    {
        Balls.Value = newBalls;
    }

    #endregion


    private void CommandsInitialize(CompositeDisposable disposables)
    {
        SetCoinsCommand.Subscribe(OnSetCoinCommand).AddTo(disposables);
    }

    private void ReactivPropertyInitialize(CompositeDisposable disposables)
    {
        Coins.Subscribe(OnCoinsChanged).AddTo(_disposables);
    }

    public void OnCoinsChanged(int newCoins)
    {
        StartCoroutine(TextCounterAnimation.CountText(int.Parse(CoinsTMP.text), newCoins, 60, 0.5f, CoinsTMP));
    }

    public void OnBallcsChangesd(int newBalls)
    {
        throw new NotImplementedException();
    }

    #region ButtonHandlers

    public void SetCoinsButton_OnClick()
    {
        SetCoinsCommand.Execute(0);
    }

    public void OpenMenuButton_OnClick()
    {
        throw new NotImplementedException();
    }

    // other buttons...

    #endregion

    #region UnityMethods
    private void Start()
    {
        CommandsInitialize(_disposables);
        ReactivPropertyInitialize(_disposables);
    }

    private void OnDestroy()
    {
        _disposables.Dispose();
    }

    #endregion
}
