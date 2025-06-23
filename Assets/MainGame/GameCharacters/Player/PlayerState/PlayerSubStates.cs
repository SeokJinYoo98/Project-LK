using UnityEngine;

namespace Player.States
{
    public class FlipSubState : State<PlayerPresenter>
    {
        bool _currFlip;
        public FlipSubState(PlayerPresenter owner) 
            : base( owner ) { }
        public override void Enter()
        {
            _currFlip = _owner.ShouldFlip;
            _owner.RequestFlip( _currFlip );
        }

        public override void Execute(float deltaTime)
        {
            FlipCheck( );
        }

        public override void Exit()
        {

        }

        private void FlipCheck()
        {
            if (_owner.ShouldFlip == _currFlip) return;

            _currFlip = _owner.ShouldFlip;
            _owner.RequestFlip( _currFlip );
        }
    }
    public class AimSubState : State<PlayerPresenter>
    {
        public AimSubState(PlayerPresenter owner) : base( owner ) { }
        public override void Enter()
        {
        }

        public override void Execute(float deltaTime)
        {
            _owner.RequestLookAt( _owner.MouseWorldPos );
        }

        public override void Exit()
        {

        }
    }

    //public class LeftHandAttack
}