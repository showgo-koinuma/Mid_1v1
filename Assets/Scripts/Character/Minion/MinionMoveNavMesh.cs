using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MinionMoveNavMesh : MonoBehaviour
{
    [SerializeField] Vector3 _start;
    [SerializeField] Vector3 _end;

    NavMeshAgent _agent;
    bool _moveToEnd = true;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Move());
    }

    private void Update()
    {
        Vector3 dir = _agent.steeringTarget - transform.position;
        dir.y = 0;
        _agent.velocity = dir.normalized * 5;
    }

    IEnumerator Move()
    {
        if (_moveToEnd) _agent.destination = _end;
        else _agent.destination = _start;
        yield return new WaitForSeconds(5);
        _moveToEnd = !_moveToEnd;
        StartCoroutine(Move());
    }
}
