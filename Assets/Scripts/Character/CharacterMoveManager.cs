using System.Collections;
using UnityEngine;

public abstract class CharacterMoveManager : MonoBehaviour
{
    [SerializeField, Tooltip("見た目だけ浮かせるKnockUp用")] protected Transform _body;
    /// <summary>対象指定のobj</summary>
    protected CharacterManagerBase _designatedObject;
    public CharacterManagerBase DesignatedObject { get => _designatedObject; set => _designatedObject = value; }

    public Vector2 This2DPos()
    {
        return new Vector2(this.transform.position.x, this.transform.position.z);
    }

    /// <summary>キャラをノックアップさせる(秒)</summary>
    /// <param name="sec"></param>
    public abstract IEnumerator KnockUp(float sec);
}