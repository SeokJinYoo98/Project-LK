using UnityEngine;
using WeaponSystem;

using Common.Interface.MVPC;
namespace HandSystem
{
    public class HandModel : IModel
    {
        private IWeapon _weapon = null;
        public IWeapon Weapon => _weapon;
    }
}

