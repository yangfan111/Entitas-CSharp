using System;
using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace MyNamespace {

    [CustomEntityIndex(typeof(TestContext))]
    public class CustomMutiEntityIndexer : MutiEntityIndexer<TestEntity, IntVector2> {

        static readonly List<IntVector2> _cachedList = new List<IntVector2>();

        public CustomMutiEntityIndexer(TestContext context)
            : base(
                "MyCustomEntityIndex",
                context.AddGetGroup(Matcher<TestEntity>.CreateAllOf(TestMatcher.Position, TestMatcher.Size)),
                (e, c) => {
                    var position = c is PositionComponent ? (PositionComponent)c : e.position;
                    var size = c is SizeComponent ? (SizeComponent)c : e.size;

                    _cachedList.Clear();
                    for (int x = position.x; x < position.x + size.width; x++) {
                        for (int y = position.y; y < position.y + size.height; y++) {
                            _cachedList.Add(new IntVector2(x, y));
                        }
                    }

                    return _cachedList.ToArray();
                }
            ) {
        }

        [EntityIndexGetMethod]
        public HashSet<TestEntity> GetEntitiesWithPosition(IntVector2 position) {
            return GetEntities(position);
        }

        [EntityIndexGetMethod]
        public HashSet<TestEntity> GetEntitiesWithPosition2(IntVector2 position, IntVector2 size) {
            return GetEntities(position);
        }
    }
}

[Test]
public class PositionComponent : IComponent {
    public int x;
    public int y;
}

[Test]
public class SizeComponent : IComponent {
    public int width;
    public int height;
}

public struct IntVector2 : IEquatable<IntVector2> {

    public int x;
    public int y;

    public IntVector2(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public bool Equals(IntVector2 other) {
        return other.x == x && other.y == y;
    }
}
