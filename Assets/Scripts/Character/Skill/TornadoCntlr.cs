using UnityEngine;
using DG.Tweening;

public class TornadoCntlr : MonoBehaviour
{
    [SerializeField, Tooltip("range到達までの時間(dotweenだから)")] float _time = 1f;

    /// <summary>方向をセットして飛ばす</summary>
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
