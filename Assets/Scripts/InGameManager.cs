using System;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    static InGameManager _instance;
    public static InGameManager Instance { get => _instance; private set => _instance = value; }
    /// <summary>updateで実行するAction</summary>
    event Action _updateAction;

    [SerializeField, Tooltip("CSの勝利条件")] int _csFinishCount = 50;
    [SerializeField, Tooltip("Kill差数勝利条件")] int _killFinishCount = 3;
    //時間での終了条件

    // game時間
    float _gameTime = 0;
    public float GameTime { get => _gameTime; }
    // CS
    int _csBlue = 0;
    public int CsBlue { get => _csBlue; set { _csBlue = value; UpdateCS(); } }
    // Kill数
    int _killCountBlue = 0;
    public int KillCountBlue { set { _killCountBlue = value; if (_killCountBlue >= _killFinishCount) FinishGame(true); } }

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(gameObject);
    }

    /// <summary>csが更新されたとき</summary>
    void UpdateCS()
    {
        // テキスト更新

        // ゲーム終了判定
        if (_csBlue >= _csFinishCount) FinishGame(true);
        // redの判定
    }

    void FinishGame(bool winTeamIsBlue)
    {
        Debug.Log("finish game");
    }

    public void SetUpdateAction(Action action)
    {
        _updateAction += action;
    }

    private void Update()
    {
        _gameTime += Time.deltaTime;
        _updateAction?.Invoke();
    }
}
