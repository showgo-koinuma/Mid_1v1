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
        Vector3 dir = _agent.steeringTarget - transform.position;
        dir.y = 0;
        _agent.velocity = dir.normalized * _movementSpeed;
        SetForward();
        _champAnimContlr.SetSpeed(_agent.velocity.magnitude);
    }

    void SetForward()
    {
        if (_agent.velocity.magnitude == 0) return;
        Quaternion targetRotation = Quaternion.LookRotation(_agent.velocity.normalized);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * _turnSpeed);  // Slerp を使うのがポイント
    }

    /// <summary>目的地のセット</summary>
    void SetDestination()
    {
        _agent.destination = PlayerInput.Instance.MouseHitBlue.point; // ターゲットの設定
        _champManager.ChampState = ChampionState.Moving;
    }
    /// <summary>対象指定、Stop用目的地のセット</summary>
    void SetDestination(Vector3 destination)
    {
        _agent.destination = destination; // ターゲットの設定
        _champManager.ChampState = ChampionState.Moving;
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
