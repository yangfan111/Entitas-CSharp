using System;
using System.Collections.Generic;
using DesperateDevs.Utils;

namespace Entitas
{
    public class ContextExt<T> : IContextExt where T : EntityExt

    {
        //entitas的回收池
        private readonly Stack<T> recycleentitiesPool = new Stack<T>();

        //entitas其他持有项
        readonly HashSet<T> retainedEntities = new HashSet<T>(EntityEqualityComparer<T>.Comparer);

       
        //entitas的内容池
        private readonly HashSet<T> selfEntities = new HashSet<T>(EntityEqualityComparer<T>.Comparer);

        //IMatcher - group列表 group统一通过addGetGroups添加，用作Group外部查询
        private readonly Dictionary<IMatcher<T>, IGroup<T>> selfMatcherGroup = new Dictionary<IMatcher<T>, IGroup<T>>();
        //component index => Group List 索引,用作Component更新后Group内部查询
        private readonly List<IGroup<T>>[] selfComponentGroups;

        //------------TODO:-----------
        private readonly ObjectPool<List<GroupChanged<T>>> groupChangedListPool;
        private readonly Dictionary<string, IEntityExtIndex> entityIndices;
        //------------TODO：-----------    
        
        private Func<T, IAERC> aercFactory;
        private int creationIndex;

        //local cache
        private T[] entitiesCache;
        private Func<T> entityFactory;


        public ContextExt(int totalComponents, Func<T> entityFactory) : this(totalComponents, 0, null, null,
            entityFactory)
        {
        }

        public ContextExt(int totalComponents, int creationIndex, ContextInfo contextInfo, Func<T, IAERC> aercFactory,
                          Func<T> entityFactory)
        {
            TotalComponentCount = totalComponents;
            this.creationIndex  = creationIndex;

            if (contextInfo != null)
            {
                SharedContextInfo = contextInfo;
                if (contextInfo.componentNames.Length != totalComponents)
                {
                    throw new ContextInfoException(this, contextInfo);
                }
            }
            else
            {
                SharedContextInfo = ContextExtUtil.CreateDefaultContextInfo(TotalComponentCount);
            }

            ///-或语法格式
            ///-factory用于动态创建结构
            this.aercFactory            = aercFactory ?? (entity => new SafeAERC(entity));
            this.entityFactory          = entityFactory;
            SharedComponentsRecyclePool = new Stack<IComponent>[totalComponents];

            selfComponentGroups = new List<IGroup<T>>[totalComponents];
            entityIndices      = new Dictionary<string, IEntityExtIndex>();
            groupChangedListPool = new ObjectPool<List<GroupChanged<T>>>(
                () => new List<GroupChanged<T>>(), list => list.Clear());
        }

        //给各个Entity共用的Components回收容器
        public Stack<IComponent>[] SharedComponentsRecyclePool { get; private set; }

        public ContextInfo SharedContextInfo { get; private set; }

        //为每一个entity创建时的Component总数
        public int TotalComponentCount { get; private set; }

        public int EntityCount
        {
            get { return selfEntities.Count; }
        }

        public event ContextExtEntityChanged OnEntityCreated;

        /// Occurs when an entity will be destroyed.
        public event ContextExtEntityChanged OnEntityBeforeDestroyed;

        /// Occurs when an entity got destroyed.
        public event ContextExtEntityChanged OnEntityAfterDestroyed;

        /// Occurs when a group gets created for the first time.
        public event ContextGroupChanged OnGroupCreated;

        public void Relive(bool hard)
        {
            DestroyAllEntities();
            creationIndex = 0;
            if (hard)
                RemoveEvents();
        }

        public void RemoveEvents()
        {
            OnEntityCreated         = null;
            OnEntityAfterDestroyed  = null;
            OnEntityBeforeDestroyed = null;
            OnGroupCreated          = null;
        }

        private void CleanCache()
        {
            entitiesCache = null;
        }

        public bool HasEntity(T entity)
        {
            return selfEntities.Contains(entity);
        }

        public IGroup<T> AddGetGroup(IMatcher<T> matcher)
        {
            IGroup<T> group;
            if (!selfMatcherGroup.TryGetValue(matcher, out group))
            {
                var groupExt = new GroupExt<T>(matcher);
                group = groupExt;
                foreach (var entity in selfEntities)
                {
                    groupExt.HandleEntityWhenConstruct(entity);
                }

                selfMatcherGroup[matcher] = group;
                for (int i = 0; i < matcher.Indices.Length; i++)
                {
                    var index = matcher.Indices[i];
                    if (selfComponentGroups[index] == null)
                    {
                        selfComponentGroups[index] = new List<IGroup<T>>();
                    }

                    selfComponentGroups[index].Add(group);
                }

                if (OnGroupCreated != null)
                    OnGroupCreated(this, group);
            }

            return group;
        }

        public override string ToString()
        {
            return SharedContextInfo.name;
        }

        #region //group

        #endregion

        #region Options
        public void AddEntityIndex(IEntityExtIndex entityIndex)
        {
            if (entityIndices.ContainsKey(entityIndex.Name))
            {
                FrameworkUtil.ThrowException("entityIndices already exists");
                return;
            }

            entityIndices.Add(entityIndex.Name, entityIndex);
        }

        /// Gets the IEntityExtIndex for the specified name.
        public IEntityExtIndex GetEntityIndex(string name)
        {
            IEntityExtIndex entityIndex;
            if (!entityIndices.TryGetValue(name, out entityIndex))
            {
                FrameworkUtil.ThrowException("entityIndices donot  exists");
                return null;
            }
            return entityIndex;
        }
        public T CreateEntity()
        {
            T entity;
            if (recycleentitiesPool.Count > 0)
            {
                entity = recycleentitiesPool.Pop();
                //-使用_creationIndex做标记索引
                entity.Relive(creationIndex++);
            }
            else
            {
                entity = entityFactory();
                entity.Initialize(creationIndex++, TotalComponentCount, SharedComponentsRecyclePool, SharedContextInfo,
                    aercFactory(entity));
            }

            selfEntities.Add(entity);
            entity.Retain(this);
            CleanCache();
            entity.OnComponentAdded    += OnEntityComponentAddOrRemove;
            entity.OnComponentRemoved  += OnEntityComponentAddOrRemove;
            entity.OnComponentReplaced += OnEntityComponentReplace;

            entity.OnEntityReleased      += OnEntityReleased;
            entity.OnEntityBeforeDestroy += OnBeforeEntityDestroyed;
            entity.OnEntityAfterDestroy  += OnAfterEntityDestroyed;


            if (OnEntityCreated != null)
            {
                OnEntityCreated(this, entity);
            }

            return entity;
        }

        public T[] GetEntities()
        {
            if (entitiesCache == null)
            {
                entitiesCache = new T[selfEntities.Count];
                selfEntities.CopyTo(entitiesCache);
            }

            return entitiesCache;
        }

        public void DestroyAllEntities()
        {
            foreach (var entity in selfEntities)
            {
                entity.Destroy();
            }

            selfEntities.Clear();
            CleanCache();
            if (retainedEntities.Count > 0)
            {
                FrameworkUtil.ThrowException("retainedEntities still retains entitas ");
            }
        }

        #endregion

        #region //callbacks

        //Enity.Comonent增删替换事件调用都是为了通知group：通知groups同步更新
        void OnEntityComponentAddOrRemove(IEntityExt entity, int index, IComponent component)
        {
            var groups = selfComponentGroups[index];
            if (groups == null)
                return;
            var tEntity = (T) entity;
            //TODO:对象池技术
            List<GroupChanged<T>> events = groupChangedListPool.Get();

        
            for (int i = 0; i < groups.Count; i++)
            {
                events.Add(groups[i].HandleEntityNotifyOutside(tEntity));
            }

            for (int i = 0; i < events.Count; i++)
            {
                var groupChangedEvent = events[i];
                if (groupChangedEvent != null)
                {
                    groupChangedEvent(groups[i], tEntity, index, component);
                }
            }

            groupChangedListPool.Push(events);
        }

        //Enity.Comonent更换：通知groups
        void OnEntityComponentReplace(IEntityExt entity, int index, IComponent previousComponent,
                                      IComponent newComponent)
        {
            var groups = selfComponentGroups[index];
            if (groups != null)
            {
                var tEntity = (T) entity;

                for (int i = 0; i < groups.Count; i++)
                {
                    groups[i].BroadcastGroupEventsIfHoldEntity(tEntity, index, previousComponent, newComponent);
                }
            }
        }

        //entity被彻底释放：加入Context回收池
        void OnEntityReleased(IEntityExt entity)
        {
            T tEntity = (T) entity;
            //selfEntities.Remove(tEntity);
            retainedEntities.Remove(tEntity);
            recycleentitiesPool.Push(tEntity);
            CleanCache();
        }

        void OnBeforeEntityDestroyed(IEntityExt entityExt)
        {
            var tEntity = (T) entityExt;
            var removed = selfEntities.Remove(tEntity);
            if (!removed)
            {
                FrameworkUtil.ThrowException("entity donot exist in selfEntities when released");
            }

            CleanCache();
            if (OnEntityBeforeDestroyed != null)
                OnEntityBeforeDestroyed(this, entityExt);
        }

        void OnAfterEntityDestroyed(IEntityExt entityExt)
        {
            var tEntity = (T) entityExt;
            if (tEntity.RetainCount > 1)
            {
                retainedEntities.Add(tEntity);
            }
            else
            {
                recycleentitiesPool.Push(tEntity);
            }

            tEntity.Release(this);
            if (OnEntityAfterDestroyed != null)
                OnEntityAfterDestroyed(this, entityExt);
        }

        #endregion
    }
}