namespace Entitas {

    public class ContextInfoException : EntitasException {

        public ContextInfoException(IContextExt context, ContextInfo contextInfo)
            : base("Invalid ContextInfo for '" + context + "'!\nExpected " +
                context.TotalComponentCount + " componentName(s) but got " +
                contextInfo.componentNames.Length + ":",
                string.Join("\n", contextInfo.componentNames)) {
        }
    }
}
