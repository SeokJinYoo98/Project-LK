using UnityEngine;
using System;
using UnityEngine.InputSystem;

namespace Common.Interface.Handler
{
    public interface IInputHandler
    {
        float MoveDirMag { get; }
        Vector2 MoveDir { get; }
        Vector2 MousePos { get; }

        event Action OnLeftClick;
        event Action OnRightClick;
    }
}

