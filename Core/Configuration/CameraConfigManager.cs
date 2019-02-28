﻿using System;
using System.Collections.Generic;
using System.Linq;
using Core.CameraControl.NewMotor;
using UnityEngine;
using Utils.Configuration;
using Utils.Utils;
using XmlConfig;

namespace Core.Configuration
{
    public class CameraConfigManager : AbstractConfigManager<CameraConfigManager>
    {
        private Dictionary<ECameraPoseMode, CameraConfigItem> _cameraConfigItems =
            new Dictionary<ECameraPoseMode, CameraConfigItem>(CommonIntEnumEqualityComparer<ECameraPoseMode>.Instance);
        
        private CameraConfig _config;
        public string XMLContent { get; private set; }

        public CameraConfig Config
        {
            get { return _config; }
        }

        public int DeadTranstionTime { get; private set; }
        public int DefaultTranstionTime { get; private set; }
        
        public int PostTransitionTime { get; private set; }

        public override void ParseConfig(string xml)
        {
            XMLContent = xml;
            _config = XmlConfigParser<CameraConfig>.Load(xml);
            _config.PoseConfigs = _config.PoseConfigs.OrderBy(var => var.CameraType).ToArray();
    
            foreach (var cameraConfigItem in _config.PoseConfigs)
            {
                _cameraConfigItems[cameraConfigItem.CameraType] = cameraConfigItem;
                if (cameraConfigItem.Far <100)
                {
                    switch (cameraConfigItem.CameraType)
                    {
                        case ECameraPoseMode.Stand:
                        case ECameraPoseMode.DriveCar:
                        case ECameraPoseMode.Prone:
                        case ECameraPoseMode.Crouch:
                        case ECameraPoseMode.Swim:
                        case ECameraPoseMode.Rescue:
                        case ECameraPoseMode.Dying:
                        case ECameraPoseMode.Dead:
                            cameraConfigItem.Far = 1500;
                            break;
                        case ECameraPoseMode.AirPlane:
                        case ECameraPoseMode.Parachuting:
                        case ECameraPoseMode.ParachutingOpen:
                        case ECameraPoseMode.Gliding:
                            cameraConfigItem.Far = 8000;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                if (Math.Abs(cameraConfigItem.Near) < 0.00001)
                {
                    cameraConfigItem.Near = 0.03f;
                }
            }

            DefaultTranstionTime = _config.DefaultTransitionTime;
            PostTransitionTime = _config.PostTransitionTime;
        }

        public int GetTransitionTime(SubCameraMotorType type, SubCameraMotorState state)
        {
            if (type == SubCameraMotorType.Pose)
                return PoseTransitionTime((ECameraPoseMode) state.LastMode, (ECameraPoseMode) state.NowMode);
            if (type == SubCameraMotorType.Free)
                return _config.FreeConfig.TransitionTime;
            return DefaultTranstionTime;
        }
        
<<<<<<< HEAD
        private int PoseTransitionTime(ECameraPoseMode lastMode,ECameraPoseMode curMode)
=======
        public int PoseTransitionTime(ECameraPoseMode lastMode,ECameraPoseMode curMode)
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        {
            var config = _config.PoseConfigs[(int) curMode];
            if (config.PoseTransitionItems == null) return config.DefaultTime;
            foreach (var item in config.PoseTransitionItems)
            {
                if (item.LastState == lastMode)
                    return item.BaseTime;
            }
            return config.DefaultTime;
        }

    }
}