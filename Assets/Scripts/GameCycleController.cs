using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class GameCycleController : MonoBehaviour
{
    [SerializeField] private int _numberLevel = 1;
    [SerializeField] private GameObject LevelCompleteUI;
    [SerializeField] private Button _btReloadLevel;
    [SerializeField] private Button _btToMenu;
    [SerializeField] private Button _btNextLevel;

                     private CompositeDisposable buttonDisposable = new CompositeDisposable();

    public int Balls { get => _balls; set => SetBalls(value); }
    public int Coins { get => _coins; set => SetCoins(value); }
    

    private int _balls = 15;
    private int _coins = 0;
    private int _needCountCoin = 1000;

    public bool CurrentBallInGame = false;


    public event Action<int> OnPlayerRewardWithCoins;
    public event Action<int> OnPlayerLooseCoins;

    public event Action<int> OnBallsChenged;
    public event Action<int> OnCoinsChanged;

    public ReactiveCommand<int> OnWin = new ReactiveCommand<int>();
    public ReactiveCommand<int> OnLose = new ReactiveCommand<int>();

    public void Init(int balls)
    {
        _balls = balls;
    }

    public void LostLastBall() {
        LevelCompleteUI.SetActive(true);
        _btNextLevel.OnClickAsObservable().Subscribe((_) => { });
        _btReloadLevel.OnClickAsObservable().Subscribe((_) => { });
        _btToMenu.OnClickAsObservable().Subscribe((_) => { });
        if (_coins >= _needCountCoin) OnWin.Execute(_numberLevel);
        else OnLose.Execute(_numberLevel);

    }


    public void UpdateGameState()
    {
        if (_balls == 0 && !CurrentBallInGame)
        {
            OnLooseHandler();
            return;
        }
        throw new NotImplementedException();
    }

    public void OnLooseHandler()
    {
        Debug.Log("Player looser.");
        throw new NotImplementedException();
    }

    public void OnWinHadler()
    {
        Debug.Log("Player Won.");
        throw new NotImplementedException();
    }

    public void RewardPlayeWithCoins(int coins)
    {
        if (TryAddCoins(coins))
        {
            OnPlayerRewardWithCoins?.Invoke(coins);
        }
    }

    public void TakeCoinsFromPlayer(int coins)
    {
        if (CheckPossibleSubtractFromCoins(coins))
        {
            int newCoins = _coins - coins;
            SetCoins(newCoins);
            return;
        }
        throw new NotImplementedException();
    }

    public void SetCoins(int coins)
    {
        if (coins == _coins) return;

        if (CheckValidCoins(coins))
        {
            _coins = coins;
            OnCoinsChanged?.Invoke(coins);
            return;
        }

        throw new Exception($"Value {nameof(coins)}: [{coins}] - is invalid.");
    }

    public bool TryAddCoins(int coins)
    {
        if (!CheckValidCoins(coins)) return false;

        int newCoins = _coins + coins;
        SetCoins(newCoins);

        return true;
    }

    public bool CheckPossibleSubtractFromCoins(int coins)
    {
        return _coins >= coins;
    }

    public bool CheckValidCoins(int coins)
    {
        if (coins < 0) return false;

        return true;
    }

    private void SetBalls(int newBalls)
    {
        if(Balls == newBalls) return;

        if (newBalls >= 0)
        {
            _balls = newBalls;
            OnBallsChenged?.Invoke(newBalls);
            return;
        }
    }

    public bool CheckBallsIsEmpty()
    {
        return _balls == 0;
    }
}
