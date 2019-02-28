<<<<<<< HEAD
﻿using App.Shared.GameModules.Weapon;
using Core;
=======
﻿using Core;
using App.Shared.GameModules.Weapon;
using Core.GameModeLogic;
using Entitas;
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
using WeaponConfigNs;

namespace App.Shared.GameModeLogic.ReservedBulletLogic
{
    /// <summary>
    /// Defines the <see cref="LocalReservedBulletLogic" />
    /// </summary>
    public class LocalReservedBulletLogic : IReservedBulletLogic
    {
        private Contexts _contexts;
<<<<<<< HEAD

=======
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        public LocalReservedBulletLogic(Contexts contexts)
        {
            _contexts = contexts;
        }

<<<<<<< HEAD
        public int GetReservedBullet(IPlayerWeaponGetter controller, EWeaponSlotType slot)
        {
            return GetReservedBullet((PlayerWeaponController)controller, slot);
        }

        private int GetReservedBullet(PlayerWeaponController controller, EWeaponSlotType slot)
        {
            return controller.HeldWeaponAgent.BaseComponent.ReservedBullet;
=======
        public int GetReservedBullet(Entity entity, EWeaponSlotType slot)
        {
            var playerEntity = entity as PlayerEntity;
            var weaponComp = playerEntity.GetWeaponData(_contexts, slot);
            return weaponComp.ReservedBullet;
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        }

        public int GetReservedBullet(IPlayerWeaponGetter controller, EBulletCaliber caliber)
        {
            return 0;
        }

        public int SetReservedBullet(IPlayerWeaponGetter controller, EWeaponSlotType slot, int count)
        {
            return SetReservedBullet((PlayerWeaponController)controller, slot, count);
        }

        private int SetReservedBullet(PlayerWeaponController controller, EWeaponSlotType slot, int count)
        {
<<<<<<< HEAD
            controller.HeldWeaponAgent.BaseComponent.ReservedBullet = count;
=======
            var playerEntity = entity as PlayerEntity;
            var weaponData = playerEntity.GetWeaponData(_contexts, slot);
            weaponData.ReservedBullet = count;
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
            return count;
        }

        public int SetReservedBullet(IPlayerWeaponGetter controller, EBulletCaliber caliber, int count)
        {
            return count;
        }
    }
}
