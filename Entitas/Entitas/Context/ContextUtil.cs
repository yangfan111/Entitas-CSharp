namespace Entitas
{
    public class ContextExtUtil
    {
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