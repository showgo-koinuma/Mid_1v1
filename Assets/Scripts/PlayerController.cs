using JetBrains.Annotations;
using UnityEngine;

/// <summary>�v���C���[�𓮂����R���|�[�l���g</summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 10;
    //[SerializeField] float _jumpPower = 10;
    /// <summary>�󒆂ł̕����]���̃X�s�[�h</summary>
    //[SerializeField] float _turnSpeed = 3;
    [SerializeField] ChampionAnimationCntlr _champAnimContlr;
    Rigidbody _rb;
    Vector3 _moveDirection;
    Vector3 _posToMove;
    bool _isMoving = false;
    //bool _isGround = true; // �ꉞ
    public bool IsMoving { get => _isMoving; }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckMove();
        Move();
        PlayAnimation();
    }

    /// <summary>player�̈ړ��A���ʂ̒���</summary>
    void Move()
    {
        Vector3 dir = _moveDirection;
        _rb.velocity = dir.normalized * _moveSpeed;

        if (dir.magnitude != 0)
        {
            SetForward(dir);
        }
    }

    public void SetForward(Vector3 dir)
    {
        dir.y = 0;
        this.transform.forward = dir;
    }

    /// <summary>pos�ɓ����������`�F�b�N</summary>
    void CheckMove()
    {
        Vector3 currentDir = _posToMove - this.transform.position;
        if (Vector3.Angle(_moveDirection, currentDir) > 90) StopMove();
    }

    /// <summary>�N���b�N����pos���Z�b�g</summary>
    void SetMovePos(RaycastHit hit)
    {
        if (!hit.collider.TryGetComponent(out CharacterBase characterBase)) // AA�o����obj������
        {
            _posToMove = hit.point;
            _moveDirection = _posToMove - this.transform.position;
            _moveDirection.y = 0;
            _isMoving = true;
        }
    }
    
    public void DesignatTarget(CharacterBase target)
    {
        _posToMove = target.gameObject.transform.position;
        _moveDirection = _posToMove - this.transform.position;
        _moveDirection.y = 0;
        _isMoving = true;
    }

    /// <summary>�ړ����~�߂�</summary>
    public void StopMove()
    {
        _moveDirection = Vector3.zero;
        _isMoving = false;
    }

    /// <summary>player�̃A�j���[�V�����Đ�</summary>
    void PlayAnimation()
    {
        if (_champAnimContlr) // �A�j���[�V�����̏���
        {
            _champAnimContlr.SetIsMove(_isMoving);
        }
    }

    private void OnEnable() => InputManager.Instance.SetEnterRaycastInput(InputType.RightClick, this.SetMovePos);
}
