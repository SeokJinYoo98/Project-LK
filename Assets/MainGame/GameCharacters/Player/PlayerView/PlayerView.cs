using Common.Interface.MVPC;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

namespace Player
{
    [RequireComponent(typeof( Rigidbody2D ))]
    [RequireComponent(typeof( Animator ))]
    [RequireComponent(typeof( SpriteRenderer ))]
    public class PlayerView : MonoBehaviour, IView
    {
        private SpriteRenderer  _sr;
        private Animator        _animator;
        private Rigidbody2D     _rb;
        private void Awake()
        {
            _animator = GetComponent<Animator>( );
            _rb = GetComponent<Rigidbody2D>( );
            _sr = GetComponent<SpriteRenderer>( );
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
        }
        void FixedUpdate()
        {
        }
        public void SetVelocity(Vector2 velocity)
        {
            _rb.linearVelocity = velocity;
            float mag = velocity.magnitude;
            if (mag < 0.01f) mag = 0f;
            SetAnimFloat( "MoveSpeed", mag );
            
        }
        public void SetFlipX(bool flipX)
        {
            _sr.flipX = flipX;
        }
        public void SetAnimBool(string name,  bool value)
            => _animator.SetBool( name, value );

        public void SetAnimFloat(string name, float value)
            => _animator.SetFloat( name, value );
    }
}