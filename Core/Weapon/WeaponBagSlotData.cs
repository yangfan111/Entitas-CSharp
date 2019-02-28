using Core.EntityComponent;
using Core.SnapshotReplication.Serialization.Serializer;

namespace Core
{



    /// <summary>
    /// 武器槽位数据结构
    /// </summary>
    public class WeaponBagSlotData
    {


        public EntityKey WeaponKey { get; private set; }
<<<<<<< HEAD
    
=======

        private WeaponSlotUpdateEvent onWeaponUpdate;
        public event WeaponSlotUpdateEvent OnWeaponUpdate
        {
            add
            {
                onWeaponUpdate += value;
                onWeaponUpdate(WeaponKey);
            }
            remove { OnWeaponUpdate -= value; }
        }
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        /// <summary>
        /// 是否赋值
        /// </summary>
        public WeaponBagSlotData()
        {
<<<<<<< HEAD
            WeaponKey = EntityKey.Default;
=======
            WeaponKey = EntityKey.EmptyWeapon;
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a

        }
        public WeaponBagSlotData Clone()
        {
            WeaponBagSlotData clone = new WeaponBagSlotData();
            clone.WeaponKey = WeaponKey;
            return clone;
        }
<<<<<<< HEAD
        //public void Arm(int keyId)
        //{
        //    Sync(new EntityKey(keyId, GameGlobalConst.WeaponEntityType));
        //}
        public void Remove(EntityKey empty)
        {
            WeaponKey = empty ;
          
=======
        public void Arm(int keyId)
        {
            Sync(new EntityKey(keyId, GameGlobalConst.WeaponEntityType));
        }
        public void Remove()
        {
            WeaponKey = EntityKey.EmptyWeapon;
            if (onWeaponUpdate != null)
                onWeaponUpdate(WeaponKey);
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        }
        public void Sync(WeaponBagSlotData from)
        {
            Sync(from.WeaponKey);
        }
        public void Sync(EntityKey key)
        {
            WeaponKey = key;
<<<<<<< HEAD
        
        }
     //   public bool IsEmpty { get { return   WeaponKey == EntityKey.Default; } }
=======
            if (onWeaponUpdate != null)
                onWeaponUpdate(WeaponKey);
        }
        public bool IsEmpty { get { return WeaponKey == EntityKey.EmptyWeapon || WeaponKey == EntityKey.Default; } }
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a

        public static bool operator ==(WeaponBagSlotData x, WeaponBagSlotData y)
        {
            return x.WeaponKey == y.WeaponKey;
        }
        public static bool operator !=(WeaponBagSlotData x, WeaponBagSlotData y)
        {
            return x.WeaponKey != y.WeaponKey;
        }

        public override string ToString()
        {
            return WeaponKey.ToString();
        }


    }








}