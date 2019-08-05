using System.Collections.Generic;

namespace Entitas
{
    /// A Collector can observe one or more groups from the same context
    public class CollectorExt<TEntity> : ICollector<TEntity> where TEntity : class, IEntityExt

    {
        private EGroupEvent[] eGroupEvents;
        private GroupExt<TEntity>[] selfGroups;
        
        public CollectorExt(GroupExt<TEntity> group, EGroupEvent eGroupEvent) : this(new[] {group}, new[] {eGroupEvent})
        {
        }

        public CollectorExt(GroupExt<TEntity>[] selfGroups, EGroupEvent[] eGroupEvents)

        {
            this.selfGroups   = selfGroups;
            this.eGroupEvents = eGroupEvents;
            if (selfGroups.Length != eGroupEvents.Length)
                FrameworkUtil.ThrowException("groups Length donot match events length");
            CollectedEntities = new HashSet<TEntity>(EntityEqualityComparer<TEntity>.Comparer);
            Activate();
        }

        public HashSet<TEntity> CollectedEntities { get; private set; }

        public void Deactivate()
        {
            for (int i = 0; i < selfGroups.Length; i++)
            {
                var group = selfGroups[i];
                group.OnEntityAddedWithComp  -= Collect;
                group.OnEntityRemovedWithCmp -= Collect;
            }

            ClearCollectedEntities();
        }

        public void ClearCollectedEntities()
        {
            foreach (var entity in CollectedEntities)
            {
                entity.Release(this);
            }

            CollectedEntities.Clear();
        }

        public void Activate()
        {
            for (int i = 0; i < selfGroups.Length; i++)
            {
                var group      = selfGroups[i];
                var groupEvent = eGroupEvents[i];
                switch (groupEvent)
                {
                    case EGroupEvent.Added:
                        group.OnEntityAddedWithComp -= Collect;
                        group.OnEntityAddedWithComp += Collect;
                        break;
                    case EGroupEvent.Removed:
                        group.OnEntityRemovedWithCmp -= Collect;
                        group.OnEntityRemovedWithCmp += Collect;
                        break;
                    case EGroupEvent.AddedOrRemoved:
                        group.OnEntityAddedWithComp  -= Collect;
                        group.OnEntityAddedWithComp  += Collect;
                        group.OnEntityRemovedWithCmp -= Collect;
                        group.OnEntityRemovedWithCmp += Collect;
                        break;
                }
            }
        }

        ~CollectorExt()
        {
            Deactivate();
        }

        void Collect(IGroup<TEntity> group, TEntity entity, int index, IComponent component)
        {
            var added = CollectedEntities.Add(entity);
            if (added)
                entity.Retain(this);
        }
    }
}