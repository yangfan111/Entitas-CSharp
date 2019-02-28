using Assets.Utils.Configuration;
using Core.EntityComponent;
using Utils.Singleton;
using WeaponConfigNs;

namespace Core
{
 
    /// <summary>
    /// 武器展示数据
    /// </summary>
    public struct WeaponScanStruct
    {
<<<<<<< HEAD
     [System.Obsolete]
=======
     
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        public EntityKey WeaponKey;
      
        public void Assign(EntityKey key, int configId)
        {
            WeaponKey = key;
            ConfigId = configId;
        }
        public void Assign(int configId)
        {
            ConfigId = configId;
            WeaponKey = EntityKey.Default;
        }

<<<<<<< HEAD
        public int ConfigId;
=======
        public int ConfigId { get; private set; }
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        public int AvatarId;
        public int Muzzle;
        public int Magazine;
        public int Stock;
        public int UpperRail;
        public int LowerRail;
        public int Bullet;
        public int ReservedBullet;
<<<<<<< HEAD
=======
        //只读
        public EFireMode FireMode;
        public bool PullBolt;
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        public readonly static WeaponScanStruct Empty = new WeaponScanStruct();

     
        public static bool operator ==(WeaponScanStruct x, WeaponScanStruct y)
        {
            return x.WeaponKey == y.WeaponKey && x.ConfigId == y.ConfigId;

        }
        public static bool operator !=(WeaponScanStruct x, WeaponScanStruct y)
        {
            return x.WeaponKey != y.WeaponKey || x.ConfigId != y.ConfigId;

        }

        public override string ToString()
        {
            return string.Format("id : {0}, avatarId {1}, muzzle {2}, magazine {3}, stock {4}, upper {5}, lower {6}, bullet {7}, reserved {8}",
                ConfigId, AvatarId, Muzzle, Magazine, Stock, UpperRail, LowerRail, Bullet, ReservedBullet);
        }
        public bool IsVailed { get { return ConfigId > 0; } }
        public bool IsSafeVailed
        {
            get
            {
                if (ConfigId < 1) return false;
<<<<<<< HEAD
                return SingletonManager.Get<WeaponResourceConfigManager>().GetConfigById(ConfigId) != null;
=======
                return SingletonManager.Get<WeaponConfigManager>().GetConfigById(ConfigId) != null;
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
            }
        }
    }

}





