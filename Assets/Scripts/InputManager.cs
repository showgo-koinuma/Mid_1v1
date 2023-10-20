using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    static InputManager _instance;
    InputSystem _inputSystem;
    public static InputManager Instance
    {
        get
        {
            //if (_instance == null)//nullならインスタンス化する
            //{
            //    var obj = new GameObject("PlayerInput");
            //    var input = obj.AddComponent<InputManager>();
            //    input.Initialization();
            //    _instance = input;
            //    DontDestroyOnLoad(obj);
            //}
            return _instance;
        }
    }
    /// <summary>カーソル重なったオブジェクトRaycastHit取得</summary>
    event Action<RaycastHit> _getCursorOnHit;
    /// <summary> 入力直後 </summary>
    private Dictionary<InputType, Action<RaycastHit>> _onEnterRaycastInputDic = new Dictionary<InputType, Action<RaycastHit>>();
    /// <summary> 入力直後 </summary>
    private Dictionary<InputType, Action> _onEnterInputDic = new Dictionary<InputType, Action>();
    /// <summary> 入力中 </summary>
    private Dictionary<InputType, Action<RaycastHit>> _onStayInputDic = new Dictionary<InputType, Action<RaycastHit>>();
    int _raycastLayerMask = 1 << 3 | 1 << 6 | 1 << 7 | 1 << 8 | 1 << 10 | 1 << 11 | 1 << 12;
    string[] _typeNameToIndex = new string[] { "RightClick", "Q", "W", "E", "R", "Space", "F", "D", "S", "ESC" };

    private void Awake()
    {
        _instance = this;
        _inputSystem = new InputSystem();
        InputInitialzie();
        Initialization();
    }

    /// <summary>inputの初期化</summary>
    private void InputInitialzie()
    {
        CharacterInputInitialzie();
    }

    /// <summary>デリゲートの初期化</summary>
    private void Initialization()
    {
        for (int i = 0; i < Enum.GetValues(typeof(InputType)).Length; i++)
        {
            _onEnterRaycastInputDic.Add((InputType)i, null);
            _onEnterInputDic.Add((InputType)i, null);
            _onStayInputDic.Add((InputType)i, null);
        }
    }

    /// <summary>キャラクター用inputの初期化</summary>
    void CharacterInputInitialzie()
    {
        _inputSystem.Player.RightClick.started += RightClick;
    }

    // inputからデリゲートを呼び出す者たち
    #region
    /// <summary>raycastしてデリゲートを呼び出す</summary>
    void RaycastInvoke(InputAction.CallbackContext context, string methodName)
    {
        var ray = Camera.main.ScreenPointToRay(context.ReadValue<Vector2>());

        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, Mathf.Infinity, _raycastLayerMask))
        {
            _onEnterRaycastInputDic[(InputType)Array.IndexOf(_typeNameToIndex, methodName)]?.Invoke(hit);
        }
    }
    public void RightClick(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started) RaycastInvoke(context, MethodBase.GetCurrentMethod().Name);
    }
    public void Q(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) RaycastInvoke(context, MethodBase.GetCurrentMethod().Name);
    }
    public void W(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) RaycastInvoke(context, MethodBase.GetCurrentMethod().Name);
    }
    public void E(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) RaycastInvoke(context, MethodBase.GetCurrentMethod().Name);
    }
    public void R(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) RaycastInvoke(context, MethodBase.GetCurrentMethod().Name);
    }
    public void F(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) RaycastInvoke(context, MethodBase.GetCurrentMethod().Name);
    }
    public void D(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) RaycastInvoke(context, MethodBase.GetCurrentMethod().Name);
    }
    public void InputS(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) _onEnterInputDic[InputType.S]?.Invoke();
    }
    public void InputESC(InputAction.CallbackContext context)
    {
        _onEnterInputDic[InputType.ESC]?.Invoke();
    }
    public void InputSpace(InputAction.CallbackContext context)
    {
        _onEnterInputDic[InputType.Space]?.Invoke();
    }
    public void InputMousePos(InputAction.CallbackContext context)
    {
        var ray = Camera.main.ScreenPointToRay(context.ReadValue<Vector2>());

        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, Mathf.Infinity, _raycastLayerMask))
        {
            _getCursorOnHit?.Invoke(hit);
        }
    }
    #endregion

    // inputのデリゲート管理たち
    #region
    public void SetEnterRaycastInput(InputType type, Action<RaycastHit> action)
    {
        _onEnterRaycastInputDic[type] += action;
    }
    public void SetEnterInput(InputType type, Action action)
    {
        _onEnterInputDic[type] += action;
    }
    public void SetStayInput(InputType type, Action<RaycastHit> action)
    {
        _onStayInputDic[type] += action;
    }
    public void SetGetCursorOnHit(Action<RaycastHit> action)
    {
        _getCursorOnHit += action;
    }
    #endregion

    private void OnDestroy()
    {
        ResetInput();
    }

    /// <summary>追加したデリゲートをすべて消す</summary>
    private void ResetInput()
    {
        for (int i = 0; i < Enum.GetValues(typeof(InputType)).Length; i++)
        {
            _instance._onEnterRaycastInputDic[(InputType)i] = null;
            _instance._onEnterInputDic[(InputType)i] = null;
            _instance._onStayInputDic[(InputType)i] = null;
        }
        _getCursorOnHit = null;
    }
}

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
