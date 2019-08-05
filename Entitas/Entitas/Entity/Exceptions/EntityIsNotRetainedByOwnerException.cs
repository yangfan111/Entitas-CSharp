namespace Entitas {

    public class EntityIsNotRetainedByOwnerException : EntitasException {

        public EntityIsNotRetainedByOwnerException(EntityExt entity, object owner)
            : base("'" + owner + "' cannot release " + entity + "!\n" +
                   "EntityExt is not retained by this object!",
                "An entity can only be released from objects that retain it.") {
        }
    }
}
