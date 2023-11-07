using UnityEngine;
using UnityEngine.AI;

public class PlayerMoveNavMesh : MonoBehaviour
{
    ChampionManager _champManager;
    ChampionAnimationCntlr _champAnimContlr;
    NavMeshAgent _agent;
    Vector3 _destination;
    float _movementSpeed;
    float _turnSpeed = 10f;

    private void Awake()
    {
        _champManager = GetComponent<ChampionManager>();
        _champAnimContlr = _champManager.ChampAnimContlr;
        _agent = GetComponent<NavMeshAgent>();
        _movementSpeed = _champManager.CharaParam.MS * 0.02f;
    }

    void Move()
    {
        if (_champManager.ChampState == ChampionState.channeling || _champManager.ChampState == ChampionState.airborne) return; // 動けないStateはreturn
        if (_destination == transform.position)
        {
            Debug.Log("onajibasho"); 
            return; // 目的地と同じ場所にいたらreturn
        }

        _agent.destination = _destination;
        Vector3 dir = _agent.steeringTarget - transform.position; // 目的地の中継地点までの向き
        dir.y = 0;
        _agent.velocity = dir.normalized * _movementSpeed;
        _champAnimContlr.SetSpeed(_agent.velocity.magnitude); // animation用speedセット

        if (_agent.velocity.magnitude != 0) _champManager.ChampState = ChampionState.Moving; // Stateセット
        else _champManager.ChampState = ChampionState.Idle;
        SetForward();
    }

    void SetForward()
    {
        if (_agent.velocity.magnitude == 0) return; // 動いてなければ無回転
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_agent.velocity.normalized), Time.deltaTime * _turnSpeed); // スムーズに回転
    }
    public void SetForward(Vector3 dir)
    {
        this.transform.rotation = Quaternion.LookRotation(dir);
    }

    /// <summary>目的地のセット</summary>
    void SetDestination()
    {
        // hitした対象がCharacterBaseでないなら目的地とする
        if (!PlayerInput.Instance.MouseHitBlue.collider.TryGetComponent(out CharacterBase characterBase)) _destination = PlayerInput.Instance.MouseHitBlue.point;
    }
    /// <summary>対象指定、Stop用の目的地、Forwardのセット</summary>
    public void SetDestination(Vector3 destination)
    {
        Vector3 nearDistination = destination + (transform.position - destination).normalized * 1.2f;
        _destination = nearDistination;
    }

    public void StopMove()
    {
        _champManager.ChampState = ChampionState.Idle;
        _destination = this.transform.position;
    }

    private void OnEnable()
    {
        PlayerInput.Instance.SetUpdateAction(Move);
        PlayerInput.Instance.SetInput(InputType.RightClick, SetDestination);
        PlayerInput.Instance.SetInput(InputType.S, StopMove);
    }
}
