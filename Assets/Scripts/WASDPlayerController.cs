using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>プレイヤーを動かすコンポーネント</summary>
[RequireComponent(typeof(Rigidbody))]
public class WASDPlayerController : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 10;
    [SerializeField] float _jumpPower = 10;
    /// <summary>空中での方向転換のスピード</summary>
    [SerializeField] float _turnSpeed = 3;
    [SerializeField] Animator _anim;
    Rigidbody _rb;
    Vector3 _moveDirection;
    Vector3 _clickPoint;
    bool _isGround;
    Vector3 _planeNormalVector;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ClickMove();
        Vector3 dir = _moveDirection;
        dir = Camera.main.transform.TransformDirection(dir); //カメラ基準のベクトルに直す
        dir.y = 0;
        dir = dir.normalized; // 単位化してある水平方向の入力ベクトル
        Vector3 velo = dir * _moveSpeed;
        //velo.y = _rb.velocity.y;
        if (!_isGround) // 空中でゆっくり方向転換が可能
        {
            velo = _rb.velocity;
            if (dir.magnitude != 0f)
            {
                // 速度の大きさを保持しながら向きを少しずつ変える
                Vector2 startHoriVelo = new Vector2(_rb.velocity.x, _rb.velocity.z);
                float horiMag = startHoriVelo.magnitude;
                if (horiMag < 10f)
                {
                    horiMag = 10;
                }
                Vector2 endHoriVelo = new Vector2(dir.x * horiMag, dir.z * horiMag);
                float turnSpeed = _turnSpeed * Time.deltaTime;
                Vector2 airHoriVelo = endHoriVelo * turnSpeed + startHoriVelo * (1 - turnSpeed);
                velo = new Vector3(airHoriVelo.x, _rb.velocity.y, airHoriVelo.y);
            }
            _rb.velocity = velo;
        }
        else // 接地中処理
        {
            // 接している面に沿ったベクトルに変える
            Vector3 onPlaneVelo = Vector3.ProjectOnPlane(velo, _planeNormalVector);
            if (Input.GetButton("Jump"))
            {
                onPlaneVelo.y = _jumpPower;
            }

            _rb.velocity = onPlaneVelo; // 接地中はvelocityを書き換える
        }

        if (dir.magnitude != 0)
        {
            velo.y = 0;
            this.transform.forward = velo;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector3 dir = context.ReadValue<Vector2>(); // 移動方向
        _moveDirection = new Vector3(dir.x, 0, dir.y).normalized;
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) Debug.Log("fire");
    }

    public void PointSet(Vector3 clickWorldPoint)
    {
        _clickPoint = clickWorldPoint;
    }

    void ClickMove()
    {
        Vector3 dir = _clickPoint - this.transform.position;
        dir.y = 0;
        if (dir.magnitude > 0.1f) _moveDirection = dir.normalized;
        else _moveDirection = Vector3.zero;
    }

    void LateUpdate()
    {
        // アニメーションの処理
        if (_anim)
        {
            _anim.SetBool("IsGrounded", _isGround);
            Vector3 walkSpeed = _rb.velocity;
            walkSpeed.y = 0;
            _anim.SetFloat("Speed", walkSpeed.magnitude);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // 接している面の法線ベクトルとworld上の上とのなす角が45°未満なら地面とみなす
        if (Vector3.Angle(Vector3.up, collision.contacts[0].normal) < 45)
        {
            _planeNormalVector = collision.contacts[0].normal;
            _isGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        _isGround = false;
    }
}
