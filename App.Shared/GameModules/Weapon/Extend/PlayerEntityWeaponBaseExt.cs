using App.Shared.Components.Player;
using App.Shared.GameModules.Weapon;
<<<<<<< HEAD
using Assets.App.Shared.EntityFactory;
using Core;
using Core.EntityComponent;
=======
using Core;
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
using Core.Utils;
using System.Collections.Generic;
using XmlConfig;

namespace App.Shared
{
    /// <summary>
    /// Defines the <see cref="PlayerEntityWeaponBaseExt" />
    /// </summary>
    public static class PlayerEntityWeaponBaseExt
    {
        private static readonly LoggerAdapter Logger = new LoggerAdapter(typeof(PlayerEntityWeaponBaseExt));

        /// <summary>
        /// 添加武器组件控制器相关 
        /// </summary>
        /// <param name="player"></param>
        public static void AttachWeaponComponentBehavior(this PlayerEntity player, Contexts contexts, List<int> greandeIds)
        {
<<<<<<< HEAD
            //    GameModuleManagement.Dispose();
            GameModuleManagement.ForceAllocate(player.entityKey.Value.EntityId, (PlayerWeaponController controller) =>
              {

                  var playerWeaponAgent = new PlayerWeaponComponentsAgent(player.FindWeaponBagSetComponent,
                      player.FindOverrideBagComponent, player.FindWeaponAuxiliaryComponent, player.FindCustomizeData);
=======
            int cookie = player.entityKey.Value.GetHashCode();
            GameModuleManagement.Dispose();
            GameModuleManagement.Allocate(cookie, (PlayerWeaponController controller) =>
              {

                  var playerWeaponAgent = new PlayerWeaponComponentsAgent(player.FindWeaponBagSetComponent,
                      player.FindOverrideBagComponent, player.FindWeaponAuxiliaryComponent);
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a

                  var weaponInteract = new PlayerEntityWeaponInteract(controller, player);
                  controller.SetOwner(player.entityKey.Value);
                  controller.SetPlayerWeaponAgent(playerWeaponAgent);
                  controller.SetInteract(weaponInteract);
<<<<<<< HEAD

                  //controller.SetWeaponContext(contexts.weapon);
#if UNITY_EDITOR
                  player.AddPlayerDebug(controller);
#endif
                  // controller.SetConfigManager(contexts.session.commonSession.PlayerWeaponResourceConfigManager);
                  controller.SetProcessListener(player.modeLogic.ModeLogic)
                  ;
                  var greandeHelper = new GrenadeSlotHelper(() => { return FindGrenadeCacheData(player); }, () => { return FindGrenadeWeaponEntity(player); }, greandeIds);
                  var commonHelper = new CommonSlotHelper(() => { return FindEmptyWeaponEntity(player); });
                  for (EWeaponSlotType i =EWeaponSlotType.None+1;i< EWeaponSlotType.Length;i++)
                  {
                      if(i == EWeaponSlotType.ThrowingWeapon)
;                       controller.SetSlotHelper(i, greandeHelper);
                      else
                         controller.SetSlotHelper(i, commonHelper);

                  }
=======
                  controller.SetWeaponContext(contexts.weapon);
                  controller.SetConfigManager(contexts.session.commonSession.PlayerWeaponConfigManager);
                  controller.SetProcessListener(player.modeLogic.ModeLogic);
                  var helper = new GrenadeBagCacheHelper(() => { return FindGrenadeCacheData(player); }, greandeIds);
                  controller.SetBagCacheHelper(EWeaponSlotType.ThrowingWeapon, helper);
                  playerWeaponAgent.SetController(controller);
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
                  controller.ResetAllComponents();
              });
        }

        public static void AttachGrenadeCacheData(this PlayerEntity player, List<int> ids)
        {
<<<<<<< HEAD
            if (!player.hasGrenadeCacheData)
            {
                var arrs = WeaponUtil.CreateEmptyGrenadeCacheArrs(ids);
                player.AddGrenadeCacheData(arrs);
            }


=======
            var arrs = WeaponUtil.CreateEmptyGrenadeCacheArrs(ids);
            player.AddGrenadeCacheData(arrs);
        }

        public static T GetController<T>(this PlayerEntity player) where T : ModuleLogicActivator<T>, new()
        {
            return GameModuleManagement.Get<T>(player.entityKey.Value.EntityId);
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        }

        public static PlayerWeaponController WeaponController(this PlayerEntity player)
        {
            return GameModuleManagement.Get<PlayerWeaponController>(player.entityKey.Value.EntityId);
        }

<<<<<<< HEAD
        public static ISharedPlayerWeaponGetter WeaponGetter(this PlayerEntity player)
        {
            return GameModuleManagement.Get<PlayerWeaponController>(player.entityKey.Value.EntityId);
=======
        public static ISharedPlayerWeaponComponentGetter WeaponAPI(this PlayerEntity player)
        {
            return GameModuleManagement.Get<PlayerWeaponController>(player.entityKey.Value.EntityId).Getter;
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        }

        /// <summary>
        /// 玩家武器背包添加
        /// </summary>
        /// <param name="player"></param>
<<<<<<< HEAD
        public static void AttachPlayerWeaponBags(this PlayerEntity player)
        {
            var containers = WeaponUtil.CreateEmptyBagContainers();
            if (!player.hasPlayerWeaponBagSet)
            {
                player.AddPlayerWeaponBagSet(containers);
            }
            else
            {
                player.ReplacePlayerWeaponBagSet(containers);
            }

        }
        public static void AttachPlayerAmmu(this PlayerEntity player)
        {
            if(!player.hasPlayerWeaponAmmunition)
            {
                player.AddPlayerWeaponAmmunition();
            }
            else
            {
                player.ReplacePlayerWeaponAmmunition();
            }
=======
        public static void AttachPlayerWeaponBags(this PlayerEntity player, Contexts context)
        {
            var containers = WeaponUtil.CreateEmptyBagContainers();
            player.AddPlayerWeaponBagSet(containers);
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        }

        public static void AttachPlayerAux(this PlayerEntity player)
        {
<<<<<<< HEAD
            if (!player.hasPlayerWeaponAuxiliary)
            {
                player.AddPlayerWeaponAuxiliary();

            }
            else
            {
                player.ReplacePlayerWeaponAuxiliary();
            }
            player.playerWeaponAuxiliary.BulletList = new List<PlayerBulletData>();

        }
        public static PlayerWeaponCustomizeComponent AttachPlayerCustomize(this PlayerEntity player)
        {
            if (!player.hasPlayerWeaponCustomize)
            {
                player.AddPlayerWeaponCustomize(EntityKey.Default, EntityKey.Default);
            }
            return player.playerWeaponCustomize;
            //else
            //{
            //    player.ReplacePlayerWeaponCustomize(EntityKey.Default, EntityKey.Default);

            //}
        }
=======
            player.AddPlayerWeaponAuxiliary(new List<PlayerBulletData>(), new List<EClientEffectType>());
        }

>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        /// <summary>
        /// 获取角色背包
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private static PlayerWeaponBagSetComponent FindWeaponBagSetComponent(this PlayerEntity player)
        {
<<<<<<< HEAD
            if (!player.hasPlayerWeaponBagSet)
            {
                player.AddPlayerWeaponBagSet(WeaponUtil.CreateEmptyBagContainers());

            }
=======
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
            return player.playerWeaponBagSet;
        }

        /// <summary>
        /// 获取后坐力
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private static OverrideBagComponent FindOverrideBagComponent(this PlayerEntity player)
        {
<<<<<<< HEAD

=======
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
            return player.overrideBag;
        }

        /// <summary>
        /// 获取本地缓存
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private static PlayerWeaponAuxiliaryComponent FindWeaponAuxiliaryComponent(this PlayerEntity player)
        {
            return player.playerWeaponAuxiliary;
        }

        /// <summary>
        /// 获取手雷信息
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private static GrenadeCacheDataComponent FindGrenadeCacheData(this PlayerEntity player)
        {
<<<<<<< HEAD
            if (!player.hasGrenadeCacheData)
            {
                var greandeIds = WeaponUtil.ForeachFilterGreandeIds();
                var arrs = WeaponUtil.CreateEmptyGrenadeCacheArrs(greandeIds);
                player.AddGrenadeCacheData(arrs);

            }
            return player.grenadeCacheData;
        }

        private static PlayerWeaponCustomizeComponent FindCustomizeData(this PlayerEntity player)
        {
            return player.playerWeaponCustomize;
        }
        private static WeaponEntity FindGrenadeWeaponEntity(this PlayerEntity player)
        {
            return WeaponEntityFactory.GetWeaponEntity(player.playerWeaponCustomize.GrenadeConstWeaponKey);
        }
        private static WeaponEntity FindEmptyWeaponEntity(this PlayerEntity player)
        {
            return WeaponEntityFactory.GetWeaponEntity(player.playerWeaponCustomize.EmptyConstWeaponkey);
        }
=======
            return player.grenadeCacheData;
        }

>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        [System.Obsolete]
        public static void PlayWeaponSound(this PlayerEntity playerEntity, EWeaponSoundType sound)
        {
            if (playerEntity.hasWeaponSound)
            {
                playerEntity.weaponSound.PlayList.Add(sound);
            }
        }
    }
}
