<<<<<<< HEAD
﻿
using App.Shared.Components.Bag;
using App.Shared.Components.Weapon;
using Core;
=======
﻿using Core;
using Core.EntityComponent;
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
using Core.WeaponLogic.Attachment;

namespace App.Shared
{
    //public enum Err_WeaponLogicErrCode
    //{
    //    Sucess = 0,
    //    Err_Default = 1,
    //    Err_IdNotExisted = 2,
    //    Err_PlayerDontHaveThrowingComponent = 3,
    //    Err_PlayerGrenadePullVertifyFailed = 4,
    //    Err_GrenadeListShowCountZero = 5,
    //    Err_GrenadeNotFindNextUsableId = 6,
    //    Err_SameWeaponPosition = 7,
    //    Err_SlotNone = 8,
    //    Err_NullComponent = 9,
    //    Err_SlotInvailed = 10,

<<<<<<< HEAD
    }
    public delegate Err_WeaponLogicErrCode WeaponBag_SlotProcessCallback(WeaponSlotHandlerBase contrller);
    public delegate void WeaponPartsModelRefresh(Contexts contexts, WeaponPartsRefreshData data);
    public delegate void WeaponSlotSwitchCallback(EWeaponSlotType slotType, int nextId);
    public delegate void WeaponSlotExpendCallback(Contexts contexts, WeaponSlotExpendData data);
    public delegate WeaponBasicInfoComponent WeaponSlotComponenExtractor(Contexts contexts, EWeaponSlotType slot);
    public delegate BagStateComponent BagStateComponentExtractor();
    public delegate GrenadeCacheDataComponent GrenadeCacheComponentExtractor();
    public struct WeaponSlotExpendData
=======
    //}
    /// <summary>
    /// Defines the <see cref="WeaponSlotExpendStruct" />
    /// </summary>
    public struct WeaponSlotExpendStruct
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
    {
        public EWeaponSlotType slotType;

        public bool needRemoveCurrent;

        public bool needAutoRestuff;

        public WeaponSlotExpendStruct(EWeaponSlotType s, bool in_needRemoveCurrent, bool in_needAutoRestuff)
        {
            slotType = s;
            needRemoveCurrent = in_needRemoveCurrent;
            needAutoRestuff = in_needAutoRestuff;
        }
    }
<<<<<<< HEAD
    public struct WeaponPartsRefreshData
    {
        public WeaponInfo weaponInfo;
        public EWeaponSlotType slot;
        public WeaponPartsStruct oldParts;
        public WeaponPartsStruct newParts;
        public bool mountInPackage;
        public bool refreshWeaponLogic;
        public int lastWeaponId;
        public void SetRefreshLogic(int in_lastWeaponId)
        {
            lastWeaponId = in_lastWeaponId;
            refreshWeaponLogic = true;
        }
        
       
    }
}
=======

    /// <summary>
    /// Defines the <see cref="WeaponPartsRefreshStruct" />
    /// </summary>
    public struct WeaponPartsRefreshStruct
    {
        public WeaponScanStruct weaponInfo;

        public EWeaponSlotType slot;

        public WeaponPartsStruct oldParts;

        public WeaponPartsStruct newParts;

        public bool armInPackage;

        public bool needRefreshWeaponLogic;

        public EntityKey lastWeaponKey;

        public void SetRefreshLogic(EntityKey in_lastWeaponKey)
        {
            lastWeaponKey = in_lastWeaponKey;
            needRefreshWeaponLogic = true;
        }
    }
}
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
