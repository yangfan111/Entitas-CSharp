using Entitas;

public class EntityReplaceComponent : IPerformanceTest {

    const int n = 1000000;
    IContext<Entity> _context;
    IEntityExt _e;

    public void Before() {
        _context = Helper.CreateContext();
        _context.GetGroup(Matcher<Entity>.CreateAllOf(new [] { CP.ComponentA }));
        _context.GetGroup(Matcher<Entity>.CreateAllOf(new [] { CP.ComponentB }));
        _context.GetGroup(Matcher<Entity>.CreateAllOf(new [] { CP.ComponentC }));
        _context.GetGroup(Matcher<Entity>.CreateAllOf(new [] {
            CP.ComponentA,
            CP.ComponentB
        }));
        _context.GetGroup(Matcher<Entity>.CreateAllOf(new [] {
            CP.ComponentA,
            CP.ComponentC
        }));
        _context.GetGroup(Matcher<Entity>.CreateAllOf(new [] {
            CP.ComponentB,
            CP.ComponentC
        }));
        _context.GetGroup(Matcher<Entity>.CreateAllOf(new [] {
            CP.ComponentA,
            CP.ComponentB,
            CP.ComponentC
        }));
        _e = _context.CreateEntity();
        _e.AddComponent(CP.ComponentA, new ComponentA());
    }

    public void Run() {
        for (int i = 0; i < n; i++) {
            _e.ReplaceComponent(CP.ComponentA, new ComponentA());
        }
    }
}
