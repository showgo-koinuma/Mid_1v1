using UnityEngine;
using DG.Tweening;

public class TornadoCntlr : MonoBehaviour
{
    [SerializeField, Tooltip("range���B�܂ł̎���(dotween������)")] float _time = 1f;

    /// <summary>�������Z�b�g���Ĕ�΂�</summary>
    /// <param name="forward"></param>
    public void SetForwardToMove(Vector3 forward)
    {
        this.transform.DOLocalMove(forward * 900 * 0.02f, _time);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out CharacterBase characterBase)) StartCoroutine(characterBase.KnockUp(1));
    }
}
