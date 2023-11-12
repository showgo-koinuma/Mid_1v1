using UnityEngine;
using DG.Tweening;

public class TornadoCntlr : MonoBehaviour
{
    [SerializeField, Tooltip("range���B�܂ł̎���(dotween������)")] float _time = 1.5f;
    [SerializeField, Tooltip("KnockUp����")] float _knockUpTime = 1;
    int _damage;

    /// <summary>�������Z�b�g���Ĕ�΂�</summary>
    /// <param name="forward"></param>
    public void Initialization(Vector3 point, int damage)
    {
        _damage = damage;
        Vector3 dir = (point - this.transform.position).normalized * 900 * 0.01f;
        dir.y = 0;
        this.transform.DOMove(this.transform.position + dir, _time);
        Destroy(gameObject, _time);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out CharacterManagerBase characterBase))
        {
            StartCoroutine(characterBase.MoveManager.KnockUp(_knockUpTime));
            characterBase.DealDamage(_damage, DamageType.AD, characterBase);
        }
    }
}
