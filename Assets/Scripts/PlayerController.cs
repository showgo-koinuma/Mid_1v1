using UnityEngine;

/// <summary>プレイヤーを動かすコンポーネント</summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 10;
    [SerializeField] float _jumpPower = 10;
    /// <summary>空中での方向転換のスピード</summary>
    [SerializeField] float _turnSpeed = 3;
    [SerializeField] Animator _anim;
    Rigidbody _rb;
    Vector3 _moveDirection;
    Vector3 _posToMove;
    bool _isGround = true; // 一応

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

    /// <summary>playerの移動、正面の調整</summary>
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

    /// <summary>posに到着したかチェック</summary>
    void CheckMove()
    {
        Vector3 currentDir = _posToMove - this.transform.position;
        if (Vector3.Angle(_moveDirection, currentDir) > 90)
        {
            _moveDirection = Vector3.zero; // _posToMoveを通り過ぎたら止める
        }
    }

    /// <summary>クリックしたposをセット</summary>
    void SetMovePos(RaycastHit hit)
    {
        if (true) // AA出来るobjか判定
        {
            _posToMove = hit.point;
            _moveDirection = _posToMove - this.transform.position;
            _moveDirection.y = 0;
        }
    }

    /// <summary>playerのアニメーション再生</summary>
    void PlayAnimation()
    {
        if (_anim) // アニメーションの処理
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
