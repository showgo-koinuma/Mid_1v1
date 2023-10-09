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
            //if (_instance == null)//null�Ȃ�C���X�^���X������
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
    /// <summary> ���͒��� </summary>
    private Dictionary<InputType, Action<RaycastHit>> _onEnterRaycastInputDic = new Dictionary<InputType, Action<RaycastHit>>();
    /// <summary> ���͒��� </summary>
    private Dictionary<InputType, Action> _onEnterInputDic = new Dictionary<InputType, Action>();
    /// <summary> ���͒� </summary>
    private Dictionary<InputType, Action<RaycastHit>> _onStayInputDic = new Dictionary<InputType, Action<RaycastHit>>();
    int _raycastLayerMask = 1 << 3;
    string[] _typeNameToIndex = new string[] { "RightClick", "Q", "W", "E", "R", "Space", "F", "D", "S", "ESC" };

    private void Awake()
    {
        _instance = this;
        _inputSystem = new InputSystem();
        InputInitialzie();
        Initialization();
    }

    /// <summary>input�̏�����</summary>
    private void InputInitialzie()
    {
        CharacterInputInitialzie();
    }

    /// <summary>�f���Q�[�g�̏�����</summary>
    private void Initialization()
    {
        for (int i = 0; i < Enum.GetValues(typeof(InputType)).Length; i++)
        {
            _onEnterRaycastInputDic.Add((InputType)i, null);
            _onEnterInputDic.Add((InputType)i, null);
            _onStayInputDic.Add((InputType)i, null);
        }
    }

    /// <summary>�L�����N�^�[�pinput�̏�����</summary>
    void CharacterInputInitialzie()
    {
        _inputSystem.Player.RightClick.started += RightClick;
    }

    // input����f���Q�[�g���Ăяo���҂���
    #region
    /// <summary>raycast���ăf���Q�[�g���Ăяo��</summary>
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
    void Q(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) RaycastInvoke(context, MethodBase.GetCurrentMethod().Name);
    }
    void W(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) RaycastInvoke(context, MethodBase.GetCurrentMethod().Name);
    }
    void E(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) RaycastInvoke(context, MethodBase.GetCurrentMethod().Name);
    }
    void R(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) RaycastInvoke(context, MethodBase.GetCurrentMethod().Name);
    }
    void F(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) RaycastInvoke(context, MethodBase.GetCurrentMethod().Name);
    }
    void D(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) RaycastInvoke(context, MethodBase.GetCurrentMethod().Name);
    }
    void InputS(InputAction.CallbackContext context)
    {
        _onEnterInputDic[InputType.S]?.Invoke();
    }
    void InputESC(InputAction.CallbackContext context)
    {
        _onEnterInputDic[InputType.ESC]?.Invoke();
    }
    void InputSpace(InputAction.CallbackContext context)
    {
        _onEnterInputDic[InputType.Space]?.Invoke();
    }
    #endregion

    // input�̃f���Q�[�g�Ǘ�����
    #region
    public void SetEnterRaycastInput(InputType type, Action<RaycastHit> action)
    {
        _onEnterRaycastInputDic[type] += action;
    }
    public void LiftEnterRaycastInput(InputType type, Action<RaycastHit> action)
    {
        _onEnterRaycastInputDic[type] -= action;
    }
    public void SetEnterInput(InputType type, Action action)
    {
        _onEnterInputDic[type] += action;
    }
    public void LiftEnterInput(InputType type, Action action)
    {
        _onEnterInputDic[type] -= action;
    }
    public void SetStayInput(InputType type, Action<RaycastHit> action)
    {
        _onStayInputDic[type] += action;
    }
    public void LiftStayInput(InputType type, Action<RaycastHit> action)
    {
        _onStayInputDic[type] -= action;
    }
    #endregion

    private void OnDestroy()
    {
        ResetInput();
    }

    /// <summary>�ǉ������f���Q�[�g�����ׂď���</summary>
    private void ResetInput()
    {
        for (int i = 0; i < Enum.GetValues(typeof(InputType)).Length; i++)
        {
            _instance._onEnterRaycastInputDic[(InputType)i] = null;
            _instance._onEnterInputDic[(InputType)i] = null;
            _instance._onStayInputDic[(InputType)i] = null;
        }
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