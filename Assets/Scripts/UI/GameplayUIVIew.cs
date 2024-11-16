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
    [SerializeField] private GameCycleController _modelContext;

    [SerializeField] private TextMeshProUGUI numberBalls_text;

    [SerializeField] private TextMeshProUGUI CoinsTMP;

    private CompositeDisposable _disposables = new();

    #region Reactive Property

    public ReactiveProperty<int> CoinsProperty;
    public ReactiveProperty<int> BallsProperty;

    #endregion

    #region Commands

    public ReactiveCommand<int> SetCoinsCommand = new ReactiveCommand<int>();
    public void OnSetCoinCommand(int newCoins)
    {
        CoinsProperty.Value = newCoins;
    }

    public ReactiveCommand<int> SetBallsCommand = new ReactiveCommand<int>();
    public void OnSetBallsCommand(int newBalls)
    {
        BallsProperty.Value = newBalls;
    }

    #endregion


    private void CommandsInitialize(CompositeDisposable disposables)
    {
        SetCoinsCommand.Subscribe(OnSetCoinCommand).AddTo(disposables);
    }

    private void ReactivPropertyInitialize(CompositeDisposable disposables)
    {
        CoinsProperty.Subscribe(OnCoinsChanged).AddTo(_disposables);
    }

    public void OnCoinsChanged(int newCoins)
    {
        StartCoroutine(TextCounterAnimation.CountText(int.Parse(CoinsTMP.text), newCoins, 60, 0.5f, CoinsTMP));
    }

    public void OnBallcsChangesd(int newBalls)
    {
        throw new NotImplementedException();
    }

    private void _modelContext_OnBallsChenged(int obj)
    {
        BallsProperty.Value = obj;
    }

    private void _modelContext_OnCoinsChanged(int obj)
    {
        CoinsProperty.Value = obj;
    }
    private void SubscribeOnModel()
    {
        _modelContext.OnBallsChenged += _modelContext_OnBallsChenged;
        _modelContext.OnCoinsChanged += _modelContext_OnCoinsChanged;
    }

    private void UnsubscriveOnModel()
    {
        _modelContext.OnBallsChenged -= _modelContext_OnBallsChenged;
        _modelContext.OnCoinsChanged -= _modelContext_OnCoinsChanged;
    }

    #region ButtonHandlers

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

    private void OnEnable()
    {
        SubscribeOnModel();
    }

    private void OnDisable()
    {
        UnsubscriveOnModel();
    }

    private void OnDestroy()
    {
        _disposables.Dispose();
    }

    #endregion
}
