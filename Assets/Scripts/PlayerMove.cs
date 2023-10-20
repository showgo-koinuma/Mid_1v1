using UnityEngine;

/// <summary>プレイヤーを動かすコンポーネント</summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 10;
    [SerializeField] ChampionAnimationCntlr _champAnimContlr;
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

    /// <summary>playerの移動、正面の調整</summary>
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

    /// <summary>posに到着したかチェック</summary>
    void CheckMove()
    {
        Vector3 currentDir = _posToMove - this.transform.position;
        if (Vector3.Angle(_moveDirection, currentDir) > 90) StopMove();
    }

    /// <summary>クリックしたposをセット</summary>
    void SetMovePos(RaycastHit hit)
    {
        if (!hit.collider.TryGetComponent(out CharacterBase characterBase)) // AA出来るobjか判定
        {
            _posToMove = hit.point;
            _moveDirection = _posToMove - this.transform.position;
            _moveDirection.y = 0;
            _isMoving = true;
        }
    }
    
    /// <summary>対象指定用 指定したobjに向かって移動</summary>
    /// <param name="target"></param>
    public void MoveToDesignatTarget(CharacterBase target)
    {
        _posToMove = target.gameObject.transform.position;
        _moveDirection = _posToMove - this.transform.position;
        _moveDirection.y = 0;
        _isMoving = true;
    }

    /// <summary>移動を止める StateがIdleになる</summary>
    public void StopMove()
    {
        _champManager.ChampState = ChampionState.Idle;
        _moveDirection = Vector3.zero;
        _rb.velocity = Vector3.zero + Vector3.up * _rb.velocity.y;
        _isMoving = false;
    }

    /// <summary>playerのアニメーション再生</summary>
    void PlayAnimation()
    {
        if (_champAnimContlr) // アニメーションの処理
        {
            _champAnimContlr.SetIsMove(_isMoving);
        }
    }
}
