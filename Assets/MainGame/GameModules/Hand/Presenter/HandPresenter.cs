using Common.Interface.Equipment;
using Common.Interface.MVPC;
using System;
using UnityEngine;
using UnityEngine.XR;
namespace HandSystem
{
    public enum HandType { None, Left, Right, Both }

    [RequireComponent( typeof( HandView ) )]
    public class HandPresenter : MonoBehaviour, IPresenter
    {
        [SerializeField] private HandType _handType;
        public HandType Type => _handType;

        private HandModel       _handModel;
        private HandView        _handView;
        private StateMachine<HandPresenter> _fsm;

        // public event Action<HandType> OnAttackFinished;

        private void Awake()
        {
            _handView  = GetComponent<HandView>( );
            
            _fsm        = new StateMachine<HandPresenter>( );
            _handModel  = new HandModel( );
        }
        void Update()
        {
           _fsm.Execute(Time.deltaTime);
        }

        public void SetFlipX(bool flip)
            => _handView.FlipX( flip );
        public void LookAt(Vector2 targetPos)
            => _handView.LookAt( targetPos - (Vector2)transform.position );
        public void SetAnim(string name, HandStateType type)
            => _handView.SetAnim( name, type );

        public void ChangeMainState(HandStateType type)
            => _fsm.ChangeMainState(CreateState(type));

        public void DoAttack()
        {
            var weapon = _handModel.Weapon;
            if (_handModel.Weapon != null)
                _handModel.Weapon.Attack();
        }
        private State<HandPresenter> CreateState(HandStateType type)
          => type switch
          {
              HandStateType.Idle   => new HandIdleState( this ),
              HandStateType.Walk   => new HandWalkState( this ),
              HandStateType.Attack => new HandAttackState( this ),
              HandStateType.Wait   => new HandWaitState( this ),
              _ => throw new ArgumentOutOfRangeException( nameof( type ), type, null )
          };
    }
}