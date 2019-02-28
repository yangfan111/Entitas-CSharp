﻿using App.Shared.GameModules.Weapon;
using Core.GameModeLogic;
using Entitas;

namespace App.Shared.GameModeLogic.WeaponInitLoigc
{
    public class DummyWeaponInitLogic : IWeaponInitLogic
    {
        public bool IsBagSwithEnabled(PlayerWeaponController controller)
        {
            return false;
        }

        public void InitDefaultWeapon(Entity playerEntity)
        {
            //DO Nothing
        }

        public void InitDefaultWeapon(Entity playerEntity, int index)
        {
            //DO Nothing
        }

        public void ResetWeaponWithBagIndex(int index, Entity playerEntity)
        {
            //DO Nothing
        }
    }
}
