using System;
using System.Collections.Generic;
using System.Text;

namespace Entitas
{
   
    public class EntityExt : IEntityExt
    {
        public int TotalComponentsCount { get; private set; }

        //-被context管理,当前索引号
        public int CreationIndex { get; private set; }

        public bool IsEnabled { get; private set; }

        public LoggerLevel LoggerLevel { get; set; }

        public Stack<IComponent>[] ComponentsRecirclePool { get; private set; }

        public ContextInfo ContextInfo { get; private set; }

        public IAERC Aerc { get; private set; }

        public int retainCount
        {
            get { return Aerc.retainCount; }
        }

        public void Retain(object owner,bool throwIfRepeated=true)
        {
            Aerc.Retain(this,throwIfRepeated);
        }

        public void Release(object owner,bool throwIfRepeated=true)
        {
            Aerc.Release(owner,throwIfRepeated);
            // TODO VD PERFORMANCE
            // _toStringCache = null;
            if (Aerc.retainCount == 0)
            {
                if (OnEntityReleased != null)
                {
                    OnEntityReleased(this);
                    OnEntityReleased = null;

                }
            }
        }

        //为Entity添加component后回调
        public event EntityExtComponentChanged OnComponentAdded;
        public event EntityExtComponentChanged  OnComponentRemoved;
        public event EntityExtComponentReplaced OnComponentReplaced;

        public event EntityExtEvent OnEntityReleased;
        public event EntityExtEvent OnEntityBeforeDestroy;
        public event EntityExtEvent OnEntityAfterDestroy;

        public void Relive(int creationIndex)
        {
            CreationIndex = creationIndex;
            IsEnabled     = true;
        }

        public void Initialize(int creationIndex, int totalComponents, Stack<IComponent>[] componentPools,
                               ContextInfo contextInfo = null, IAERC aerc = null)
        {
            Relive(creationIndex);
            TotalComponentsCount   = totalComponents;
            entityComponents       = new IComponent[TotalComponentsCount];
            ComponentsRecirclePool = componentPools;

            ContextInfo = contextInfo ?? CreateDefaultContextInfo();
            //-内部包含一个hashset
            Aerc = aerc ?? new SafeAERC(this);
        }

        ContextInfo CreateDefaultContextInfo()
        {
            var componentNames = new string[TotalComponentsCount];
            for (int i = 0; i < componentNames.Length; i++)
            {
                componentNames[i] = i.ToString();
            }

            return new ContextInfo("No Context", componentNames, null);
        }

        #region //local var

        //返回的数据对象,在增删查改时清除
        int[] componentIndicesCache;
        List<int> indexBuffer;
        IComponent[] componentsCache;
        List<IComponent> componentsBuffer;

        //实际entity使用的Components
        IComponent[] entityComponents;

        private void CleanCache()
        {
            componentsCache       = null;
            componentIndicesCache = null;
        }

        #endregion

        #region // getter

        public Stack<IComponent> GetComponentPool(int index)
        {
            var componentPool = ComponentsRecirclePool[index];
            if (componentPool == null)
            {
                componentPool                 = new Stack<IComponent>();
                ComponentsRecirclePool[index] = componentPool;
            }

            return componentPool;
        }

        public bool HasComponent(int index)
        {
            return entityComponents[index] != null;
        }

        public bool HasComponents(int[] indices)
        {
            for (int i = 0; i < indices.Length; i++)
            {
                if (!HasComponent(indices[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public bool HasAnyComponent(int[] indices)
        {
            for (int i = 0; i < indices.Length; i++)
            {
                if (HasComponent(indices[i]))
                {
                    return true;
                }
            }

            return false;
        }

        public IComponent GetComponent(int index)
        {
            return entityComponents[index];
        }

        public IComponent GetComponentAlarm(int index)
        {
            var comp = entityComponents[index];
            if (comp == null)
            {
                FrameworkUtil.ThrowException("Cannot get before you has one component");
            }

            return comp;
        }

        public int[] GetComponentIndices()
        {
            if (componentIndicesCache == null)
            {
                indexBuffer.Clear();
                for (int i = 0; i < entityComponents.Length; i++)
                {
                    if (HasComponent(i))
                    {
                        indexBuffer.Add(i);
                    }
                }

                componentIndicesCache = indexBuffer.ToArray();
            }

            return componentIndicesCache;
        }

        public IComponent[] GetComponents()
        {
            if (componentsCache == null)
            {
                componentsBuffer.Clear();
                for (int i = 0; i < entityComponents.Length; i++)
                {
                    if (HasComponent(i))
                        componentsBuffer.Add(entityComponents[i]);
                }
            }

            componentsCache = componentsBuffer.ToArray();
            return componentsCache;
        }

        #endregion

        #region // entity option

        public void Destroy()
        {
            if (OnEntityBeforeDestroy != null)
            {
                OnEntityBeforeDestroy(this);
                OnEntityBeforeDestroy = null;
            }

            IsEnabled = false;
            RemoveAllComponents();
            OnComponentAdded    = null;
            OnComponentReplaced = null;
            OnComponentRemoved  = null;
            if (OnEntityAfterDestroy != null)
            {
                OnEntityAfterDestroy(this);
                OnEntityAfterDestroy = null;
            }
        }

        public void RemoveAllComponents()
        {
            for (int i = 0; i < TotalComponentsCount; i++)
            {
                InternalReplaceComponent(i, null, true);
            }
        }

        public IComponent CreateComponent(int index, Type type)
        {
            var componentPool = GetComponentPool(index);
            return componentPool.Count > 0 ? componentPool.Pop() : (IComponent) Activator.CreateInstance(type);
        }

        public T CreateComponent<T>(int index) where T : new()
        {
            var componentPool = GetComponentPool(index);
            return componentPool.Count > 0 ? (T) componentPool.Pop() : new T();
        }

        //Component添加时从外部传递进来
        public void AddComponent(int index, IComponent component)
        {
            if (!IsEnabled)
            {
                FrameworkUtil.ThrowException( "Cannot add component", "entity is not enabled");
                return;
            }

            var tComponent = entityComponents[index];
            if (tComponent != null)
            {
                FrameworkUtil.ThrowException("Cannot add component", "entity already has one comp");
                return;
            }

            CleanCache();
            entityComponents[index] = component;
            if (OnComponentAdded != null)
            {
                OnComponentAdded(this, index, component);
            }
        }

        public void RemoveComponent(int index)
        {
            if (!IsEnabled)
            {
                FrameworkUtil.ThrowException("Cannot remove component", "entity is not enabled");
                return;
            }

            var tComponent = entityComponents[index];
            if (tComponent == null)
            {
                FrameworkUtil.ThrowException("Cannot remove component", "entity not has one comp");
                return;
            }

            InternalReplaceComponent(index, null);
        }

        public void ReplaceComponent(int index, IComponent component)
        {
            if (component == null)
            {
                FrameworkUtil.ThrowException("Cannot replace null component ,use remove instead");
                return;
            }

            if (!IsEnabled)
            {
                FrameworkUtil.ThrowException("Cannot remove component", "entity is not enabled");
                return;
            }

            var tComponent = entityComponents[index];
            if (tComponent == null)
            {
                AddComponent(index, component);
            }
            else
            {
                InternalReplaceComponent(index, component);
            }
        }

        void InternalReplaceComponent(int index, IComponent replacement, bool ignorePreviousNull = false)
        {
            var previousComponent = entityComponents[index];
            // TODO VD PERFORMANCE
            // _toStringCache = null;
            if (!ignorePreviousNull && previousComponent == null)
            {
                FrameworkUtil.ThrowException("Cannot replace previous null Component ");
                return;
            }

            if (previousComponent == replacement)
            {
                FrameworkUtil.ThrowException("replaced component is same with previous component ");
                return;
            }

            CleanCache();

            if (replacement == null)
            {
                entityComponents[index] = null;
                if (OnComponentRemoved != null)
                {
                    OnComponentRemoved(this, index, previousComponent);
                }
            }
            else
            {
                entityComponents[index] = replacement;
                if (OnComponentReplaced != null)
                {
                    OnComponentReplaced(this, index, previousComponent, replacement);
                }
            }

            GetComponentPool(index).Push(previousComponent);
        }

        #endregion

        public override string ToString()
        {
            return FrameworkUtil.ToString(this);
        }
    }
}