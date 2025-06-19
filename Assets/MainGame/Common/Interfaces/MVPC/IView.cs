using UnityEngine;

namespace GameSystem.MVPC
{
    public interface IView
    {
        void MoveTo(Vector2 velocity);
        void SetFlipX(bool flip);
    }
}