//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class FlagEventRemovedEventSystem : Entitas.ReactiveSystem<TestEntity> {

    readonly Entitas.IGroup<TestEntity> _listeners;

    public FlagEventRemovedEventSystem(Contexts contexts) : base(contexts.test) {
        _listeners = contexts.test.GetGroup(TestMatcher.FlagEventRemovedListener);
    }

    protected override Entitas.ICollector<TestEntity> GetTrigger(Entitas.IContext<TestEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Removed(TestMatcher.FlagEvent)
        );
    }

    protected override bool Filter(TestEntity entity) {
        return !entity.isFlagEvent;
    }

    protected override void Execute(System.Collections.Generic.List<TestEntity> entities) {
        foreach (var e in entities) {
            
            foreach (var listenerEntity in _listeners) {
                foreach (var listener in listenerEntity.flagEventRemovedListener.value) {
                    listener.OnFlagEventRemoved(e);
                }
            }
        }
    }
}