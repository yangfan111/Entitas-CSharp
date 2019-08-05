using Entitas;

public class ObjectGetProperty : IPerformanceTest {

    const int n = 10000000;
    ContextExt<EntityExt> _context;

    public void Before() {
        _context = new ContextExt<EntityExt>(1, () => new EntityExt());
    }

    public void Run() {
        for (int i = 0; i < n; i++) {
            var c = _context.TotalComponentCount;
        }
    }
}
