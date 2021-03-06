﻿namespace Entitas {

    public class EntityIsAlreadyRetainedByOwnerException : EntitasException {

        public EntityIsAlreadyRetainedByOwnerException(EntityExt entity, object owner)
            : base("'" + owner + "' cannot retain " + entity + "!\n" +
                   "EntityExt is already retained by this object!",
                "The entity must be released by this object first.") {
        }
    }
}
