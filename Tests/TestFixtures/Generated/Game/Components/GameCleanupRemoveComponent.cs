//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly CleanupRemoveComponent cleanupRemoveComponent = new CleanupRemoveComponent();

    public bool isCleanupRemove {
        get { return HasComponent(GameComponentsLookup.CleanupRemove); }
        set {
            if (value != isCleanupRemove) {
                var index = GameComponentsLookup.CleanupRemove;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : cleanupRemoveComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
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
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherCleanupRemove;

    public static Entitas.IMatcher<GameEntity> CleanupRemove {
        get {
            if (_matcherCleanupRemove == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.CreateAllOf(GameComponentsLookup.CleanupRemove);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCleanupRemove = matcher;
            }

            return _matcherCleanupRemove;
        }
    }
}
