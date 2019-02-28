using Core.Utils;
using System;
using UnityEngine;
using Utils.Configuration;
using Utils.Singleton;
using WeaponConfigNs;
using XmlConfig;

namespace App.Shared.Audio
{
    /// <summary>
    /// Defines the <see cref="GameAudioMedium" />
    /// </summary>
    public class GameAudioMedium
    {
        private static readonly LoggerAdapter audioLogger = new LoggerAdapter(typeof(AKAudioDispatcher));

<<<<<<< HEAD
        public static void PlayWeaponAudio(int weaponId, GameObject target, Func<AudioWeaponItem, int> propertyFilter)
        {
            AudioEventItem evtConfig = SingletonManager.Get<AudioWeaponManager>().FindById(weaponId, propertyFilter);
            if (evtConfig != null && AKAudioEntry.Dispatcher != null)
                AKAudioEntry.Dispatcher.PostEvent(evtConfig, target);
=======
        public static void PlayWeaponAudio(int weaponId,GameObject target, Func<AudioWeaponItem, int> propertyFilter)
        {
            AudioEventItem evtConfig = SingletonManager.Get<AudioWeaponManager>().FindById(weaponId, propertyFilter);
            if(evtConfig != null && AKAudioEntry.Dispatcher != null)
                AKAudioEntry.Dispatcher.PostEvent(evtConfig, target);

>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        }
        /// <summary>
        /// 枪械切换
        /// </summary>
        /// <param name="weaponCfg"></param>
<<<<<<< HEAD
        public static void SwitchFireModelAudio(EFireMode model, GameObject target)
=======
        public static void SwitchFireModelAudio(EFireMode model,GameObject target)
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        {
#if UNITY_EDITOR
            if (AudioInfluence.IsForbidden) return;
#endif
            AudioGrp_ShotModelIndex shotModelIndex = model.ToAudioGrpIndex();
            if (AKAudioEntry.Dispatcher != null)
                AKAudioEntry.Dispatcher.SetSwitch(target, shotModelIndex);
<<<<<<< HEAD
=======

>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        }

        public static void PostAutoRegisterGameObjAudio(Vector3 position, bool createObject)
        {
        }
    }
}
