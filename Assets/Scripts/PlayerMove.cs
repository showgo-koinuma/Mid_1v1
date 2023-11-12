using UnityEngine;
#if false
/// <summary>�v���C���[�𓮂����R���|�[�l���g</summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 10;
    [SerializeField] ChampionAnimationCntlr _champAnimContlr;
    [SerializeField] GameObject _clickEffectObj;
    ChampionManager _champManager;
    Rigidbody _rb;
    Vector3 _moveDirection;
    Vector3 _posToMove;
    bool _isMoving = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _champManager = GetComponent<ChampionManager>();
        InputManager.Instance.SetEnterRaycastInput(InputType.RightClick, this.SetMovePos);
        InputManager.Instance.SetEnterInput(InputType.S, StopMove);
    }

    void Update()
    {
        if (_champManager.ChampState != ChampionState.Idle && _champManager.ChampState != ChampionState.Moving) return;
        CheckMove();
        Move();
        PlayAnimation();
    }

    /// <summary>player�̈ړ��A���ʂ̒���</summary>
    void Move()
    {
        Vector3 dir = _moveDirection.normalized * _moveSpeed;
        dir.y = _rb.velocity.y;
        _rb.velocity = dir;

        if (dir.x != 0 || dir.z != 0)
        {
            SetForward(dir);
            _champManager.ChampState = ChampionState.Moving;
        }
        else _champManager.ChampState = ChampionState.Idle;
    }

    public void SetForward(Vector3 dir)
    {
        if (dir.magnitude == 0) return;
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
        if (!hit.collider.TryGetComponent(out CharacterManagerBase characterBase)) // AA�o����obj������
        {
            _posToMove = hit.point;
            _moveDirection = _posToMove - this.transform.position;
            _moveDirection.y = 0;
            _isMoving = true;
            Instantiate(_clickEffectObj).transform.position = hit.point + Vector3.up * 0.01f;
        }
    }
    
    /// <summary>�Ώێw��p �w�肵��obj�Ɍ������Ĉړ�</summary>
    /// <param name="target"></param>
    public void MoveToDesignatTarget(CharacterManagerBase target)
    {
        _posToMove = target.gameObject.transform.position;
        _moveDirection = _posToMove - this.transform.position;
        _moveDirection.y = 0;
        _isMoving = true;
    }

    /// <summary>�ړ����~�߂� State��Idle�ɂȂ�</summary>
    public void StopMove()
    {
        _champManager.ChampState = ChampionState.Idle;
        _moveDirection = Vector3.zero;
        _rb.velocity = Vector3.zero + Vector3.up * _rb.velocity.y;
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
}
#endif