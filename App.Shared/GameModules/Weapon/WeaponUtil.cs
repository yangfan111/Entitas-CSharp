<<<<<<< HEAD
﻿using App.Shared.Components.Weapon;
using Assets.Utils.Configuration;
using Assets.XmlConfig;
using Core;
using Core.CharacterState;
using Core.Configuration;
using Core.EntityComponent;
using Core.Room;
using System.Collections.Generic;
using Utils.Configuration;
using Utils.Singleton;
using WeaponConfigNs;

namespace App.Shared
{
   
=======
﻿using Assets.Utils.Configuration;
using Assets.XmlConfig;
using Core;
using Core.Configuration;
using Core.EntityComponent;
using System.Collections.Generic;
using Utils.Singleton;
using WeaponConfigNs;

namespace App.Shared.GameModules.Weapon
{
    /// <summary>
    /// Defines the <see cref="WeaponLogicUtil" />
    /// </summary>
    public static class WeaponLogicUtil
    {
    }
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a

    /// <summary>
    /// Defines the <see cref="WeaponUtil" />
    /// </summary>
    public static class WeaponUtil
    {
        public static int EmptyHandId
        {
<<<<<<< HEAD
            get { return SingletonManager.Get<WeaponResourceConfigManager>().EmptyHandId; }
        }
        public static readonly WeaponEntity EmptyWeapon = new WeaponEntity();
        public readonly static WeaponRuntimeDataComponent EmptyRun = new WeaponRuntimeDataComponent();

        public readonly static WeaponBasicDataComponent EmptyWeaponBase = new WeaponBasicDataComponent();
        public static EWeaponSlotType GetEWeaponSlotTypeById(int weaponId)
        {
            var configType = (EWeaponType_Config)SingletonManager.Get<WeaponResourceConfigManager>().GetConfigById(weaponId).Type;
            return configType.ToWeaponSlot();
=======
            get { return SingletonManager.Get<WeaponConfigManager>().EmptyHandId; }
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        }

        /// <summary>
        /// 验证WeaponId合法
        /// </summary>
        /// <param name="weaponId"></param>
        /// <returns></returns>
        public static bool VertifyWeaponConfigId(int weaponId)
        {
<<<<<<< HEAD
            WeaponResConfigItem config;
            return VertifyWeaponConfigId(weaponId, out config);
        }

        public static bool VertifyWeaponConfigId(int weaponId, out WeaponResConfigItem config)
=======
            NewWeaponConfigItem config;
            return VertifyWeaponConfigId(weaponId, out config);
        }

        public static bool VertifyWeaponConfigId(int weaponId, out NewWeaponConfigItem config)
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        {
            config = null;
            if (weaponId == 0)
                return false;
<<<<<<< HEAD
            config = SingletonManager.Get<WeaponResourceConfigManager>().GetConfigById(weaponId);
            return config != null;
        }

      
        public static List<WeaponBagContainer> CreateEmptyBagContainers()
        {
            var containerSet = new List<WeaponBagContainer>(GameGlobalConst.WeaponBagMaxCount);
            for (int i = 0; i < GameGlobalConst.WeaponBagMaxCount; i++)
            {
                containerSet.Add(new WeaponBagContainer());
            }
            return containerSet;
        }

=======
            config = SingletonManager.Get<WeaponConfigManager>().GetConfigById(weaponId);
            return config != null;
        }
        public static bool IsWeaponKeyVaild (EntityKey key)
        {
            return key != EntityKey.Default || key != EntityKey.EmptyWeapon;
        }
        public static WeaponBagContainer[] CreateEmptyBagContainers()
        {
            var containerSet = new WeaponBagContainer[GameGlobalConst.WeaponBagMaxCount];
            for (int i = 0; i < GameGlobalConst.WeaponBagMaxCount; i++)
            {
                containerSet[i] = new WeaponBagContainer();
            }
            return containerSet;
        }
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        public static WeaponScanStruct CreateScan(int configId, System.Action<WeaponScanStruct> initFunc)
        {
            WeaponScanStruct val = new WeaponScanStruct();
            val.Assign(configId);
            initFunc(val);
            return val;
        }
<<<<<<< HEAD

        public static WeaponScanStruct CreateScan(PlayerWeaponData weaponData)
        {
            WeaponScanStruct val = new WeaponScanStruct();
            val.Assign(weaponData.WeaponTplId);
            val.AvatarId = weaponData.WeaponTplId;
            val.UpperRail = weaponData.UpperRail;
            val.LowerRail = weaponData.LowerRail;
            val.Magazine = weaponData.Magazine;
            val.Muzzle = weaponData.Muzzle;
            val.Stock = weaponData.Stock;
            return val;
        }

        //public static WeaponScanStruct CreateScan(Components.SceneObject.WeaponObjectComponent weaponObject)
        //{
        //    WeaponScanStruct val = new WeaponScanStruct();
        //    val.Assign(weaponObject.ConfigId);
        //    val.AvatarId = weaponObject.WeaponAvatarId;
        //    val.UpperRail = weaponObject.UpperRail;
        //    val.LowerRail = weaponObject.LowerRail;
        //    val.Magazine = weaponObject.Magazine;
        //    val.Muzzle = weaponObject.Muzzle;
        //    val.Stock = weaponObject.Stock;
        //    return val;
        //}

=======
        public static WeaponScanStruct CreateScan(Components.SceneObject.WeaponObjectComponent weaponObject)
        {
            WeaponScanStruct val = new WeaponScanStruct();
            val.Assign(weaponObject.WeaponKey, weaponObject.ConfigId);
            return val;
        }
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        public static WeaponScanStruct CreateScan(int configId)
        {
            WeaponScanStruct val = new WeaponScanStruct();
            val.Assign(configId);
            return val;
        }
<<<<<<< HEAD

=======
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        public static WeaponScanStruct CreateScan(WeaponEntity entity)
        {
            WeaponScanStruct val = new WeaponScanStruct();
            val.Assign(entity.entityKey.Value, entity.weaponBasicData.ConfigId);
            return val;
        }
<<<<<<< HEAD

=======
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        public static bool VertifyEweaponSlotIndex(int index, bool ignoreNone = false)
        {
            return (index > 0 || ignoreNone) && index < (int)EWeaponSlotType.Length;
        }

        public static float GetWeaponDefaultSpeed()
        {
<<<<<<< HEAD
            var config = SingletonManager.Get<WeaponConfigManagement>().FindConfigById(EmptyHandId);
            return config.S_Speed;
        }

        public static List<GrenadeCacheData> CreateEmptyGrenadeCacheArrs(List<int> ids)
        {
            var containerSet = new List<GrenadeCacheData>(ids.Count);
            for (int i = 0; i < ids.Count; i++)
            {
                containerSet.Add(new GrenadeCacheData());
=======
            var config = SingletonManager.Get<WeaponDataConfigManager>().GetConfigById(EmptyHandId);
            return config.WeaponLogic.MaxSpeed;
        }

        public static GrenadeCacheData[] CreateEmptyGrenadeCacheArrs(List<int> ids)
        {
            var containerSet = new GrenadeCacheData[ids.Count];
            for (int i = 0; i < ids.Count; i++)
            {
                containerSet[i] = new GrenadeCacheData();
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
                containerSet[i].grenadeId = ids[i];
            }
            return containerSet;
        }

        public static List<int> ForeachFilterGreandeIds()
        {
<<<<<<< HEAD
            var configs = SingletonManager.Get<WeaponResourceConfigManager>().GetConfigs();
            var grenadeConfigIds = new List<int>();
            foreach (var config in configs)
            {
                switch ((EWeaponType_Config)config.Value.Type)
                {
                    case EWeaponType_Config.ThrowWeapon:
=======
            var configs = SingletonManager.Get<WeaponConfigManager>().GetConfigs();
            var grenadeConfigIds = new List<int>();
            foreach (var config in configs)
            {
                switch ((EWeaponType)config.Value.Type)
                {
                    case EWeaponType.ThrowWeapon:
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
                        var subType = (EWeaponSubType)config.Value.SubType;
                        switch (subType)
                        {
                            case EWeaponSubType.BurnBomb:
                            case EWeaponSubType.FlashBomb:
                            case EWeaponSubType.FogBomb:
                            case EWeaponSubType.Grenade:
                                break;
                            default:
                                break;
                        }
                        grenadeConfigIds.Add(config.Value.Id);
                        break;
                }
            }
            return grenadeConfigIds;
        }
<<<<<<< HEAD

        public static int GetRealAttachmentId(int attachId, int weaponId)
        {
            var cfg = SingletonManager.Get<WeaponPartSurvivalConfigManager>().GetConfigById(attachId);
            if (null == cfg)
            {
                return 0;
            }
            for (int i = 0; i < cfg.PartsList.Length; i++)
            {
                if (SingletonManager.Get<WeaponPartsConfigManager>().IsPartMatchWeapon(cfg.PartsList[i], weaponId))
                {
                    return cfg.PartsList[i];
                }
            }
            return 0;
        }

        public static bool IsC4p(int configId)
        {
            return SingletonManager.Get<WeaponResourceConfigManager>().IsC4(configId);
        }

        public static float GetHolsterParam(EWeaponSlotType slot)
        {
            return GetHolsterParam(slot == EWeaponSlotType.SecondaryWeapon);
        }

        public static float GetHolsterParam(bool val)
        {
            return val ?
                 AnimatorParametersHash.Instance.HolsterFromLeftValue :
                 AnimatorParametersHash.Instance.HolsterFromRightValue;
        }

=======
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
    }
}
