//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class TestEntity {

    public AnyMixedEventListenerComponent anyMixedEventListener { get { return (AnyMixedEventListenerComponent)GetComponent(TestComponentsLookup.AnyMixedEventListener); } }
    public bool hasAnyMixedEventListener { get { return HasComponent(TestComponentsLookup.AnyMixedEventListener); } }

    public void AddAnyMixedEventListener(System.Collections.Generic.List<IAnyMixedEventListener> newValue) {
        var index = TestComponentsLookup.AnyMixedEventListener;
        var component = (AnyMixedEventListenerComponent)CreateComponent(index, typeof(AnyMixedEventListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceAnyMixedEventListener(System.Collections.Generic.List<IAnyMixedEventListener> newValue) {
        var index = TestComponentsLookup.AnyMixedEventListener;
        var component = (AnyMixedEventListenerComponent)CreateComponent(index, typeof(AnyMixedEventListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveAnyMixedEventListener() {
        RemoveComponent(TestComponentsLookup.AnyMixedEventListener);
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

    static Entitas.IMatcher<TestEntity> _matcherAnyMixedEventListener;

    public static Entitas.IMatcher<TestEntity> AnyMixedEventListener {
        get {
            if (_matcherAnyMixedEventListener == null) {
                var matcher = (Entitas.Matcher<TestEntity>)Entitas.Matcher<TestEntity>.CreateAllOf(TestComponentsLookup.AnyMixedEventListener);
                matcher.componentNames = TestComponentsLookup.componentNames;
                _matcherAnyMixedEventListener = matcher;
            }

            return _matcherAnyMixedEventListener;
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

    public void AddAnyMixedEventListener(IAnyMixedEventListener value) {
        var listeners = hasAnyMixedEventListener
            ? anyMixedEventListener.value
            : new System.Collections.Generic.List<IAnyMixedEventListener>();
        listeners.Add(value);
        ReplaceAnyMixedEventListener(listeners);
    }

    public void RemoveAnyMixedEventListener(IAnyMixedEventListener value, bool removeComponentWhenEmpty = true) {
        var listeners = anyMixedEventListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveAnyMixedEventListener();
        } else {
            ReplaceAnyMixedEventListener(listeners);
        }
    }
}
