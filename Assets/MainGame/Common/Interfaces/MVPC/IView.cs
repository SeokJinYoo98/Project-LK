using UnityEngine;

namespace GameSystem.MVPC
{
    public interface IView
    {
        void SetVelocity(Vector2 velocity);
        void SetFlipX(bool flip);
        void SetAnimFloat(string name, float value);
        void SetAnimBool(string name, bool value);

    }
}