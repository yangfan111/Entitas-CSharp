//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class TestEntity {

    public TestAnyMultipleEventsStandardEventListenerComponent testAnyMultipleEventsStandardEventListener { get { return (TestAnyMultipleEventsStandardEventListenerComponent)GetComponent(TestComponentsLookup.TestAnyMultipleEventsStandardEventListener); } }
    public bool hasTestAnyMultipleEventsStandardEventListener { get { return HasComponent(TestComponentsLookup.TestAnyMultipleEventsStandardEventListener); } }

    public void AddTestAnyMultipleEventsStandardEventListener(System.Collections.Generic.List<ITestAnyMultipleEventsStandardEventListener> newValue) {
        var index = TestComponentsLookup.TestAnyMultipleEventsStandardEventListener;
        var component = (TestAnyMultipleEventsStandardEventListenerComponent)CreateComponent(index, typeof(TestAnyMultipleEventsStandardEventListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceTestAnyMultipleEventsStandardEventListener(System.Collections.Generic.List<ITestAnyMultipleEventsStandardEventListener> newValue) {
        var index = TestComponentsLookup.TestAnyMultipleEventsStandardEventListener;
        var component = (TestAnyMultipleEventsStandardEventListenerComponent)CreateComponent(index, typeof(TestAnyMultipleEventsStandardEventListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveTestAnyMultipleEventsStandardEventListener() {
        RemoveComponent(TestComponentsLookup.TestAnyMultipleEventsStandardEventListener);
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

    static Entitas.IMatcher<TestEntity> _matcherTestAnyMultipleEventsStandardEventListener;

    public static Entitas.IMatcher<TestEntity> TestAnyMultipleEventsStandardEventListener {
        get {
            if (_matcherTestAnyMultipleEventsStandardEventListener == null) {
                var matcher = (Entitas.Matcher<TestEntity>)Entitas.Matcher<TestEntity>.CreateAllOf(TestComponentsLookup.TestAnyMultipleEventsStandardEventListener);
                matcher.componentNames = TestComponentsLookup.componentNames;
                _matcherTestAnyMultipleEventsStandardEventListener = matcher;
            }

            return _matcherTestAnyMultipleEventsStandardEventListener;
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

    public void AddTestAnyMultipleEventsStandardEventListener(ITestAnyMultipleEventsStandardEventListener value) {
        var listeners = hasTestAnyMultipleEventsStandardEventListener
            ? testAnyMultipleEventsStandardEventListener.value
            : new System.Collections.Generic.List<ITestAnyMultipleEventsStandardEventListener>();
        listeners.Add(value);
        ReplaceTestAnyMultipleEventsStandardEventListener(listeners);
    }

    public void RemoveTestAnyMultipleEventsStandardEventListener(ITestAnyMultipleEventsStandardEventListener value, bool removeComponentWhenEmpty = true) {
        var listeners = testAnyMultipleEventsStandardEventListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveTestAnyMultipleEventsStandardEventListener();
        } else {
            ReplaceTestAnyMultipleEventsStandardEventListener(listeners);
        }
    }
}
