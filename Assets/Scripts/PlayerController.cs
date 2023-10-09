using UnityEngine;

/// <summary>�v���C���[�𓮂����R���|�[�l���g</summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 10;
    [SerializeField] float _jumpPower = 10;
    /// <summary>�󒆂ł̕����]���̃X�s�[�h</summary>
    [SerializeField] float _turnSpeed = 3;
    [SerializeField] Animator _anim;
    Rigidbody _rb;
    Vector3 _moveDirection;
    Vector3 _posToMove;
    bool _isGround = true; // �ꉞ

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
            dir.y = 0;
            this.transform.forward = dir;
        }
    }

    /// <summary>pos�ɓ����������`�F�b�N</summary>
    void CheckMove()
    {
        Vector3 currentDir = _posToMove - this.transform.position;
        if (Vector3.Angle(_moveDirection, currentDir) > 90)
        {
            _moveDirection = Vector3.zero; // _posToMove��ʂ�߂�����~�߂�
        }
    }

    /// <summary>�N���b�N����pos���Z�b�g</summary>
    void SetMovePos(RaycastHit hit)
    {
        if (true) // AA�o����obj������
        {
            _posToMove = hit.point;
            _moveDirection = _posToMove - this.transform.position;
            _moveDirection.y = 0;
        }
    }

    /// <summary>player�̃A�j���[�V�����Đ�</summary>
    void PlayAnimation()
    {
        if (_anim) // �A�j���[�V�����̏���
        {
            _anim.SetBool("IsGrounded", _isGround);
            _anim.SetFloat("Speed", _rb.velocity.magnitude);
        }
    }

    private void OnEnable()
    {
        InputManager.SetEnterRaycastInput(InputType.RightClick, this.SetMovePos);
    }
}
