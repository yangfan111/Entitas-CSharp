namespace Entitas {

    public class ContextEntityIndexDoesNotExistException : EntitasException {

        public ContextEntityIndexDoesNotExistException(IContextExt context, string name)
            : base("Cannot get EntityIndex '" + name + "' from context '" +
                   context + "'!", "No EntityIndex with this name has been added.") {
        }
    }
}
