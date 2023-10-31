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
            if (!_instance) //null�Ȃ�C���X�^���X������
            {
                var obj = new GameObject("PlayerInput");
                var input = obj.AddComponent<PlayerInput>();
                _instance = input;
                DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }
    /// <summary>�R�[���o�b�N�o�^�p</summary>
    GameInput _gameInput;
    /// <summary>�}�E�X�̉�ʏ�Pos</summary>
    Vector2 _mousePos;
    /// <summary>mouse���Q�[����ŏd�Ȃ��Ă������</summary>
    RaycastHit _hitBlue;
    /// <summary>mouse��hit���Ă�����̎擾</summary>
    public RaycastHit MouseHitBlue { get => _hitBlue; }
    int _blueLayerMask = 1 << 3 | 1 << 6 | 1 << 7 | 1 << 8 | 1 << 10 | 1 << 11 | 1 << 12;

    /// <summary>InputType�Ƃ���ɑΉ�����Action��Dictionary</summary>
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

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _blueLayerMask)) _hitBlue = hit; // blue��hit���
        // TODO:red�̂������
    }

    /// <summary>�������������s��</summary>
    void Initialization()
    {
        for (int i = 0; i < Enum.GetValues(typeof(InputType)).Length; i++)
        {
            _inputDic.Add((InputType)i, null); // ������
        }

        // �R�[���o�b�N��o�^���Ă��� TODO:���삪�������ꍇ���������K�v������
        _gameInput.InGame.MousePos.performed += OnMoveMouse;
        _gameInput.InGame.RightClick.started += OnRightClick;
        _gameInput.InGame.S.started += OnS;
        _gameInput.InGame.Q.started += OnQ;
    }

    #region �e�A�N�V�������Z�b�g
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

    /// <summary>�R�[���o�b�N�ɓo�^����Action���Z�b�g�o����</summary>
    /// <param name="inputType"></param><param name="action"></param>
    public void SetInput(InputType inputType, Action action)
    {
        _inputDic[inputType] += action;
    }

    /// <summary>update�Ŏ��s����action���Z�b�g</summary>
    /// <param name="action"></param>
    public void SetUpdateAction(Action action)
    {
        _updateAction += action;
    }
}

/// <summary>Input�̎��</summary>
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