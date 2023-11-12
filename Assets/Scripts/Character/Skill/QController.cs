using System;
using System.Collections;
using UnityEngine;

public class QController : MonoBehaviour
{
    [SerializeField] GameObject _Q3obj;
    PlayerMoveNavMesh _playerMove;
    ChampionManager _champManager;
    CharacterParameter _charaParam;
    ChampionMoveManager _moveManager;
    ChampionAnimationCntlr _animationCntlr;
    float _cd = 1;
    bool _isCD = false;
    int _stack = 0;
    Vector3 _hitPoint;
    /// <summary>アニメーション発生から当たり判定発生までのdelay</summary>
    float _hitOccurrenceDelay = 0.1f;
    /// <summary>Channeling時間</summary>
    float _channelingTimeQ = 0.3f;
    float _channelingTimeQ3 = 0.5f;
    int _layerMask = 1 << 10 | 1 << 11 | 1 << 12;

    private void Awake()
    {
        _playerMove = GetComponent<PlayerMoveNavMesh>();
        _champManager = GetComponent<ChampionManager>();
        _charaParam = _champManager.CharaParam;
        _moveManager = GetComponent<ChampionMoveManager>();
        _animationCntlr = _champManager.ChampAnimContlr;
    }
    
    void Start() => PlayerInput.Instance.SetInput(InputType.Q, OnQ);

    void OnQ()
    {
        if (!_isCD)
        {
            _hitPoint = PlayerInput.Instance.MouseHitBlue.point;
            if (_stack < 2) StartCoroutine(Qstart());
            else StartCoroutine(Q3start());
            _isCD = true;
        }
    }

    IEnumerator Qstart()
    {
        ChampionState currentState = _moveManager.ChampState;
        _playerMove.StopMove();
        _moveManager.ChampState = ChampionState.channeling;
        _playerMove.SetForward(_hitPoint - this.transform.position);
        _animationCntlr.StartQAnimation();
        Invoke(nameof(QOccurrenceJudg), _hitOccurrenceDelay);
        yield return new WaitForSeconds(_channelingTimeQ);
        _moveManager.FinishChanneling(currentState);
        yield return new WaitForSeconds(_cd - _channelingTimeQ); // TODO:UI反映出来なさそう
        _isCD = false;
    }

    IEnumerator Q3start()
    {
        ChampionState currentState = _moveManager.ChampState;
        _playerMove.StopMove();
        _moveManager.ChampState = ChampionState.channeling;
        _playerMove.SetForward(_hitPoint - this.transform.position);
        _animationCntlr.StartQ3Animation();
        Invoke(nameof(Q3OccurrenceJudg), _hitOccurrenceDelay);
        _stack = 0;
        yield return new WaitForSeconds(_channelingTimeQ3);
        _moveManager.FinishChanneling(currentState);
        yield return new WaitForSeconds(_cd - _channelingTimeQ3); // TODO:UI反映出来なさそう
        _isCD = false;
    }

    /// <summary>当たり判定とhit効果</summary>
    void QOccurrenceJudg() // 9.5
    {
        var hitObjects = Physics.OverlapCapsule(this.transform.position, this.transform.position + this.transform.forward * 9, 0.5f,  _layerMask, QueryTriggerInteraction.Collide);
        int damage = 20 + (int)(_charaParam.AD * 1.05f);
        bool isHit = false;

        foreach (var hitObject in hitObjects)
        {
            if (hitObject.TryGetComponent(out CharacterManagerBase characterBase))
            {
                _champManager.DealDamage(damage, DamageType.AD, characterBase);
                isHit = true;
            }
        }

        if (isHit && _stack < 2) _stack++;
    }

    /// <summary>Q3を飛ばす</summary>
    void Q3OccurrenceJudg()
    {
        GameObject q3obj = Instantiate(_Q3obj, this.transform.position, Quaternion.identity);
        q3obj.GetComponent<TornadoCntlr>().Initialization(_hitPoint, 20 + (int)(_charaParam.AD * 1.05f));
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        DrawCapsuleGizmo(this.transform.position, this.transform.position + this.transform.forward * 9, 0.5f);
    }

    /// <summary>Q当たり判定カプセルのGizmoを表示</summary>
    /// <param name="start"></param><param name="end"></param>param name="radius"></param>
    public void DrawCapsuleGizmo(Vector3 start, Vector3 end, float radius)
    {
        var preMatrix = Gizmos.matrix;

        Gizmos.matrix = Matrix4x4.TRS(start, Quaternion.FromToRotation(Vector3.forward, end), Vector3.one);

        var distance = (end - start).magnitude;
        var capsuleStart = Vector3.zero;
        var capsuleEnd = Vector3.forward * distance;
        Gizmos.DrawWireSphere(capsuleStart, radius);
        Gizmos.DrawWireSphere(capsuleEnd, radius);

        var offsets = new Vector3[] { new Vector3(-1.0f, 0.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f), new Vector3(1.0f, 0.0f, 0.0f), new Vector3(0.0f, -1.0f, 0.0f) };
        for (int i = 0; i < offsets.Length; i++)
        {
            Gizmos.DrawLine(capsuleStart + offsets[i] * radius, capsuleEnd + offsets[i] * radius);
        }

        Gizmos.matrix = preMatrix;
    }
}
