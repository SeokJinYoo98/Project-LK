using UnityEngine;
using Common.Interface.MVPC;
namespace Common.Interface.ppp
{
    public interface IPlayerDataProvider
    {
        public Vector2 MouseWorldPos { get; }
        public Vector2 PlayerPos { get; }
        public Vector2 Velocity { get; }
        public bool ShouldFlip { get; }
        public bool CanMove { get; }


    }

}