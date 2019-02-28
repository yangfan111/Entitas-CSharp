﻿using App.Shared.GameModules.Player;
using App.Shared.GameModules.Weapon;
using Core;
using Core;
using Core.Configuration;
using Core.Utils;
using UnityEngine;

namespace App.Shared.GameModeLogic.PickupLogic
{
    public class SurvivalPickupLogic : DummyPickupLogic 
    {
        private PlayerContext _playerContext;
        private ISceneObjectEntityFactory _sceneObjectEntityFactory;
        private RuntimeGameConfig _runtimeGameConfig;
        private AutoPickupLogic _autoPickupLogic;
        private Contexts _contexts;

        public SurvivalPickupLogic(Contexts contexts,
            ISceneObjectEntityFactory sceneObjectEntityFactory,
            RuntimeGameConfig runtimeGameConfig)
        {
            _contexts = contexts;
            _playerContext = contexts.player;
            _runtimeGameConfig = runtimeGameConfig;
            _sceneObjectEntityFactory = sceneObjectEntityFactory;
            _autoPickupLogic = new AutoPickupLogic(contexts, sceneObjectEntityFactory);
        }

        public override void Drop(int playerEntityId, EWeaponSlotType slot)
        {
            //使用服务器操作
<<<<<<< HEAD
            //var player = _playerContext.GetEntityWithEntityKey(new Core.EntityComponent.EntityKey(playerEntityId, (short)EEntityType.Player));
            //player.WeaponController().HeldWeaponAgent.SetFlagWaitDestroy();
           
            return;
           // var weaponAchive = player.WeaponController();
           //var curWeapon = weaponAchive.GetSlotWeaponInfo(_contexts, slot);
           // if (curWeapon.ConfigId > 0)
           // {
           //     var dropPos = player.GetHandWeaponPosition();
           //     var playerTrans = player.characterContoller.Value.transform;
           //     var forward = playerTrans.forward;
           //     var pos = dropPos + forward * _runtimeGameConfig.WeaponDropOffset;
           //     RaycastHit hhit;
           //     SceneObjectEntity sceneObjectEntity;
           //     if(Physics.Raycast(dropPos, forward, out hhit, _runtimeGameConfig.WeaponDropOffset, UnityLayerManager.GetLayerMask(EUnityLayerName.Default) | UnityLayerManager.GetLayerMask(EUnityLayerName.Terrain)))
           //     {
           //         RaycastHit vhit;
           //         if(Physics.Raycast(hhit.point, Vector3.down, out vhit, 100, UnityLayerManager.GetLayerMask(EUnityLayerName.Default) | UnityLayerManager.GetLayerMask(EUnityLayerName.Terrain)))
           //         {
           //             sceneObjectEntity = _sceneObjectEntityFactory.CreateWeaponEntity(curWeapon, vhit.point) as SceneObjectEntity;
           //         }
           //         else
           //         {
           //             sceneObjectEntity = _sceneObjectEntityFactory.CreateWeaponEntity(curWeapon, playerTrans.position) as SceneObjectEntity;
           //         }
           //     }
           //     else
           //     {
           //         RaycastHit vhit;
           //         if(Physics.Raycast(pos, Vector3.down, out vhit, 100, UnityLayerManager.GetLayerMask(EUnityLayerName.Default) | UnityLayerManager.GetLayerMask(EUnityLayerName.Terrain)))
           //         {
           //             sceneObjectEntity = _sceneObjectEntityFactory.CreateWeaponEntity(curWeapon, vhit.point) as SceneObjectEntity;
           //         }
           //         else
           //         {
           //             sceneObjectEntity = _sceneObjectEntityFactory.CreateWeaponEntity(curWeapon, playerTrans.position) as SceneObjectEntity;
           //         }
           //     }
           //     player.WeaponController().DropSlotWeapon(_contexts, slot);
           // }
=======
            var player = _playerContext.GetEntityWithEntityKey(new Core.EntityComponent.EntityKey(playerEntityId, (short)EEntityType.Player));
            player.WeaponController().SetWeaponFlagDestory();
           
            return;
            var weaponAchive = player.WeaponController();
           var curWeapon = weaponAchive.GetSlotWeaponInfo(_contexts, slot);
            if (curWeapon.ConfigId > 0)
            {
                var dropPos = player.GetHandWeaponPosition();
                var playerTrans = player.characterContoller.Value.transform;
                var forward = playerTrans.forward;
                var pos = dropPos + forward * _runtimeGameConfig.WeaponDropOffset;
                RaycastHit hhit;
                SceneObjectEntity sceneObjectEntity;
                if(Physics.Raycast(dropPos, forward, out hhit, _runtimeGameConfig.WeaponDropOffset, UnityLayerManager.GetLayerMask(EUnityLayerName.Default) | UnityLayerManager.GetLayerMask(EUnityLayerName.Terrain)))
                {
                    RaycastHit vhit;
                    if(Physics.Raycast(hhit.point, Vector3.down, out vhit, 100, UnityLayerManager.GetLayerMask(EUnityLayerName.Default) | UnityLayerManager.GetLayerMask(EUnityLayerName.Terrain)))
                    {
                        sceneObjectEntity = _sceneObjectEntityFactory.CreateWeaponEntity(curWeapon, vhit.point) as SceneObjectEntity;
                    }
                    else
                    {
                        sceneObjectEntity = _sceneObjectEntityFactory.CreateWeaponEntity(curWeapon, playerTrans.position) as SceneObjectEntity;
                    }
                }
                else
                {
                    RaycastHit vhit;
                    if(Physics.Raycast(pos, Vector3.down, out vhit, 100, UnityLayerManager.GetLayerMask(EUnityLayerName.Default) | UnityLayerManager.GetLayerMask(EUnityLayerName.Terrain)))
                    {
                        sceneObjectEntity = _sceneObjectEntityFactory.CreateWeaponEntity(curWeapon, vhit.point) as SceneObjectEntity;
                    }
                    else
                    {
                        sceneObjectEntity = _sceneObjectEntityFactory.CreateWeaponEntity(curWeapon, playerTrans.position) as SceneObjectEntity;
                    }
                }
                player.WeaponController().DropSlotWeapon(_contexts, slot);
            }
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        }

        public override void AutoPickupWeapon(int playerEntityId, int entityId)
        {
            _autoPickupLogic.AutoPickupWeapon(playerEntityId, entityId);
        }
    }
}
