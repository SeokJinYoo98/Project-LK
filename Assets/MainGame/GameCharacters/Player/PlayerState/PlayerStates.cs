using UnityEngine;

namespace Player.States
{
    public class IdleState : State<PlayerPresenter>
    {
        public IdleState(PlayerPresenter owner) : base(owner) { }
        public override void Enter()
        {
            Debug.Log( "Idle Enter" );
            _owner.SetVelocity( Vector2.zero );
        }

        public override void Execute(float deltaTime)
        {
            if (0.001f < _owner.Velocity.sqrMagnitude)
                _owner.ChangeMainState( new WalkState( _owner ) );
            // 스테미너 회복 등 추가
        }

        public override void Exit()
        {

        }
    }

    public class WalkState : State<PlayerPresenter>
    {
        public WalkState(PlayerPresenter owner) : base( owner ) { }
        public override void Enter()
        {
            Debug.Log( "Walk Enter" );
            _owner.SetVelocity( _owner.Velocity );
        }

        public override void Execute(float deltaTime)
        {
            if (_owner.Velocity.sqrMagnitude <= 0.01f)
                _owner.ChangeMainState(new IdleState( _owner ) );
            // 가속도 추가
            _owner.SetVelocity( _owner.Velocity );
        }

        public override void Exit()
        {
            Debug.Log( "Walk Exit" );
        }
    }
    //public class FlipState : State<PlayerPresenter>
    //{
    //    bool _currFlip = false;
    //    public FlipState(PlayerPresenter owner) : base( owner ) { }
    //    public override void Enter()
    //    {
    //        _currFlip = _owner.ShouldFlip;
    //        _owner.SetFlipX( _currFlip );
    //    }

    //    public override void Execute(float deltaTime)
    //    {
    //        if (_owner.ShouldFlip == _currFlip) return;

    //        _currFlip = _owner.ShouldFlip;
    //        _owner.SetFlipX( _currFlip );
    //    }

    //    public override void Exit()
    //    {
            
    //    }
    //}
}
