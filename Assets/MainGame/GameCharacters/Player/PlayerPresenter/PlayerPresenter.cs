using Common.Interface.Handler;
using Common.Interface.MVPC;
using HandSystem;
using Player.States;
using System;
using UnityEngine;
using UnityEngine.XR;

namespace Player {
    [RequireComponent( typeof( PlayerInputHandler ) )]
    [RequireComponent( typeof( PlayerModel ) )]
    [RequireComponent( typeof( PlayerView ) )]
    [RequireComponent( typeof( HandManager ) )]
    public class PlayerPresenter : MonoBehaviour, IPresenter
    {
        private IInputHandler    _input;
        private IView            _view;
        private PlayerModel      _model;
        private Camera           _camera;
        private HandManager      _handManager;
        private StateMachine<PlayerPresenter> _fsm;

        public Vector2  MouseWorldPos { get; private set; }
        public Vector2  PlayerPos { get; private set; }
        public Vector2  Velocity { get; private set; }
        public bool     ShouldFlip { get; private set; }
        public bool     CanMove { get; private set; }

        void Awake()
        {
            _input       = GetComponent<PlayerInputHandler>( );
            _view        = GetComponent<PlayerView>( );
            _model       = GetComponent<PlayerModel>( );
            _handManager = GetComponent<HandManager>( );
            
            _camera = Camera.main;
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        { 
            _fsm = new( );
            _fsm.ChangeMainState( new PlayerIdleState( this ) );
            _fsm.AddSubState(new FlipSubState( this ) );
            _fsm.AddSubState( new AimSubState( this ) );

            _input.OnRightClick += RightClicked;
            _input.OnLeftClick  += LeftClicked;
        }
        private void OnDestroy()
        {
            _input.OnRightClick -= RightClicked;
            _input.OnLeftClick  -= LeftClicked;
        }
        // Update is called once per frame
        void Update()
        {
            UpdateCacheData();
            _fsm.Execute( Time.deltaTime );
        }
        private void UpdateCacheData()
        {
            PlayerPos = transform.position;

            MouseWorldPos = _camera.ScreenToWorldPoint( _input.MousePos );
            
            Velocity = (_input.MoveDirMag < 0.01f) ? Vector2.zero 
                : _input.MoveDir * _model.MoveSpeed;

            ShouldFlip = MouseWorldPos.x < PlayerPos.x;
        }

        public void RequestFlip( bool flip )
        {
            _view.SetFlipX( flip );
            _handManager.SetFlipX( flip );
        }
        public void RequestLookAt(Vector2 pos )
            => _handManager.LookAt( pos );
        public void ChangeMainState(PlayerStateType type)
            => _fsm.ChangeMainState( CreateState( type ) );
        public void ChangeHandState(HandType type, HandStateType stateType)
            => _handManager.ChangeHandState( type, stateType );
        public void SetVelocity(Vector2 velocity)
            => _view.SetVelocity( velocity );

        private void LeftClicked()
        {
            Debug.Log( "Left Clicked" );
            ChangeMainState( PlayerStateType.Attack);
            ChangeHandState( HandType.Left, HandStateType.Attack );
        }
        private void RightClicked()
        {
            Debug.Log( "Right Clicked" );
            ChangeMainState( PlayerStateType.Attack );
            ChangeHandState( HandType.Right, HandStateType.Attack );
        }

        State<PlayerPresenter> CreateState(PlayerStateType type)
            => type switch
            {
                PlayerStateType.Idle    => new PlayerIdleState( this ),
                PlayerStateType.Walk    => new PlayerWalkState( this ),
                PlayerStateType.Attack  => new PlayerAttackState( this ),
                _ => throw new ArgumentOutOfRangeException( nameof( type ), type, null )
            };
    }
}