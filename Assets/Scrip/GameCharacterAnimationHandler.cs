using System;
using UnityEngine;

public class GameCharacterAnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator animator; // Reference to the Animator component
    [SerializeField] private float inputThreshold = 0.1f; // Minimum input to consider as movement

    private GameCharacterInputHandler _characterInput;

    private Vector2 lastFacingDirection = Vector2.down; // Start facing down
    private Vector2 _direction;

    private void Awake()
    {
        _characterInput = GetComponent<GameCharacterInputHandler>();
        // Initialize facing down (IdleDown)
        animator.SetFloat("X", 0f);
        animator.SetFloat("Y", -1f);
    }

    private void OnEnable()
    {
        _characterInput.onCharacterMove += GatherInput;
    }

    private void GatherInput(Vector2 direction)
    {
        this._direction = direction;
    }

    private void OnDisable()
    {
        _characterInput.onCharacterMove -= GatherInput;
    }

    private void FixedUpdate()
    {
        OnAnimatorMove();
    }

    private void OnAnimatorMove()
    {
        // Check if player is moving based on input magnitude
        bool isMoving = Mathf.Sqrt(_direction.x * _direction.x + _direction.y * _direction.y) > inputThreshold;

        if (isMoving)
        {
            // Update facing direction only when moving
            lastFacingDirection = new Vector2(_direction.x, _direction.y).normalized;
            animator.SetFloat("Speed", 1);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }

        // Set animator parameters based on facing direction
        animator.SetFloat("X", lastFacingDirection.x);
        animator.SetFloat("Y", lastFacingDirection.y);
    }
}