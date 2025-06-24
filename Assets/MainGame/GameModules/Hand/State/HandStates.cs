using System;
using UnityEngine;

namespace HandSystem
{
    public enum HandStateType { Idle, Walk, Attack, Wait };
    public class HandWaitState : State<HandPresenter>
    {
        public HandWaitState(HandPresenter owner) : base( owner ) { }
        public override void Enter()
        {
            _owner.SetAnim("HandState", HandStateType.Wait);
        }
    }
    public class HandIdleState : State<HandPresenter>
    {
        public HandIdleState(HandPresenter owner) : base( owner ) { }
        public override void Enter()
        {
            _owner.SetAnim( "HandState", HandStateType.Idle );
        }

        public override void Execute(float deltaTime)
        {
           
        }

        public override void Exit()
        {
        }
    }
    public class HandWalkState : State<HandPresenter>
    {
        public HandWalkState(HandPresenter owner) : base( owner ) { }
        public override void Enter()
        {
            _owner.SetAnim( "HandState", HandStateType.Walk );
        }

        public override void Execute(float deltaTime)
        {

        }

        public override void Exit()
        {

        }
    }
    public class HandAttackState : State<HandPresenter>
    {
        public HandAttackState(HandPresenter owner) : base( owner ) { }
        public override void Enter()
        {
            _owner.SetAnim( "HandState", HandStateType.Attack );

        }

        public override void Execute(float deltaTime)
        {

        }

        public override void Exit()
        {

        }
    }
}

