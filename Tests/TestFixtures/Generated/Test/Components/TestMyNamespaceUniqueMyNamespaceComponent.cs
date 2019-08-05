//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class TestContext {

    public TestEntity myNamespaceUniqueMyNamespaceEntity { get { return AddGetGroup(TestMatcher.MyNamespaceUniqueMyNamespace).GetSingleEntity(); } }
    public My.Namespace.UniqueMyNamespaceComponent myNamespaceUniqueMyNamespace { get { return myNamespaceUniqueMyNamespaceEntity.myNamespaceUniqueMyNamespace; } }
    public bool hasMyNamespaceUniqueMyNamespace { get { return myNamespaceUniqueMyNamespaceEntity != null; } }

    public TestEntity SetMyNamespaceUniqueMyNamespace(string newValue) {
        if (hasMyNamespaceUniqueMyNamespace) {
            throw new Entitas.EntitasException("Could not set MyNamespaceUniqueMyNamespace!\n" + this + " already has an entity with My.Namespace.UniqueMyNamespaceComponent!",
                "You should check if the context already has a myNamespaceUniqueMyNamespaceEntity before setting it or use context.ReplaceMyNamespaceUniqueMyNamespace().");
        }
        var entity = CreateEntity();
        entity.AddMyNamespaceUniqueMyNamespace(newValue);
        return entity;
    }

    public void ReplaceMyNamespaceUniqueMyNamespace(string newValue) {
        var entity = myNamespaceUniqueMyNamespaceEntity;
        if (entity == null) {
            entity = SetMyNamespaceUniqueMyNamespace(newValue);
        } else {
            entity.ReplaceMyNamespaceUniqueMyNamespace(newValue);
        }
    }

    public void RemoveMyNamespaceUniqueMyNamespace() {
        myNamespaceUniqueMyNamespaceEntity.Destroy();
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class TestEntity {

    public My.Namespace.UniqueMyNamespaceComponent myNamespaceUniqueMyNamespace { get { return (My.Namespace.UniqueMyNamespaceComponent)GetComponent(TestComponentsLookup.MyNamespaceUniqueMyNamespace); } }
    public bool hasMyNamespaceUniqueMyNamespace { get { return HasComponent(TestComponentsLookup.MyNamespaceUniqueMyNamespace); } }

    public void AddMyNamespaceUniqueMyNamespace(string newValue) {
        var index = TestComponentsLookup.MyNamespaceUniqueMyNamespace;
        var component = (My.Namespace.UniqueMyNamespaceComponent)CreateComponent(index, typeof(My.Namespace.UniqueMyNamespaceComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceMyNamespaceUniqueMyNamespace(string newValue) {
        var index = TestComponentsLookup.MyNamespaceUniqueMyNamespace;
        var component = (My.Namespace.UniqueMyNamespaceComponent)CreateComponent(index, typeof(My.Namespace.UniqueMyNamespaceComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveMyNamespaceUniqueMyNamespace() {
        RemoveComponent(TestComponentsLookup.MyNamespaceUniqueMyNamespace);
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

    static Entitas.IMatcher<TestEntity> _matcherMyNamespaceUniqueMyNamespace;

    public static Entitas.IMatcher<TestEntity> MyNamespaceUniqueMyNamespace {
        get {
            if (_matcherMyNamespaceUniqueMyNamespace == null) {
                var matcher = (Entitas.Matcher<TestEntity>)Entitas.Matcher<TestEntity>.CreateAllOf(TestComponentsLookup.MyNamespaceUniqueMyNamespace);
                matcher.componentNames = TestComponentsLookup.componentNames;
                _matcherMyNamespaceUniqueMyNamespace = matcher;
            }

            return _matcherMyNamespaceUniqueMyNamespace;
        }
    }
}
