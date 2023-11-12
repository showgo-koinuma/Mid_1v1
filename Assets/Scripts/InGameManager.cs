using System;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    static InGameManager _instance;
    public static InGameManager Instance { get => _instance; private set => _instance = value; }
    /// <summary>update�Ŏ��s����Action</summary>
    event Action _updateAction;

    [SerializeField, Tooltip("CS�̏�������")] int _csFinishCount = 50;
    [SerializeField, Tooltip("Kill������������")] int _killFinishCount = 3;
    //���Ԃł̏I������

    // game����
    float _gameTime = 0;
    public float GameTime { get => _gameTime; }
    // CS
    int _csBlue = 0;
    public int CsBlue { get => _csBlue; set { _csBlue = value; UpdateCS(); } }
    // Kill��
    int _killCountBlue = 0;
    public int KillCountBlue { set { _killCountBlue = value; if (_killCountBlue >= _killFinishCount) FinishGame(true); } }

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(gameObject);
    }

    /// <summary>cs���X�V���ꂽ�Ƃ�</summary>
    void UpdateCS()
    {
        // �e�L�X�g�X�V

        // �Q�[���I������
        if (_csBlue >= _csFinishCount) FinishGame(true);
        // red�̔���
    }

    void FinishGame(bool winTeamIsBlue)
    {
        Debug.Log("finish game");
    }

    public void SetUpdateAction(Action action)
    {
        _updateAction += action;
    }

    private void Update()
    {
        _gameTime += Time.deltaTime;
        _updateAction?.Invoke();
    }
}
