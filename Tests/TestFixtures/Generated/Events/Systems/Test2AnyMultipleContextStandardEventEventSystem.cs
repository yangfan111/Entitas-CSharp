//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class Test2AnyMultipleContextStandardEventEventSystem : Entitas.ReactiveSystem<Test2Entity> {

    readonly Entitas.IGroup<Test2Entity> _listeners;
    readonly System.Collections.Generic.List<Test2Entity> _entityBuffer;
    readonly System.Collections.Generic.List<ITest2AnyMultipleContextStandardEventListener> _listenerBuffer;

    public Test2AnyMultipleContextStandardEventEventSystem(Contexts contexts) : base(contexts.test2) {
        _listeners = contexts.test2.AddGetGroup(Test2Matcher.Test2AnyMultipleContextStandardEventListener);
        _entityBuffer = new System.Collections.Generic.List<Test2Entity>();
        _listenerBuffer = new System.Collections.Generic.List<ITest2AnyMultipleContextStandardEventListener>();
    }

    protected override Entitas.ICollector<Test2Entity> GetTrigger(Entitas.IContext<Test2Entity> context) {
        return Entitas.CollectorUtil.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(Test2Matcher.MultipleContextStandardEvent)
        );
    }

    protected override bool Filter(Test2Entity entity) {
        return entity.hasMultipleContextStandardEvent;
    }

    protected override void Execute(System.Collections.Generic.List<Test2Entity> entities) {
        foreach (var e in entities) {
            var component = e.multipleContextStandardEvent;
            foreach (var listenerEntity in _listeners.GetEntities(_entityBuffer)) {
                _listenerBuffer.Clear();
                _listenerBuffer.AddRange(listenerEntity.test2AnyMultipleContextStandardEventListener.value);
                foreach (var listener in _listenerBuffer) {
                    listener.OnAnyMultipleContextStandardEvent(e, component.value);
                }
            }
        }
    }
}
