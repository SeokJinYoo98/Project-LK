using UnityEngine;

namespace Player.States
{
    public class InputSubState : State<PlayerPresenter>
    {
        bool _currFlip;
        public InputSubState(PlayerPresenter owner) 
            : base( owner ) { }
        public override void Enter()
        {
            _currFlip = _owner.ShouldFlip;
            _owner.RequestFlip( _currFlip );
        }

        public override void Execute(float deltaTime)
        {
            FlipCheck( );
            MouseCheck( );
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
        private void MouseCheck()
        {
            _owner.RequestLookAt( _owner.MouseWorldPos );
        }
    }

    //public class LeftHandAttack
}