using GameSystem.MVPC;
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
    }
    public void MoveTo(Vector2 velocity)
    {
        float mag = velocity.magnitude;
        _animator.SetFloat( "MoveSpeed", mag );
        _rb.linearVelocity = velocity;
    }
    public void SetFlipX(bool flipX)
    {
        if (_sr.flipX == flipX) return;

        _sr.flipX = flipX;
    }
}  
