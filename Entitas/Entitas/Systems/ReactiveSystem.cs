using System.Collections.Generic;

namespace Entitas
{
    /// 掌管Collector的System
    /// A ReactiveSystem calls Execute(entities) if there were changes based on
    /// the specified Collector and will only pass in changed entities.
    /// A common use-case is to react to changes, e.g. a change of the position
    /// of an entity to update the gameObject.transform.position
    /// of the related gameObject.
    public abstract class ReactiveSystem<TEntity> : IReactiveSystem where TEntity : class, IEntityExt
    {
        readonly ICollector<TEntity> collector;
        readonly List<TEntity> entityBuffer;
        string _toStringCache;

        protected ReactiveSystem(IContextExt<TEntity> context)
        {
            collector    = GetTrigger(context);
            entityBuffer = new List<TEntity>();
        }

        protected ReactiveSystem(ICollector<TEntity> collector)
        {
            this.collector = collector;
            entityBuffer   = new List<TEntity>();
        }

        /// Activates the ReactiveSystem and starts observing changes
        /// based on the specified Collector.
        /// ReactiveSystem are activated by default.
        public void Activate()
        {
            collector.Activate();
        }

        /// Deactivates the ReactiveSystem.
        /// No changes will be tracked while deactivated.
        /// This will also clear the ReactiveSystem.
        /// ReactiveSystem are activated by default.
        public void Deactivate()
        {
            collector.Deactivate();
        }

        /// Clears all accumulated changes.
        public void Clear()
        {
            collector.ClearCollectedEntities();
        }

        /// Will call Execute(entities) with changed entities
        /// if there are any. Otherwise it will not call Execute(entities).
        public void Execute()
        {
            if (collector.CollectedEntities.Count == 0)
                return;
            {
                foreach (var e in collector.CollectedEntities)
                {
                    if (Filter(e))
                    {
                        e.Retain(this);
                        entityBuffer.Add(e);
                    }
                }

                collector.ClearCollectedEntities();

                if (entityBuffer.Count != 0)
                {
                    try
                    {
                        Execute(entityBuffer);
                    }
                    finally
                    {
                        for (int i = 0; i < entityBuffer.Count; i++)
                        {
                            entityBuffer[i].InternalRelease(this);
                        }

                        entityBuffer.Clear();
                    }
                }
            }
        }

        /// Specify the collector that will trigger the ReactiveSystem.
        protected abstract ICollector<TEntity> GetTrigger(IContextExt<TEntity> context);

        /// This will exclude all entities which don't pass the filter.
        protected abstract bool Filter(TEntity entity);

        protected abstract void Execute(List<TEntity> entities);

        public override string ToString()
        {
            if (_toStringCache == null)
            {
                _toStringCache = "ReactiveSystem(" + GetType().Name + ")";
            }

            return _toStringCache;
        }

        ~ReactiveSystem()
        {
            Deactivate();
        }
    }
}