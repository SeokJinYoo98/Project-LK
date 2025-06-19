using UnityEngine;
using GameSystem.MVPC;

[RequireComponent( typeof( PlayerInputView ) )]
[RequireComponent( typeof( PlayerModel ) )]
[RequireComponent( typeof( PlayerView ) )]
[RequireComponent( typeof( HandSystem ) )]
public class PlayerPresenter : MonoBehaviour, IPresenter
{
    private IInputView   _inputView;
    private IView        _view;
    private PlayerModel  _model;
    private Camera       _camera;
    private HandSystem   _handSystem;
    void Awake()
    {
        _inputView  = GetComponent<PlayerInputView>();
        _view       = GetComponent<PlayerView>();
        _model      = GetComponent<PlayerModel>();
        _handSystem = GetComponent<HandSystem>();
        _camera = Camera.main;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = MouseWorldPos( );
        _handSystem.LookAt( mousePos );

        bool    flipX = ShouldFlip( mousePos );
        _view.SetFlipX( flipX );
        _handSystem.SetFlipX( flipX );

        Vector2 velocity = Velocity( );
        _view.MoveTo( velocity );
    }
    bool ShouldFlip(Vector2 targetPos) => transform.position.x > targetPos.x;
    Vector2 Velocity() => _inputView.MoveDir * _model.MoveSpeed;
    Vector2 MouseWorldPos()
    {
        Vector2 screenPos = _inputView.MousePos;
        return _camera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0f));
    }
}
