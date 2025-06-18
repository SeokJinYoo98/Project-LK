using UnityEngine;
using UnityEngine.InputSystem;

namespace GameSystem.MVP
{
    public interface IInputView
    {
        Vector2 MoveDir { get; }
        Vector2 MousePos { get; }
    }
}

