using System.Collections.Generic;

namespace Entitas {

    public delegate void GroupChanged<TEntity>(
        IGroup<TEntity> group, TEntity entity, int index, IComponent component
    ) where TEntity : class, IEntityExt;

    public delegate void GroupUpdated<TEntity>(
        IGroup<TEntity> group, TEntity entity, int index,
        IComponent previousComponent, IComponent newComponent
    ) where TEntity : class, IEntityExt;



    public interface IGroup<TEntity> :IGroup where TEntity : class, IEntityExt{
        

        TEntity[] GetEntities();
        GroupChanged<TEntity> HandleEntityNotifyOutside(TEntity entity);

        void BroadcastGroupEventsIfHoldEntity(TEntity entity, int index, IComponent previousComponent,
                                              IComponent newComponent);
    }

    public interface IGroup
    {
        int Count { get; }

        void RemoveAllEvents();
    }
}
