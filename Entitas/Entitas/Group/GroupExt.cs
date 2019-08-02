using System.Collections.Generic;

namespace Entitas
{
    /// <summary>
    ///     Matcher容器
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class GroupExt<TEntity> : IGroup<TEntity> where TEntity : class,IEntityExt
    {
        readonly HashSet<TEntity> entities = new HashSet<TEntity>(EntityEqualityComparer<TEntity>.Comparer);
        TEntity[] entitiesCache;
        TEntity singleEntityCache;

        public GroupExt(IMatcher<TEntity> matcher)
        {
            Matcher = matcher;
        }

        public IMatcher<TEntity> Matcher { get; private set; }

        public void RemoveAllEvents()
        {
            OnEntityAddedWithComp  = null;
            OnEntityRemovedWithCmp = null;
            OnEntityUpdatedWithCmp = null;
        }

        public int Count
        {
            get { return entities.Count; }
        }

        public event GroupChanged<TEntity> OnEntityAddedWithComp;
        public event GroupChanged<TEntity> OnEntityRemovedWithCmp;
        public event GroupUpdated<TEntity> OnEntityUpdatedWithCmp;

        public TEntity[] GetEntities()
        {
            if (entitiesCache == null)
            {
                entitiesCache = new TEntity[entities.Count];
                entities.CopyTo(entitiesCache);
            }

            return entitiesCache;
        }

        public bool ContainsEntity(TEntity entity)
        {
            return entities.Contains(entity);
        }

        private void CleanCache()
        {
            entitiesCache     = null;
            singleEntityCache = null;
        }

        public TEntity GetSingleEntity()
        {
            if (singleEntityCache == null)
            {
                var c = entities.Count;
                if (c == 1)
                {
                    using (var enumerator = entities.GetEnumerator())
                    {
                        enumerator.MoveNext();
                        singleEntityCache = enumerator.Current;
                    }
                }
                else if (c == 0)
                {
                    return null;
                }
                else
                {
                    throw new GroupSingleEntityException<TEntity>(this);
                }
            }

            return singleEntityCache;
        }


        public void HandleEntityNotifyInside(TEntity entity, int index, IComponent component)
        {
            if (Matcher.Matches(entity))
            {
                InternalAddEntity(entity, index, component);
            }
            else
            {
                InternalRemoveEntity(entity, index, component);
            }
        }

        public void BroadcastGroupEventsIfHoldEntity(TEntity entity, int index, IComponent previousComponent,
                                                     IComponent newComponent)
        {
            if (entities.Contains(entity))
            {
                if (OnEntityRemovedWithCmp != null)
                {
                    OnEntityRemovedWithCmp(this, entity, index, previousComponent);
                }

                if (OnEntityAddedWithComp != null)
                {
                    OnEntityAddedWithComp(this, entity, index, newComponent);
                }

                if (OnEntityUpdatedWithCmp != null)
                {
                    OnEntityUpdatedWithCmp(this, entity, index, previousComponent, newComponent);
                }
            }
        }

        public GroupChanged<TEntity> HandleEntityNotifyOutside(TEntity entity)
        {
            return Matcher.Matches(entity)
                            ? (InternalAddEntitySilently(entity) ? OnEntityAddedWithComp : null)
                            : (InternalRemoveEntitySilently(entity) ? OnEntityRemovedWithCmp : null);
        }

        //添加到enties
        public void HandleEntityWhenConstruct(TEntity entity)
        {
            if (Matcher.Matches(entity))
            {
                InternalAddEntitySilently(entity);
            }
            else
            {
                InternalRemoveEntitySilently(entity);
            }
        }

        void InternalAddEntity(TEntity entity, int index, IComponent component)
        {
            if (InternalAddEntitySilently(entity) && OnEntityAddedWithComp != null)
            {
                OnEntityAddedWithComp(this, entity, index, component);
            }
        }

        void InternalRemoveEntity(TEntity entity, int index, IComponent component)
        {
            var removed = entities.Remove(entity);
            if (removed)
            {
                CleanCache();
                if (OnEntityRemovedWithCmp != null)
                {
                    OnEntityRemovedWithCmp(this, entity, index, component);
                }

                entity.Release(this);
            }
        }

        bool InternalAddEntitySilently(TEntity entity)
        {
            if (entity.IsEnabled)
            {
                var added = entities.Add(entity);
                if (added)
                {
                    CleanCache();
                    entity.Retain(this);
                }

                return added;
            }

            return false;
        }

        bool InternalRemoveEntitySilently(TEntity entity)
        {
            var removed = entities.Remove(entity);
            if (removed)
            {
                CleanCache();
                //-移除当前对象对它的引用
                entity.Release(this);
            }

            return removed;
        }

        public IEnumerable<TEntity> AsEnumerable()
        {
            return entities;
        }

        public HashSet<TEntity>.Enumerator GetEnumerator()
        {
            return entities.GetEnumerator();
        }
    }
}