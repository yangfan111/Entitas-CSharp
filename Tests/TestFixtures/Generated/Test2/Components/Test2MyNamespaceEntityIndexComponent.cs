//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class Test2Entity {

    public My.Namespace.EntityIndexComponent myNamespaceEntityIndex { get { return (My.Namespace.EntityIndexComponent)GetComponent(Test2ComponentsLookup.MyNamespaceEntityIndex); } }
    public bool hasMyNamespaceEntityIndex { get { return HasComponent(Test2ComponentsLookup.MyNamespaceEntityIndex); } }

    public void AddMyNamespaceEntityIndex(string newValue) {
        var index = Test2ComponentsLookup.MyNamespaceEntityIndex;
        var component = (My.Namespace.EntityIndexComponent)CreateComponent(index, typeof(My.Namespace.EntityIndexComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceMyNamespaceEntityIndex(string newValue) {
        var index = Test2ComponentsLookup.MyNamespaceEntityIndex;
        var component = (My.Namespace.EntityIndexComponent)CreateComponent(index, typeof(My.Namespace.EntityIndexComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveMyNamespaceEntityIndex() {
        RemoveComponent(Test2ComponentsLookup.MyNamespaceEntityIndex);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiInterfaceGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class Test2Entity : IMyNamespaceEntityIndexEntity { }

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class Test2Matcher {

    static Entitas.IMatcher<Test2Entity> _matcherMyNamespaceEntityIndex;

    public static Entitas.IMatcher<Test2Entity> MyNamespaceEntityIndex {
        get {
            if (_matcherMyNamespaceEntityIndex == null) {
                var matcher = (Entitas.Matcher<Test2Entity>)Entitas.Matcher<Test2Entity>.CreateAllOf(Test2ComponentsLookup.MyNamespaceEntityIndex);
                matcher.componentNames = Test2ComponentsLookup.componentNames;
                _matcherMyNamespaceEntityIndex = matcher;
            }

            return _matcherMyNamespaceEntityIndex;
        }
    }
}
