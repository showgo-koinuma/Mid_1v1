using UnityEngine;
using DG.Tweening;

public class EController : MonoBehaviour
{
    PlayerMove _playerMove;
    ChampionManager _champManager;
    CharacterParameter _charaParam;
    ChampionAnimationCntlr _animationCntlr;
    float _cd = 1;
    bool _isCD = false;
    int _stack = 0;
    /// <summary>ChannelingŽžŠÔ</summary>
    float _channelingTime = 0.3f;

    private void Awake()
    {
        _playerMove = GetComponent<PlayerMove>();
        _champManager = GetComponent<ChampionManager>();
        _charaParam = _champManager.CharaParam;
        _animationCntlr = _champManager.ChampAnimContlr;
    }

    void Start() => InputManager.Instance.SetEnterRaycastInput(InputType.E, this.OnE);

    void OnE(RaycastHit hit)
    {
        if (!_isCD && hit.collider.gameObject.TryGetComponent(out CharacterBase characterBase))
        {
            _champManager.DesignatedObject = characterBase;
            _champManager.TargetDesignationCheck(9.5f, StartE);
        }
    }

    void StartE()
    {

    }
}
