﻿using System;
using System.Collections.Generic;
using App.Shared.Components;
using App.Shared.Player;
using Assets.App.Server.GameModules.GamePlay.Free;
using Assets.Utils.Configuration;
using Assets.XmlConfig;
using Core;
using Core.EntityComponent;
using Core.Enums;
using Core.Free;
using Core.GameTime;
using Core.Utils;
using Entitas;
using Free.framework;
using UnityEngine;
using Utils.Singleton;

namespace App.Shared.EntityFactory
{
    public class ServerSceneObjectEntityFactory : ISceneObjectEntityFactory
    {
        private static readonly LoggerAdapter _logger = new LoggerAdapter(typeof(ServerSceneObjectEntityFactory));
        private readonly SceneObjectContext _sceneObjectContext;
        private readonly PlayerContext _playerContext;
        protected readonly IEntityIdGenerator _idGenerator;
        private readonly IEntityIdGenerator _equipGenerator;
        private readonly ICurrentTime _currentTime;

        public List<int> FreeCastEntityToDestoryList
        {
            get 
            {
                return _freeCastEntityToDestroyList;
            }
        }
        private readonly List<int> _freeCastEntityToDestroyList = new List<int>();

        public ServerSceneObjectEntityFactory(SceneObjectContext sceneObjectContext,  PlayerContext playerContext,
            IEntityIdGenerator entityIdGenerator,
            IEntityIdGenerator equipGenerator, ICurrentTime currentTime)
        {
            _sceneObjectContext = sceneObjectContext;
            _playerContext = playerContext;
            _idGenerator = entityIdGenerator;
            _equipGenerator = equipGenerator;
            _currentTime = currentTime;
        }

        public virtual SceneObjectEntity CreateSceneObjectEntity()
        {
            var entity = _sceneObjectContext.CreateEntity();
            return entity;
        }

        public virtual IEntity CreateSimpleEquipmentEntity(
            ECategory category,
            int id,
            int count,
            Vector3 position)
        {
            var entity = _sceneObjectContext.CreateEntity();
            entity.AddEntityKey(new EntityKey(_equipGenerator.GetNextEntityId(), (short) EEntityType.SceneObject));
            entity.isFlagSyncNonSelf = true;
            entity.AddPosition(position);
            entity.AddSimpleEquipment(id, count, (int) category);
            entity.AddFlagImmutability(_currentTime.CurrentTime);
            return entity;
        }

        public virtual IEntity CreateSceneWeaponObjectEntity(WeaponScanStruct weaponScan, Vector3 position)
        {
            var entity = _sceneObjectContext.CreateEntity();
            entity.AddEntityKey(new EntityKey(_equipGenerator.GetNextEntityId(), (short) EEntityType.SceneObject));
            entity.isFlagSyncNonSelf = true;
            entity.AddPosition(position);
            entity.AddWeaponObject();
<<<<<<< HEAD
            entity.weaponObject.GameCopyFrom(weaponScan); 
=======
            entity.weaponObject.SyncSelf(weaponScan); 
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
            entity.AddFlagImmutability(_currentTime.CurrentTime);
            return entity;
        }

   
        public virtual IEntity CreateDropSceneWeaponObjectEntity(WeaponScanStruct weaponScan, Vector3 position, int lifeTime)
        {
            var entity = CreateSceneWeaponObjectEntity(weaponScan, position);
<<<<<<< HEAD
=======
            if(null == entity)
            {
                return null;
            }
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
            if(lifeTime > 0)
            {
                var sceneObjectEntity = entity as SceneObjectEntity;
                if(null != sceneObjectEntity)
                {
                    sceneObjectEntity.AddLifeTime(DateTime.Now, lifeTime);
                }
            }
<<<<<<< HEAD
=======
            if(SingletonManager.Get<WeaponConfigManager>().IsC4(weaponScan.ConfigId))
            {
                var sceneObjectEntity = entity as SceneObjectEntity;
                if(null != sceneObjectEntity)
                {
                    sceneObjectEntity.AddCastFlag((int)EPlayerCastState.C4Pickable);
                }
            }
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
            return entity;
        }

        public virtual void DestroySceneWeaponObjectEntity(int entityKey)
        {
            var entity = _sceneObjectContext.GetEntityWithEntityKey(new EntityKey(entityKey, (short) EEntityType.SceneObject));
            entity.isFlagDestroy = true;
        }

        public virtual IEntity CreateCastEntity(Vector3 position, float size, int key, string tip)
        {
            var entity = _sceneObjectContext.CreateEntity();
            entity.AddEntityKey(new EntityKey(_equipGenerator.GetNextEntityId(), (short) EEntityType.SceneObject));
            entity.AddSimpleCastTarget(key, size, tip);
            entity.AddPosition(position);
            entity.isFlagSyncNonSelf = true;
            entity.AddFlagImmutability(_currentTime.CurrentTime);
            return entity;
        }

    }
}