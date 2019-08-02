using System.Collections.Generic;
using Entitas;

public class TestReactiveSystem : ReactiveSystem<TestEntity> {

    public TestReactiveSystem(IContext<TestEntity> context) : base(context) { }

    protected override ICollector<TestEntity> GetTrigger(IContext<TestEntity> context) {
        return context.CreateCollector(Matcher<TestEntity>.CreateAllOf(0));
    }

    protected override bool Filter(TestEntity entity) {
        return true;
    }

    protected override void Execute(List<TestEntity> entities) {
    }
}
