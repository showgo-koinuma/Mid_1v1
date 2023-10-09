using UnityEngine;
using UnityEngine.InputSystem;

public class RightClickController : MonoBehaviour
{
    Vector3 _clickWorldPoint;
    PlayerController _playerController;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }

    public void OnRightClick(InputAction.CallbackContext _context)
    {
        Debug.Log(_context.action);
        if (_context.phase == InputActionPhase.Performed)
        {
            Debug.Log(_clickWorldPoint);
            //_playerController.PointSet(_clickWorldPoint);
        }
    }

    public void CursorWorldPosition(InputAction.CallbackContext _context)
    {
        Vector2 mousePos2 = _context.ReadValue<Vector2>();
        var ray = Camera.main.ScreenPointToRay(mousePos2);

        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, Mathf.Infinity, (1 << 3)))
        {
            _clickWorldPoint = hit.point;
        }
    }
}
