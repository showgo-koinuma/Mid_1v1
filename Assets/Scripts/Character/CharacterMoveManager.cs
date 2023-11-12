using System.Collections;
using UnityEngine;

public abstract class CharacterMoveManager : MonoBehaviour
{
    [SerializeField, Tooltip("�����ڂ�����������KnockUp�p")] protected Transform _body;
    /// <summary>�Ώێw���obj</summary>
    protected CharacterManagerBase _designatedObject;
    public CharacterManagerBase DesignatedObject { get => _designatedObject; set => _designatedObject = value; }

    public Vector2 This2DPos()
    {
        return new Vector2(this.transform.position.x, this.transform.position.z);
    }

    /// <summary>�L�������m�b�N�A�b�v������(�b)</summary>
    /// <param name="sec"></param>
    public abstract IEnumerator KnockUp(float sec);
}