//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class Test2Entity {

    public Test2MultipleEventsStandardEventRemovedListenerComponent test2MultipleEventsStandardEventRemovedListener { get { return (Test2MultipleEventsStandardEventRemovedListenerComponent)GetComponent(Test2ComponentsLookup.Test2MultipleEventsStandardEventRemovedListener); } }
    public bool hasTest2MultipleEventsStandardEventRemovedListener { get { return HasComponent(Test2ComponentsLookup.Test2MultipleEventsStandardEventRemovedListener); } }

    public void AddTest2MultipleEventsStandardEventRemovedListener(System.Collections.Generic.List<ITest2MultipleEventsStandardEventRemovedListener> newValue) {
        var index = Test2ComponentsLookup.Test2MultipleEventsStandardEventRemovedListener;
        var component = (Test2MultipleEventsStandardEventRemovedListenerComponent)CreateComponent(index, typeof(Test2MultipleEventsStandardEventRemovedListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceTest2MultipleEventsStandardEventRemovedListener(System.Collections.Generic.List<ITest2MultipleEventsStandardEventRemovedListener> newValue) {
        var index = Test2ComponentsLookup.Test2MultipleEventsStandardEventRemovedListener;
        var component = (Test2MultipleEventsStandardEventRemovedListenerComponent)CreateComponent(index, typeof(Test2MultipleEventsStandardEventRemovedListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveTest2MultipleEventsStandardEventRemovedListener() {
        RemoveComponent(Test2ComponentsLookup.Test2MultipleEventsStandardEventRemovedListener);
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
public sealed partial class Test2Matcher {

    static Entitas.IMatcher<Test2Entity> _matcherTest2MultipleEventsStandardEventRemovedListener;

    public static Entitas.IMatcher<Test2Entity> Test2MultipleEventsStandardEventRemovedListener {
        get {
            if (_matcherTest2MultipleEventsStandardEventRemovedListener == null) {
                var matcher = (Entitas.Matcher<Test2Entity>)Entitas.Matcher<Test2Entity>.CreateAllOf(Test2ComponentsLookup.Test2MultipleEventsStandardEventRemovedListener);
                matcher.componentNames = Test2ComponentsLookup.componentNames;
                _matcherTest2MultipleEventsStandardEventRemovedListener = matcher;
            }

            return _matcherTest2MultipleEventsStandardEventRemovedListener;
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
public partial class Test2Entity {

    public void AddTest2MultipleEventsStandardEventRemovedListener(ITest2MultipleEventsStandardEventRemovedListener value) {
        var listeners = hasTest2MultipleEventsStandardEventRemovedListener
            ? test2MultipleEventsStandardEventRemovedListener.value
            : new System.Collections.Generic.List<ITest2MultipleEventsStandardEventRemovedListener>();
        listeners.Add(value);
        ReplaceTest2MultipleEventsStandardEventRemovedListener(listeners);
    }

    public void RemoveTest2MultipleEventsStandardEventRemovedListener(ITest2MultipleEventsStandardEventRemovedListener value, bool removeComponentWhenEmpty = true) {
        var listeners = test2MultipleEventsStandardEventRemovedListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveTest2MultipleEventsStandardEventRemovedListener();
        } else {
            ReplaceTest2MultipleEventsStandardEventRemovedListener(listeners);
        }
    }
}
