using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>�v���C���[�𓮂����R���|�[�l���g</summary>
[RequireComponent(typeof(Rigidbody))]
public class WASDPlayerController : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 10;
    [SerializeField] float _jumpPower = 10;
    /// <summary>�󒆂ł̕����]���̃X�s�[�h</summary>
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
        dir = Camera.main.transform.TransformDirection(dir); //�J������̃x�N�g���ɒ���
        dir.y = 0;
        dir = dir.normalized; // �P�ʉ����Ă��鐅�������̓��̓x�N�g��
        Vector3 velo = dir * _moveSpeed;
        //velo.y = _rb.velocity.y;
        if (!_isGround) // �󒆂ł����������]�����\
        {
            velo = _rb.velocity;
            if (dir.magnitude != 0f)
            {
                // ���x�̑傫����ێ����Ȃ���������������ς���
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
        else // �ڒn������
        {
            // �ڂ��Ă���ʂɉ������x�N�g���ɕς���
            Vector3 onPlaneVelo = Vector3.ProjectOnPlane(velo, _planeNormalVector);
            if (Input.GetButton("Jump"))
            {
                onPlaneVelo.y = _jumpPower;
            }

            _rb.velocity = onPlaneVelo; // �ڒn����velocity������������
        }

        if (dir.magnitude != 0)
        {
            velo.y = 0;
            this.transform.forward = velo;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector3 dir = context.ReadValue<Vector2>(); // �ړ�����
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
        // �A�j���[�V�����̏���
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
        // �ڂ��Ă���ʂ̖@���x�N�g����world��̏�Ƃ̂Ȃ��p��45�������Ȃ�n�ʂƂ݂Ȃ�
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
