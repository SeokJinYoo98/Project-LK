using UnityEngine;
using System.Collections.Generic;
public class HandSystem : MonoBehaviour
{
    [SerializeField] private HandController _leftHand;
    [SerializeField] private HandController _rightHand;

    private List<HandController> _hands;
    private void Start()
    {
        _hands = new List<HandController> { _leftHand, _rightHand };
        _leftHand.SetSwapPos( _rightHand.transform.position );
        _rightHand.SetSwapPos( _leftHand.transform.position );
    }
    public void SetFlipX(bool flip)
    {
        foreach ( HandController hand in _hands ) 
            hand.SetFlipX(flip);
    }
    public void LookAt(Vector2 targetPos)
    {
        foreach( HandController hand in _hands )
            hand.LookAt(targetPos);
    }
    public void TempFunc(Vector2 velocity)
    {
        foreach (HandController hand in _hands)
            hand.TempFunc(velocity);
    }
}