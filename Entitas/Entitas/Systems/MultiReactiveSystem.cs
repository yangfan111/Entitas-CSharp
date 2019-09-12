using System.Collections.Generic;

namespace Entitas
{
    /// A ReactiveSystem calls Execute(entities) if there were changes based on
    /// the specified Collector and will only pass in changed entities.
    /// A common use-case is to react to changes, e.g. a change of the position
    /// of an entity to update the gameObject.transform.position
    /// of the related gameObject.
    public abstract class MultiReactiveSystem<TEntity, TContexts> : IReactiveSystem where TEntity : class, IEntityExt
    where TContexts : class, IContexts
    {
        readonly List<TEntity> buffer;
        readonly HashSet<TEntity> collectedEntities;

        readonly ICollector<TEntity>[] collectors;
        string _toStringCache;

        protected MultiReactiveSystem(TContexts contexts)
        {
            collectors        = GetTrigger(contexts);
            collectedEntities = new HashSet<TEntity>();
            buffer            = new List<TEntity>();
        }

        protected MultiReactiveSystem(ICollector<TEntity>[] collectors)
        {
            this.collectors   = collectors;
            collectedEntities = new HashSet<TEntity>();
            buffer            = new List<TEntity>();
        }

        /// Activates the ReactiveSystem and starts observing changes
        /// based on the specified Collector.
        /// ReactiveSystem are activated by default.
        public void Activate()
        {
            for (int i = 0; i < collectors.Length; i++)
            {
                collectors[i].Activate();
            }
        }

        /// Deactivates the ReactiveSystem.
        /// No changes will be tracked while deactivated.
        /// This will also clear the ReactiveSystem.
        /// ReactiveSystem are activated by default.
        public void Deactivate()
        {
            for (int i = 0; i < collectors.Length; i++)
            {
                collectors[i].Deactivate();
            }
        }

        /// Clears all accumulated changes.
        public void Clear()
        {
            for (int i = 0; i < collectors.Length; i++)
            {
                collectors[i].ClearCollectedEntities();
            }
        }

        /// Will call Execute(entities) with changed entities
        /// if there are any. Otherwise it will not call Execute(entities).
        public void Execute()
        {
            for (int i = 0; i < collectors.Length; i++)
            {
                var collector = collectors[i];
                if (collector.CollectedEntities.Count != 0)
                {
                    collectedEntities.UnionWith(collector.CollectedEntities);
                    collector.ClearCollectedEntities();
                }
            }

            foreach (var e in collectedEntities)
            {
                if (Filter(e))
                {
                    e.Retain(this);
                    buffer.Add(e);
                }
            }

            if (buffer.Count != 0)
            {
                try
                {
                    Execute(buffer);
                }
                finally
                {
                    for (int i = 0; i < buffer.Count; i++)
                    {
                        buffer[i].InternalRelease(this);
                    }

                    collectedEntities.Clear();
                    buffer.Clear();
                }
            }
        }

        /// Specify the collector that will trigger the ReactiveSystem.
        protected abstract ICollector<TEntity>[] GetTrigger(TContexts contexts);

        /// This will exclude all entities which don't pass the filter.
        protected abstract bool Filter(TEntity entity);

        protected abstract void Execute(List<TEntity> entities);

        public override string ToString()
        {
            if (_toStringCache == null)
            {
                _toStringCache = "MultiReactiveSystem(" + GetType().Name + ")";
            }

            return _toStringCache;
        }

        ~MultiReactiveSystem()
        {
            Deactivate();
        }
    }
}