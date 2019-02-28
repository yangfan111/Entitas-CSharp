<<<<<<< HEAD
﻿using Assets.Utils.Configuration;
using Core;
using Core.Configuration;
using Core.EntityComponent;
using System.Collections.Generic;
=======
﻿using Core;
using Core.Configuration;
using Core.EntityComponent;
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
using Utils.Singleton;
using WeaponConfigNs;

namespace App.Shared
{
    /// <summary>
    /// 定义entity原子操作
    /// </summary>
    public static class WeaponEntityExt
    {
<<<<<<< HEAD

        internal static void SetFlagOwner(this WeaponEntity weaponEntity,EntityKey owner)
        {
            if (weaponEntity.entityKey.Value == EntityKey.Default)
                return;
            if(owner == EntityKey.Default)
            {
                if (weaponEntity.hasOwnerId)
                    weaponEntity.RemoveOwnerId();
                weaponEntity.isFlagSyncSelf = false;
                weaponEntity.isFlagSyncNonSelf = true;
                //weaponEntity.isFlagSyncSelf = false;
            }
            else
            {
                if(!weaponEntity.hasOwnerId)
                {
                    weaponEntity.AddOwnerId(owner);
                }
                else
                {
                    weaponEntity.ownerId.Value = owner;
                }
                weaponEntity.isFlagSyncSelf = true;
                weaponEntity.isFlagSyncNonSelf = false;
            }
           
        }
        internal static void SetFlagNoOwner(this WeaponEntity weaponEnity)
        {
            SetFlagOwner(weaponEnity,EntityKey.Default);
        }
      
    
        internal static void SetFlagWaitDestroy(this WeaponEntity weaponEntity)
        {
            if (weaponEntity.hasOwnerId)
                weaponEntity.RemoveOwnerId();
            weaponEntity.isFlagSyncSelf = false;
            weaponEntity.isFlagSyncNonSelf = false;
            weaponEntity.isFlagDestroy = true;
        }
        [System.Obsolete]
=======
        internal static void SetFlagHasOwnwer(this WeaponEntity weaponEntity, EntityKey entityKey)
        {
            if (!weaponEntity.hasOwnerId)
            {
                weaponEntity.AddOwnerId(entityKey);
            }
            else
            {
                weaponEntity.ownerId.Value = entityKey;
            }
            weaponEntity.isFlagSyncSelf = true;
            weaponEntity.isFlagDestroy = false;
        }

        internal static void SetFlagNoOwner(this WeaponEntity weaponEntity)
        {

            weaponEntity.isFlagSyncSelf = false;
            weaponEntity.isFlagDestroy = true;
            weaponEntity.RemoveOwnerId();

        }

        internal static void SetFlagWaitDestroy(this WeaponEntity weaponEntity)
        {
            if(!weaponEntity.isFlagSyncSelf)
                weaponEntity.isFlagDestroy = true;
        }

>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        internal static void Activate(this WeaponEntity weaponEntity, bool activate)
        {
            weaponEntity.isActive = activate;
        }

        internal static WeaponScanStruct ToWeaponScan(this WeaponEntity weaponEntity)
        {
<<<<<<< HEAD
            if (!weaponEntity.hasWeaponScan)
                weaponEntity.AddWeaponScan();
            weaponEntity.weaponScan.SyncSelf(EntityKey.Default,weaponEntity.weaponBasicData);
            return weaponEntity.weaponScan.Value;
        }

    
      
        internal static void Recycle(this WeaponEntity weapon,bool hard = false)
        {
            weapon.weaponBasicData.Reset();
            weapon.weaponRuntimeData.Reset();
            if (hard)
                weapon.entityKey.Value = EntityKey.Default;


        }
        //public static void FillPartList(this WeaponEntity entity, List<int> partList)
        //{
        //    if (null == partList)
        //    {
        //        return;
        //    }
        //    partList.Clear();
        //    if (entity.weaponBasicData.UpperRail > 0)
        //    {
        //        partList.Add(entity.weaponBasicData.UpperRail);
        //    }
        //    if (entity.weaponBasicData.LowerRail > 0)
        //    {
        //        partList.Add(entity.weaponBasicData.LowerRail);
        //    }
        //    if (entity.weaponBasicData.Magazine > 0)
        //    {
        //        partList.Add(entity.weaponBasicData.Magazine);
        //    }
        //    if (entity.weaponBasicData.Stock > 0)
        //    {
        //        partList.Add(entity.weaponBasicData.Stock);
        //    }
        //    if (entity.weaponBasicData.Muzzle > 0)
        //    {
        //        partList.Add(entity.weaponBasicData.Muzzle);
        //    }
        //}
=======
            weaponEntity.weaponScan.Value.SyncSelf(weaponEntity);
            return weaponEntity.weaponScan.Value;
        }

        internal static void SetFireMode(this WeaponEntity weapon, EFireMode model)
        {
            weapon.weaponBasicData.FireMode = (int)model;
        }

        internal static void ResetFireModel(this WeaponEntity weapoon)
        {
            if (weapoon.weaponBasicData.FireMode == 0)
            {
                bool hasAutoFireModel = SingletonManager.Get<WeaponDataConfigManager>().HasAutoFireMode(weapoon.weaponBasicData.ConfigId);
                if (hasAutoFireModel)
                    SetFireMode(weapoon, EFireMode.Auto);
                else
                    SetFireMode(weapoon, SingletonManager.Get<WeaponDataConfigManager>().GetFirstAvaliableFireMode(weapoon.weaponBasicData.ConfigId));
            }
        }
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
    }
}
