using Assets.XmlConfig;
using Core.EntityComponent;
<<<<<<< HEAD
using System;
=======
using Core.Enums;
using System;
using System.Collections.Generic;
using WeaponConfigNs;
using XmlConfig;
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a

namespace Core
{
    public enum EWeaponSlotType
    {
        None = 0,
        PrimeWeapon,
        SecondaryWeapon,
        PistolWeapon,
        MeleeWeapon,
        ThrowingWeapon,
        TacticWeapon,
        Length,

        Pointer =99,
<<<<<<< HEAD
        LastPointer = 100,
    }
    public partial class GameGlobalConst
    {
        public const int WeaponBagMaxCount = 4;
=======
    }
    public partial class GameGlobalConst
    {
        public const int WeaponBagMaxCount = 2;
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        public static readonly int WeaponSlotMaxLength = (int)EWeaponSlotType.Length;
        public const int WeaponEntityType = 11;

    }

    public enum EWeaponSlotsGroupType
    {
        Default,
        Group,
    }
    public delegate void WeaponSlotUpdateEvent(EntityKey key);
    public delegate void WeaponHeldUpdateEvent();
<<<<<<< HEAD
    public delegate void WeaponProcessEvent(IPlayerWeaponGetter controller, EWeaponSlotType slot);
    public delegate void WeaponDropEvent(IPlayerWeaponGetter controller, EWeaponSlotType slot,EntityKey dropedWeapon);

=======
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class WeaponSpeciesAttribute : Attribute
    {
        public readonly EWeaponSlotType slotType;

        public WeaponSpeciesAttribute(EWeaponSlotType stype)
        {
            this.slotType = stype;
        }
    }
<<<<<<< HEAD

    public interface IPlayerWeaponGetter
    {
        EntityKey Owner { get; }
    }
=======
  
    public interface IPlayerWeaponComponentGetter
    {
    }
    public interface IPlayerWeaponControllerFrameWork
    { }

    public interface IPlayerModuleComponentAgent
    {
       
    }
    public interface IPlayerModuleController { }
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a

    /// <summary>
    /// 武器相关的人物动作 地面-当前武器-背包
    /// </summary>
   
    
    public struct ItemInfo
    {
        public ECategory Category;
        public int ItemId;
        public int Count;
    }


}