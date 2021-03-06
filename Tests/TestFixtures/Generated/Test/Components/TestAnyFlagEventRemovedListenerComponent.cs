//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class TestEntity {

    public AnyFlagEventRemovedListenerComponent anyFlagEventRemovedListener { get { return (AnyFlagEventRemovedListenerComponent)GetComponent(TestComponentsLookup.AnyFlagEventRemovedListener); } }
    public bool hasAnyFlagEventRemovedListener { get { return HasComponent(TestComponentsLookup.AnyFlagEventRemovedListener); } }

    public void AddAnyFlagEventRemovedListener(System.Collections.Generic.List<IAnyFlagEventRemovedListener> newValue) {
        var index = TestComponentsLookup.AnyFlagEventRemovedListener;
        var component = (AnyFlagEventRemovedListenerComponent)CreateComponent(index, typeof(AnyFlagEventRemovedListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceAnyFlagEventRemovedListener(System.Collections.Generic.List<IAnyFlagEventRemovedListener> newValue) {
        var index = TestComponentsLookup.AnyFlagEventRemovedListener;
        var component = (AnyFlagEventRemovedListenerComponent)CreateComponent(index, typeof(AnyFlagEventRemovedListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveAnyFlagEventRemovedListener() {
        RemoveComponent(TestComponentsLookup.AnyFlagEventRemovedListener);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class TestMatcher {

    static Entitas.IMatcher<TestEntity> _matcherAnyFlagEventRemovedListener;

    public static Entitas.IMatcher<TestEntity> AnyFlagEventRemovedListener {
        get {
            if (_matcherAnyFlagEventRemovedListener == null) {
                var matcher = (Entitas.Matcher<TestEntity>)Entitas.Matcher<TestEntity>.CreateAllOf(TestComponentsLookup.AnyFlagEventRemovedListener);
                matcher.componentNames = TestComponentsLookup.componentNames;
                _matcherAnyFlagEventRemovedListener = matcher;
            }

            return _matcherAnyFlagEventRemovedListener;
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class TestEntity {

    public void AddAnyFlagEventRemovedListener(IAnyFlagEventRemovedListener value) {
        var listeners = hasAnyFlagEventRemovedListener
            ? anyFlagEventRemovedListener.value
            : new System.Collections.Generic.List<IAnyFlagEventRemovedListener>();
        listeners.Add(value);
        ReplaceAnyFlagEventRemovedListener(listeners);
    }

    public void RemoveAnyFlagEventRemovedListener(IAnyFlagEventRemovedListener value, bool removeComponentWhenEmpty = true) {
        var listeners = anyFlagEventRemovedListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveAnyFlagEventRemovedListener();
        } else {
            ReplaceAnyFlagEventRemovedListener(listeners);
        }
    }
}
