using UnityEngine;

public class GaugeLookCamera : MonoBehaviour
{
    // �I�u�W�F�N�g���f���J����
    [SerializeField] private Camera _targetCamera;

    // UI��\��������ΏۃI�u�W�F�N�g
    [SerializeField] private Transform _target;

    // �\������UI
    [SerializeField] private Transform _targetUI;

    // �I�u�W�F�N�g�ʒu�̃I�t�Z�b�g
    [SerializeField] private Vector3 _worldOffset;

    private RectTransform _parentUI;

    private void Awake()
    {
        // �J�������w�肳��Ă��Ȃ���΃��C���J�����ɂ���
        if (_targetCamera == null)
            _targetCamera = Camera.main;

        // �eUI��RectTransform��ێ�
        _parentUI = _targetUI.parent.GetComponent<RectTransform>();
    }

    // UI�̈ʒu�𖈃t���[���X�V
    private void Update()
    {
        OnUpdatePosition();
    }

    // UI�̈ʒu���X�V����
    private void OnUpdatePosition()
    {
        var cameraTransform = _targetCamera.transform;

        // �J�����̌����x�N�g��
        var cameraDir = cameraTransform.forward;
        // �I�u�W�F�N�g�̈ʒu
        var targetWorldPos = _target.position + _worldOffset;
        // �J��������^�[�Q�b�g�ւ̃x�N�g��
        var targetDir = targetWorldPos - cameraTransform.position;

        // ���ς��g���ăJ�����O�����ǂ����𔻒�
        var isFront = Vector3.Dot(cameraDir, targetDir) > 0;

        // �J�����O���Ȃ�UI�\���A����Ȃ��\��
        _targetUI.gameObject.SetActive(isFront);
        if (!isFront) return;

        // �I�u�W�F�N�g�̃��[���h���W���X�N���[�����W�ϊ�
        var targetScreenPos = _targetCamera.WorldToScreenPoint(targetWorldPos);

        // �X�N���[�����W�ϊ���UI���[�J�����W�ϊ�
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _parentUI,
            targetScreenPos,
            null,
            out var uiLocalPos
        );

        // RectTransform�̃��[�J�����W���X�V
        _targetUI.localPosition = uiLocalPos;
    }
}