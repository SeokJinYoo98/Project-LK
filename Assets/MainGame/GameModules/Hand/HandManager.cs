using Common.Interface.ppp;
using Player;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace HandSystem
{
    public class HandManager : MonoBehaviour
    {
        [SerializeField] private HandPresenter _leftHand;
        [SerializeField] private HandPresenter _rightHand;
        private readonly Dictionary<HandType, HandPresenter> _handMap = new( );

        [SerializeField] private Transform _leftRoot;
        [SerializeField] private Transform _rightRoot;
        Vector3 _defaultLeft;
        Vector3 _defaultRight;
        Vector3 _swapLeft;
        Vector3 _swapRight;

        bool _isFlipped = false;
        private void Awake()
        {
            _handMap[HandType.Left] = _leftHand;
            _handMap[HandType.Right] = _rightHand;

            _defaultLeft = _leftRoot.localPosition;
            _defaultRight = _rightRoot.localPosition;

            _swapLeft  = new Vector3( -_defaultRight.x, _defaultRight.y, _defaultRight.z );
            _swapRight = new Vector3( -_defaultLeft.x, _defaultLeft.y, _defaultLeft.z );
        }

        public void SetFlipX(bool flip)
        {
            if (_isFlipped == flip) return;
            _isFlipped = flip;
        }

        public void LookAt(Vector2 targetPos)
        {
            RotateRootTowards( _leftRoot, targetPos );
            RotateRootTowards( _rightRoot, targetPos );
        }

        void RotateRootTowards(Transform root, Vector2 target)
        {
            Vector2 dir = target - (Vector2)root.position;
            if (dir.sqrMagnitude < 0.0001f) return;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (_isFlipped) angle += 180f;
            root.rotation = Quaternion.Euler( 0f, 0f, angle );
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