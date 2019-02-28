using App.Shared.GameModeLogic.BagSlotLogic;
using App.Shared.GameModeLogic.PickupLogic;
using App.Shared.GameModeLogic.ReservedBulletLogic;
using App.Shared.GameModeLogic.WeaponActionListener;
using App.Shared.GameModeLogic.WeaponInitLoigc;
using App.Shared.GameModules.Weapon;
using Assets.Utils.Configuration;
using Core;
using Core.GameModule.System;
using Utils.Configuration;
using Utils.Singleton;

namespace App.Shared.GameModeLogic.LogicFactory
{
    public class NormalModeLogicFactory : AbstractGameeModeLogicFactory
    {
        private Contexts _contexts;
        private ICommonSessionObjects _commonSessionObjects;

        private int _modeId;

        public NormalModeLogicFactory(Contexts contexts, ICommonSessionObjects commonSessionObjects, int modeId)
        {
            UnityEngine.Debug.Log("normal mode ");
            _contexts = contexts;
            _commonSessionObjects = commonSessionObjects;
            _modeId = modeId;
        }

        protected override IBagSlotLogic GetBagSlotLogic()
        {
            var bagSlotLogic = new NormalBagSlotLogic();

            return bagSlotLogic;
        }

        protected override IPickupLogic GetPickupLogic()
        {
            var pickupLogic = new NormalPickupLogic(_contexts,
                _contexts.session.entityFactoryObject.SceneObjectEntityFactory,
                _commonSessionObjects.RuntimeGameConfig,
                SingletonManager.Get<GameModeConfigManager>().GetWepaonStayTime(_modeId));
            return pickupLogic;
        }

        protected override IReservedBulletLogic GetReservedBulletLogic()
        {
            var reservedBulletLogic = new LocalReservedBulletLogic(_contexts);
            return reservedBulletLogic;
        }

        protected override IWeaponProcessListener GetWeaponActionListener()
        {
            var weaponActionListener = new NormalWeaponActionListener();


            return weaponActionListener;
        }

        protected override IWeaponInitHandler InitializeHandler()
        {
<<<<<<< HEAD
            var weaponInitLogic = new ServerWeaponInitHandler(
                _commonSessionObjects.RoomInfo.ModeId);
                //SingletonManager.Get<WeaponResourceConfigManager>(),
                //SingletonManager.Get<WeaponPropertyConfigManager>(),
                //SingletonManager.Get<WeaponPartsConfigManager>());
=======
            var weaponInitLogic = new NormalWeaponInitLogic(
                _contexts,
                _commonSessionObjects.RoomInfo.ModeId,
                SingletonManager.Get<GameModeConfigManager>(),
                SingletonManager.Get<WeaponConfigManager>(),
                SingletonManager.Get<WeaponPropertyConfigManager>(),
                SingletonManager.Get<WeaponPartsConfigManager>());
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a


            return weaponInitLogic;
        }

        protected override IWeaponSlotsLibrary GetWeaponSlotLibary()
        {
            return WeaponSlotsLibrary.Allocate(Core.EWeaponSlotsGroupType.Group);
        }
    }
}