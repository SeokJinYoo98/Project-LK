using UnityEngine;
using HandSystem;
using Common.Interface.MVPC;

namespace Player.States
{
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
                _owner.ChangeMainState( new PlayerWalkState( _owner ) );
                _owner.ChangeHandState( HandType.Both, HandStateType.Walk );
            }
                
            // ���׹̳� ȸ�� �� �߰�
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
                _owner.ChangeMainState( new PlayerIdleState( _owner ) );
                _owner.ChangeHandState( HandType.Both, HandStateType.Idle );
            }
                

            _owner.SetVelocity( _owner.Velocity );
            // ���� ���ӵ� �߰�
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
        readonly HandType   _handType;
        float               _attackTime = 1.0f;
        bool                _canMove = false;
        public PlayerAttackState(PlayerPresenter owner, HandType handType) : base( owner )
        {
            _handType = handType;
        }
        public override void Enter()
        {
            _canMove = _owner.CanMove;
        }
        public override void Execute(float deltaTime)
        {
            _attackTime -= deltaTime;
            if (_attackTime <= 0.0f)
            {
               _owner.ChangeMainState(new PlayerIdleState(_owner) );
               _owner.ChangeHandState( HandType.Both, HandStateType.Idle );
            }
              
        }
    }

}
