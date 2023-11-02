using UnityEngine;
using UnityEngine.AI;

public class PlayerMoveNavMesh : MonoBehaviour
{
    ChampionManager _champManager;
    ChampionAnimationCntlr _champAnimContlr;
    NavMeshAgent _agent;
    float _movementSpeed;
    float _turnSpeed = 10f;

    private void Awake()
    {
        _champManager = GetComponent<ChampionManager>();
        _champAnimContlr = GetComponentInChildren<ChampionAnimationCntlr>();
        _agent = GetComponent<NavMeshAgent>();
        _movementSpeed = _champManager.CharaParam.MS * 0.02f;
    }

    void Move()
    {
        if (_champManager.ChampState == ChampionState.channeling || _champManager.ChampState == ChampionState.airborne) return; // �����Ȃ�State��return

        Vector3 dir = _agent.steeringTarget - transform.position; // �ړI�n�̒��p�n�_�܂ł̌���
        dir.y = 0;
        _agent.velocity = dir.normalized * _movementSpeed;
        SetForward();
        _champAnimContlr.SetSpeed(_agent.velocity.magnitude); // animation�pspeed�Z�b�g

        if (_agent.velocity.magnitude != 0) _champManager.ChampState = ChampionState.Moving; // State�Z�b�g
        else _champManager.ChampState = ChampionState.Idle;
    }

    void SetForward()
    {
        if (_agent.velocity.magnitude == 0) return; // �����ĂȂ���Ζ���]
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_agent.velocity.normalized), Time.deltaTime * _turnSpeed); // �X���[�Y�ɉ�]
    }

    /// <summary>�ړI�n�̃Z�b�g</summary>
    void SetDestination()
    {
        _agent.destination = PlayerInput.Instance.MouseHitBlue.point; // �^�[�Q�b�g�̐ݒ�
    }
    /// <summary>�Ώێw��AStop�p�ړI�n�̃Z�b�g</summary>
    void SetDestination(Vector3 destination)
    {
        _agent.destination = destination; // �^�[�Q�b�g�̐ݒ�
        if (destination - this.transform.position != Vector3.zero) this.transform.rotation = Quaternion.LookRotation(destination - this.transform.position);
    }

    void StopMove()
    {
        _champManager.ChampState = ChampionState.Idle;
        SetDestination(this.transform.position);
    }

    private void OnEnable()
    {
        PlayerInput.Instance.SetUpdateAction(Move);
        PlayerInput.Instance.SetInput(InputType.RightClick, SetDestination);
        PlayerInput.Instance.SetInput(InputType.S, StopMove);
    }
}
