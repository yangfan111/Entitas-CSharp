using System;
using UnityEngine;

namespace Entitas.Unity {

    public class EntityLink : MonoBehaviour {

        public IEntityExt entity { get { return _entity; } }

        IEntityExt _entity;
        bool _applicationIsQuitting;

        public void Link(IEntityExt entity) {
            if (_entity != null) {
                throw new Exception("EntityLink is already linked to " + _entity + "!");
            }

            _entity = entity;
            _entity.Retain(this);
        }

        public void Unlink() {
            if (_entity == null) {
                throw new Exception("EntityLink is already unlinked!");
            }

            _entity.Release(this);
            _entity = null;
        }

        void OnDestroy() {
            if (!_applicationIsQuitting && _entity != null) {
                Debug.LogWarning("EntityLink got destroyed but is still linked to " + _entity + "!\n" +
                                 "Please call gameObject.Unlink() before it is destroyed."
                );
            }
        }

        void OnApplicationQuit() {
            _applicationIsQuitting = true;
        }

        public override string ToString() {
            return "EntityLink(" + gameObject.name + ")";
        }
    }

    public static class EntityLinkExtension {

        public static EntityLink GetEntityLink(this GameObject gameObject) {
            return gameObject.GetComponent<EntityLink>();
        }

        public static EntityLink Link(this GameObject gameObject, IEntityExt entity) {
            var link = gameObject.GetEntityLink();
            if (link == null) {
                link = gameObject.AddComponent<EntityLink>();
            }

            link.Link(entity);
            return link;
        }

        public static void Unlink(this GameObject gameObject) {
            gameObject.GetEntityLink().Unlink();
        }
    }
}
