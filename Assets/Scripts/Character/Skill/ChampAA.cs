using System.Collections;
using UnityEngine;

public class ChampAA : MonoBehaviour
{
    ChampionManager _champManager;
    CharacterParameter _charaParam;
    ChampionAnimationCntlr _animationCntlr;
    Coroutine _AACoroutine;

    private void Awake()
    {
        _champManager = GetComponent<ChampionManager>();
        _charaParam = _champManager.CharaParam;
        _animationCntlr = _champManager.ChampAnimContlr;
    }

    /// <summary>animatin event AAヒット効果</summary>
    public void AA()
    {
        _champManager.DealDamage((int)_charaParam.AD, DamageType.AD, _champManager.DesignatedObject);
    }

    IEnumerator AAing()
    {
        while (_champManager.ChampState == ChampionState.AAing)
        {
            _animationCntlr.StartAAAnimation();
            yield return new WaitForSeconds(1f);
        }
        _AACoroutine = null;
    }

    void StartAA()
    {
        _champManager.ChampState = ChampionState.AAing;
        if (_AACoroutine == null) _AACoroutine = StartCoroutine(AAing());
    }

    /// <summary>AAのターゲットをセットする</summary>
    /// <param name="hit"></param>
    void AATargetSet(RaycastHit hit)
    {
        if (hit.collider.gameObject.TryGetComponent(out CharacterBase characterBase)) // AA出来るobjか判定
        {
            // AA中でない、または違う敵をターゲットにしたら対象指定のコルーチン開始
            if (_AACoroutine == null || _champManager.DesignatedObject != characterBase)
            {
                _champManager.DesignatedObject = characterBase;
                StartCoroutine(_champManager.TargetDesignationCheck(_charaParam.Range, StartAA));
            }
        }
        else
        {
            _champManager.DesignatedObject = null;

            if (_AACoroutine != null)
            {
                StopCoroutine(_AACoroutine);
                _AACoroutine = null;
            }
        }
    }

    private void Start() => InputManager.Instance.SetEnterRaycastInput(InputType.RightClick, this.AATargetSet);
}
