using Core.EntityComponent;
using Core.Event;
using Core.ObjectPool;
using Entitas;
using UnityEngine;
using App.Shared.GameModules.Weapon;
using App.Shared.Audio;

namespace App.Shared.Player.Events
{
    public class FireEvent : DefaultEvent
    {
       
        public class ObjcetFactory :CustomAbstractObjectFactory
        {
            public ObjcetFactory() : base(typeof(FireEvent)){}
            public override object MakeObject()
            {
                return new FireEvent();
            }

        }
       public FireEvent()
        {
        }

        public override EEventType EventType
        {
            get { return EEventType.Fire; }
        }

       
    }
    
    public class FireEventHandler:DefaultEventHandler
    {
        public override EEventType EventType
        {
            get { return EEventType.Fire; }
        }

      
        public override void DoEventClient( Entitas.IContexts contexts, IEntity entity, IEvent e)
        {
<<<<<<< HEAD
            var controller= (entity as PlayerEntity).WeaponController();
            if (controller != null)
            {
                controller.PlayFireAudio();
                controller.EffectList.Add(XmlConfig.EClientEffectType.MuzzleSpark);
=======
            var playerEntity = entity as PlayerEntity;
            var allContexts = contexts as Contexts;
            if (playerEntity != null)
            {
                playerEntity.WeaponController().EffectList.Add(XmlConfig.EClientEffectType.MuzzleSpark);
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
               // GameAudioMedium.ProcessWeaponAudio(playerEntity,allContexts,(item)=>item.Fire);
                // if (playerEntity.appearanceInterface.Appearance.IsFirstPerson)
                // {

                // }
                // else
                // {
                ////     GameAudioMedium.PerformOnGunFire();
                // }
            }
        }

      

        public override bool ClientFilter(IEntity entity, IEvent e)
        {
            var playerEntity = entity as PlayerEntity;
            return playerEntity != null && playerEntity.WeaponController().EffectList != null;
        }
     
    }
}