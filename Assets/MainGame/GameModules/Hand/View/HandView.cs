using UnityEngine;

using GameSystem.MVPC;

[RequireComponent(typeof(SpriteRenderer))]
public class HandView : MonoBehaviour
{
    private SpriteRenderer _sr;

    private Vector3 _defaultPos;
    private Vector3 _swapPos;
    
    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _defaultPos = transform.position;
    }
    public void FlipX(bool flipX)
    {
        if (_sr.flipX == flipX) return;

        _sr.flipX = flipX;
        if (flipX) transform.localPosition = _swapPos;
        else transform.localPosition = _defaultPos;
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
}
