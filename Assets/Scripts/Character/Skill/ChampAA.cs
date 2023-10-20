using System.Collections;
using UnityEngine;

public class ChampAA : MonoBehaviour
{
    ChampionManager _champManager;
    CharacterParameter _charaParam;
    ChampionAnimationCntlr _animationCntlr;
    //bool _AAing;
    Coroutine _AACoroutine;

    private void Awake()
    {
        _champManager = GetComponent<ChampionManager>();
        _charaParam = _champManager.CharaParam;
        _animationCntlr = _champManager.ChampAnimContlr;
    }

    /// <summary>animatin event AA�q�b�g����</summary>
    public void AA()
    {
        _champManager.DealDamage((int)_charaParam.AD, DamageType.AD, _champManager.DesignatedObject);
    }

    IEnumerator AAing()
    {
        //_AAing = true;
        _animationCntlr.StartAAAnimation();
        yield return new WaitForSeconds(1);
        if (_AACoroutine != null) _AACoroutine = StartCoroutine(AAing());
    }

    void StartAA()
    {
        if (_AACoroutine == null) _AACoroutine = StartCoroutine(AAing());
    }

    /// <summary>AA�̃^�[�Q�b�g���Z�b�g����</summary>
    /// <param name="hit"></param>
    void AATargetSet(RaycastHit hit)
    {
        if (hit.collider.gameObject.TryGetComponent(out CharacterBase characterBase)) // AA�o����obj������
        {
            _champManager.DesignatedObject = characterBase;
            if (_AACoroutine == null) StartCoroutine(_champManager.TargetDesignationCheck(_charaParam.Range, StartAA));
        }
        else
        {
            _champManager.DesignatedObject = null;
            if (_AACoroutine != null)
            {
                StopCoroutine(_AACoroutine);
                _AACoroutine = null;
            }
            //_AAing = false;
        }
    }

    private void Start() => InputManager.Instance.SetEnterRaycastInput(InputType.RightClick, this.AATargetSet);
}
