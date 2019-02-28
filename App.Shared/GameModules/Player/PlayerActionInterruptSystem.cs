using App.Shared.GameModules.Weapon;
using App.Shared.Util;
using Core.GameModule.Interface;
using Core.Prediction.UserPrediction.Cmd;

namespace App.Shared.GameModules.Player
{
    public class PlayerActionInterruptSystem : IUserCmdExecuteSystem
    {
        public void ExecuteUserCmd(IUserCmdOwner owner, IUserCmd cmd)
        {
            if (!cmd.IsInterrupt)
            {
                return;
            }
<<<<<<< HEAD
            var controller = GameModuleManagement.Get<PlayerWeaponController>(owner.OwnerEntityKey.EntityId);
=======
            var controller = GameModuleManagement.Get<PlayerWeaponController>(owner.OwnerEntityKey.GetHashCode());
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
            controller.Interrupt();
        }
    }
}
