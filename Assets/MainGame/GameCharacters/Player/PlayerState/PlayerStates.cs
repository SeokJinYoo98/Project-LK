using UnityEngine;
using HandSystem;
using Common.Interface.MVPC;

namespace Player.States
{
    public enum PlayerStateType
    {
        Idle,
        Walk,
        Attack,
        Death
    }
    public class PlayerIdleState : State<PlayerPresenter>
    {
        public PlayerIdleState(PlayerPresenter owner) : base(owner) { }
        public override void Enter()
        {
            Debug.Log( "Idle Enter" );
            _owner.SetVelocity( Vector2.zero );
        }

        public override void Execute(float deltaTime)
        {
            if (_owner.Velocity.sqrMagnitude > 0.01f)
            {
                _owner.ChangeMainState( PlayerStateType.Walk );
                _owner.ChangeHandState( HandType.Both, HandStateType.Walk );
            }
                
            // 스테미너 회복 등 추가
        }

        public override void Exit()
        {

        }
    }

    public class PlayerWalkState : State<PlayerPresenter>
    {
        Vector2 _velocity;
        //float   _accelTime = 3.0f;
        public PlayerWalkState(PlayerPresenter owner) : base( owner ) { }
        public override void Enter()
        {
            Debug.Log( "Walk Enter" );
            _velocity = _owner.Velocity;
        }

        public override void Execute(float deltaTime)
        {
            if (_owner.Velocity.sqrMagnitude < 0.01f)
            {
                _owner.ChangeMainState( PlayerStateType.Idle );
                _owner.ChangeHandState( HandType.Both, HandStateType.Idle );
            }
                

            _owner.SetVelocity( _owner.Velocity );
            // 추후 가속도 추가
            _velocity = _owner.Velocity;
        }

        public override void Exit()
        {
            _owner.SetVelocity( Vector2.zero );
            Debug.Log( "Walk Exit" );
        }
    }
    public class PlayerAttackState : State<PlayerPresenter>
    {
        float               _attackTime = 1.0f;

        public PlayerAttackState(PlayerPresenter owner) : base( owner ) { }
        public override void Enter()
        {

        }
        public override void Execute(float deltaTime)
        {
            _attackTime -= deltaTime;
            if (_attackTime <= 0.0f)
            {
               _owner.ChangeMainState( PlayerStateType.Idle );
               _owner.ChangeHandState( HandType.Both, HandStateType.Idle );
            }
              
        }
    }

}
