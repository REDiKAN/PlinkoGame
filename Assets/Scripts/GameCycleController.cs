using System;
using UnityEngine;

public class GameCycleController : MonoBehaviour
{

    public int Balls;
    public int Coins;

    public bool CurrentBallInGame = false;


    public event Action<int> OnPlayerRewardWithCoins;

    public void Init(int balls)
    {
        Balls = balls;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void UpdateGameState()
    {
        if (Balls == 0 && !CurrentBallInGame)
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
            int newCoins = Coins - coins;
            SetCoins(newCoins);
            return;
        }
        throw new NotImplementedException();
    }

    public void SetCoins(int coins)
    {
        if (CheckValidCoins(coins))
        {
            Coins = coins;
            return;
        }

        throw new Exception($"Value {nameof(coins)}: [{coins}] - is invalid.");
    }

    public bool TryAddCoins(int coins)
    {
        if (!CheckValidCoins(coins)) return false;

        int newCoins = Coins + coins;
        SetCoins(newCoins);

        return true;
    }

    public bool CheckBallsIsEmpty()
    {
        return Balls == 0;
    }

    public bool CheckPossibleSubtractFromCoins(int coins)
    {
        return Coins >= coins;
    }

    public bool CheckValidCoins(int coins)
    {
        if (coins < 0) return false;

        return true;
    }
}
