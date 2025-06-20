using UnityEngine;
using GameSystem.MVPC;
using Player.States;

namespace Player {
    [RequireComponent( typeof( PlayerInputView ) )]
    [RequireComponent( typeof( PlayerModel ) )]
    [RequireComponent( typeof( PlayerView ) )]
    [RequireComponent( typeof( HandSystem ) )]
    public class PlayerPresenter : MonoBehaviour, IPresenter
    {
        private IInputView   _inputView;
        private IView        _view;
        private IModel       _model;
        private Camera       _camera;
        private HandSystem   _handSystem;
        private StateMachine<PlayerPresenter> _fsm;
        private string _currAnim;

        private Vector2 _cachedMouseWorldPos;
        private Vector2 _cachedVelocity;
        private Vector2 _cachedPlayerPosition;
        private bool    _cachedShouldFlip;
        public Vector2 MouseWorldPos => _cachedMouseWorldPos;
        public Vector2 PlayerPos     => _cachedPlayerPosition;
        public Vector2 Velocity      => _cachedVelocity;


        void Awake()
        {
            _inputView  = GetComponent<PlayerInputView>( );
            _view       = GetComponent<PlayerView>( );
            _model      = GetComponent<PlayerModel>( );
            _handSystem = GetComponent<HandSystem>( );
            
            _camera = Camera.main;
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        { 
            _fsm = new( );
            _fsm.ChangeMainState( new IdleState( this ) );
            //_fsm.AddSubState( new FlipState( this ) );
        }

        // Update is called once per frame
        void Update()
        {
            UpdateCacheData();
            _fsm.Execute( Time.deltaTime );
        }
        private void UpdateCacheData()
        {
            
            _cachedPlayerPosition   = transform.position;
            
            _cachedMouseWorldPos    = _camera.ScreenToWorldPoint( _inputView.MousePos );
            _handSystem.LookAt( _cachedMouseWorldPos );

            Vector2 moveDir = _inputView.MoveDir;
            _cachedVelocity = (moveDir.sqrMagnitude < 0.0001f) ? Vector2.zero : moveDir * _model.MoveSpeed;

            bool shouldFlip = _cachedMouseWorldPos.x < _cachedPlayerPosition.x;
            if (_cachedShouldFlip != shouldFlip)
            {
                _cachedShouldFlip = shouldFlip;
                SetFlipX( _cachedShouldFlip );
            }
                
        }
        private void SetFlipX( bool flip )
        {
            _view.SetFlipX( flip );
            _handSystem.SetFlipX( flip );
        }
        public void ChangeMainState(State<PlayerPresenter> newState) 
            => _fsm.ChangeMainState( newState );
        public void SetVelocity(Vector2 velocity)
        {
            _view.SetVelocity( velocity );
            _handSystem.TempFunc(velocity);
        }
    }
}