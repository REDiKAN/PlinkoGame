using System;
using UnityEngine;

public class GameCycleController : MonoBehaviour
{
    public int Balls;
    public int Coins;

    public void Init(int balls)
    {
        Balls = balls;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void OnReward(int reward)
    {
        Coins += reward;
    }

    public void OnLoose()
    {
        throw new NotImplementedException();
    }
}
