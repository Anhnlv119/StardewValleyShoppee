using UnityEngine;
using UnityEngine.Windows;

public class GameCharacterMovementHandler : MonoBehaviour
{
    [SerializeField]
    private float m_MovementSpeed = 10f;

    private GameCharacterInputHandler _characterInput;
    private Vector2 _direction;
    private Rigidbody2D _rb;
   
    private void Awake()
    {
        _characterInput= GetComponent<GameCharacterInputHandler>();
        _rb = GetComponent<Rigidbody2D>();
        
    }

    private void OnEnable()
    {
        _characterInput.onCharacterMove += GatherInput;
    }

    private void OnDisable()
    {
        _characterInput.onCharacterMove -= GatherInput;
    }
    private void GatherInput(Vector2 direction)
    {
        this._direction = direction;
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        Vector2 movement = Vector2.zero;

        if (Mathf.Abs(_direction.x) > Mathf.Abs(_direction.y))
        {
            movement.x = _direction.x; // Move horizontally only
        }
        else if (Mathf.Abs(_direction.y) > 0.1f)
        {
            movement.y = _direction.y; // Move vertically only
        }
        _rb.linearVelocity = m_MovementSpeed * movement;
       
    }
}
