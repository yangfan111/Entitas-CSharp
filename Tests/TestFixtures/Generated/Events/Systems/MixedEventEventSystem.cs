//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class MixedEventEventSystem : Entitas.ReactiveSystem<TestEntity> {

    readonly System.Collections.Generic.List<IMixedEventListener> _listenerBuffer;

    public MixedEventEventSystem(Contexts contexts) : base(contexts.test) {
        _listenerBuffer = new System.Collections.Generic.List<IMixedEventListener>();
    }

    protected override Entitas.ICollector<TestEntity> GetTrigger(Entitas.IContext<TestEntity> context) {
        return Entitas.CollectorUtil.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(TestMatcher.MixedEvent)
        );
    }

    protected override bool Filter(TestEntity entity) {
        return entity.hasMixedEvent && entity.hasMixedEventListener;
    }

    protected override void Execute(System.Collections.Generic.List<TestEntity> entities) {
        foreach (var e in entities) {
            var component = e.mixedEvent;
            _listenerBuffer.Clear();
            _listenerBuffer.AddRange(e.mixedEventListener.value);
            foreach (var listener in _listenerBuffer) {
                listener.OnMixedEvent(e, component.value);
            }
        }
    }
}
