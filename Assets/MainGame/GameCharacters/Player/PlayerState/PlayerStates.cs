using Common.Interface.MVPC;
using HandSystem;
using System;
using UnityEngine;

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
    public class PlayerAttackState : State<PlayerPresenter>, IAttackableState
    {
        float _attackTime   = 1.0f;
        HandType _handType  = HandType.None;
        public PlayerAttackState(PlayerPresenter owner) : base( owner ) { }
        public override void Enter()
        {
            _owner.SetAnimBool( "IsAttack", true );
        }
        public override void Execute(float deltaTime)
        {
            _attackTime -= deltaTime;
            if (_attackTime <= 0.0f)
            {
                TryEndAttack( );
            }
              
        }
        public override void Exit()
        {
            _owner.EndAttack( );
            _owner.SetAnimBool( "IsAttack", false );
        }
        public void OnAttackInput(HandType type)
        {
            var other = type == HandType.Left ? HandType.Right : HandType.Left;

            // [어택 미구현 임시 Idle 수행]
            _owner.ChangeHandState( type,  HandStateType.Attack );
            _owner.ChangeHandState( other, HandStateType.Wait );
            //_owner.ChangeHandState(HandType.Both, HandStateType.Wait );

            //_owner.ChangeHandState( type,  HandStateType.Attack );
            //_owner.ChangeHandState( other, HandStateType.Wait );

            _handType = type;
        }
        private void TryEndAttack()
        {
            if (_owner.Velocity.sqrMagnitude < 0.01f)
            {
                _owner.ChangeMainState( PlayerStateType.Idle );
                _owner.ChangeHandState( HandType.Both, HandStateType.Idle );
            }
            else
            {
                _owner.ChangeMainState( PlayerStateType.Walk );
                _owner.ChangeHandState( HandType.Both, HandStateType.Walk );
            }
        }
    }

}
