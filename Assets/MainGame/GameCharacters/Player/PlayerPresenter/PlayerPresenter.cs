using Common.Interface.Handler;
using Common.Interface.MVPC;
using HandSystem;
using Player.States;
using System;
using UnityEngine;
using UnityEngine.XR;

namespace Player {
    [RequireComponent( typeof( PlayerInputHandler ) )]
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
        public bool     CanMove { get; private set; } = true;

        void Awake()
        {
            _input       = GetComponent<PlayerInputHandler>( );
            _view        = GetComponent<PlayerView>( );
            _handManager = GetComponent<HandManager>( );
            
            _camera = Camera.main;
            _model  = new PlayerModel( );
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
            Attack( HandType.Left );
        }
        private void RightClicked()
        {
            Debug.Log( "Right Clicked" );
            Attack(HandType.Right );
        }
        private void Attack(HandType type)
        {
            ChangeMainState(PlayerStateType.Attack);

            if (_fsm.MainState is IAttackableState attackState)
            {
                attackState.OnAttackInput( type );
                CanMove = false;
            }
                
        }
        public void EndAttack() => CanMove = true;
        public void SetAnimBool(string name,  bool value)
            => _view.SetAnimBool( name, value );
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