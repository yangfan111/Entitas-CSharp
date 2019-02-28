using Core;
using WeaponConfigNs;

namespace App.Shared
{
    /// <summary>
    /// Defines the <see cref="ISharedPlayerWeaponComponentGetter" />
    /// </summary>
    public interface ISharedPlayerWeaponComponentGetter : IPlayerModuleComponentAgent
    {
        /// <summary>
        /// 当前武器槽位
        /// </summary>
        EWeaponSlotType HeldSlotType { get; }

<<<<<<< HEAD
        /// 获取当前槽位信息
        /// </summary>
        /// <returns></returns>
        int? CurrSlotWeaponId(Contexts contexts);
        /// <summary>
        /// 获取当前槽位信息
        /// </summary>
        /// <returns></returns>
        /// <summary>
        WeaponInfo CurrSlotWeaponInfo(Contexts contexts);
=======
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        /// <summary>
        /// 当前槽位是否为空
        /// </summary>
<<<<<<< HEAD
        /// <param name="slot"></param>
        /// <returns></returns>
        WeaponInfo GetSlotWeaponInfo(Contexts contexts, EWeaponSlotType slot);
=======
        bool IsHeldSlotEmpty { get; }

>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        /// <summary>
        /// 指定槽位是否为空
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
<<<<<<< HEAD
        int? GetSlotWeaponId(Contexts contexts, EWeaponSlotType slot);


        bool TryGetSlotWeaponInfo(Contexts contexts, EWeaponSlotType slot, out WeaponInfo wpInfo);
=======
        bool IsWeaponSlotEmpty(EWeaponSlotType slot);
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a

        /// <summary>
        /// 上一次切换的槽位
        /// </summary>
        EWeaponSlotType LastSlotType { get; }

        /// <summary>
        /// 当前背包索引
        /// </summary>
<<<<<<< HEAD
        /// <param name="slot"></param>
        /// <returns></returns>
        bool IsWeaponSlotStuffed(Contexts contexts, EWeaponSlotType slot);
=======
        int HeldBagPointer { get; }
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a

        /// <summary>
        /// 判断是否为当前槽位
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
<<<<<<< HEAD
        bool IsWeaponStuffedInSlot(Contexts contexts, int weaponId);

=======
        bool IsHeldSlotType(EWeaponSlotType slot);
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a

        /// <summary>
        /// 轮询获取上一把武器配置id
        /// </summary>
        /// <returns></returns>
<<<<<<< HEAD
        EWeaponSlotType PopGetLastWeaponId(Contexts contexts);

        /// <summary>
        /// 开火模式(武器)
        /// </summary>
        int GetSlotFireMode(Contexts contexts, EWeaponSlotType slot);
        bool IsCurrSlotType(EWeaponSlotType slot);
=======
        EWeaponSlotType PollGetLastSlotType();

        int GetReservedBullet(EWeaponSlotType slot);

        int GetReservedBullet(EBulletCaliber caliber);

>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        int GetReservedBullet();
    }
}
