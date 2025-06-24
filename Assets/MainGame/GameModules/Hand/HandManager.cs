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
            _handMap[HandType.Left]  = _leftHand;
            _handMap[HandType.Right] = _rightHand;

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
                foreach (var hand in _handMap.Values)
                    hand.ChangeMainState( stateType );

            else if (type == HandType.Left || type == HandType.Right)
                _handMap[type].ChangeMainState( stateType );

            else { }
                
            
        }
    }
}