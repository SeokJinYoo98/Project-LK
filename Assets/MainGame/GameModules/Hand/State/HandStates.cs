using System;
using UnityEngine;

namespace HandSystem
{
    public enum HandStateType { Idle, Walk, Attack };
    public class HandIdleState : State<HandPresenter>
    {
        public HandIdleState(HandPresenter owner) : base( owner ) { }
        public override void Enter()
        {
            _owner.SetAnim( "Idle", true );
        }

        public override void Execute(float deltaTime)
        {

        }

        public override void Exit()
        {
            _owner.SetAnim( "Idle", false );
        }
    }
    public class HandWalkState : State<HandPresenter>
    {
        public HandWalkState(HandPresenter owner) : base( owner ) { }
        public override void Enter()
        {
            _owner.SetAnim( "Walk", true );

        }

        public override void Execute(float deltaTime)
        {

        }

        public override void Exit()
        {
            _owner.SetAnim( "Walk", false );
        }
    }
    public class HandAttackState : State<HandPresenter>
    {
        public HandAttackState(HandPresenter owner) : base( owner ) { }
        public override void Enter()
        {
            Debug.Log( "Idle Enter" );

        }

        public override void Execute(float deltaTime)
        {

        }

        public override void Exit()
        {

        }
    }
}

