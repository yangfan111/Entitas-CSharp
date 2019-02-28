using App.Shared.Audio;
using App.Shared.GameModules.Weapon;
using Core;
using Core.EntityComponent;
using Core.Utils;

namespace App.Shared.GameModeLogic.WeaponActionListener
{
    public class NormalWeaponActionListener : IWeaponProcessListener
    {
        private static readonly LoggerAdapter Logger = new LoggerAdapter(typeof(NormalWeaponActionListener));
        public void OnExpend(IPlayerWeaponGetter controller, EWeaponSlotType slot)
        {
            //TODO:音频播放
           //GameAudioMedium.PlayWeaponAudio(controller., RelatedAppearence.WeaponHandObject(), (item) => item.Fire);

            if (!slot.IsSlotChangeByCost())
            {
                return;
            }
<<<<<<< HEAD
            if(Logger.IsDebugEnabled)
            {
                Logger.DebugFormat("{0} OnExpend", controller.Owner);
            }
            ((PlayerWeaponController)controller).BagLockState = true;
=======
            var player = playerEntity as PlayerEntity;
            if(Logger.IsDebugEnabled)
            {
                Logger.DebugFormat("{0} OnExpend", player.entityKey.Value);
            }
            LockPlayerBag(player);
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        }

        public void OnDrop(IPlayerWeaponGetter controller, EWeaponSlotType slot, EntityKey key)
        {
            Logger.DebugFormat("{0} OnDrop", controller.Owner);
            ((PlayerWeaponController)controller).BagLockState = true;
        }

        public void OnPickup(IPlayerWeaponGetter controller, EWeaponSlotType slot)
        {
            Logger.DebugFormat("{0} OnPickup", controller.Owner);
            ((PlayerWeaponController)controller).BagLockState = true;
        }
        private void LockPlayerBag(IPlayerWeaponGetter controller)
        {
            Logger.DebugFormat("{0} LockPlayerBag", controller.Owner);
            ((PlayerWeaponController)controller).BagLockState = true;

        }
    }
}
