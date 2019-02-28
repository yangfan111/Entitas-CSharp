using Core.GameModule.Interface;
using Core.GameModule.System;
using Core.Prediction.UserPrediction.Cmd;
using Core.Utils;
using Entitas;
using XmlConfig;
using Core.WeaponLogic;
using UnityEngine;
using Core;
using WeaponConfigNs;
using Assets.Utils.Configuration;
using Core.GameInputFilter;
using Assets.XmlConfig;
using Core.Common;
using com.wd.free.@event;
using App.Shared.FreeFramework.framework.@event;
using Utils.Singleton;
using App.Shared.GameModules.Weapon;

namespace App.Shared.GameModules.GamePlay.SimpleTest
{
    //TODO 移到firelogic
    public class SimpleLoadBulletSystem : IUserCmdExecuteSystem
    {
        private static LoggerAdapter _logger = new LoggerAdapter(typeof(SimpleLoadBulletSystem));

        private bool _reloading;
        private IFilteredInput _filteredResult;
        private Contexts _contexts;
        private ICommonSessionObjects _sessonObjects;

        public SimpleLoadBulletSystem(Contexts contexts, ICommonSessionObjects sessionObjects)
        {
            this._contexts = contexts;
            this._sessonObjects = sessionObjects;
        }


        public void ExecuteUserCmd(IUserCmdOwner owner, IUserCmd cmd)
        {
            PlayerEntity player = (PlayerEntity)owner.OwnerEntity;
<<<<<<< HEAD
            PlayerWeaponController controller = player.WeaponController();
=======
            ISharedPlayerWeaponComponentGetter sharedWeaponAPI = player.WeaponController();
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
            if (cmd.IsReload)// && cmd.FilteredInput.IsInput(EPlayerInput.IsReload))
            {
                if (!cmd.FilteredInput.IsInput(EPlayerInput.IsReload) && !player.playerMove.IsAutoRun)
                {
                    return;
                }

<<<<<<< HEAD
                var heldAgent = controller.HeldWeaponAgent;
                if (!heldAgent.IsVailed()) return;
                var config = SingletonManager.Get<WeaponResourceConfigManager>().GetConfigById(controller.HeldWeaponAgent.ConfigId);
=======

                WeaponScanStruct? currWeapon = sharedWeaponAPI.HeldWeaponScan;
                if (!currWeapon.HasValue) return;
                var config = SingletonManager.Get<WeaponConfigManager>().GetConfigById(currWeapon.Value.ConfigId);
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
                if (NoReloadAction(config))
                {
                    return;
                }
<<<<<<< HEAD
                var commonfireConfig = heldAgent.CommonFireCfg;
                if (commonfireConfig == null) return;
                if (heldAgent.BaseComponent.Bullet >= heldAgent.CommonFireCfg.MagazineCapacity)
                {
                    return;
                }
                if (HasNoReservedBullet(controller, player))
=======
                PlayerWeaponController controller = player.WeaponController();
                var configAssy = controller.HeldWeaponLogicConfigAssy;
                if (null == configAssy)
                {
                    return;
                }
                if (currWeapon.Value.Bullet >= configAssy.CommonFireCfg.MagazineCapacity)
                {
                    return;
                }
                if (HasNoReservedBullet(sharedWeaponAPI, player))
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
                {
                    return;
                }
                if (!_reloading)
                {
                    player.PlayWeaponSound(EWeaponSoundType.ClipDrop);
                    _reloading = true;
                }

<<<<<<< HEAD
                var reloadSpeed = heldAgent.ReloadSpeed;
=======
                var weaponData = sharedWeaponAPI.HeldWeaponScan.Value;
                var commonfireConfig = configAssy.CommonFireCfg;
                var reloadSpeed = configAssy.GetReloadSpeed();
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
                reloadSpeed = Mathf.Max(0.1f, reloadSpeed);

                player.autoMoveInterface.PlayerAutoMove.StopAutoMove();

                player.animatorClip.ClipManager.SetReloadSpeedBuff(reloadSpeed);
                if (commonfireConfig.SpecialReloadCount > 0)
                {
<<<<<<< HEAD
                    var target = commonfireConfig.MagazineCapacity - heldAgent.BaseComponent.Bullet;
=======
                    var target = commonfireConfig.MagazineCapacity - weaponData.Bullet;
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
                    target = Mathf.Min(target, player.WeaponController().GetReservedBullet());
                    var count = Mathf.CeilToInt((float)target / commonfireConfig.SpecialReloadCount);
                    count = Mathf.Max(1, count);
                    player.stateInterface.State.SpecialReload(
                        () =>
                        {
<<<<<<< HEAD
                            SpecialReload(controller);
=======
                            SpecialReload(_contexts, player);
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
                        },
                        count,
                        () =>
                        {//换弹结束回调
                            player.animatorClip.ClipManager.ResetReloadSpeedBuff();
                        });
                }
                else
                {
<<<<<<< HEAD
                    if (heldAgent.BaseComponent.Bullet > 0 && !heldAgent.IsWeaponEmptyReload)
                    {
                        var needActionDeal = CheckNeedActionDeal(controller, ActionDealEnum.Reload);
=======
                    if (weaponData.Bullet > 0 && !sharedWeaponAPI.IsHeldWeaponEmptyReload())
                    {
                        var needActionDeal = CheckNeedActionDeal(sharedWeaponAPI, ActionDealEnum.Reload);
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
                        if (needActionDeal)
                        {
                            player.appearanceInterface.Appearance.MountWeaponOnAlternativeLocator();
                        }
                        player.stateInterface.State.Reload(() =>
                        {
                            if (needActionDeal)
                            {
                                player.appearanceInterface.Appearance.RemountWeaponOnRightHand();
                            }
<<<<<<< HEAD
                            Reload(controller);
=======
                            Reload(_contexts, player);
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
                            player.animatorClip.ClipManager.ResetReloadSpeedBuff();
                            _reloading = false;
                        });
                    }
                    else
                    {
<<<<<<< HEAD
                        var needActionDeal = CheckNeedActionDeal(controller, ActionDealEnum.ReloadEmpty);
=======
                        var needActionDeal = CheckNeedActionDeal(sharedWeaponAPI, ActionDealEnum.ReloadEmpty);
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
                        if (needActionDeal)
                        {
                            player.appearanceInterface.Appearance.MountWeaponOnAlternativeLocator();
                        }
                        player.stateInterface.State.ReloadEmpty(() =>
                        {
                            if (needActionDeal)
                            {
                                player.appearanceInterface.Appearance.RemountWeaponOnRightHand();
                            }
<<<<<<< HEAD
                            Reload(controller);
=======
                            Reload(_contexts, player);
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
                            player.animatorClip.ClipManager.ResetReloadSpeedBuff();
                            _reloading = false;
                        });
                    }
                }
            }
        }

        private bool NoReloadAction(WeaponResConfigItem config)
        {
            if (null == config)
            {
                return true;
            }
            return config.Type == (int)EWeaponType_Config.ThrowWeapon || config.Type == (int)EWeaponType_Config.MeleeWeapon;
        }


        private bool HasNoReservedBullet(PlayerWeaponController controller, PlayerEntity playerEntity)
        {
            if (controller.GetReservedBullet() < 1)
            {
                if (SharedConfig.CurrentGameMode == Components.GameMode.Normal)
                {
                    playerEntity.tip.TipType = ETipType.BulletRunout;
                }
                else
                {
                    playerEntity.tip.TipType = ETipType.NoBulletInPackage;
                }
                return true;
            }
            return false;
        }

<<<<<<< HEAD
        private void SpecialReload(PlayerWeaponController controller)
        {
            var cfg = controller.HeldWeaponAgent.CommonFireCfg;
            var loadCount = cfg.SpecialReloadCount;
            var target = cfg.MagazineCapacity - controller.HeldWeaponAgent.BaseComponent.Bullet;
            loadCount = Mathf.Min(loadCount, target);
            DoRealod(controller, loadCount);
        }

        private void Reload(PlayerWeaponController controller)
        {
            var cfg = controller.HeldWeaponAgent.CommonFireCfg;
            var target = cfg.MagazineCapacity - controller.HeldWeaponAgent.BaseComponent.Bullet;
            target = Mathf.Max(0, target);
            DoRealod(controller, target);
        }

        private void DoRealod(PlayerWeaponController controller, int target)
        {
            //PlayerWeaponController controller = playerEntity.WeaponController();
            //var configAssy = controller.HeldWeaponLogicConfigAssy;
            var cfg = controller.HeldWeaponAgent.CommonFireCfg;
            var lastReservedBullet = controller.GetReservedBullet();
            target = Mathf.Min(target, lastReservedBullet);
            controller.HeldWeaponAgent.BaseComponent.Bullet += target;
            controller.SetReservedBullet(lastReservedBullet - target);
=======
        private void SpecialReload(Contexts contexts, PlayerEntity playerEntity)
        {
            PlayerWeaponController controller = playerEntity.WeaponController();
            var configAssy = controller.HeldWeaponLogicConfigAssy;
            if (configAssy == null) return;
            WeaponScanStruct weaponScan = controller.HeldWeaponScan.Value;
            var commonFireConfig = configAssy.CommonFireCfg;
            var loadCount = commonFireConfig.SpecialReloadCount;
            var target = commonFireConfig.MagazineCapacity - weaponScan.Bullet;
            loadCount = Mathf.Min(loadCount, target);
            DoRealod(contexts, playerEntity, loadCount);
        }

        private void Reload(Contexts contexts, PlayerEntity playerEntity)
        {
            PlayerWeaponController controller = playerEntity.WeaponController();
            var configAssy = controller.HeldWeaponLogicConfigAssy;
            if (configAssy == null) return;
            var weaponData = controller.HeldWeaponScan.Value;

            var commonFireConfig = configAssy.CommonFireCfg;
            var target = commonFireConfig.MagazineCapacity - weaponData.Bullet;
            target = Mathf.Max(0, target);
            DoRealod(contexts, playerEntity, target);
        }

        private void DoRealod(Contexts contexts, PlayerEntity playerEntity, int target)
        {
            //PlayerWeaponController controller = playerEntity.WeaponController();
            //var configAssy = controller.HeldWeaponLogicConfigAssy;
            var weaponData = playerEntity.WeaponController().HeldWeaponScan;
            if (!weaponData.HasValue) return;
            var lastReservedBullet = playerEntity.WeaponController().GetReservedBullet();
            target = Mathf.Min(target, lastReservedBullet);
            //  weaponData.Value.Bullet += target;
            playerEntity.WeaponController().SetReservedBullet(lastReservedBullet - target);
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a

            IEventArgs args = (IEventArgs)(_sessonObjects).FreeArgs;

            if (!args.Triggers.IsEmpty((int)EGameEvent.WeaponState))
            {
                //TODO Implement
                //SimpleParaList dama = new SimpleParaList();
                //dama.AddFields(new ObjectFields(weaponState));
                //dama.AddPara(new IntPara("CarryClip", weaponState.ReservedBulletCount));
                //dama.AddPara(new IntPara("Clip", weaponState.LoadedBulletCount));
                //dama.AddPara(new IntPara("ClipType", (int)weaponState.Caliber));
                //dama.AddPara(new IntPara("id", (int)weaponState.CurrentWeapon));
                //SimpleParable sp = new SimpleParable(dama);

                //args.Trigger((int)EGameEvent.WeaponState, new TempUnit[] { new TempUnit("state", sp), new TempUnit("current", (FreeData)((PlayerEntity)weaponState.Owner).freeData.FreeData) });
            }
        }

<<<<<<< HEAD
        private bool CheckNeedActionDeal(PlayerWeaponController sharedApi, ActionDealEnum action)
        {
            return SingletonManager.Get<WeaponResourceConfigManager>().NeedActionDeal(sharedApi.HeldWeaponAgent.ConfigId, action);
=======
        private bool CheckNeedActionDeal(ISharedPlayerWeaponComponentGetter sharedApi, ActionDealEnum action)
        {
            return SingletonManager.Get<WeaponConfigManager>().NeedActionDeal(sharedApi.HeldWeaponConfigId.Value, action);
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        }
    }
}