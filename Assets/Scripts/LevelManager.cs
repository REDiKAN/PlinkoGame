using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get => _instance; }

    private static LevelManager _instance;

    private int[] _levelsToSceneId;
    /// <summary>
    /// �������� Id ���� ���� �������
    /// </summary>
    public IEnumerable<int> LevelsToSceneId => _levelsToSceneId;

    private bool[] _unlockLevels;
    /// <summary>
    /// �������� ��� ����� � ���� �������� � �� �������� �������, ��� true �������� ������� ������.
    /// </summary>
    public IEnumerable<bool> UnlockLevels => _unlockLevels;

    private int _currentLevelId;
    /// <summary>
    /// Id �������� ������
    /// </summary>
    public int CurrentLevelId => _currentLevelId;

    private GameCycleController _cycleController;

    private CompositeDisposable _disposables;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _cycleController.OnLose.Subscribe(LooseHandler).AddTo(_disposables);
        _cycleController.OnWin.Subscribe(WinHandler).AddTo(_disposables);
    }

    /// <summary>
    /// ���������� 1 ���� ����� ������.
    /// </summary>
    public bool LevelIsUnlock(int levelId)
    {
        return _unlockLevels[levelId];
    }

    public int NextLevelId()
    {
        int newLevel = _currentLevelId++;
        if(LevelsToSceneId.Count() < newLevel)
        {
            return newLevel;
        }

        throw new System.Exception("����� �� ������� �������");
    }

    public void LoadUnlockedLevel(int nextLevelId)
    {
        if (LevelIsUnlock(nextLevelId))
        {
            SceneManager.LoadScene(nextLevelId);
        }
    }

    private void LooseHandler(int levelId)
    {
    }

    private void WinHandler(int levelId)
    {
        int nextId = NextLevelId();
        _unlockLevels[nextId] = true;
    }

    private void OnDestroy()
    {
        _disposables.Dispose();
    }
}
