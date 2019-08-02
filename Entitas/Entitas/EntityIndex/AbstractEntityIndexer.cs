using System;

namespace Entitas
{
    /// <summary>
    /// Group有变化会通知EntityIndexer
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class AbstractEntityIndexer<TEntity, TKey> :IEntityExtIndex  where TEntity : class, IEntityExt
    {

        public string Name { get; private set; }
        protected readonly Func<TEntity, IComponent, TKey[]> getKeysFunc;
        private Func<TEntity, IComponent, TKey> getKeyFunc;
        private GroupExt<TEntity> group;
        private bool isSingleKey;

        protected AbstractEntityIndexer(string name, GroupExt<TEntity> group, Func<TEntity, IComponent, TKey> getKeyFunc)
        {
            Name            = name;
            this.group      = group;
            this.getKeyFunc = getKeyFunc;
            isSingleKey     = true;
        }

        protected AbstractEntityIndexer(string name, GroupExt<TEntity> group,
                                      Func<TEntity, IComponent, TKey[]> getKeysFunc)
        {
            Name             = name;
            this.group       = group;
            this.getKeysFunc = getKeysFunc;
            isSingleKey      = false;
        }

        public virtual void Activate()
        {
            group.OnEntityAddedWithComp  += OnGroupEntityAddWithComp;
            group.OnEntityRemovedWithCmp += OnGroupEntityRemoveWithComp;
            InitGroupEntities(group);
        }

        protected abstract void Clear();

        public virtual void Deactivate()
        {
            group.OnEntityAddedWithComp  -= OnGroupEntityAddWithComp;
            group.OnEntityRemovedWithCmp -= OnGroupEntityRemoveWithComp;
            Clear();
        }

        protected void InitGroupEntities(IGroup<TEntity> group)
        {
            GroupExt<TEntity> groupExt = (GroupExt<TEntity>)group;
            foreach (var entity in groupExt)
            {
                if (isSingleKey)
                {
                    AddEntityWithKey(getKeyFunc(entity, null), entity);
                }
                else
                {
                    var keys = getKeysFunc(entity, null);
                    for (int i = 0; i < keys.Length; i++)
                    {
                        AddEntityWithKey(keys[i], entity);
                    }
                }
            }
        }

        private void OnGroupEntityAddWithComp(IGroup<TEntity> group1, TEntity entity, int index, IComponent component)
        {
            if (isSingleKey)
            {
                AddEntityWithKey(getKeyFunc(entity, component), entity);
            }
            else
            {
                var keys = getKeysFunc(entity, component);
                for (int i = 0; i < keys.Length; i++)
                {
                    AddEntityWithKey(keys[i], entity);
                }
            }
        }

        private void OnGroupEntityRemoveWithComp(IGroup<TEntity> group1, TEntity entity, int index,
                                                 IComponent component)
        {
            if (isSingleKey)
            {
                RemoveEntityWithKey(getKeyFunc(entity, component), entity);
            }
            else
            {
                var keys = getKeysFunc(entity, component);
                for (int i = 0; i < keys.Length; i++)
                {
                    RemoveEntityWithKey(keys[i], entity);
                }
            }
        }

        protected virtual void AddEntityWithKey(TKey key, TEntity entity)
        {
            entity.Retain(this,false);
        }

        protected virtual void RemoveEntityWithKey(TKey key, TEntity entity)
        {
            entity.Release(this,false);
        }

        ~AbstractEntityIndexer()
        {
            Deactivate();
        }
    }

   
}