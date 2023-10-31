using UnityEngine;
using UnityEngine.AI;

public class PlayerMoveNavMesh : MonoBehaviour
{
    ChampionManager _champManager;
    float _movementSpeed;
    Vector3 _posToMove;
    NavMeshAgent _agent;

    private void Awake()
    {
        _champManager = GetComponent<ChampionManager>();
        _movementSpeed = _champManager.CharaParam.MS;
        _agent = GetComponent<NavMeshAgent>();
    }

    void Move()
    {
        Vector3 dir = _agent.steeringTarget - transform.position;
        dir.y = 0;
        _agent.velocity = dir.normalized * 10;
    }

    void SetMovePos()
    {
        _posToMove = PlayerInput.Instance.MouseHitBlue.point;
        _agent.destination = _posToMove; // ターゲットの設定
        Debug.Log(_posToMove);
    }

    private void OnEnable()
    {
        PlayerInput.Instance.SetUpdateAction(Move);
        PlayerInput.Instance.SetInput(InputType.RightClick, SetMovePos);
    }
}
