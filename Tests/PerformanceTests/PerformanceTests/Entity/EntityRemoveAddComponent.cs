using Entitas;

public class EntityRemoveAddComponent : IPerformanceTest {

    const int n = 1000000;
    ContextExt<EntityExt> _context;
    IEntityExt _e;
    ComponentA _componentA;

    public void Before() {
        _context = Helper.CreateContext();
        _context.AddGetGroup(Matcher<EntityExt>.CreateAllOf(new [] { CP.ComponentA }));
        _context.AddGetGroup(Matcher<EntityExt>.CreateAllOf(new [] { CP.ComponentB }));
        _context.AddGetGroup(Matcher<EntityExt>.CreateAllOf(new [] { CP.ComponentC }));
        _context.AddGetGroup(Matcher<EntityExt>.CreateAllOf(new [] {
            CP.ComponentA,
            CP.ComponentB
        }));
        _context.AddGetGroup(Matcher<EntityExt>.CreateAllOf(new [] {
            CP.ComponentA,
            CP.ComponentC
        }));
        _context.AddGetGroup(Matcher<EntityExt>.CreateAllOf(new [] {
            CP.ComponentB,
            CP.ComponentC
        }));
        _context.AddGetGroup(Matcher<EntityExt>.CreateAllOf(new [] {
            CP.ComponentA,
            CP.ComponentB,
            CP.ComponentC
        }));
        _e = _context.CreateEntity();
        _componentA = new ComponentA();
        _e.AddComponent(CP.ComponentA, _componentA);
    }

    public void Run() {
        for (int i = 0; i < n; i++) {
            _e.RemoveComponent(CP.ComponentA);
            _e.AddComponent(CP.ComponentA, _e.CreateComponent<ComponentA>(CP.ComponentA));
        }
    }
}
