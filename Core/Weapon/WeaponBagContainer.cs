using Core.Animation;
using Core.Utils;
using System;
using System.IO;
using Core.SnapshotReplication.Serialization.Serializer;
using Utils.Utils.Buildin;
using Core.EntityComponent;
<<<<<<< HEAD
using System.Collections.Generic;
=======
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a

namespace Core
{

    public class WeaponBagContainer : IPatchClass<WeaponBagContainer>, IDisposable
    {
<<<<<<< HEAD
=======
        private BitArrayWrapper bitArrayWraper;
        public event WeaponHeldUpdateEvent OnHeldWeaponUpdate;
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        public WeaponBagContainer() : this(true)
        {
        }
        public WeaponBagContainer(bool initialize)
        {
            for (int i = 0; i < GameGlobalConst.WeaponSlotMaxLength; i++)
            {
<<<<<<< HEAD
                slotWeapons.Add(new WeaponBagSlotData());
=======
                slotWeapons[i] = new WeaponBagSlotData();
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
            }

        }
        public int ToIndex(EWeaponSlotType slot)
        {
            switch (slot)
            {
                case EWeaponSlotType.Pointer:
                    return HeldSlotPointer;
                default:
                    return (int)slot;

            }
        }
<<<<<<< HEAD
        private List<WeaponBagSlotData> slotWeapons = new List<WeaponBagSlotData>(GameGlobalConst.WeaponSlotMaxLength);
=======
        private WeaponBagSlotData[] slotWeapons = new WeaponBagSlotData[GameGlobalConst.WeaponSlotMaxLength];
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a

        public int HeldSlotPointer { get; private set; }

        public int LastSlotPointer { get; private set; }


        public void ChangeSlotPointer(int nowSlot)
        {
<<<<<<< HEAD

            LastSlotPointer = HeldSlotPointer;
            HeldSlotPointer = nowSlot;

        }
        public void ClearPointer()
        {
            LastSlotPointer = 0;
            HeldSlotPointer = 0;
        }
=======
        
            LastSlotPointer = HeldSlotPointer;
            HeldSlotPointer = nowSlot;
            if (OnHeldWeaponUpdate != null)
                OnHeldWeaponUpdate();
        }

>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        public WeaponBagSlotData HeldSlotData { get { return slotWeapons[HeldSlotPointer]; } }

        public WeaponBagSlotData LastSlotData { get { return slotWeapons[LastSlotPointer]; } }

        public WeaponBagSlotData this[EWeaponSlotType slot]
        {
            get
            {
                int index = ToIndex(slot);
<<<<<<< HEAD
                AssertUtility.Assert(index < slotWeapons.Count);
=======
                AssertUtility.Assert(index < slotWeapons.Length);
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
                return slotWeapons[index];
            }
        }
        public bool HasValue { get; set; }



        public WeaponBagContainer Clone()
        {

            WeaponBagContainer clone = new WeaponBagContainer(false);
            clone.HeldSlotPointer = HeldSlotPointer;
            clone.LastSlotPointer = LastSlotPointer;
            for (int i = 0; i < GameGlobalConst.WeaponSlotMaxLength; i++)
            {
                clone.slotWeapons[i] = slotWeapons[i].Clone();
            }
            return clone;
        }
        ~WeaponBagContainer()
        {
            Dispose();
        }
<<<<<<< HEAD
        //public void Trash()
        //{
        //    HeldSlotPointer = 0;
        //    LastSlotPointer = 0;
        //    for (int i = 0; i < GameGlobalConst.WeaponSlotMaxLength; i++)
        //    {
        //        slotWeapons[i].Remove();
        //    }
        //}
        public void MergeFromPatch(WeaponBagContainer from)
        {
         
=======

        public void MergeFromPatch(WeaponBagContainer from)
        {
            bool hasUtil = from.bitArrayWraper != null;
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
            HeldSlotPointer = from.HeldSlotPointer;
            LastSlotPointer = from.LastSlotPointer;
            for (int i = 0; i < GameGlobalConst.WeaponSlotMaxLength; i++)
            {
<<<<<<< HEAD
               // if (from._bitArray[i+2] )
               slotWeapons[i].Sync(from.slotWeapons[i]);
            }
            //from._bitArray.ReleaseReference();
            //from._bitArray = null;
=======
                if (!hasUtil || from.bitArrayWraper[i])
                    slotWeapons[i].Sync(from.slotWeapons[i]);
            }
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        }

        /// <summary>
        /// 等值判定
        /// </summary>
        /// <param name="right"></param>
        /// <returns></returns>
        public bool IsSimilar(WeaponBagContainer right)
        {
            if (right == null) return false;
            if (HeldSlotPointer != right.HeldSlotPointer || LastSlotPointer != right.LastSlotPointer) return false;
            for (int i = 0; i < GameGlobalConst.WeaponSlotMaxLength; i++)
            {
                if (slotWeapons[i] != right.slotWeapons[i])
                    return false;
            }
            return true;
        }
<<<<<<< HEAD
        private BitArrayWrapper _bitArray;
        //public void Read(BinaryReader reader)
        //{
            
        //    if (_bitArray != null) _bitArray.ReleaseReference();
        //    _bitArray =  reader.ReadBitArray();
        //    HeldSlotPointer = _bitArray[0] ? reader.ReadInt32(): HeldSlotPointer;
        //    LastSlotPointer = _bitArray[1] ? reader.ReadInt32() : LastSlotPointer;
        //    for (int i = 0; i < GameGlobalConst.WeaponSlotMaxLength; i++)
        //    {
        //        if (_bitArray[i+2])
        //        {
        //            var entityId = reader.ReadInt32();
        //            var entityType = reader.ReadInt16();
        //            var weaponKey = new EntityKey(entityId, entityType);
        //            slotWeapons[i].Sync(weaponKey);
        //        }
        //    }
        //}
        public void Read(BinaryReader reader)
        {

            HeldSlotPointer =  reader.ReadInt32() ;
            LastSlotPointer = reader.ReadInt32();
            for (int i = 0; i < GameGlobalConst.WeaponSlotMaxLength; i++)
            {
=======

        public void Read(BinaryReader reader)
        {
            HeldSlotPointer = reader.ReadInt32();
            LastSlotPointer = reader.ReadInt32();
            if (bitArrayWraper != null) bitArrayWraper.ReleaseReference();
            bitArrayWraper = reader.ReadBitArray();
            for (int i = 0; i < GameGlobalConst.WeaponSlotMaxLength; i++)
            {
                if (bitArrayWraper[i])
                {
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
                    var entityId = reader.ReadInt32();
                    var entityType = reader.ReadInt16();
                    var weaponKey = new EntityKey(entityId, entityType);
                    slotWeapons[i].Sync(weaponKey);
<<<<<<< HEAD
            }
        }
        public void Write(WeaponBagContainer comparedInfo, MyBinaryWriter writer)
        {

           
            writer.Write(HeldSlotPointer);
            writer.Write(LastSlotPointer);
            for (int i = 0; i < GameGlobalConst.WeaponSlotMaxLength; i++)
            {
            
                    writer.Write(slotWeapons[i].WeaponKey.EntityId);
                    writer.Write(slotWeapons[i].WeaponKey.EntityType);

            }
        }
=======
                }
            }
        }

>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        public void RewindTo(WeaponBagContainer right)
        {
            HeldSlotPointer = right.HeldSlotPointer;
            LastSlotPointer = right.LastSlotPointer;
            for (int i = 0; i < GameGlobalConst.WeaponSlotMaxLength; i++)
            {
                slotWeapons[i].Sync(right.slotWeapons[i]);
            }

        }
        /// <summary>
        /// 判断与比较值不相同的情况下，将当前值写入Wirter
        /// </summary>
        /// <param name="comparedInfo"></param>
        /// <param name="writer"></param>
<<<<<<< HEAD
        //public void Write(WeaponBagContainer comparedInfo, MyBinaryWriter writer)
        //{

        //    BitArrayWrapper bitArray = BitArrayWrapper.Allocate(2+ GameGlobalConst.WeaponSlotMaxLength, false);
        //    if (comparedInfo == null)
        //    {
        //        bitArray.SetAll(true);
        //    }
        //    else
        //    {
        //        bitArray[0] = true;
        //        bitArray[1] = true;
        //        for (int i = 0; i < GameGlobalConst.WeaponSlotMaxLength; i++)
        //        {
        //            bitArray[i+2] = slotWeapons[i] != comparedInfo.slotWeapons[i];
        //        }
        //    }
        //    writer.Write(bitArray);
        //    writer.Write(HeldSlotPointer);
        //    writer.Write(LastSlotPointer);
        //    for (int i = 0; i < GameGlobalConst.WeaponSlotMaxLength; i++)
        //    {
        //        if(bitArray[i + 2])
        //        {
        //            writer.Write(slotWeapons[i].WeaponKey.EntityId);
        //            writer.Write(slotWeapons[i].WeaponKey.EntityType);
        //        }

        //    }
        //    bitArray.ReleaseReference();
        //}
        public void Dispose()
        {
            if (_bitArray != null)
            {
                _bitArray.ReleaseReference();
=======
        public void Write(WeaponBagContainer comparedInfo, MyBinaryWriter writer)
        {

            writer.Write(HeldSlotPointer);
            writer.Write(LastSlotPointer);
            BitArrayWrapper bitArray = BitArrayWrapper.Allocate(GameGlobalConst.WeaponSlotMaxLength, false);
            //  return new BitArray(5, true);
            if (comparedInfo == null)
            {
                bitArray.SetAll(true);
            }
            else
            {
                for (int i = 0; i < GameGlobalConst.WeaponSlotMaxLength; i++)
                {
                    bitArray[i] = slotWeapons[i] != comparedInfo.slotWeapons[i];

                }
            }

            writer.Write(bitArray);
            for (int i = 0; i < GameGlobalConst.WeaponSlotMaxLength; i++)
            {
                if (bitArray[i])
                {
                    writer.Write(slotWeapons[i].WeaponKey.EntityId);
                    writer.Write(slotWeapons[i].WeaponKey.EntityType);
                }
            }


            bitArray.ReleaseReference();
        }
        public void Dispose()
        {
            if (bitArrayWraper != null)
            {
                bitArrayWraper.ReleaseReference();
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
            }
        }
        public override string ToString()
        {
<<<<<<< HEAD
            string s = "Weapon bag container\n";
=======
            string s = "*****Weapon bag container*****\n";
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
            foreach (var wp in slotWeapons)
            {
                s += wp.ToString() + "\n";
            }
<<<<<<< HEAD
            s += "--------+\n";
=======
            s += "*****************";
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
            return s;
        }


    }
}