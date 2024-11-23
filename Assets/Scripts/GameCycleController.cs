using System;
using UnityEngine;

public class GameCycleController : MonoBehaviour
{
    public int Balls { get => _balls; set => SetBalls(value); }
    public int Coins { get => _coins; set => SetCoins(value); }

    private int _balls;
    private int _coins;

    public bool CurrentBallInGame = false;


    public event Action<int> OnPlayerRewardWithCoins;
    public event Action<int> OnPlayerLooseCoins;

    public event Action<int> OnBallsChenged;
    public event Action<int> OnCoinsChanged;

    public void Init(int balls)
    {
        _balls = balls;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
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
        if(Balls == _balls) return;

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
