using UnityEngine;

namespace Common.Interfaces.Equipment
{
    public interface IEquipment
    {

    }
    public interface IEquippable
    {
        void Equip(IEquipment equipment);
        void Unequip();
        IEquipment CurrentEquipment { get; }
    }
}