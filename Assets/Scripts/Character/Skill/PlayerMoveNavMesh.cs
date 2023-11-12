using UnityEngine;
using UnityEngine.AI;

public class PlayerMoveNavMesh : MonoBehaviour
{
    ChampionMoveManager _moveManager;
    ChampionAnimationCntlr _champAnimContlr;
    NavMeshAgent _agent;
    Vector3 _destination;
    float _movementSpeed;
    float _turnSpeed = 10f;

    private void Awake()
    {
        ChampionManager champManager = GetComponent<ChampionManager>();
        _moveManager = GetComponent<ChampionMoveManager>();
        _champAnimContlr = champManager.ChampAnimContlr;
        _agent = GetComponent<NavMeshAgent>();
        _movementSpeed = champManager.CharaParam.MS * 0.02f;
    }

    void Move()
    {
        if (_moveManager.ChampState != ChampionState.Moving) return;

        _agent.SetDestination(_destination);
        Vector3 dir = _agent.steeringTarget - transform.position; // �ړI�n�̒��p�n�_�܂ł̌���
        dir.y = 0;
        _agent.velocity = dir.normalized * _movementSpeed;
        SetForward();
    }

    void SetForward()
    {
        if (_agent.velocity.magnitude == 0) return; // �����ĂȂ���Ζ���]
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_agent.velocity.normalized), Time.deltaTime * _turnSpeed); // �X���[�Y�ɉ�]
    }
    public void SetForward(Vector3 dir)
    {
        this.transform.rotation = Quaternion.LookRotation(dir);
    }

    /// <summary>�ړI�n�̃Z�b�g</summary>
    void SetDestination()
    {
        // hit�����Ώۂ�CharacterBase�łȂ��Ȃ�ړI�n�Ƃ���
        if (PlayerInput.Instance.MouseHitBlue.collider.TryGetComponent(out CharacterManagerBase characterBase)) return;
        _destination = PlayerInput.Instance.MouseHitBlue.point;
        _moveManager.ChampState = ChampionState.Moving;
    }
    /// <summary>�Ώێw��AStop�p�̖ړI�n�AForward�̃Z�b�g</summary>
    public void SetDestination(Vector3 destination)
    {
        Vector3 nearDistination = destination + (transform.position - destination).normalized * 1.2f;
        _destination = nearDistination;
        _moveManager.ChampState = ChampionState.Moving;
    }

    public void StopMove()
    {
        _moveManager.ChampState = ChampionState.Idle;
        _agent.velocity = Vector3.zero;
        _agent.ResetPath();
    }

    void SetAnimation()
    {
        _champAnimContlr.SetSpeed(_agent.velocity.magnitude); // animation�pspeed�Z�b�g
    }

    private void OnEnable()
    {
        PlayerInput.Instance.SetUpdateAction(Move);
        PlayerInput.Instance.SetUpdateAction(SetAnimation);
        PlayerInput.Instance.SetInput(InputType.RightClick, SetDestination);
        PlayerInput.Instance.SetInput(InputType.S, StopMove);
    }
}
