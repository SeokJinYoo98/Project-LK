using UnityEngine;
using GameSystem.MVP;

[RequireComponent(
    typeof(PlayerInputView), 
    typeof(PlayerModel),
    typeof(PlayerView)
    )]
public class PlayerPresenter : MonoBehaviour, IPresenter
{
    private IInputView   _inputView;
    private IView        _view;
    private PlayerModel  _model;
    private Camera       _camera;
    void Awake()
    {
        _inputView = GetComponent<PlayerInputView>();
        _view      = GetComponent<PlayerView>();
        _model     = GetComponent<PlayerModel>();

        _camera = Camera.main;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateView();
    }

    void FixedUpdate()
    {
        FixedUpdateView( );
    }

    void FixedUpdateView()
    {
        Vector2 velocity = _inputView.MoveDir * _model.MoveSpeed;
        _view.MoveTo( velocity );
    }
    void UpdateView()
    {
        Vector2 mousePos = MouseWorldPos();
        _view.LookAt( mousePos );
    }

    Vector2 MouseWorldPos()
    {
        Vector2 screenPos = _inputView.MousePos;
        return _camera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0f));
    }
}
