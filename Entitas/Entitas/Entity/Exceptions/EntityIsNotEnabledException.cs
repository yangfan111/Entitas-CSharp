namespace Entitas {

    public class EntityIsNotEnabledException : EntitasException {

        public EntityIsNotEnabledException(string message)
            : base(message + "\nEntityExt is not enabled!",
                "The entity has already been destroyed. " +
                "You cannot modify destroyed entities.") {
        }
    }
}
