using Entitas;

public class ComponentA : IComponent {}
public class ComponentB : IComponent {}
public class ComponentC : IComponent {}
public class ComponentD : IComponent {}
public class ComponentE : IComponent {}
public class ComponentF : IComponent {}

public static class Component {
    public static readonly ComponentA A = new ComponentA();
    public static readonly ComponentB B = new ComponentB();
    public static readonly ComponentC C = new ComponentC();
}

public static class CID {

    public const int ComponentA = 1;
    public const int ComponentB = 2;
    public const int ComponentC = 3;
    public const int ComponentD = 4;

    public const int TotalComponents = 5;
}

public static class EntityTestExtensions {

    public static TestEntity AddComponentA(this IEntityExt e) { e.AddComponent(CID.ComponentA, Component.A); return (TestEntity)e; }
    public static TestEntity AddComponentB(this IEntityExt e) { e.AddComponent(CID.ComponentB, Component.B); return (TestEntity)e; }
    public static TestEntity AddComponentC(this IEntityExt e) { e.AddComponent(CID.ComponentC, Component.C); return (TestEntity)e; }

    public static bool HasComponentA(this IEntityExt e) { return e.HasComponent(CID.ComponentA); }
    public static bool HasComponentB(this IEntityExt e) { return e.HasComponent(CID.ComponentB); }
    public static bool HasComponentC(this IEntityExt e) { return e.HasComponent(CID.ComponentC); }

    public static TestEntity RemoveComponentA(this IEntityExt e) { e.RemoveComponent(CID.ComponentA); return (TestEntity)e; }
    public static TestEntity RemoveComponentB(this IEntityExt e) { e.RemoveComponent(CID.ComponentB); return (TestEntity)e; }
    public static TestEntity RemoveComponentC(this IEntityExt e) { e.RemoveComponent(CID.ComponentC); return (TestEntity)e; }

    public static ComponentA GetComponentA(this IEntityExt e) { return (ComponentA)e.GetComponent(CID.ComponentA); }
    public static ComponentB GetComponentB(this IEntityExt e) { return (ComponentB)e.GetComponent(CID.ComponentB); }
    public static ComponentC GetComponentC(this IEntityExt e) { return (ComponentC)e.GetComponent(CID.ComponentC); }

    public static TestEntity ReplaceComponentA(this IEntityExt e, ComponentA component) { e.ReplaceComponent(CID.ComponentA, component); return (TestEntity)e; }
    public static TestEntity ReplaceComponentB(this IEntityExt e, ComponentB component) { e.ReplaceComponent(CID.ComponentB, component); return (TestEntity)e; }
    public static TestEntity ReplaceComponentC(this IEntityExt e, ComponentC component) { e.ReplaceComponent(CID.ComponentC, component); return (TestEntity)e; }
}
