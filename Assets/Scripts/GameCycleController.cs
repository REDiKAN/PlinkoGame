using UnityEngine;

public class GameCycleController : MonoBehaviour
{
    public int Balls;
    public int Coins;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void OnReward(int reward)
    {
        Coins = reward;
    }
}
