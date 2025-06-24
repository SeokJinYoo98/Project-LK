using UnityEngine;
using Common.Interface.MVPC;

namespace Player
{
    public class PlayerModel : IModel
    {
        public float MoveSpeed { get; private set; } = 1f;
        public float Hp { get; private set; } = 0;
        public bool  CanMove = true;

        public bool IsDead() => Hp <= 0;
    }
}
