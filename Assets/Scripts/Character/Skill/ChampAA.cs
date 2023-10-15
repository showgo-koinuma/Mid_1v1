using System.Collections;
using UnityEngine;

public class ChampAA : MonoBehaviour
{
    ChampionManager _champManager;
    CharacterParameter _charaParam;
    ChampionAnimationCntlr _animationCntlr;
    bool _AAing;

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
        _AAing = true;
        _animationCntlr.StartAAAnimation();
        yield return new WaitForSeconds(1);
        if (_AAing) StartCoroutine(AAing());
    }

    void StartAA()
    {
        if (!_AAing) StartCoroutine(AAing());
    }

    /// <summary>AAのターゲットをセットする</summary>
    /// <param name="hit"></param>
    void AATargetSet(RaycastHit hit)
    {
        if (hit.collider.gameObject.TryGetComponent(out CharacterBase characterBase)) // AA出来るobjか判定
        {
            _champManager.DesignatedObject = characterBase;
            StartCoroutine(_champManager.TargetDesignationCheck(_charaParam.Range, StartAA));
        }
        else
        {
            _champManager.DesignatedObject = null;
            _AAing = false;
        }
    }

    private void Start() => InputManager.Instance.SetEnterRaycastInput(InputType.RightClick, this.AATargetSet);
}
