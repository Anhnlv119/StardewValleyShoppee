using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameCharacterInputHandler : MonoBehaviour
{
    private CharacterInput characterInput;
    public Action<Vector2> onCharacterMove;
    private Vector2 direction;
    private List<string> holdItem = new List<string>() { "hoe","water"};
    private Vector2 lastFacingDirection = Vector2.down;
    private void Awake()
    {
        characterInput = new CharacterInput();
    }
    private void OnEnable()
    {
        characterInput.Character.Interact.performed += OnInteracted;

        characterInput.Enable();
    }
    private void OnInteracted(InputAction.CallbackContext context)
    {
       
        UseTool();
        
        
    }
    [SerializeField] private float offSetDistance=2f;
    [SerializeField] private float sizeOfCast = 3f;
    private void UseTool()
    {
        Debug.Log("use E"+ lastFacingDirection);
        RaycastHit2D hit= Physics2D.Raycast(transform.position,direction, 1f);
        Debug.DrawRay(transform.position, lastFacingDirection, Color.red);
    }

    private void OnDisable()
    {
        characterInput.Character.Interact.performed -= OnInteracted;
        characterInput.Disable();
    }

    private void Update()
    {
        direction = characterInput.Character.Movement.ReadValue<Vector2>();
        onCharacterMove?.Invoke(direction);
        if(direction!= Vector2.zero)
        {
            lastFacingDirection = new Vector2(direction.x, direction.y).normalized;
        }
    }
}
