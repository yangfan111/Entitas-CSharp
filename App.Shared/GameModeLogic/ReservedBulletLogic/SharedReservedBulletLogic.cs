﻿using Assets.Utils.Configuration;
using Core;
using Core;
using Core.Utils;
using Entitas;
using WeaponConfigNs;
using App.Shared.GameModules.Weapon;
using Utils.Singleton;

namespace App.Shared.GameModeLogic.ReservedBulletLogic
{
    public class SharedReservedBulletLogic : IReservedBulletLogic
    {
        private static readonly LoggerAdapter Logger = new LoggerAdapter(typeof(SharedReservedBulletLogic));
<<<<<<< HEAD
        private const int BulletLimit = int.MaxValue;
        private Contexts _contexts;
        public SharedReservedBulletLogic(Contexts contexts)
        {
            _contexts = contexts;
=======
        private IWeaponConfigManager _weaponConfigManager;
        private const int BulletLimit = int.MaxValue;
        private Contexts _contexts;
        public SharedReservedBulletLogic(Contexts contexts, IWeaponConfigManager weaponConfigManager)
        {
            _contexts = contexts;
            _weaponConfigManager = weaponConfigManager;
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        }

        public int SetReservedBullet(IPlayerWeaponGetter controller, EBulletCaliber caliber, int count)
        {
            var relatedAmmunition = (controller as PlayerWeaponController).RelatedAmmunition;
            switch (caliber)
            {
                case EBulletCaliber.E12No:
                    return DoSetReserveBullet(ref relatedAmmunition.ReservedBullet12, count);
                case EBulletCaliber.E300Mag:
                    return DoSetReserveBullet(ref relatedAmmunition.ReservedBullet300, count);
                case EBulletCaliber.E45apc:
                    return DoSetReserveBullet(ref relatedAmmunition.ReservedBullet45, count);
                case EBulletCaliber.E50AE:
                    return DoSetReserveBullet(ref relatedAmmunition.ReservedBullet50, count);
                case EBulletCaliber.E556mm:
                    return DoSetReserveBullet(ref relatedAmmunition.ReservedBullet556, count);
                case EBulletCaliber.E762mm:
                    return DoSetReserveBullet(ref relatedAmmunition.ReservedBullet762, count);
                case EBulletCaliber.E9mm:
                    return DoSetReserveBullet(ref relatedAmmunition.ReservedBullet9, count);
                case EBulletCaliber.E57mm:
                    return DoSetReserveBullet(ref relatedAmmunition.ReservedBullet57, count);
                default:
                    Logger.ErrorFormat("Illegal caliber {0}", caliber);
                    return 0;
            }
        }

        public int SetReservedBullet(IPlayerWeaponGetter controller, EWeaponSlotType slot, int count)
        {
            var relatedAmmunition = (controller as PlayerWeaponController).RelatedAmmunition;

            var caliber = GetCaliber(controller as PlayerWeaponController, slot);
            return SetReservedBullet(controller, caliber, count);
        }

        private int DoSetReserveBullet( ref int bagReservedBullet,  int count)
        {
            if(count > BulletLimit)
            {
                bagReservedBullet = BulletLimit;
                return count - BulletLimit;
            }
            else
            {
                bagReservedBullet = count;
                return 0;
            }
        }

        public int GetReservedBullet(IPlayerWeaponGetter controller, EBulletCaliber caliber)
        {
            var relatedAmmunition = (controller as PlayerWeaponController).RelatedAmmunition;

            switch (caliber)
            {
                case EBulletCaliber.E12No:
                    return relatedAmmunition.ReservedBullet12;
                case EBulletCaliber.E300Mag:
                    return relatedAmmunition.ReservedBullet300;
                case EBulletCaliber.E45apc:
                    return relatedAmmunition.ReservedBullet45;
                case EBulletCaliber.E50AE:
                    return relatedAmmunition.ReservedBullet50;
                case EBulletCaliber.E556mm:
                    return relatedAmmunition.ReservedBullet556;
                case EBulletCaliber.E762mm:
                    return relatedAmmunition.ReservedBullet762;
                case EBulletCaliber.E9mm:
                    return relatedAmmunition.ReservedBullet9;
                case EBulletCaliber.E57mm:
                    return relatedAmmunition.ReservedBullet57;
                default:
                    Logger.ErrorFormat("Illegal caliber {0}", caliber);
                    return 0;
            }
        }

        public int GetReservedBullet(IPlayerWeaponGetter controller, EWeaponSlotType slot)
        {
            var caliber = GetCaliber(controller as PlayerWeaponController, slot);
            return GetReservedBullet(controller, caliber);
        }

        private EBulletCaliber GetCaliber(PlayerWeaponController controller, EWeaponSlotType slot)
        {
<<<<<<< HEAD
            var weapon = controller.GetWeaponAgent(slot);
            if (weapon.ConfigId>0)
=======
            var playerEntity = entity as PlayerEntity;
            var weapon = playerEntity.WeaponController().GetSlotWeaponInfo(_contexts, slot);
            var weaponId = weapon.ConfigId;
            if (weaponId > 0)
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
            {
                
                var cfg =   SingletonManager.Get<WeaponResourceConfigManager>().GetConfigById(weapon.ConfigId);
                return (EBulletCaliber)cfg.Caliber;
            }
            Logger.ErrorFormat("no weapon in slot {0} !!", slot);
            return EBulletCaliber.Length;
        }
    }
}
