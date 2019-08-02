using System;
using System.Collections.Generic;

namespace Entitas
{
    public class MutiEntityIndexer<TEntity, TKey> : AbstractEntityIndexer<TEntity, TKey> where TEntity : class, IEntityExt
    {
        readonly Dictionary<TKey, HashSet<TEntity>> indexs;

        public MutiEntityIndexer(string name, GroupExt<TEntity> group, Func<TEntity, IComponent, TKey> getKey) : base(name,
            group, getKey)
        {
            indexs = new Dictionary<TKey, HashSet<TEntity>>();
            Activate();
        }

        public MutiEntityIndexer(string name, GroupExt<TEntity> group, Func<TEntity, IComponent, TKey[]> getKeys) : base(name,
            group, getKeys)
        {
            indexs = new Dictionary<TKey, HashSet<TEntity>>();
            Activate();
        }

        public MutiEntityIndexer(string name, GroupExt<TEntity> group, Func<TEntity, IComponent, TKey> getKey,
                           IEqualityComparer<TKey> comparer) : base(name, group, getKey)
        {
            indexs = new Dictionary<TKey, HashSet<TEntity>>(comparer);
            Activate();
        }

        public MutiEntityIndexer(string name, GroupExt<TEntity> group, Func<TEntity, IComponent, TKey[]> getKeys,
                           IEqualityComparer<TKey> comparer) : base(name, group, getKeys)
        {
            indexs = new Dictionary<TKey, HashSet<TEntity>>(comparer);
            Activate();
        }

      
        public HashSet<TEntity> GetEntities(TKey key)
        {
            HashSet<TEntity> entities;
            if (!indexs.TryGetValue(key, out entities))
            {
                entities = new HashSet<TEntity>(EntityEqualityComparer<TEntity>.Comparer);
                indexs.Add(key, entities);
            }

            return entities;
        }

      
        protected override void Clear()
        {
            foreach (var entities in indexs.Values)
            {
                foreach (var entity in entities)
                {
                    entity.Release(this,false);
                }
            }

            indexs.Clear();
        }

        protected override void AddEntityWithKey(TKey key, TEntity entity)
        {
            GetEntities(key).Add(entity);
            entity.Retain(this,false);
        }

        protected override void RemoveEntityWithKey(TKey key, TEntity entity)
        {
            GetEntities(key).Remove(entity);
            entity.Release(this,false);
        }
    }
}