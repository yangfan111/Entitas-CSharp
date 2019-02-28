﻿using System.Collections;
using App.Shared.Components.Player;
using App.Shared.GameModules.Weapon;
using App.Shared.Components.Player;
using App.Shared.GameModules.Weapon;
using Core.CameraControl.NewMotor;
using Core.Utils;
using Core.Configuration;
using Utils.Singleton;
using XmlConfig;

namespace App.Shared.GameModules.Camera.Utils
{
    public static class CameraUtility
    {
        private static readonly LoggerAdapter Logger = new LoggerAdapter(typeof(CameraUtility));
        private static 
            CameraConfigManager _manager = SingletonManager.Get<CameraConfigManager>();
        
        public static float GetPostureTransitionTime(SubCameraMotorType motorType, SubCameraMotorState state)
        {
<<<<<<< HEAD
            return _manager.GetTransitionTime(motorType, state);
=======
            var transitionTime = _manager.GetTransitionTime(motorType, state);
            if (transitionTime <= 100) return transitionTime;
            if (transitionTime > 100 && transitionTime <= 200) return 100;
            return transitionTime - 100;
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        }

        public static bool IsCameraCanFire(this PlayerEntity playerEntity)
        {
            return playerEntity.cameraStateNew.CanFire;
        }

        public static FreeMoveEntity GetAirPlane(this PlayerEntity playerEntity, FreeMoveContext freeMoveContext)
        {
           foreach (var freeMoveEntity in freeMoveContext.GetEntities())
           {
               if (freeMoveEntity.hasFreeData && freeMoveEntity.hasPosition &&
                   freeMoveEntity.freeData.Key.Equals("plane"))
               {
                   return freeMoveEntity;
               }
           }
            return null;
        }

        public static bool IsAiming(this PlayerEntity playerEntity)
        {
            if(playerEntity.hasCameraStateNew)
            {
                return playerEntity.cameraStateNew.ViewNowMode == (short) ECameraViewMode.GunSight;
            }
            LogError("playerEntity has no cameraStateNew");
            return false;
        }

        public static ECameraArchorType GetCameraArchorType(this PlayerEntity player)
        {
            return player.cameraArchor.ArchorType;
        }

        private static void LogError(string msg)
        {
            Logger.Error(msg);
            System.Console.WriteLine(msg);
        }
    }
}