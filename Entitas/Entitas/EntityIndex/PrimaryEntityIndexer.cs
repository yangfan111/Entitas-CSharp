using System;
using System.Collections.Generic;

namespace Entitas
{
    /// <summary>
    ///     唯一键值索引
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class PrimaryEntityIndexer<TEntity, TKey> : AbstractEntityIndexer<TEntity, TKey>
    where TEntity : class, IEntityExt
    {
        readonly Dictionary<TKey, TEntity> indexs;

        public PrimaryEntityIndexer(string name, GroupExt<TEntity> group, Func<TEntity, IComponent, TKey> getKey) :
                        base(name, group, getKey)
        {
            indexs = new Dictionary<TKey, TEntity>();
            Activate();
        }

        public PrimaryEntityIndexer(string name, GroupExt<TEntity> group, Func<TEntity, IComponent, TKey[]> getKeys) :
                        base(name, group, getKeys)
        {
            indexs = new Dictionary<TKey, TEntity>();
            Activate();
        }

        public PrimaryEntityIndexer(string name, GroupExt<TEntity> group, Func<TEntity, IComponent, TKey> getKey,
                                    IEqualityComparer<TKey> comparer) : base(name, group, getKey)
        {
            indexs = new Dictionary<TKey, TEntity>(comparer);
            Activate();
        }

        public PrimaryEntityIndexer(string name, GroupExt<TEntity> group, Func<TEntity, IComponent, TKey[]> getKeys,
                                    IEqualityComparer<TKey> comparer) : base(name, group, getKeys)
        {
            indexs = new Dictionary<TKey, TEntity>(comparer);
            Activate();
        }

        public TEntity GetEntity(TKey key)
        {
            TEntity entity;
            indexs.TryGetValue(key, out entity);
            return entity;
        }

        protected override void Clear()
        {
            foreach (var entity in indexs.Values)
            {
                entity.Release(this);
            }

            indexs.Clear();
        }

        protected override void AddEntityWithKey(TKey key, TEntity entity)
        {
            if (indexs.ContainsKey(key))
            {
                FrameworkUtil.ThrowException("Entity for key '" + key + "' already exists");
                return;
            }

            indexs.Add(key, entity);

            base.AddEntityWithKey(key, entity);
        }

        protected override void RemoveEntityWithKey(TKey key, TEntity entity)
        {
            indexs.Remove(key);
            base.RemoveEntityWithKey(key, entity);
        }
    }
}