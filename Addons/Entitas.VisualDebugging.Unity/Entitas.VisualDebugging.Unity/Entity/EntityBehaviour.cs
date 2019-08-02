using System.Collections.Generic;
using UnityEngine;

namespace Entitas.VisualDebugging.Unity {

    [ExecuteInEditMode]
    public class EntityBehaviour : MonoBehaviour {

        public IContext context { get { return _context; } }
        public IEntityExt entity { get { return _entity; } }

        IContext _context;
        IEntityExt _entity;
        Stack<EntityBehaviour> _entityBehaviourPool;
        string _cachedName;

        public void Init(IContext context, IEntityExt entity, Stack<EntityBehaviour> entityBehaviourPool) {
            _context = context;
            _entity = entity;
            _entityBehaviourPool = entityBehaviourPool;
            _entity.OnEntityReleased += onEntityReleased;
            gameObject.hideFlags = HideFlags.None;
            gameObject.SetActive(true);
            Update();
        }

        void onEntityReleased(IEntityExt e) {
            _entity.OnEntityReleased -= onEntityReleased;
            gameObject.SetActive(false);
            gameObject.hideFlags = HideFlags.HideInHierarchy;
            _entityBehaviourPool.Push(this);
            _cachedName = null;
            name = string.Empty;
        }

        void Update() {
            if (_entity != null && _cachedName != _entity.ToString()) {
                name = _cachedName = _entity.ToString();
            }
        }

        void OnDestroy() {
            if (_entity != null) {
                _entity.OnEntityReleased -= onEntityReleased;
            }
        }
    }
}
