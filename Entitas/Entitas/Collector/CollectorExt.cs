using System.Collections.Generic;

namespace Entitas
{
    //持有若干Groups
    /// A Collector can observe one or more groups from the same context
    public class CollectorExt<TEntity> : ICollector<TEntity> where TEntity : class, IEntityExt

    {

        private MatcherEvent<TEntity>[] triggers;
        
        // public CollectorExt(GroupExt<TEntity> group, EGroupEvent eGroupEvent) : this(new[] {group}, new[] {eGroupEvent})
        // {
        // }
        public CollectorExt(MatcherEvent<TEntity>[] triggers)
        {
            this.triggers = triggers;
            CollectedEntities = new HashSet<TEntity>(EntityEqualityComparer<TEntity>.Comparer);
            Activate();
        }
  

        public HashSet<TEntity> CollectedEntities { get; private set; }

        public void Deactivate()
        {
            for (int i = 0; i < triggers.Length; i++)
            {
                GroupExt<TEntity> group = triggers[i].MatchedGroup;
                group.OnEntityAddedWithComp  -= Collect;
                group.OnEntityRemovedWithCmp -= Collect;
            }

            ClearCollectedEntities();
        }

        public void ClearCollectedEntities()
        {
            foreach (var entity in CollectedEntities)
            {
                entity.InternalRelease(this);
            }

            CollectedEntities.Clear();
        }

        public void Activate()
        {
            for (int i = 0; i < triggers.Length; i++)
            {
                var group      = triggers[i].MatchedGroup;
                var groupEvent = triggers[i].EGroupEvent;
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