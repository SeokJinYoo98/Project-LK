using UnityEngine;

namespace GameSystem.MVP
{
    public interface IView
    {
        void MoveTo(Vector2 velocity);
        void LookAt(Vector2 mouseScreenPos);
    }
}