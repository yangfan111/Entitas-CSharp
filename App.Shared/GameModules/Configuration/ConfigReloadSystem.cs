using System;
using Utils.AssetManager;
using Core.GameModule.Interface;
using Core.Configuration;
using UnityEngine;
using Utils.Configuration;
using Core.Configuration.Sound;
using System.Collections.Generic;
using Utils.Singleton;
using App.Shared.GameModules.Weapon;
using App.Shared.GameModules.Weapon;
<<<<<<< HEAD
using Assets.Utils.Configuration;
=======
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a

namespace App.Shared.GameModules.Configuration
{
    public class ConfigReloadSystem : IResourceLoadSystem
    {
        public class ConfigReloadItem
        {
            public string Asset;
            public Action<TextAsset> OnReload;
            public IConfigParser Parser;
        } 
        private AssetInfo _weapon;
        private AssetInfo _weaponPart;
        private AssetInfo _sound;
        private AssetInfo _environmentType;
        public static bool Reload;
        private PlayerContext _playerContext;
        private Contexts _contexts;
        private List<ConfigReloadItem> _configs = new List<ConfigReloadItem>(); 

        public ConfigReloadSystem(Contexts contexts)
        {
            var weaponData= new ConfigReloadItem
            {
                Asset = "WeaponData",
                OnReload = (text) => WeaponReload(contexts, text),
            };
            _configs.Add(weaponData);
            var weaponPart = new ConfigReloadItem
            {
                Asset = "weapon_parts",
                Parser = SingletonManager.Get<WeaponPartsConfigManager>(),
            };
            _configs.Add(weaponPart);
            var sound = new ConfigReloadItem
            {
                Asset = "sound",
                Parser = SingletonManager.Get<SoundConfigManager>(),
            };
            _configs.Add(sound);
            var environmentType = new ConfigReloadItem
            {
                Asset = "EnvironmentType",
                Parser = SingletonManager.Get<EnvironmentTypeConfigManager>(),
            };
            _configs.Add(environmentType);
            var stateTrans = new ConfigReloadItem
            {
                Asset = "StateTransition",
                OnReload = StateTransitionReload,
            };
            _configs.Add(stateTrans);
            _playerContext = contexts.player;
            _contexts = contexts;
        }

        public void OnLoadResources(IUnityAssetManager assetManager)
        {
            if(Reload)
            {
                foreach(var cfg in _configs)
                {
                    assetManager.LoadAssetAsync("ConfigReloadSystem", new AssetInfo("tables", cfg.Asset), OnLoadSucc);
                }
                Reload = false;
            }
        }

        public void OnLoadSucc(string source, UnityObject unityObj)
        {
            var assetInfo = unityObj.Address;
            var obj = unityObj.As<TextAsset>();

            foreach (var cfg in _configs)
            {
                if(assetInfo.AssetName.Equals(cfg.Asset))
                {
                    if(null != cfg.OnReload)
                    {
                        cfg.OnReload(obj as TextAsset);
                    }
                    else
                    {
                        CommonReload(obj as TextAsset, cfg.Parser);
                    }
                }
            }
        }

        public void CommonReload(TextAsset textAsset, IConfigParser parser)
        {
            var xmlContent = textAsset.text;
            parser.ParseConfig(xmlContent);
        }

        public void WeaponReload(Contexts contexts, TextAsset textAsset)
        {
            SingletonManager.Get<WeaponConfigManagement>().ParseConfig(textAsset.text);
            foreach(var player in _playerContext.GetEntities())
            {
<<<<<<< HEAD
                player.WeaponController().TryArmWeapon(Core.EWeaponSlotType.Pointer);
=======
                player.WeaponController().TryArmSlotWeapon(contexts, player.WeaponController().HeldSlotType);
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
            }
        }

        public void StateTransitionReload(TextAsset textAsset)
        {
            SingletonManager.Get<StateTransitionConfigManager>().ParseConfig(textAsset.text);
            _contexts.session.commonSession.GameStateProcessorFactory.GetStatePool().Reload(
                SingletonManager.Get<StateTransitionConfigManager>().GetTransitons());
        }
    }
}
