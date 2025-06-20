using Common.Interfaces.Equipment;
using GameSystem.MVPC;
using UnityEngine;
public enum HandType { Left, Right }
[RequireComponent(typeof(HandModel))]
[RequireComponent(typeof(HandView))]
public class HandController : MonoBehaviour, IEquippable
{
    [SerializeField] private HandType _handType;
    public HandType Type => _handType;

    private HandModel      _handModel;
    private HandView       _handView;
    
    private void Awake()
    {
        _handView   = GetComponent<HandView>();
        _handModel  = GetComponent<HandModel>();
    }
    public IEquipment CurrentEquipment => throw new System.NotImplementedException( );
    public void Equip(IEquipment equipment)
    {
        throw new System.NotImplementedException( );
    }

    public void Unequip()
    {
        throw new System.NotImplementedException( );
    }
    public void SetFlipX(bool flip)
    {
        _handView.FlipX(flip);
    }
    public void LookAt(Vector2 targetPos)
    {
        Vector2 direction = targetPos - (Vector2)transform.position;
        _handView.LookAt( direction );
    }
    public void TempFunc(Vector2 velocity)
    {
        if (velocity.sqrMagnitude < 0.01f)
            _handView.SetAnim( "Walk", false );
        else
            _handView.SetAnim( "Walk", true );
    }
    public void SetSwapPos(Vector3 pos) => _handView.SetSwapPos( pos );
}
