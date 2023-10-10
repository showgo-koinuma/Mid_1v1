using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ChampAA : MonoBehaviour
{
    ChampionController _champContlr;
    CharacterParameter _charaParam;
    ChampionAnimationCntlr _animationCntlr;
    bool _AAing;

    private void Awake()
    {
        _champContlr = GetComponent<ChampionController>();
        _charaParam = _champContlr.CharaParam;
        _animationCntlr = _champContlr.ChampAnimContlr;
    }

    public void AA()
    {
        _champContlr.DealDamage((int)_charaParam.AD, DamageType.AD, _champContlr.DesignatedObject);
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
            _champContlr.DesignatedObject = characterBase;
            StartCoroutine(_champContlr.TargetDesignationCheck(_charaParam.Range, StartAA));
        }
        else
        {
            _champContlr.DesignatedObject = null;
            _AAing = false;
        }
    }

    private void OnEnable() => InputManager.Instance.SetEnterRaycastInput(InputType.RightClick, this.AATargetSet);
}
