using UnityEngine;

using Common.Interface.MVPC;

namespace HandSystem
{

    [RequireComponent( typeof( SpriteRenderer ) )]
    [RequireComponent( typeof( Animator ) )]
    public class HandView : MonoBehaviour
    {
        private SpriteRenderer  _sr;
        private Animator        _anim;

        private void Awake()
        {
            _sr = GetComponent<SpriteRenderer>( );
            _anim = GetComponent<Animator>( );
        }
        public void FlipX(bool flipX)
            => _sr.flipX = flipX;
        public void LookAt(Vector2 dir)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = _sr.flipX
                ? Quaternion.Euler( 0, 0, angle + 180f )
                : Quaternion.Euler( 0, 0, angle );
        }

        public void SetAnim(string name, bool activate)
        {
            _anim.SetBool( name, activate );
        }
        public void SetAnim(string name, HandStateType type)
            => _anim.SetInteger( name, (int)type );
    }
}