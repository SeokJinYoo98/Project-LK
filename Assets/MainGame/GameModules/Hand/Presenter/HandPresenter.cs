using System;
using Common.Interface.Equipment;
using Common.Interface.MVPC;
using UnityEngine;
namespace HandSystem
{
    public enum HandType { Left, Right, Both }

    [RequireComponent( typeof( HandModel ) )]
    [RequireComponent( typeof( HandView ) )]
    public class HandPresenter : MonoBehaviour, IPresenter
    {
        [SerializeField] private HandType _handType;
        public HandType Type => _handType;

        private HandModel      _handModel;
        private HandView       _handView;
        private StateMachine<HandPresenter> _fsm;

        // public event Action<HandType> OnAttackFinished;

        private void Awake()
        {
            _handView = GetComponent<HandView>( );
            _handModel = GetComponent<HandModel>( );
            _fsm = new StateMachine<HandPresenter>( );
        }
        void Update()
        {
           _fsm.Execute(Time.deltaTime);
        }
        
        public void SetFlipX(bool flip)
        {
            _handView.FlipX( flip );
        }
        public void LookAt(Vector2 targetPos)
        {
            Vector2 direction = targetPos - (Vector2)transform.position;
            _handView.LookAt( direction );
        }
        public void SetAnim(string name, bool anim)
            => _handView.SetAnim( name, anim );
        public void SetSwapPos(Vector3 pos) 
            => _handView.SetSwapPos( pos );
        public void ChangeMainState(State<HandPresenter> newState)
            => _fsm.ChangeMainState( newState );
    }
}