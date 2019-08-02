namespace Entitas.VisualDebugging.Unity.Editor {

    public static class VisualDebuggingEntitasExtension {

        public static IEntityExt CreateEntity(this IContext context) {
            return (IEntityExt)context.GetType().GetMethod("CreateEntity").Invoke(context, null);
        }
    }
}
