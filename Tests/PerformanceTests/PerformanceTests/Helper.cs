using Entitas;

public static class Helper {

    public static ContextExt<EntityExt> CreateContext() {
        return new ContextExt<EntityExt>(
            CP.NumComponents,
            0,
            new ContextInfo("Test Context", new string[CP.NumComponents], null),
            entity => new SafeAERC(entity),
            () => new EntityExt()
        );
    }
}
