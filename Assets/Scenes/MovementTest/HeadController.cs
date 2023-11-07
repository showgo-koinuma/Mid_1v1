using UnityEngine;

public class HeadController : MonoBehaviour
{
    [SerializeField] Transform _head;
    [SerializeField] Transform _orientation;
    private float _xRotation;
    [SerializeField] float _XSensitivity = 50f;
    [SerializeField] float _YSensitivity = 50f;
    /// <summary>なにこれ</summary>
    private float _sensMultiplier = 1f; // 感度変更用？

    private void Update()
    {
        Look();
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * _XSensitivity * Time.fixedDeltaTime * _sensMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * _YSensitivity * Time.fixedDeltaTime * _sensMultiplier;

        //Find current look rotation
        Vector3 rot = _orientation.localRotation.eulerAngles;
        float desiredX = rot.y + mouseX;

        //Rotate, and also make sure we dont over- or under-rotate.
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        //Perform the rotations
        _orientation.transform.localRotation = Quaternion.Euler(0, desiredX, 0);
        _head.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
    }
}
