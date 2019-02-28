using App.Shared.Components.Player;
using Assets.Utils.Configuration;
using Core.Utils;
using System;
using System.Collections.Generic;
using Utils.Singleton;

namespace App.Shared.GameModules.Weapon
{
    /// <summary>
    /// Defines the <see cref="GrenadeBagCacheHelper" />
    /// </summary>
    public class GrenadeBagCacheHelper : IBagDataCacheHelper
    {
        private static readonly LoggerAdapter Logger = new LoggerAdapter(typeof(GrenadeBagCacheHelper));
<<<<<<< HEAD
        private readonly Dictionary<int, int> heldGrenades;
        private GrenadeCacheComponentExtractor componentExtractor;
=======

        private readonly Dictionary<int, int> heldGrenades;

        private Func<GrenadeCacheDataComponent> componentExtractor;

        private GrenadeCacheDataComponent grandeCache;

>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        /// <summary>
        /// 手雷config id集合
        /// </summary>
        private readonly List<int> grenadeConfigIds;

        private readonly List<int> grenadeValuedIds;

        public GrenadeBagCacheHelper(Func<GrenadeCacheDataComponent> extractor, List<int> grenadeIds)
        {
            componentExtractor = extractor;
            var configs = SingletonManager.Get<WeaponConfigManager>().GetConfigs();
            grenadeConfigIds = grenadeIds;
            grenadeValuedIds = new List<int>();
            heldGrenades = new Dictionary<int, int>();
<<<<<<< HEAD
            foreach (var config in configs)
            {
                switch ((EWeaponType)config.Value.Type)
                {
                    case EWeaponType.ThrowWeapon:
                        var subType = (EWeaponSubType)config.Value.SubType;
                        switch (subType)
                        {
                            case EWeaponSubType.BurnBomb:
                            case EWeaponSubType.FlashBomb:
                            case EWeaponSubType.FogBomb:
                            case EWeaponSubType.Grenade:
                                break;
                            default:
                                Logger.ErrorFormat("new subtype {0} in tactic weapon", subType);
                                break;
                        }
                        grenadeConfigIds.Add(config.Value.Id);
                        break;
                }
            }

=======
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        }

        public bool AddCache(int id)
        {
            if (!grenadeConfigIds.Contains(id)) return false;
            int value;
            if (heldGrenades.TryGetValue(id, out value))
            {
                heldGrenades[id] += 1;
            }
            else
            {
                heldGrenades.Add(id, 1);
            }
            Sync();
            return true;
        }

        public bool RemoveCache(int id)
        {
            if (!grenadeConfigIds.Contains(id)) return false;
            int value;
            if (heldGrenades.TryGetValue(id, out value))
            {
                value -= 1;
                if (value < 1) heldGrenades.Remove(id);
                else heldGrenades[id] = value;
                Sync();
                return true;
            }
            return false;
        }

        public void ClearCache()
        {
            lastGrenadeId = 0;
            heldGrenades.Clear();
            Sync();
        }

        public int GetCount(int id)
        {
            return heldGrenades.ContainsKey(id) ?
                heldGrenades[id] : 0;
        }

        public List<int> GetOwnedIds()
        {
            grenadeValuedIds.Clear();
            foreach (KeyValuePair<int, int> pair in heldGrenades)
            {
                if (pair.Value > 0)
                    grenadeValuedIds.Add(pair.Key);
            }
            grenadeValuedIds.Sort();
            return grenadeValuedIds;
        }

        /// <summary>
        /// 扔出手雷实现自动填充
        /// </summary>
        /// <returns></returns>
        public int PickupNextAutomatic(int lastId)
        {
            //TODO:if (heldGrenades.Count < 1)
<<<<<<< HEAD
                return -1;
=======
            return -1;
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
            var list = GetOwnedIds();
            if (lastId < 1)
                return list[0];
            int finalIndx = list.FindIndex((data) => lastId <= data);
            return Math.Max(finalIndx, 0);
        }

        /// <summary>
        /// 手动切换手雷
        /// </summary>
        public int PickupNextManually(int lastId)
        {
            if (heldGrenades.Count < 1)
                return -1;
            int finalIndx = 0;
            var list = GetOwnedIds();
            if (lastId > 0)
            {
                finalIndx = list.FindIndex((data) => lastId < data);
                finalIndx = Math.Max(finalIndx, 0);
            }
            return list[finalIndx];
        }

        public void CacheLastGrenade(int lastId)
        {
            lastGrenadeId = lastId;
        }

        public void ClearLastCache()
        {
            lastGrenadeId = 0;
        }

        private int lastGrenadeId = 0;

        private void SetCache(int id, int count)
        {
            if (!grenadeConfigIds.Contains(id)) return;
            heldGrenades[id] = count;
<<<<<<< HEAD
           // Sync();

=======
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        }

        private void Sync()
        {
            grandeCache = componentExtractor();
            for (int i = 0; i < grenadeConfigIds.Count; i++)
                grandeCache.GrenadeArr[i].grenadeCount = GetCount(grenadeConfigIds[i]);
        }

        public void Rewind()
        {
            grandeCache = componentExtractor();
            for (int i = 0; i < grenadeConfigIds.Count; i++)
                SetCache(grenadeConfigIds[i], grandeCache.GrenadeArr[i].grenadeCount);
        }

        public int ShowCount(int id)
        {
            return GetCount(id);
        }
    }
}
