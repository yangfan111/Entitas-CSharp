﻿using App.Shared.GameModules.Weapon;
using Core;
using Core.GameModule.Interface;
using Core.Prediction.UserPrediction.Cmd;
using Core.Utils;

namespace App.Shared.GameModules.Player
{
    public class PlayerGrenadeInventorySyncSystem : IUserCmdExecuteSystem
    {
        private static readonly LoggerAdapter Logger = new LoggerAdapter(typeof(PlayerGrenadeInventorySyncSystem));
        public void ExecuteUserCmd(IUserCmdOwner owner, IUserCmd cmd)
        {
            var player = owner.OwnerEntity as PlayerEntity;
            if(null == player)
            {
                Logger.Error("player entity is null");
                return;
            }
<<<<<<< HEAD
            IGrenadeCacheHelper helper = player.WeaponController().GetBagCacheHelper(EWeaponSlotType.ThrowingWeapon);
=======
            Weapon.IBagDataCacheHelper helper = player.WeaponController().GetBagCacheHelper(EWeaponSlotType.ThrowingWeapon);
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
            helper.Rewind();
        }
    }
}
