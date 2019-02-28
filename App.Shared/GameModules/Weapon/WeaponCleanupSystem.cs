using Entitas;
using System.Collections.Generic;

namespace App.Shared.GameModules.Weapon
{
<<<<<<< HEAD
    public class WeaponCleanupSystem : ReactiveSystem<WeaponEntity>
    {
        private Contexts _contexts;
        public WeaponCleanupSystem(Contexts contexts):base(contexts.weapon)
=======
    /// <summary>
    /// Defines the <see cref="WeaponCleanupSystem" />
    /// </summary>
    public class WeaponCleanupSystem : ReactiveSystem<WeaponEntity>
    {
        private Contexts _contexts;

        public WeaponCleanupSystem(Contexts contexts) : base(contexts.weapon)
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        {
            _contexts = contexts;
        }

        protected override void Execute(List<WeaponEntity> entities)
        {
<<<<<<< HEAD
            foreach(var entity in entities)
            {
                entity.weaponRuntimeInfo.Reset();
=======
            foreach (var entity in entities)
            {
                entity.weaponRuntimeData.Reset();
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
            }
        }

        protected override bool Filter(WeaponEntity entity)
        {
            return !entity.isFlagDestroy;
        }

        protected override ICollector<WeaponEntity> GetTrigger(IContext<WeaponEntity> context)
        {
            return context.CreateCollector(WeaponMatcher.Active.Removed());
        }
    }
}
