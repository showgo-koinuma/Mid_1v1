using UnityEngine;

public class ChampAA : MonoBehaviour
{
    ChampionManager _champManager;
    CharacterParameter _charaParam;
    ChampionMoveManager _moveManager;
    ChampionAnimationCntlr _animationCntlr;
    Coroutine _AACoroutine;
    float _AATimeRate = 1;
    float _AATimer = 0;

    private void Awake()
    {
        _champManager = GetComponent<ChampionManager>();
        _charaParam = _champManager.CharaParam;
        _moveManager = GetComponent<ChampionMoveManager>();
        _animationCntlr = _champManager.ChampAnimContlr;
    }

    /// <summary>animatin event AA�q�b�g����</summary>
    public void AA()
    {
        _champManager.DealDamage((int)_charaParam.AD, DamageType.AD, _moveManager.DesignatedObject);
    }

    void AATimer()
    {
        if (_AATimer > 0) _AATimer -= Time.deltaTime;
        else
        {
            if (_moveManager.ChampState == ChampionState.AAing)
            {
                _animationCntlr.AATrigger();
                _AATimer = _AATimeRate;
            }
        }
    }

    /// <summary>AA�̃^�[�Q�b�g���Z�b�g����</summary>
    /// <param name="hit"></param>
    void AATargetSet()
    {
        if (PlayerInput.Instance.MouseHitBlue.collider.gameObject.TryGetComponent(out CharacterManagerBase characterBase)) // AA�o����obj������
        {
            // AA���łȂ��A�܂��͈Ⴄ�G���^�[�Q�b�g�ɂ�����Ώێw��̃R���[�`���J�n
            if (_AACoroutine == null || _moveManager.DesignatedObject != characterBase)
            {
                _moveManager.DesignatedObject = characterBase;
                StartCoroutine(_moveManager.TargetDesignationCheck(_charaParam.Range, () => { _moveManager.ChampState = ChampionState.AAing; }));
            }
        }
        else
        {
            _moveManager.DesignatedObject = null;

            if (_AACoroutine != null)
            {
                StopCoroutine(_AACoroutine);
                _AACoroutine = null;
            }
        }
    }

    private void Start()
    {
        InGameManager.Instance.SetUpdateAction(AATimer);
        PlayerInput.Instance.SetInput(InputType.RightClick, this.AATargetSet);
    }
}
