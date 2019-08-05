using System;
using System.Text;

namespace Entitas
{
     public enum LoggerLevel
    {
        Ignore,
        Warning,
        Error
    }

     public interface IEntityExt:IAERC
     {
         bool IsEnabled { get; }
         int CreationIndex { get; }
         int[] GetComponentIndices();
         IComponent GetComponent(int index);
         IComponent CreateComponent(int index, Type getType);
         void ReplaceComponent(int index, IComponent clonedComponent);
         void AddComponent(int index, IComponent clonedComponent);
         bool HasComponent(int index);

         void RemoveComponent(int index);
     }

    public delegate void EntityExtComponentChanged(IEntityExt entity, int index, IComponent component);

    public delegate void EntityExtComponentReplaced(IEntityExt entity, int index, IComponent previousComponent,
                                                    IComponent newComponent);

    public delegate void EntityExtEvent(IEntityExt entity);

   

}