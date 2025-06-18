using GameSystem.MVP;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

[RequireComponent(
    typeof(Rigidbody2D),
    typeof(Animator),
    typeof(SpriteRenderer)
    )]
public class PlayerView : MonoBehaviour, IView
{
    private SpriteRenderer  _sr;
    private Animator        _animator;

    private Vector2         _velocity = Vector2.zero;
    private Rigidbody2D     _rb;
    private void Awake()
    {
        _animator   = GetComponent<Animator>();
        _rb         = GetComponent<Rigidbody2D>();
        _sr         = GetComponent<SpriteRenderer>();
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
        _rb.linearVelocity = _velocity;
    }
    public void MoveTo(Vector2 velocity)
    {
        _velocity = velocity;
        _animator.SetFloat( "MoveSpeed", velocity.magnitude );
    }
    public void LookAt(Vector2 mousePos)
    {
        bool isFlip = (transform.position.x < mousePos.x) ? false : true;
        if (_sr.flipX == isFlip) return;

        _sr.flipX = isFlip;
        _animator.SetBool( "FlipX", isFlip );
    }
}  
