using System;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Common.Interface.ppp;

namespace HandSystem
{
    public class HandManager : MonoBehaviour
    {
        [SerializeField] private HandPresenter _leftHand;
        [SerializeField] private HandPresenter _rightHand;

        private readonly Dictionary<HandType, HandPresenter> _handMap = new( );

        private void Awake()
        {
            _handMap[_leftHand.Type]  = _leftHand;
            _handMap[_rightHand.Type] = _rightHand;

        }
        private void Start()
        {
            _leftHand.SetSwapPos( _rightHand.transform.position );
            _rightHand.SetSwapPos( _leftHand.transform.position );
        }
        public void SetFlipX(bool flip)
        {
            foreach (var hand in _handMap.Values)
                hand.SetFlipX( flip );
        }
        public void LookAt(Vector2 targetPos)
        {
            foreach (var hand in _handMap.Values)
                hand.LookAt( targetPos );
        }

        public void ChangeHandState(HandType type, HandStateType stateType)
        {
            if (type == HandType.Both)
            {
                foreach (var hand in _handMap.Values)
                    hand.ChangeMainState( NewState( hand, stateType ) );
            }
            else if (_handMap.TryGetValue( type, out var hand ))
            {
                hand.ChangeMainState( NewState( hand, stateType ) );
            }
            else
            {
                Debug.LogWarning( $"HandManager: Unknown hand type: {type}" );
            }
        }
        private State<HandPresenter> NewState(HandPresenter hand, HandStateType type)
        => type switch
         {
             HandStateType.Idle     => new HandIdleState( hand ),
             HandStateType.Walk     => new HandWalkState( hand ),
             HandStateType.Attack   => new HandAttackState( hand ),
             _ => throw new ArgumentOutOfRangeException( nameof( type ), type, null )
         };
    }
}