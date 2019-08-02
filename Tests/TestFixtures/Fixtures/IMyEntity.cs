public interface IMyEntity : Entitas.IEntityExt, INameAgeEntity { }

public partial class TestEntity : IMyEntity { }
public partial class Test2Entity : IMyEntity { }
