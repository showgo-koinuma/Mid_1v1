using System.Collections;
using UnityEngine;

public class QController : MonoBehaviour
{
    PlayerMove _playerMove;
    ChampionManager _champManager;
    CharacterParameter _charaParam;
    ChampionAnimationCntlr _animationCntlr;
    float _cd = 4;
    bool _isCD = false;
    /// <summary>アニメーション発生から当たり判定発生までのdelay</summary>
    float _hitOccurrenceDelay = 0.1f;
    /// <summary>Channeling時間</summary>
    float _channelingTime = 0.5f;

    private void Awake()
    {
        _playerMove = GetComponent<PlayerMove>();
        _champManager = GetComponent<ChampionManager>();
        _charaParam = _champManager.CharaParam;
        _animationCntlr = _champManager.ChampAnimContlr;
    }
    
    void Start() => InputManager.Instance.SetEnterRaycastInput(InputType.Q, this.OnQ);

    void OnQ(RaycastHit hit)
    {
        if (!_isCD)
        {
             StartCoroutine(Qstart(hit));
            _isCD = true;
        }
    }

    IEnumerator Qstart(RaycastHit hit)
    {
        _champManager.ChampState = ChampionState.channeling;
        _playerMove.StopMove();
        _playerMove.SetForward(hit.point - this.transform.position);
        _animationCntlr.StartQAnimation();
        Invoke(nameof(QOccurrenceJudg), _hitOccurrenceDelay);
        yield return new WaitForSeconds(_channelingTime);
        if (_champManager.ChampState != ChampionState.dead) _champManager.ChampState = ChampionState.Idle;
        yield return new WaitForSeconds(_cd - _channelingTime); // TODO:UI反映出来なさそう
        _isCD = false;
    }

    /// <summary>当たり判定とhit効果</summary>
    void QOccurrenceJudg() // 9.5
    {
        var hitObjects = Physics.OverlapCapsule(this.transform.position, this.transform.position + this.transform.forward * 9, 0.5f, 1 << 7);
        int damage = 20 + (int)(_charaParam.AD * 1.05f);

        foreach (var hitObject in hitObjects)
        {
            if (hitObject.TryGetComponent(out CharacterBase characterBase))
            {
                _champManager.DealDamage(damage, DamageType.AD, characterBase);
            }
        }
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
