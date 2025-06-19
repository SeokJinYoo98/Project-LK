using UnityEngine;
using System.Collections.Generic;
public class HandSystem : MonoBehaviour
{
    [SerializeField] private HandController _leftHand;
    [SerializeField] private HandController _rightHand;

    private void Start()
    {
        _leftHand.SetSwapPos( _rightHand.transform.position );
        _rightHand.SetSwapPos( _leftHand.transform.position );
    }
    public void SetFlipX(bool flip)
    {
        _leftHand.SetFlipX(flip);
        _rightHand.SetFlipX(flip);
    }
    public void LookAt(Vector2 targetPos)
    {
        _leftHand.LookAt(targetPos);
        _rightHand.LookAt(targetPos);
    }
}