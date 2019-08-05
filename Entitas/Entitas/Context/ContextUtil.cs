namespace Entitas
{
    public static class ContextExtUtil
    {
        public static TEntity[] GetEntities<TEntity>(this IContextExt<TEntity> context, IMatcher<TEntity> matcher)
        where TEntity : class, IEntityExt
        {
            return context.GetGroup(matcher).GetEntities();
        }

        /// Creates a new entity and adds copies of all
        /// specified components to it.
        /// If replaceExisting is true it will replace exisintg components.
        public static TEntity CloneEntity<TEntity>(this IContextExt<TEntity> context, IEntityExt entity,
                                                   bool replaceExisting = false, params int[] indices)
        where TEntity : class, IEntityExt
        {
            var target = context.CreateEntity();
            entity.CopyTo(target, replaceExisting, indices);
            return target;
        }

        public static ContextInfo CreateDefaultContextInfo(int TotalComponentCount)
        {
            var          componentNames = new string[TotalComponentCount];
            const string prefix         = "Index ";
            for (int i = 0; i < componentNames.Length; i++)
            {
                componentNames[i] = prefix + i;
            }

            return new ContextInfo("Unnamed Context", componentNames, null);
        }
    }

    public delegate void ContextExtEntityChanged(IContextExt context, IEntityExt entity);

    public interface IContextExt
    {
           int TotalComponentCount { get;}
    }
    
    public interface IContextExt<TEntity> where TEntity:class,IEntityExt
    {
         TEntity CreateEntity();
         IGroup<TEntity> GetGroup(IMatcher<TEntity> matcher); //where TEntity : class, IEntityExt;
    }


    public delegate void ContextEntityChanged(IContextExt context, IEntityExt entity);
    public delegate void ContextGroupChanged(IContextExt context, IGroup group);
}