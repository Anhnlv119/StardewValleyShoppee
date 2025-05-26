using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameCharacterInputHandler : MonoBehaviour
{
    private CharacterInput characterInput;
    public Action<Vector2> onCharacterMove;
    private Vector2 direction;
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
        Debug.Log("Interacted");
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
    }
}
