﻿using App.Shared.GameModules.Weapon;
using App.Shared.GameModules.Weapon.Behavior;
using Core.GameModule.Interface;
using Core.Prediction.UserPrediction.Cmd;
using Core.Utils;

namespace App.Shared.GameModules.Player
{
    public class PlayerAttackSystem : IUserCmdExecuteSystem
    {
        private static LoggerAdapter _logger = new LoggerAdapter(typeof(PlayerAttackSystem));
        private WeaponLogicManager _weaponLogicManager;
        private Contexts _contexts;

        public PlayerAttackSystem(Contexts contexts)
        {
            _contexts = contexts;
            _weaponLogicManager = contexts.session.commonSession.WeaponLogicManager as WeaponLogicManager;
        }

        public void ExecuteUserCmd(IUserCmdOwner owner, IUserCmd cmd)
        {
<<<<<<< HEAD
            var controller =  GameModuleManagement.Get<PlayerWeaponController>(owner.OwnerEntityKey.EntityId);
            var weaponId = controller.HeldWeaponAgent.ConfigId;
            if (weaponId < 1) return;
=======
            var controller =  GameModuleManagement.Get<PlayerWeaponController>(owner.OwnerEntityKey.GetHashCode());
            var weaponId = controller.HeldWeaponAgent.ConfigId;
            if (!weaponId.HasValue) return;
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
            var weaponLogic = _weaponLogicManager.GetWeaponLogic(weaponId);
            if(null != weaponLogic)
            {
                weaponLogic.Update(controller, cmd);
            }
        }
    }
}