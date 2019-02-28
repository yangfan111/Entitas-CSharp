<<<<<<< HEAD
﻿using App.Shared.Audio;
using App.Shared.Components;
using App.Shared.Util;
using Assets.Utils.Configuration;
using Assets.XmlConfig;
using Core;
using Core.CharacterState;
using Core.Enums;
using Core.GameModeLogic;
using Core.Utils;
using System;
using Utils.Appearance;
using Utils.Singleton;
using Utils.Utils;
=======
﻿using App.Server.GameModules.GamePlay.free.player;
using App.Shared.Audio;
using App.Shared.Components;
using App.Shared.Components.Player;
using App.Shared.Util;
using Assets.App.Shared.EntityFactory;
using Assets.Utils.Configuration;
using Assets.XmlConfig;
using Core;
using Core.Appearance;
using Core.Attack;
using Core.CharacterBone;
using Core.CharacterState;
using Core.Common;
using Core.EntityComponent;
using Core.Free;
using Core.GameModeLogic;
using Core.Statistics;
using Core.Utils;
using Core.WeaponLogic.Attachment;
using Core.WeaponLogic.Throwing;
using System;
using System.Collections.Generic;
using Utils.Appearance;
using Utils.Singleton;
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
using WeaponConfigNs;
using XmlConfig;

namespace App.Shared.GameModules.Weapon
{
    /// <summary>
    /// Defines the <see cref="PlayerWeaponController" />
    /// </summary>
    public partial class PlayerWeaponController : ModuleLogicActivator<PlayerWeaponController>, ISharedPlayerWeaponComponentGetter, IPlayerWeaponControllerFrameWork
    {
        private PlayerEntityWeaponInteract weaponInteract;

        private readonly WeaponSlotsAux slotsAux;

        private PlayerWeaponComponentsAgent playerWeaponAgent;

<<<<<<< HEAD
        private WeaponSlotsAux slotsAux;
        private PlayerWeaponComponentAgent componentAgent;
        
        public PlayerWeaponController()
            //PlayerWeaponComponentAgent weaponAgent)
        {
           // componentAgent = weaponAgent;
            slotsAux = new WeaponSlotsAux();
=======
        public EntityKey OwnerKey { get; private set; }

        public ISharedPlayerWeaponComponentGetter Getter
        {
            get { return playerWeaponAgent; }
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        }

        #region//initialization
        public void SetOwner(EntityKey owner)
        {
            OwnerKey = owner;
        }

<<<<<<< HEAD
        #region //initialize
=======
        public void SetPlayerWeaponAgent(PlayerWeaponComponentsAgent agent)
        {
            playerWeaponAgent = agent;
            playerWeaponAgent.AddHeldWeaponListener(UpdateHeldWeaponAgent);
        }

        public void SetInteract(PlayerEntityWeaponInteract interact)
        {
            weaponInteract = interact;
        }
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a

        public void SetProcessListener(IWeaponProcessListener processListener)
        {
            weaponInteract.ProcessListener = processListener;
        }

        public void SetWeaponContext(WeaponContext context)
        {
            WeaponComponentsAgent.WeaponContext = context;
        }

        public void SetConfigManager(IPlayerWeaponConfigManager configManager)
        {
            WeaponComponentsAgent.ConfigManager = configManager;
        }

        public void SetBagCacheHelper(EWeaponSlotType slotType, IBagDataCacheHelper helper)
        {
            WeaponSlotHandlerBase handler = slotsAux.FindHandler(slotType);
            handler.SetHelper(helper);
        }
<<<<<<< HEAD
        #endregion

        void OnDrawWeaponCallback(Contexts contexts, WeaponInfo weapon, EWeaponSlotType slot)
        {
            OnSlotMountFinish(contexts, weapon, slot);
            entityMedium.Appearance_RemountP3WeaponOnRightHand();
        }
        void OnUnmountWeaponCallback(Contexts contexts, int weaponId, Action onfinish)
        {
            entityMedium.Appearance_UnmountWeaponFromHand();
            componentAgent.SetCurrSlotTypeProcess(contexts, EWeaponSlotType.None,OnSetCurrSlotDetailProcess);
            if (SingletonManager.Get<WeaponConfigManager>().IsC4(weaponId))
            {
                entityMedium.UnmountC4();
            }
            if (null != onfinish)
            {
                onfinish();
            }
        }

        #region// parts相关
        /// <summary>
        /// API:parts
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public EFuncResult SetSlotWeaponPart(Contexts contexts, EWeaponSlotType slot, int id)
        {
            return componentAgent.SetSlotWeaponPart(contexts, slot, id, OnCurrWeaponAttachmentRefresh, OnModelWeaponPartsRefresh);
        }
        /// <summary>
        /// API:parts
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EFuncResult SetSlotWeaponPart(Contexts contexts, int id)
        {
            return componentAgent.SetSlotWeaponPart(contexts, CurrSlotType, id, OnCurrWeaponAttachmentRefresh, OnModelWeaponPartsRefresh);
        }
        /// <summary>
        /// 刷新当前武器parts
        /// </summary>
        private void OnCurrWeaponAttachmentRefresh(Contexts contexts)
        {
            EWeaponSlotType currType = CurrSlotType;
            if (!currType.MayHasPart())
            {
                return;
            }
            WeaponInfo weapon = CurrSlotWeaponInfo(contexts);
            if (weapon.Id < 1) return;
            var attachments = weapon.GetParts();
            //影响表现
            entityMedium.Apperance_RefreshABreath(contexts);
            // 添加到背包的时候会执行刷新模型逻辑
            entityMedium.Model_RefreshWeaponModel(weapon.Id, currType, attachments);
        }
        private void OnModelWeaponPartsRefresh(Contexts contexts, WeaponPartsRefreshData refreshData)
        {
         
            if (refreshData.mountInPackage)
            {
                var avatarId = refreshData.weaponInfo.AvatarId;
                if (avatarId < 1)
                    avatarId = SingletonManager.Get<WeaponConfigManager>().GetConfigById(refreshData.weaponInfo.Id).AvatorId;
                entityMedium.Appearence_ProcessMountWeaponInPackage(refreshData.slot.ToWeaponInPackage(), refreshData.weaponInfo.Id, avatarId);
            }
            entityMedium.Model_RefreshWeaponParts(refreshData.weaponInfo.Id, refreshData.slot, refreshData.oldParts, refreshData.newParts);
            if (refreshData.refreshWeaponLogic)
            {
                if (refreshData.slot == CurrSlotType)
                    RefreshCurrWeapon(contexts);
                var handler = slotsAux.FindHandler(refreshData.slot);
                if (refreshData.lastWeaponId != refreshData.weaponInfo.Id)
                    handler.RecordLastWeaponId(refreshData.lastWeaponId);
            }
        }
        /// <summary>
        /// API:parts
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="part"></param>
        public void DeleteSlotWeaponPart(Contexts contexts, EWeaponSlotType slot, EWeaponPartType part)
        {
            componentAgent.DeleteSlotWeaponPart(contexts, slot, part, OnCurrWeaponAttachmentRefresh, OnModelWeaponPartsRefresh);
        }
        private void OnSetCurrSlotDetailProcess(Contexts contexts)
        {
            RefreshCurrWeapon(contexts);
            // 需要执行刷新配件逻辑，因为配件会影响相机动作等属性
            OnCurrWeaponAttachmentRefresh(contexts);
=======
        public void ResetAllComponents()
        {
            if (RelatedOrient != null)
                RelatedOrient.Reset();
        }
        #endregion

        #region//auxliary  component 
        public bool HasWeaponAutoState
        {
            get { return true; }
        }
        public void AddAuxBullet(PlayerBulletData bulletData)
        {
            if (playerWeaponAgent.AuxCache.BulletList != null)
                playerWeaponAgent.AuxCache.BulletList.Add(bulletData);
        }
        public void AddAuxEffect()
        {
            playerWeaponAgent.AuxCache.EffectList = new List<EClientEffectType>();
        }
        public void AddAuxBullet()
        {
            playerWeaponAgent.AuxCache.BulletList = new List<PlayerBulletData>();
        }
        public void AddAuxEffect(EClientEffectType effectType)
        {
            if (playerWeaponAgent.AuxCache.EffectList != null)
                playerWeaponAgent.AuxCache.EffectList.Add(effectType);
        }
        public List<PlayerBulletData> BulletList { get { return playerWeaponAgent.AuxCache.BulletList; } }
        public List<EClientEffectType> EffectList { get { return playerWeaponAgent.AuxCache.EffectList; } }
        public int ForceInterruptGunSight
        {
            get { return playerWeaponAgent.AuxCache.ForceInterruptGunSight; }
            set { playerWeaponAgent.AuxCache.ForceInterruptGunSight = value; }
        }
        public int AutoFire
        {
            get { return playerWeaponAgent.AuxCache.AutoFire; }
            set { playerWeaponAgent.AuxCache.AutoFire = value; }
        }
        public int BagOpenLimitTIme
        {
            get { return playerWeaponAgent.AuxCache.BagOpenLimitTime; }
            set { playerWeaponAgent.AuxCache.BagOpenLimitTime = value; }
        }
        public bool AutoThrowing
        {
            get { return playerWeaponAgent.AuxCache.AutoThrowing; }
            set { playerWeaponAgent.AuxCache.AutoThrowing = value; }
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        }

        ///overridebag components
        public int OverrideBagTactic
        {
            get { return playerWeaponAgent.OverrideCache != null ? playerWeaponAgent.OverrideCache.TacticWeapon : 0; }
            set { if (playerWeaponAgent.OverrideCache != null) playerWeaponAgent.OverrideCache.TacticWeapon = value; }

        }





















        #endregion
<<<<<<< HEAD
        /// <summary>
        /// API:相比UseWeapon多了动作,需经由UserCmd触发
        /// </summary>
        /// <param name="slot"></param>
        public void DrawSlotWeapon(Contexts contexts, EWeaponSlotType slot,bool includeAction = true)
        {
            Logger.DebugFormat("DrawWeapon {0}", slot);
            if (componentAgent.IsCurrSlotType(slot))
                return;
            WeaponInfo lastWeapon = componentAgent.CurrSlotWeaponInfo(contexts);
            WeaponInfo destWeapon = componentAgent.GetSlotWeaponInfo(contexts, slot);
            if (destWeapon.Id < 1)
=======
        public void DrawSlotWeapon(EWeaponSlotType slot, bool includeAction = true)
        {
            if (playerWeaponAgent.IsHeldSlotType(slot))
                return;
            WeaponScanStruct? lastWeapon = HeldWeaponAgent.BaseComponentScan;
            WeaponScanStruct? destWeapon = GetWeaponAgent(slot).BaseComponentScan;
            if (!destWeapon.HasValue)
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
                return;
            AppearanceSpecific();
            //DoDrawInterrupt();
            bool armOnLeft = slot == EWeaponSlotType.SecondaryWeapon;
<<<<<<< HEAD
            float holsterParam = (componentAgent.IsCurrSlotType(EWeaponSlotType.SecondaryWeapon)) ?
=======
            float holsterParam = (playerWeaponAgent.IsHeldSlotType(EWeaponSlotType.SecondaryWeapon)) ?
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
                                     AnimatorParametersHash.Instance.HolsterFromLeftValue :
                                     AnimatorParametersHash.Instance.HolsterFromRightValue;
            float drawParam = armOnLeft ?
                                    AnimatorParametersHash.Instance.DrawLeftValue :
                                    AnimatorParametersHash.Instance.DrawRightValue;
            if (includeAction)
            {
                float switchParam = holsterParam * 10 + drawParam;
<<<<<<< HEAD
                if (lastWeapon.Id > 0)
                {
                    entityMedium.CharacterState_SwitchWeapon(() => WeaponToHand(destWeapon.Id, lastWeapon.Id, slot, armOnLeft),
                        () => OnDrawWeaponCallback(contexts, destWeapon, slot), switchParam);
                }
                else
                {
                    WeaponToHand(destWeapon.Id, lastWeapon.Id, slot, armOnLeft);
                    OnSlotMountFinish(contexts, destWeapon, slot);
                    entityMedium.CharacterState_Draw(entityMedium.Appearance_RemountP3WeaponOnRightHand, drawParam);
=======
                if (lastWeapon.HasValue)
                {
                    weaponInteract.CharacterState_SwitchWeapon(() => WeaponToHand(destWeapon.Value.ConfigId, lastWeapon.Value.ConfigId, slot, armOnLeft),
                        () => OnDrawWeaponCallback(destWeapon.Value, slot), switchParam);
                }
                else
                {
                    WeaponToHand(destWeapon.Value.ConfigId, lastWeapon.Value.ConfigId, slot, armOnLeft);
                    OnSlotArmFinish(destWeapon.Value, slot);
                    weaponInteract.CharacterState_Draw(weaponInteract.Appearance_RemountP3WeaponOnRightHand, drawParam);
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
                }
            }
            else
            {
                //CharacterState控制动作相关
<<<<<<< HEAD
                WeaponToHand(destWeapon.Id, lastWeapon.Id, slot, armOnLeft);
                OnDrawWeaponCallback(contexts, destWeapon, slot);
                OnSlotMountFinish(contexts, destWeapon, slot);
                entityMedium.Appearance_RemountP3WeaponOnRightHand();
=======
                WeaponToHand(destWeapon.Value.ConfigId, lastWeapon.Value.ConfigId, slot, armOnLeft);
                OnDrawWeaponCallback(destWeapon.Value, slot);
                OnSlotArmFinish(destWeapon.Value, slot);
                weaponInteract.Appearance_RemountP3WeaponOnRightHand();
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
            }
          
        }
<<<<<<< HEAD
        private void AppearanceSpecific()
        {
            if (componentAgent.CurrSlotType == EWeaponSlotType.SecondaryWeapon)
                entityMedium.Appearance_MountP3WeaponOnAlternativeLocator();
        }
        private void DoDrawInterrupt()
        {
            entityMedium.CharacterState_DrawInterrupt();
        }
        /// <summary>
        /// API:装备武器
        /// </summary>
        /// <param name="slot"></param>
        public void TryMountSlotWeapon(Contexts contexts, EWeaponSlotType slot)
        {
            WeaponInfo currWeapon = componentAgent.CurrSlotWeaponInfo(contexts);
            WeaponInfo weaponInfo;
            if (componentAgent.TryGetSlotWeaponInfo(contexts, slot, out weaponInfo))
            {
                WeaponToHand(weaponInfo.Id, currWeapon.Id, slot);
                OnSlotMountFinish(contexts, weaponInfo, slot);
            }

        }
        public void UnmountCurrWeapon(Contexts contexts, Action onfinish)
        {
            WeaponInfo weaponInfo = componentAgent.CurrSlotWeaponInfo(contexts);
            AppearanceSpecific();
            float holsterParam = (componentAgent.CurrSlotType == EWeaponSlotType.SecondaryWeapon) ?
                AnimatorParametersHash.Instance.HolsterFromLeftValue :
                AnimatorParametersHash.Instance.HolsterFromRightValue;
            entityMedium.CharacterState_Unmount(() => OnUnmountWeaponCallback(contexts, weaponInfo.Id, onfinish), holsterParam);
=======

        public void TryArmSlotWeapon(EWeaponSlotType slot)
        {
            WeaponScanStruct? currWeapon = HeldWeaponAgent.BaseComponentScan;
            if (!currWeapon.HasValue) return;
            WeaponScanStruct? destWeapon = GetWeaponAgent(slot).BaseComponentScan;
            if (!destWeapon.HasValue) return;
            WeaponToHand(destWeapon.Value.ConfigId, currWeapon.Value.ConfigId, slot);
            OnSlotArmFinish(destWeapon.Value, slot);
        }

        public void UnArmHeldWeapon(Action onfinish)
        {
            WeaponScanStruct weaponInfo = HeldWeaponAgent.BaseComponentScan.Value;
            AppearanceSpecific();
            float holsterParam = (playerWeaponAgent.HeldSlotType == EWeaponSlotType.SecondaryWeapon) ?
                AnimatorParametersHash.Instance.HolsterFromLeftValue :
                AnimatorParametersHash.Instance.HolsterFromRightValue;
            weaponInteract.CharacterState_Unmount(() => OnUnArmWeaponCallback(weaponInfo.ConfigId, onfinish), holsterParam);
        }
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a

        public void UnArmHeldWeapon()//float holsterParam)
        {
            UnArmHeldWeapon(null);
        }
<<<<<<< HEAD
        /// <summary>
        /// API:卸载武器
        /// </summary>
        public void UnmountCurrWeapon(Contexts contexts)//float holsterParam)
        {
            UnmountCurrWeapon(contexts, null);
        }
        public IBagDataCacheHelper GetBagCacheHelper(EWeaponSlotType slotType) { return slotsAux.FindHandler(slotType).Helper; }
=======

        public IBagDataCacheHelper GetBagCacheHelper(EWeaponSlotType slotType)
        {
            return slotsAux.FindHandler(slotType).Helper;
        }
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a

        public void ForceUnarmCurrWeapon()
        {
            weaponInteract.Appearance_UnmountWeaponFromHand();
            SetHeldSlotTypeProcess(EWeaponSlotType.None);
            weaponInteract.ThrowAction_Execute();
        }

<<<<<<< HEAD

        /// <summary>
        /// API:卸载武器
        /// </summary>
        public void ForceUnmountCurrWeapon(Contexts contexts)
        {
            entityMedium.Appearance_UnmountWeaponFromHand();
            componentAgent.SetCurrSlotTypeProcess(contexts, EWeaponSlotType.None,OnSetCurrSlotDetailProcess);
            entityMedium.ThrowAction_Execute();
=======
        public void DropSlotWeapon(EWeaponSlotType slot)
        {
            if (slot == EWeaponSlotType.None || slot == EWeaponSlotType.ThrowingWeapon)
            {
                return;
            }
            WeaponEntity lastWeapon = GetWeaponAgent(slot).Entity;
            if (SingletonManager.Get<WeaponConfigManager>().IsC4(lastWeapon.weaponBasicData.ConfigId))
            {
                weaponInteract.RemoveC4();
            }
            weaponInteract.Listener_Drop(slot);
            RemoveSlotWeapon(slot);
            var handler = slotsAux.FindHandler(slot);
            handler.OnDrop();
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        }

        public void RemoveSlotWeapon(EWeaponSlotType slot, bool interrupt = true)
        {
            if (slot == EWeaponSlotType.None || IsWeaponSlotEmpty(slot)) return;
            WeaponComponentsAgent weaponAgent = GetWeaponAgent(slot);
            if (!weaponAgent.IsVailed()) return;
            weaponAgent.SetFlagNoOwner();
            playerWeaponAgent.RemoveSlotWeapon(slot);
            if (IsHeldSlotType(slot))
            {
                SetHeldSlotTypeProcess(EWeaponSlotType.None);
            }
            WeaponInPackage pos = slot.ToWeaponInPackage();
            weaponInteract.Appearance_UnmountWeaponInPackage(pos);
            if (interrupt)
                Interrupt();
        }

        public bool AutoPickUpWeapon(WeaponScanStruct orient)
        {
            NewWeaponConfigItem itemConfig;
            if (!WeaponUtil.VertifyWeaponConfigId(orient.ConfigId, out itemConfig))
                return false;

            var weaponType = SingletonManager.Get<WeaponConfigManager>().GetWeaponType(orient.ConfigId);
            if (!weaponType.CanAutoPick())
            {
                return false;
            }
            var slotType = GetMatchSlot((EWeaponType)itemConfig.Type);
            if (slotType != EWeaponSlotType.None)
            {
                if (!IsWeaponSlotEmpty(slotType))
                {
                    return false;
                }
                var noWeaponInHand = playerWeaponAgent.HeldSlotType == EWeaponSlotType.None;
                weaponInteract.Listener_Pickup(slotType);
                ReplaceWeaponToSlot(slotType, orient);
                if (noWeaponInHand)
                {
                    TryArmSlotWeapon(slotType);
                }
                return true;
            }
            return false;
        }

<<<<<<< HEAD
        private void OnSlotMountFinish(Contexts contexts, WeaponInfo weapon, EWeaponSlotType slot)
        {
            componentAgent.SetCurrSlotTypeProcess(contexts, slot, OnSetCurrSlotDetailProcess);
            if (weapon.Bullet <= 0)
=======
        public EntityKey PickUpWeapon(WeaponScanStruct orient)
        {
            NewWeaponConfigItem weaponCfg;
            if (!WeaponUtil.VertifyWeaponConfigId(orient.ConfigId, out weaponCfg))
                return EntityKey.EmptyWeapon;
            var slotType = GetMatchSlot((EWeaponType)weaponCfg.Type);
            if (slotType != EWeaponSlotType.None)
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
            {
                weaponInteract.Listener_Pickup(slotType);
                //除去手雷已经填充到当前槽位的情况
                if (FilterGrenadeStuffedCond(slotType))
                {
                    EntityKey last;
                    var noWeaponInHand = HeldSlotType == EWeaponSlotType.None;
                    if (ReplaceWeaponToSlot(slotType, orient, true, out last))
                    {
                        if (noWeaponInHand)
                        {
                            TryArmSlotWeapon(slotType);
                        }
                        return last;
                    }
                }
            }
            return EntityKey.EmptyWeapon;
        }

        public void SwitchIn(EWeaponSlotType in_slot)
        {

            if (IsWeaponSlotEmpty(in_slot))
            {
                weaponInteract.ShowTip(Core.Common.ETipType.NoWeaponInSlot);
                return;
            }

            if (IsHeldSlotType(in_slot))
            {
                ProcessSameSpeciesSwitchIn(in_slot);
            }
            else
            {
                DrawSlotWeapon(in_slot);
                GameAudioMedium.PlayWeaponAudio(HeldWeaponAgent.ConfigId.Value, weaponInteract.WeaponHandObject(), (item) => item.SwitchIn);
            }
        }
<<<<<<< HEAD
       
        /// <summary>
        /// API: 扔武器
        /// </summary>
        /// <param name="slot"></param>
        public void DropSlotWeapon(Contexts contexts, EWeaponSlotType slot)
        {
            if (slot == EWeaponSlotType.None || slot == EWeaponSlotType.ThrowingWeapon)
=======

        public void PureSwitchIn(EWeaponSlotType in_slot)
        {
            if (in_slot == EWeaponSlotType.None)
                return;
            EWeaponSlotType from_slot = playerWeaponAgent.HeldSlotType;

            //int from_Id= componentAgent.GetSlotWeaponId(from_slot);

            if (IsWeaponSlotEmpty(in_slot))
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
            {
                weaponInteract.ShowTip(Core.Common.ETipType.NoWeaponInSlot);
                return;
            }
<<<<<<< HEAD
            var lastWeapon = componentAgent.GetSlotWeaponInfo(contexts, slot);
            if (SingletonManager.Get<WeaponConfigManager>().IsC4(lastWeapon.Id))
=======
            if (!IsHeldSlotType(in_slot))
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
            {
                DrawSlotWeapon(in_slot, false);
            }
<<<<<<< HEAD
            Logger.DebugFormat("DropWeapon {0}", slot);
            entityMedium.Listener_Drop(slot);
            RemoveSlotWeapon(contexts, slot);
            var handler = slotsAux.FindHandler(slot);
            handler.OnDrop();
=======
        }
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a

        public void ExpendAfterAttack()
        {
            ExpendAfterAttack(HeldSlotType);
        }
<<<<<<< HEAD
        /// <summary>
        /// API:interrupt
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="interrupt"></param>
        public void RemoveSlotWeapon(Contexts contexts, EWeaponSlotType slot,bool interrupt = true)
        {
            if (componentAgent.RemoveSlotWeapon(contexts, slot,OnSetCurrSlotDetailProcess))
            {
                WeaponInPackage pos = slot.ToWeaponInPackage();
                entityMedium.Appearance_UnmountWeaponInPackage(pos);
                if(interrupt)
                    Interrupt();
            }
=======

        public void ExpendAfterAttack(EWeaponSlotType slot)
        {
            if (slot == EWeaponSlotType.None)
                return;
            var weaponId = heldWeaponAgent.ConfigId;
            if (!weaponId.HasValue) return;
            weaponInteract.Listener_OnExpend(slot);
            var handler = slotsAux.FindHandler(slot);
            handler.OnExpend(HeldWeaponAgent, OnWeaponAutoRestuff);
            GameAudioMedium.PlayWeaponAudio(weaponId.Value, weaponInteract.WeaponHandObject(), (item) => item.Fire);
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        }

        public bool ReplaceWeaponToSlot(EWeaponSlotType slotType, WeaponScanStruct orient)
        {
            EntityKey lastKey;
            return ReplaceWeaponToSlot(slotType, orient, true, out lastKey);
        }
<<<<<<< HEAD
        public int GetSlotWeaponBullet(Contexts contexts, EWeaponSlotType slot)
        {
            return componentAgent.GetSlotWeaponBullet(contexts, slot);
        }

        public void SetSlotWeaponBullet(Contexts contexts, EWeaponSlotType slot, int count)
        {
            componentAgent.SetSlotWeaponBullet(contexts, slot, count);
        }

        public void SetSlotWeaponBullet(Contexts contexts, int count)
        {
            componentAgent.SetSlotWeaponBullet(contexts, count);
=======

        public bool ReplaceWeaponToSlot(EWeaponSlotType slotType, WeaponScanStruct orient, bool vertify, out EntityKey lastKey)
        {
            lastKey = EntityKey.EmptyWeapon;
            //  if (vertify)
            if (slotType == EWeaponSlotType.None) return false;
            var weaonCfg = SingletonManager.Get<WeaponConfigManager>().GetConfigById(orient.ConfigId);
            if (weaonCfg == null)
                return false;
            if (!weaponInteract.Model_IsSlotAvailable(slotType)) return false;
            WeaponPartsRefreshStruct refreshParams = new WeaponPartsRefreshStruct();
            if (AddWeaponToSlot(slotType, orient, ref lastKey, ref refreshParams))
            {
                RefreshModelWeaponParts(refreshParams);
                return true;
            }
            return false;
        }

        private bool AddWeaponToSlot(EWeaponSlotType slot, WeaponScanStruct orient, ref EntityKey lastWeaponKey, ref WeaponPartsRefreshStruct refreshParams)
        {

            if (slot == EWeaponSlotType.None)
            {
                return false;
            }
            var lastWeaponAgent = GetWeaponAgent(slot);
            lastWeaponAgent.SetFlagNoOwner();
            lastWeaponKey = lastWeaponAgent.EntityKey;
            var orientKey = orient.WeaponKey;
            ///捡起即创建
            WeaponEntity orientEntity = WeaponEntityFactory.GetOrCreateWeaponEntity(WeaponComponentsAgent.WeaponContext, OwnerKey, ref orient);
            bool createNewEntity = orientEntity.entityKey.Value != orientKey;
            playerWeaponAgent.AddSlotWeapon(slot, orientEntity.entityKey.Value);
            if (!createNewEntity)
                orient.CopyToWeaponComponentWithDefaultParts(orientEntity);

            WeaponPartsStruct parts = orient.GetParts();
            var avatarId = orient.AvatarId;
            if (avatarId < 1)
            {
                avatarId = SingletonManager.Get<WeaponConfigManager>().GetConfigById(orient.ConfigId).AvatorId;
            }
            refreshParams.weaponInfo = orient;
            refreshParams.slot = slot;
            refreshParams.oldParts = new WeaponPartsStruct();
            refreshParams.newParts = parts;
            refreshParams.armInPackage = true;
            if (lastWeaponKey != EntityKey.EmptyWeapon)
                refreshParams.SetRefreshLogic(lastWeaponKey);
            return true;
        }

        public void Interrupt()
        {
            weaponInteract.CharacterState_Interrupt();
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        }

        public void ProcessMountWeaponInPackage(WeaponInPackage pos, int weaponId, int avatarId)
        {
            weaponInteract.Appearence_ProcessMountWeaponInPackage(pos, weaponId, avatarId);
        }

        public void SetReservedBullet(int count)
        {
            var currSlot = HeldSlotType;
            if (currSlot.IsSlotWithBullet())
                weaponInteract.Model_SetReservedBullet(HeldSlotType, count);
        }

        public void SetReservedBullet(EWeaponSlotType slot, int count)
        {
            if (slot.IsSlotWithBullet())
                weaponInteract.Model_SetReservedBullet(slot, count);
        }

        public int SetReservedBullet(EBulletCaliber caliber, int count)
        {
            return weaponInteract.Model_SetReservedBullet(caliber, count);
        }

        private void SetHeldSlotTypeProcess(EWeaponSlotType slotType)
        {
            playerWeaponAgent.SetHeldSlotType(slotType);
            RefreshHeldWeaponDetail();
        }

        private void OnDrawWeaponCallback(WeaponScanStruct weapon, EWeaponSlotType slot)
        {
            OnSlotArmFinish(weapon, slot);
            weaponInteract.Appearance_RemountP3WeaponOnRightHand();
        }

        private void OnUnArmWeaponCallback(int weaponId, Action onfinish)
        {
            weaponInteract.Appearance_UnmountWeaponFromHand();
            SetHeldSlotTypeProcess(EWeaponSlotType.None);
            if (SingletonManager.Get<WeaponConfigManager>().IsC4(weaponId))
            {
                weaponInteract.UnmountC4();
            }
            if (null != onfinish)
            {
                onfinish();
            }
        }

<<<<<<< HEAD
        /// <summary>
        /// API:自动拾取
        /// </summary>
        /// <param name="weaponInfo"></param>
        /// <returns></returns>
        public bool AutoPickUpWeapon(Contexts contexts, WeaponInfo weaponInfo)
=======
        private void RefreshHeldWeaponAttachment()
        {
            EWeaponSlotType currType = HeldSlotType;
            if (!currType.MayHasPart())
            {
                return;
            }
            WeaponScanStruct? weapon = HeldWeaponAgent.BaseComponentScan;
            if (!weapon.HasValue) return;
            var attachments = weapon.Value.GetParts();
            weaponInteract.Apperance_RefreshABreath(HeldWeaponAgent.BreathFactor);
            // 添加到背包的时候会执行刷新模型逻辑
            weaponInteract.Model_RefreshWeaponModel(weapon.Value.ConfigId, currType, attachments);
        }

        private void RefreshModelWeaponParts(WeaponPartsRefreshStruct refreshData)
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        {

            if (refreshData.armInPackage)
            {
                var avatarId = refreshData.weaponInfo.AvatarId;
                if (avatarId < 1)
                    avatarId = SingletonManager.Get<WeaponConfigManager>().GetConfigById(refreshData.weaponInfo.ConfigId).AvatorId;
                weaponInteract.Appearence_ProcessMountWeaponInPackage(refreshData.slot.ToWeaponInPackage(), refreshData.weaponInfo.ConfigId, avatarId);
            }
<<<<<<< HEAD
            var slotType = GetMatchSlot(contexts, (EWeaponType)itemConfig.Type);
            if (slotType != EWeaponSlotType.None)
            {
                if (componentAgent.IsWeaponSlotStuffed(contexts, slotType))
                {
                    return false;
                }
                var noWeaponInHand = componentAgent.CurrSlotType == EWeaponSlotType.None;
                entityMedium.Listener_Pickup(slotType);
                ReplaceWeaponToSlot(contexts, slotType, weaponInfo, false);
                if (noWeaponInHand)
                {
                    TryMountSlotWeapon(contexts, slotType);
                }
                return true;
=======
            weaponInteract.Model_RefreshWeaponParts(refreshData.weaponInfo.ConfigId, refreshData.slot, refreshData.oldParts, refreshData.newParts);
            if (refreshData.needRefreshWeaponLogic)
            {
                if (refreshData.slot == HeldSlotType)
                    RefreshHeldWeapon();
                var handler = slotsAux.FindHandler(refreshData.slot);
                //if (refreshData.lastWeaponId != refreshData.weaponInfo.ConfigId)
                //    handler.RecordLastWeaponId(refreshData.lastWeaponId);
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
            }
        }

<<<<<<< HEAD
        private EWeaponSlotType GetMatchSlot(Contexts contexts, EWeaponType weaponType)
        {
            switch (weaponType)
            {
                case EWeaponType.PrimeWeapon:
                    var hasPrime = componentAgent.IsWeaponSlotStuffed(contexts, EWeaponSlotType.PrimeWeapon);
                    if (hasPrime && entityMedium.Model_IsSlotAvailable(EWeaponSlotType.SecondaryWeapon))
                        return EWeaponSlotType.SecondaryWeapon;
                    return EWeaponSlotType.PrimeWeapon;
                default:
                    return weaponType.ToWeaponSlot();
            }
=======
        private void RefreshHeldWeaponDetail()
        {
            RefreshHeldWeapon();
            // 需要执行刷新配件逻辑，因为配件会影响相机动作等属性
            RefreshHeldWeaponAttachment();
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        }

        private void AppearanceSpecific()
        {
            if (playerWeaponAgent.HeldSlotType == EWeaponSlotType.SecondaryWeapon)
                weaponInteract.Appearance_MountP3WeaponOnAlternativeLocator();
        }
<<<<<<< HEAD
        /// <summary>
        /// API:手动拾取武器
        /// </summary>
        /// <param name="weaponInfo"></param>
        /// <returns></returns>
        public WeaponInfo PickUpWeapon(Contexts contexts, WeaponInfo weaponInfo)
        {
            NewWeaponConfigItem weaponCfg;
            if (!WeaponUtil.VertifyWeaponInfo(weaponInfo, out weaponCfg))
                return WeaponInfo.Empty;
            var slotType = GetMatchSlot(contexts, (EWeaponType)weaponCfg.Type);
            if (slotType != EWeaponSlotType.None)
            {
                entityMedium.Listener_Pickup(slotType);
                //除去手雷已经填充到当前槽位的情况
                if (FilterGrenadeStuffedCond(contexts, slotType))
                {
                    WeaponInfo last;
                    var noWeaponInHand = componentAgent.CurrSlotType == EWeaponSlotType.None;
                    if (ReplaceWeaponToSlot(contexts, slotType, weaponInfo, out last))
                    {
                        if(noWeaponInHand)
                        {
                            TryMountSlotWeapon(contexts, slotType);
                        }
                        return last;
                    }
=======

        private void DoDrawInterrupt()
        {
            weaponInteract.CharacterState_DrawInterrupt();
        }

        private void WeaponToHand(int weaponId, int lastWeaponId, EWeaponSlotType slot, bool armOnLeft = false)
        {
            if (SingletonManager.Get<WeaponConfigManager>().IsC4(lastWeaponId))
            {
                weaponInteract.UnmountC4();
            }
            if (SingletonManager.Get<WeaponConfigManager>().IsC4(weaponId))
            {
                weaponInteract.MountC4(weaponId);
            }
            WeaponInPackage pos = slot.ToWeaponInPackage();
            weaponInteract.Appearance_MountWeaponToHand(pos);
            if (armOnLeft)
                weaponInteract.Appearance_MountP3WeaponOnAlternativeLocator();
        }

        private void OnSlotArmFinish(WeaponScanStruct weapon, EWeaponSlotType slot)
        {
            SetHeldSlotTypeProcess(slot);
            if (weapon.Bullet <= 0)
            {
                if (SharedConfig.CurrentGameMode == GameMode.Normal)
                {
                    //TODO 判断弹药数量是否足够，如果弹药不足，弹提示框
                    weaponInteract.CharacterState_ReloadEmpty(() => { });
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
                }
            }
            else
            {
                //if (!bag.CurBolted)
                //{
                //    //TODO 拉栓动作
                //}
            }
        }
<<<<<<< HEAD
        /// <summary>
        /// API:当前武器被消耗（射击，投弹）
        /// </summary>
        /// <param name="slot"></param>
        public void OnExpend(Contexts contexts, EWeaponSlotType slot)
        {
            if (slot == EWeaponSlotType.None)
                return;

            var weapoonId = CurrSlotWeaponId(contexts);
          
            entityMedium.Listener_OnExpend(slot);
            var handler = slotsAux.FindHandler(slot);
            handler.OnExpend(contexts, componentAgent, OnWeaponAutoRestuff);
            if (weapoonId.HasValue)
                GameAudioMedium.PlayWeaponAudio(weapoonId.Value, entityMedium.WeaponHandObject(), (item) => item.Fire);
        }
        /// <summary>
        /// 武器消耗完完自动填充逻辑
        /// </summary>
        /// <param name="slot"></param>
        private void OnWeaponAutoRestuff(Contexts contexts, WeaponSlotExpendData cbData)
        {
            //消耗掉当前武器
            if (cbData.needRemoveCurrent)
                RemoveSlotWeapon(contexts, cbData.slotType, false);
            //自动填充下一项武器
            if (componentAgent.IsWeaponSlotStuffed(contexts, cbData.slotType) || !cbData.needAutoRestuff) return;
=======

        private EWeaponSlotType GetMatchSlot(EWeaponType weaponType)
        {
            switch (weaponType)
            {
                case EWeaponType.PrimeWeapon:
                    var hasPrime = !playerWeaponAgent.IsWeaponSlotEmpty(EWeaponSlotType.PrimeWeapon);
                    if (hasPrime && weaponInteract.Model_IsSlotAvailable(EWeaponSlotType.SecondaryWeapon))
                        return EWeaponSlotType.SecondaryWeapon;
                    return EWeaponSlotType.PrimeWeapon;
                default:
                    return weaponType.ToWeaponSlot();
            }
        }

        private void OnWeaponAutoRestuff(WeaponSlotExpendStruct cbData)
        {
            //消耗掉当前武器
            if (cbData.needRemoveCurrent)
                RemoveSlotWeapon(cbData.slotType, false);
            //自动填充下一项武器
            if (!IsWeaponSlotEmpty(cbData.slotType) || !cbData.needAutoRestuff) return;
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
            var handler = slotsAux.FindHandler(cbData.slotType);
            int nextId = handler.PickNextId(false);
            if (nextId > 0)
            {
<<<<<<< HEAD
                WeaponInfo last;
                if (ReplaceWeaponToSlot(contexts, cbData.slotType, new WeaponInfo() { Id = nextId }, out last))
                {
                    TryMountSlotWeapon(contexts, cbData.slotType);
                }
            }
        }
        /// 当前槽位同种武器切换逻辑
        private void OnSameSpeciesSwitch(Contexts contexts, EWeaponSlotType slotType, int nextWeaponId)
        {
            if (ReplaceWeaponToSlot(contexts, EWeaponSlotType.ThrowingWeapon, new WeaponInfo
            {
                Id = nextWeaponId,
            }))
            {
                TryMountSlotWeapon(contexts, EWeaponSlotType.ThrowingWeapon);

            }
        }
        /// 不处理手雷已装备情况
        private bool FilterGrenadeStuffedCond(Contexts contexts, EWeaponSlotType slotType)
        {
            return slotType != EWeaponSlotType.ThrowingWeapon||
               !componentAgent.IsWeaponSlotStuffed(contexts, slotType);

        }
        /// <summary>
        /// API:武器槽位切换
        /// </summary>
        /// <param name="in_slot"></param>
        public void SwitchIn(Contexts contexts, EWeaponSlotType in_slot)
        {
            if (in_slot == EWeaponSlotType.None)
                return;
            EWeaponSlotType from_slot = componentAgent.CurrSlotType;
            
            //int from_Id= componentAgent.GetSlotWeaponId(from_slot);
            WeaponInfo wpInfo = componentAgent.GetSlotWeaponInfo(contexts, in_slot);
           
            if (!WeaponUtil.VertifyWeaponInfo(wpInfo))
            {
                entityMedium.ShowTip(Core.Common.ETipType.NoWeaponInSlot);
                return;
            }
            if(from_slot ==in_slot)
            {
                ProcessSameSpeciesSwitchIn(contexts, in_slot);
            }
            else
            {
                DrawSlotWeapon(contexts, in_slot);
                var weaponId = componentAgent.CurrSlotWeaponId(contexts);
                if(weaponId.HasValue)
                {
                    GameAudioMedium.PlayWeaponAudio(weaponId.Value,entityMedium.WeaponHandObject(),(item)=>item.SwitchIn);
                }
                else
                {
                    Logger.Error("weaponId doesn't exist");
                }
            }
         
        }
        public void PureSwitchIn(Contexts contexts, EWeaponSlotType in_slot)
        {
            if (in_slot == EWeaponSlotType.None)
                return;
            EWeaponSlotType from_slot = componentAgent.CurrSlotType;

            //int from_Id= componentAgent.GetSlotWeaponId(from_slot);
            WeaponInfo wpInfo = componentAgent.GetSlotWeaponInfo(contexts, in_slot);

            if (!WeaponUtil.VertifyWeaponInfo(wpInfo))
            {
                entityMedium.ShowTip(Core.Common.ETipType.NoWeaponInSlot);
                return;
            }
            if (from_slot != in_slot)
            {
                DrawSlotWeapon(contexts, in_slot, false);
            }
        }

        ///相同种类武器切换处理逻辑
        private void ProcessSameSpeciesSwitchIn(Contexts contexts, EWeaponSlotType slot )
        {
            //非手雷类型先不做处理
            if (slot != EWeaponSlotType.ThrowingWeapon) return;
            if (entityMedium.CanUseGreande() != Err_WeaponLogicErrCode.Sucess) return;
=======
                EntityKey last;
                if (ReplaceWeaponToSlot(cbData.slotType, WeaponUtil.CreateScan(nextId), true, out last))
                {
                    TryArmSlotWeapon(cbData.slotType);
                }
            }
        }

        private void OnSameSpeciesSwitch(EWeaponSlotType slotType, int nextWeaponId)
        {
            if (ReplaceWeaponToSlot(EWeaponSlotType.ThrowingWeapon, WeaponUtil.CreateScan(nextWeaponId)))
            {
                TryArmSlotWeapon(EWeaponSlotType.ThrowingWeapon);
            }
        }

        private bool FilterGrenadeStuffedCond(EWeaponSlotType slotType)
        {
            return slotType != EWeaponSlotType.ThrowingWeapon ||
                IsWeaponSlotEmpty(slotType);
        }

        private void RefreshHeldWeapon()
        {
            RelatedOrient.Reset();

            if (IsHeldSlotEmpty)
                return;

            //重置开火模式
            HeldWeaponAgent.ResetFireModel();
        }

        private void ProcessSameSpeciesSwitchIn(EWeaponSlotType slot)
        {
            //非手雷类型先不做处理
            if (slot != EWeaponSlotType.ThrowingWeapon) return;
            if (!weaponInteract.CanUseGreande()) return;
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
            int nextId = slotsAux.FindHandler(slot).PickNextId(true);
            if (nextId > 0)
            {
<<<<<<< HEAD
                if (ReplaceWeaponToSlot(contexts, EWeaponSlotType.ThrowingWeapon, new WeaponInfo { Id = nextId, }))
                {
                    TryMountSlotWeapon(contexts, EWeaponSlotType.ThrowingWeapon);
=======
                if (ReplaceWeaponToSlot(EWeaponSlotType.ThrowingWeapon, WeaponUtil.CreateScan(nextId)))
                {
                    TryArmSlotWeapon(EWeaponSlotType.ThrowingWeapon);
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
                }
            }
        }

<<<<<<< HEAD
        public bool ReplaceWeaponToSlot(Contexts contexts, EWeaponSlotType slotType, WeaponInfo weaponInfo, bool vertify = true)
        {
            WeaponInfo lastWp;
            return ReplaceWeaponToSlot(contexts, slotType, weaponInfo, vertify, out lastWp);
            //  return lastWp;
        }
        public bool ReplaceWeaponToSlot(Contexts contexts, EWeaponSlotType slotType, WeaponInfo weaponInfo)
        {
            WeaponInfo lastWp;
            return ReplaceWeaponToSlot(contexts, slotType, weaponInfo, true, out lastWp);
            //  return lastWp;
        }
        public bool ReplaceWeaponToSlot(Contexts contexts, EWeaponSlotType slotType, WeaponInfo weaponInfo, out WeaponInfo lastWp)
        {
            return ReplaceWeaponToSlot(contexts, slotType, weaponInfo, true, out lastWp);
            //  return lastWp;
        }
        /// <summary>
        /// API:替换新的武器到槽位上去
        /// </summary>
        /// <param name="slotType"></param>
        /// <param name="weaponInfo"></param>
        /// <param name="vertify"></param>
        /// <param name="lastWp"></param>
        /// <returns></returns>
        public bool ReplaceWeaponToSlot(Contexts contexts, EWeaponSlotType slotType, WeaponInfo weaponInfo, bool vertify, out WeaponInfo lastWp)
        {
            lastWp = WeaponInfo.Empty;
            if (vertify)
            {
                if (slotType == EWeaponSlotType.None) return false;
                var weaonCfg = SingletonManager.Get<WeaponConfigManager>().GetConfigById(weaponInfo.Id);
                if (weaonCfg == null)
                    return false;
            }
            if (!entityMedium.Model_IsSlotAvailable(slotType)) return false;
           
            var errCode = componentAgent.AddWeaponToSlot(contexts, slotType, weaponInfo, OnModelWeaponPartsRefresh, out lastWp);
            return errCode == Err_WeaponLogicErrCode.Sucess;
              
        }
  ///更新当前武器的功能，逻辑
        private void RefreshCurrWeapon(Contexts contexts)
        {
            EWeaponSlotType slot = CurrSlotType;
            // 清理之前的枪械状态信息
           entityMedium.Player_ClearPlayerWeaponState(contexts);
            var weapon = GetSlotWeaponInfo(contexts, slot);
            if (!WeaponUtil.VertifyWeaponId(weapon.Id))
            {
                entityMedium.Player_RefreshPlayerWeaponLogic(contexts, UniversalConsts.InvalidIntId);
                return;
            }
            entityMedium.Player_RefreshPlayerWeaponLogic(contexts, weapon.Id);
            //重置开火模式
            componentAgent.ResetSlotFireModel(contexts, slot);
=======
        public EWeaponSlotType HeldSlotType
        {
            get { return Getter.HeldSlotType; }
        }

        public bool IsHeldSlotEmpty
        {
            get { return Getter.IsHeldSlotEmpty; }
        }

        public EWeaponSlotType LastSlotType
        {
            get
            {
                return Getter.LastSlotType;
            }
        }

        public int HeldBagPointer
        {
            get
            {
                return Getter.HeldBagPointer;
            }
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        }

<<<<<<< HEAD
        public int? CurrSlotWeaponId(Contexts contexts) { return componentAgent.CurrSlotWeaponId(contexts); }

        public WeaponInfo CurrSlotWeaponInfo(Contexts contexts) { return componentAgent.CurrSlotWeaponInfo(contexts); }

        public int CurrWeaponBullet(Contexts contexts) {  return componentAgent.CurrWeaponBullet(contexts); }
      

        public WeaponInfo GetSlotWeaponInfo(Contexts contexts, EWeaponSlotType slot)
        {
            return componentAgent.GetSlotWeaponInfo(contexts, slot);
        }

        public int? GetSlotWeaponId(Contexts contexts, EWeaponSlotType slot)
        {
            return componentAgent.GetSlotWeaponId(contexts, slot);
        }

        public bool TryGetSlotWeaponInfo(Contexts contexts, EWeaponSlotType slot, out WeaponInfo wpInfo)
        {
            return componentAgent.TryGetSlotWeaponInfo(contexts, slot, out wpInfo);
=======
        public int GetReservedBullet(EBulletCaliber caliber)
        {
            return weaponInteract.Model_GetReserveBullet(caliber);
        }

        public int GetReservedBullet()
        {
            return weaponInteract.Model_GetReserveBullet(HeldSlotType);
        }

        public int GetReservedBullet(EWeaponSlotType slot)
        {
            return weaponInteract.Model_GetReserveBullet(slot);
        }

        public bool IsWeaponSlotEmpty(EWeaponSlotType slot)
        {
            return Getter.IsWeaponSlotEmpty(slot);
        }

        public bool IsHeldSlotType(EWeaponSlotType slot)
        {
            return Getter.IsHeldSlotType(slot);
        }

        public EWeaponSlotType PollGetLastSlotType()
        {
            return Getter.PollGetLastSlotType();
        }
        #region//related components and operation
        public OrientationComponent RelatedOrient
        {
            get { return weaponInteract.RelatedOrient; }
        }

        public FirePosition RelatedFirePos
        {
            get { return weaponInteract.RelatedFirePos; }
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        }

        public TimeComponent RelatedTime
        {
            get { return weaponInteract.RelatedTime; }
        }

<<<<<<< HEAD
        public bool IsWeaponSlotStuffed(Contexts contexts, EWeaponSlotType slot)
        {
            return componentAgent.IsWeaponSlotStuffed(contexts, slot);
        }

        public bool IsWeaponCurrSlotStuffed(Contexts contexts)
        {
            return componentAgent.IsWeaponCurrSlotStuffed(contexts);
        }

        public bool IsWeaponStuffedInSlot(Contexts contexts, int weaponId)
        {
            return componentAgent.IsWeaponStuffedInSlot(contexts, weaponId);
        }

        public EWeaponSlotType PopGetLastWeaponId(Contexts contexts)
        {
            return componentAgent.PopGetLastWeaponId(contexts);
        }

        public int GetSlotFireModeCount(Contexts contexts, EWeaponSlotType slot)
        {
            return componentAgent.GetSlotFireModeCount(contexts, slot);
        }

        public bool GetSlotWeaponBolted(Contexts contexts, EWeaponSlotType slot)
        {
            return componentAgent.GetSlotWeaponBolted(contexts, slot);
        }

        public int GetSlotFireMode(Contexts contexts, EWeaponSlotType slot)
        {
            return componentAgent.GetSlotFireMode(contexts, slot);
=======
        public CameraFinalOutputNewComponent RelatedCameraFinal
        {
            get { return weaponInteract.RelatedCameraFinal; }
        }

        public ThrowingActionInfo RelatedThrowActionInfo
        {
            get { return weaponInteract.RelatedThrowAction.ActionInfo; }
        }

        public ICharacterState RelatedStateInterface
        {
            get { return weaponInteract.RelatedCharState.State; }
        }

        public ThrowingUpdateComponent RelatedThrowUpdate
        {

            get { return weaponInteract.RelatedThrowUpdate; }
        }

        public StatisticsData RelatedStatics
        {
            get { return weaponInteract.RelatedStatistics.Statistics; }
        }

        public CameraStateNewComponent RelatedCameraSNew
        {
            get { return weaponInteract.RelatedCameraSNew; }
        }

        public ICharacterAppearance RelatedAppearence
        {
            get { return weaponInteract.RelatedAppearence.Appearance; }
        }
        public ICharacterBone RelatedBones
        {
            get { return weaponInteract.RelatedBones.CharacterBone; }
        }
        public PlayerInfoComponent RelatedPlayerInfo
        {
            get { return weaponInteract.RelatedPlayerInfo; }
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        }

        public PlayerMoveComponent RelatedPlayerMove
        {
            get { return weaponInteract.RelatedPlayerMove; }
        }
        public FreeData RelatedFreeData
        {
            get { return (FreeData)weaponInteract.RelatedFreeData.FreeData; }
        }
        public LocalEventsComponent RelatedLocalEvents
        {
            get { return weaponInteract.RelatedLocalEvents; }
        }
        public IWeaponModeLogic RelatedModelLogic
        {
            get { return weaponInteract.RelatedModelLogic.ModeLogic; }
        }

        public void ShowTip(ETipType tip)
        {
            weaponInteract.ShowTip(tip);
        }

        public void CreateSetMeleeAttackInfo(MeleeAttackInfo attackInfo, MeleeFireLogicConfig config)
        {
            weaponInteract.CreateSetMeleeAttackInfo(attackInfo, config);
        }

        public void CreateSetMeleeAttackInfoSync(int atk)
        {
            weaponInteract.CreateSetMeleeAttackInfoSync(atk);
        }

        public override string ToString()
        {
            var slotType = CurrSlotType;
            //var slotCmp = CurrSlotWeaponInfo();
            //TODO log
            //return string.Format("playerControllerInfo==>\n slot:{0},info:{1}", slotType, slotCmp);
            return string.Format("playerControllerInfo==>\n slot:{0}", slotType);
        }
            
        #endregion
    }
}
