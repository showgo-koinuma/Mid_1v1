using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    static PlayerInput _instance = default;
    public static PlayerInput Instance
    {
        get
        {
            if (!_instance) //nullならインスタンス化する
            {
                var obj = new GameObject("PlayerInput");
                var input = obj.AddComponent<PlayerInput>();
                _instance = input;
                DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }
    /// <summary>コールバック登録用</summary>
    GameInput _gameInput;
    /// <summary>マウスの画面上Pos</summary>
    Vector2 _mousePos;
    /// <summary>mouseがゲーム上で重なっているもの</summary>
    RaycastHit _hitBlue;
    /// <summary>mouseがhitしているもの取得</summary>
    public RaycastHit MouseHitBlue { get => _hitBlue; }
    int _blueLayerMask = 1 << 3 | 1 << 6 | 1 << 7 | 1 << 8 | 1 << 10 | 1 << 11 | 1 << 12;

    /// <summary>InputTypeとそれに対応するActionのDictionary</summary>
    Dictionary<InputType, Action> _inputDic = new Dictionary<InputType, Action>();
    event Action _updateAction;

    private void Awake()
    {
        _gameInput = new GameInput();
        _gameInput.Enable();
        Initialization();
    }

    private void Update()
    {
        _updateAction.Invoke();
        var ray = Camera.main.ScreenPointToRay(_mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _blueLayerMask)) _hitBlue = hit; // blueのhit情報
        // TODO:redのやつ書くか
    }

    /// <summary>初期化処理を行う</summary>
    void Initialization()
    {
        for (int i = 0; i < Enum.GetValues(typeof(InputType)).Length; i++)
        {
            _inputDic.Add((InputType)i, null); // 初期化
        }

        // コールバックを登録していく TODO:操作が増えた場合書き足す必要がある
        _gameInput.InGame.MousePos.performed += OnMoveMouse;
        _gameInput.InGame.RightClick.started += OnRightClick;
        _gameInput.InGame.S.started += OnS;
        _gameInput.InGame.Q.started += OnQ;
    }

    #region 各アクションをセット
    void OnMoveMouse(InputAction.CallbackContext context)
    {
        _mousePos = context.ReadValue<Vector2>();
        Debug.Log("koko");
    }
    void OnRightClick(InputAction.CallbackContext context)
    {
        _inputDic[InputType.RightClick]?.Invoke();
    }
    void OnQ(InputAction.CallbackContext context)
    {
        _inputDic[InputType.Q]?.Invoke();
    }
    void OnS(InputAction.CallbackContext context)
    {
        _inputDic[InputType.S]?.Invoke();
    }
    #endregion

    /// <summary>コールバックに登録するActionをセット出来る</summary>
    /// <param name="inputType"></param><param name="action"></param>
    public void SetInput(InputType inputType, Action action)
    {
        _inputDic[inputType] += action;
    }

    /// <summary>updateで実行するactionをセット</summary>
    /// <param name="action"></param>
    public void SetUpdateAction(Action action)
    {
        _updateAction += action;
    }
}

/// <summary>Inputの種類</summary>
public enum InputType
{
    RightClick,
    Q,
    W,
    E,
    R,
    Space,
    F,
    D,
    S,
    ESC
}