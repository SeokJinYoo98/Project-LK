using UnityEngine;

using GameSystem.MVPC;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class HandView : MonoBehaviour
{
    private SpriteRenderer  _sr;
    private Animator        _anim;
    [SerializeField] private Vector3 _animOffset = Vector3.zero;
    private Vector3 _defaultPos;
    private Vector3 _swapPos;

    private void Awake()
    {
        _sr         = GetComponent<SpriteRenderer>();
        _anim       = GetComponent<Animator>();
        _defaultPos = transform.position;
    }
    private void Update()
    {
        Vector3 pos = _animOffset;
        if (_sr.flipX) pos += _swapPos;
        else pos += _defaultPos;

        transform.localPosition = pos;
    }
    public void FlipX(bool flipX)
    {
        if (_sr.flipX == flipX) return;
        _sr.flipX = flipX;
    }
    public void LookAt(Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = _sr.flipX
            ? Quaternion.Euler( 0, 0, angle + 180f )
            : Quaternion.Euler( 0, 0, angle );
    }
    public void SetSwapPos(Vector3 swapPos)
    {
        _swapPos = swapPos;
        _swapPos.x -= 0.1f;
    }

    public void SetAnim(string name, bool activate)
    {
        _anim.SetBool( "Move", activate );
    }
}
