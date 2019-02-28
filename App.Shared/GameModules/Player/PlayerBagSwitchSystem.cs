using App.Shared.GameModules.Weapon;
using Core.GameModule.Interface;
using Core.GameModule.System;
using Core.Prediction.UserPrediction.Cmd;

namespace App.Shared.GameModules.Player
{
    public class PlayerBagSwitchSystem : IUserCmdExecuteSystem
    {
        private ICommonSessionObjects _commonSessionObjects;
        public PlayerBagSwitchSystem(ICommonSessionObjects commonSessionObjects)
        {
            _commonSessionObjects = commonSessionObjects;
        }

        public void ExecuteUserCmd(IUserCmdOwner owner, IUserCmd cmd)
        {
            
            if (cmd.BagIndex > 0)
            {
                var player = owner.OwnerEntity as PlayerEntity;
<<<<<<< HEAD
                var controller = GameModuleManagement.Get<PlayerWeaponController>(owner.OwnerEntityKey.EntityId);
                if(!controller.RelatedModelLogic.CanModeSwitchBag(controller))
=======
                var controller = GameModuleManagement.Get<PlayerWeaponController>(owner.OwnerEntityKey.GetHashCode());
                if(!controller.RelatedModelLogic.IsBagSwithEnabled(controller))
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
                {
                    return;
                }
                var bags = player.playerInfo.WeaponBags;
                var realBagIndex = cmd.BagIndex -1;
                player.modeLogic.ModeLogic.RecoveryBagContainer(realBagIndex, controller);
            }
        }
    }
}
