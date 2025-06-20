using UnityEngine;
using UnityEngine.InputSystem;

using GameSystem.MVPC;

namespace Player
{
    [RequireComponent( typeof( PlayerInput ) )]
    public class PlayerInputView : MonoBehaviour, IInputView
    {
        private PlayerInput _input;
        public Vector2 MoveDir { get; private set; } = Vector2.zero;
        public Vector2 MousePos { get; private set; } = Vector2.zero;
        void Awake()
        {
            _input = GetComponent<PlayerInput>( );
            _input.actions["Move"].performed += OnMove;
            _input.actions["Move"].canceled += OnMove;

            _input.actions["MousePos"].performed += OnMouseMove;
        }
        private void OnDestroy()
        {
            _input.actions["Move"].performed -= OnMove;
            _input.actions["Move"].canceled -= OnMove;

            _input.actions["MousePos"].performed -= OnMouseMove;
        }
        private void OnMove(InputAction.CallbackContext context)
            => MoveDir = context.ReadValue<Vector2>( );
        private void OnMouseMove(InputAction.CallbackContext context)
            => MousePos = context.ReadValue<Vector2>( );
    }
}