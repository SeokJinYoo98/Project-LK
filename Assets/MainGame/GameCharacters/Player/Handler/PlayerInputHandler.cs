using UnityEngine;
using UnityEngine.InputSystem;
using Common.Interface.Handler;
using System;
using Common.Interface.MVPC;

namespace Player
{
    [RequireComponent( typeof( PlayerInput ) )]
    public class PlayerInputHandler : MonoBehaviour, IInputHandler
    {
        private PlayerInput _input;
        public float MoveDirMag => MoveDir.magnitude;
        public Vector2 MoveDir { get; private set; } = Vector2.zero;
        public Vector2 MousePos { get; private set; } = Vector2.zero;

        public event Action OnLeftClick;
        public event Action OnRightClick;


        void Awake()
        {
            _input = GetComponent<PlayerInput>( );
            _input.actions["Move"].performed += OnMove;
            _input.actions["Move"].canceled  += OnMove;

            _input.actions["Look"].performed += OnMouseMove;

            _input.actions["LeftButton"].performed  += OnLeftMouseClick;
            _input.actions["RightButton"].performed += OnRightMouseClick;
        }
        private void OnDestroy()
        {
            _input.actions["Move"].performed -= OnMove;
            _input.actions["Move"].canceled  -= OnMove;

            _input.actions["Look"].performed -= OnMouseMove;

            _input.actions["LeftButton"].performed  -= OnLeftMouseClick;
            _input.actions["RightButton"].performed -= OnRightMouseClick;
        }
        private void OnMove(InputAction.CallbackContext context)
            => MoveDir = context.ReadValue<Vector2>( );
        private void OnMouseMove(InputAction.CallbackContext context)
            => MousePos = context.ReadValue<Vector2>( );
        private void OnLeftMouseClick(InputAction.CallbackContext context)
            => OnLeftClick?.Invoke( );
        private void OnRightMouseClick(InputAction.CallbackContext context)
            => OnRightClick?.Invoke( );
    }
}