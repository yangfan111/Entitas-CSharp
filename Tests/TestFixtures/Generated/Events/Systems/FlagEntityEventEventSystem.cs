//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class FlagEntityEventEventSystem : Entitas.ReactiveSystem<TestEntity> {

    readonly System.Collections.Generic.List<IFlagEntityEventListener> _listenerBuffer;

    public FlagEntityEventEventSystem(Contexts contexts) : base(contexts.test) {
        _listenerBuffer = new System.Collections.Generic.List<IFlagEntityEventListener>();
    }

    protected override Entitas.ICollector<TestEntity> GetTrigger(Entitas.IContext<TestEntity> context) {
        return Entitas.CollectorUtil.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(TestMatcher.FlagEntityEvent)
        );
    }

    protected override bool Filter(TestEntity entity) {
        return entity.isFlagEntityEvent && entity.hasFlagEntityEventListener;
    }

    protected override void Execute(System.Collections.Generic.List<TestEntity> entities) {
        foreach (var e in entities) {
            
            _listenerBuffer.Clear();
            _listenerBuffer.AddRange(e.flagEntityEventListener.value);
            foreach (var listener in _listenerBuffer) {
                listener.OnFlagEntityEvent(e);
            }
        }
    }
}
