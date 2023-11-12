using UnityEngine;
using DG.Tweening;

public class EController : MonoBehaviour
{
    ChampionManager _champManager;
    CharacterParameter _charaParam;
    ChampionMoveManager _moveManager;
    ChampionAnimationCntlr _animationCntlr;
    float _cd = 1;
    bool _isCD = false;
    int _stack = 0;
    /// <summary>ChannelingŽžŠÔ</summary>
    float _channelingTime = 0.3f;

    private void Awake()
    {
        _champManager = GetComponent<ChampionManager>();
        _charaParam = _champManager.CharaParam;
        _moveManager = GetComponent<ChampionMoveManager>();
        _animationCntlr = _champManager.ChampAnimContlr;
    }

    void Start() => InputManager.Instance.SetEnterRaycastInput(InputType.E, this.OnE);

    void OnE(RaycastHit hit)
    {
        if (!_isCD && hit.collider.gameObject.TryGetComponent(out CharacterManagerBase characterBase))
        {
            _moveManager.DesignatedObject = characterBase;
            _moveManager.TargetDesignationCheck(9.5f, StartE);
        }
    }

    void StartE()
    {

    }
}
