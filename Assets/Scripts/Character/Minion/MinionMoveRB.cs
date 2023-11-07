using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class MinionMoveRB : MonoBehaviour
{
    [SerializeField] float _speed = 5;
    Rigidbody _rb;
    int _direction = 1;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Invoke(nameof(ChangeDirection), 3);
    }

    private void Update()
    {
        _rb.velocity = Vector3.right * _speed * Time.deltaTime * _direction;
    }

    void ChangeDirection()
    {
        _direction *= -1;
        Invoke(nameof(ChangeDirection), 3);
    }
}
