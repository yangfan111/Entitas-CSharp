using Entitas;
using NSpec;
using System.Collections.Generic;
using System.Linq;

class describe_EntityIndex : nspec {

    void when_primary_index() {

        context["single key"] = () => {

            PrimaryEntityIndexer<TestEntity, string> indexer = null;
            IContext<TestEntity> ctx = null;
            IGroup<TestEntity> group = null;

            before = () => {
                ctx = new MyTestContext();
                group = ctx.GetGroup(Matcher<TestEntity>.CreateAllOf(CID.ComponentA));
                indexer = new PrimaryEntityIndexer<TestEntity, string>("TestIndex", group, (e, c) => {
                    var nameAge = c as NameAgeComponent;
                    return nameAge != null
                        ? nameAge.name
                        : ((NameAgeComponent)e.GetComponent(CID.ComponentA)).name;
                });
            };

            context["when entity for key doesn't exist"] = () => {

                it["returns null when getting entity for unknown key"] = () => {
                    indexer.GetEntity("unknownKey").should_be_null();
                };
            };

            context["when entity for key exists"] = () => {

                const string name = "Max";
                TestEntity entity = null;

                before = () => {
                    var nameAgeComponent = new NameAgeComponent();
                    nameAgeComponent.name = name;
                    entity = ctx.CreateEntity();
                    entity.AddComponent(CID.ComponentA, nameAgeComponent);
                };

                it["gets entity for key"] = () => {
                    indexer.GetEntity(name).should_be_same(entity);
                };

                it["retains entity"] = () => {
                    entity.retainCount.should_be(3); // Context, Group, EntityIndex
                };

                it["has existing entity"] = () => {
                    var newIndex = new PrimaryEntityIndexer<TestEntity, string>("TestIndex", group, (e, c) => {
                        var nameAge = c as NameAgeComponent;
                        return nameAge != null
                            ? nameAge.name
                            : ((NameAgeComponent)e.GetComponent(CID.ComponentA)).name;
                    });
                    newIndex.GetEntity(name).should_be_same(entity);
                };

                it["releases and removes entity from index when component gets removed"] = () => {
                    entity.RemoveComponent(CID.ComponentA);
                    indexer.GetEntity(name).should_be_null();
                    entity.retainCount.should_be(1); // Context
                };

                it["throws when adding an entity for the same key"] = expect<EntityIndexException>(() => {
                    var nameAgeComponent = new NameAgeComponent();
                    nameAgeComponent.name = name;
                    entity = ctx.CreateEntity();
                    entity.AddComponent(CID.ComponentA, nameAgeComponent);
                });

                it["can ToString"] = () => {
                    indexer.ToString().should_be("PrimaryEntityIndex(TestIndex)");
                };

                context["when deactivated"] = () => {

                    before = () => {
                        indexer.Deactivate();
                    };

                    it["clears index and releases entity"] = () => {
                        indexer.GetEntity(name).should_be_null();
                        entity.retainCount.should_be(2); // Context, Group
                    };

                    it["doesn't add entities anymore"] = () => {
                        var nameAgeComponent = new NameAgeComponent();
                        nameAgeComponent.name = name;
                        ctx.CreateEntity().AddComponent(CID.ComponentA, nameAgeComponent);
                        indexer.GetEntity(name).should_be_null();
                    };

                    context["when activated"] = () => {

                        before = () => {
                            indexer.Activate();
                        };

                        it["has existing entity"] = () => {
                            indexer.GetEntity(name).should_be_same(entity);
                        };

                        it["adds new entities"] = () => {
                            var nameAgeComponent = new NameAgeComponent();
                            nameAgeComponent.name = "Jack";
                            entity = ctx.CreateEntity();
                            entity.AddComponent(CID.ComponentA, nameAgeComponent);

                            indexer.GetEntity("Jack").should_be_same(entity);
                        };
                    };
                };
            };
        };

        context["multiple keys"] = () => {

            PrimaryEntityIndexer<TestEntity, string> indexer = null;
            IContext<TestEntity> ctx = null;
            IGroup<TestEntity> group = null;

            before = () => {
                ctx = new MyTestContext();
                group = ctx.GetGroup(Matcher<TestEntity>.CreateAllOf(CID.ComponentA));
                indexer = new PrimaryEntityIndexer<TestEntity, string>("TestIndex", group, (e, c) => {
                    var nameAge = c as NameAgeComponent;
                    return nameAge != null
                        ? new [] { nameAge.name + "1", nameAge.name + "2" }
                        : new [] { ((NameAgeComponent)e.GetComponent(CID.ComponentA)).name + "1", ((NameAgeComponent)e.GetComponent(CID.ComponentA)).name + "2" };
                });
            };

            context["when entity for key exists"] = () => {

                const string name = "Max";
                TestEntity entity = null;

                before = () => {
                    var nameAgeComponent = new NameAgeComponent();
                    nameAgeComponent.name = name;
                    entity = ctx.CreateEntity();
                    entity.AddComponent(CID.ComponentA, nameAgeComponent);
                };

                it["retains entity"] = () => {
                    entity.retainCount.should_be(3);

                    var safeAerc = entity.aerc as SafeAERC;
                    if (safeAerc != null) {
                        safeAerc.owners.should_contain(indexer);
                    }
                };

                it["gets entity for key"] = () => {
                    indexer.GetEntity(name + "1").should_be_same(entity);
                    indexer.GetEntity(name + "2").should_be_same(entity);
                };

                it["releases and removes entity from index when component gets removed"] = () => {
                    entity.RemoveComponent(CID.ComponentA);
                    indexer.GetEntity(name + "1").should_be_null();
                    indexer.GetEntity(name + "2").should_be_null();
                    entity.retainCount.should_be(1);

                    var safeAerc = entity.aerc as SafeAERC;
                    if (safeAerc != null) {
                        safeAerc.owners.should_not_contain(indexer);
                    }
                };

                it["has existing entity"] = () => {
                    indexer.Deactivate();
                    indexer.Activate();
                    indexer.GetEntity(name + "1").should_be_same(entity);
                    indexer.GetEntity(name + "2").should_be_same(entity);
                };
            };
        };
    }

    void when_index() {

        context["single key"] = () => {

            MutiEntityIndexer<TestEntity, string> indexer = null;
            IContext<TestEntity> ctx = null;
            IGroup<TestEntity> group = null;

            before = () => {
                ctx = new MyTestContext();
                group = ctx.GetGroup(Matcher<TestEntity>.CreateAllOf(CID.ComponentA));
                indexer = new MutiEntityIndexer<TestEntity, string>("TestIndex", group, (e, c) => {
                    var nameAge = c as NameAgeComponent;
                    return nameAge != null
                        ? nameAge.name
                        : ((NameAgeComponent)e.GetComponent(CID.ComponentA)).name;
                });
            };

            context["when entity for key doesn't exist"] = () => {

                it["has no entities"] = () => {
                    indexer.GetEntities("unknownKey").should_be_empty();
                };
            };

            context["when entity for key exists"] = () => {

                const string name = "Max";
                NameAgeComponent nameAgeComponent = null;
                TestEntity entity1 = null;
                TestEntity entity2 = null;

                before = () => {
                    nameAgeComponent = new NameAgeComponent();
                    nameAgeComponent.name = name;
                    entity1 = ctx.CreateEntity();
                    entity1.AddComponent(CID.ComponentA, nameAgeComponent);
                    entity2 = ctx.CreateEntity();
                    entity2.AddComponent(CID.ComponentA, nameAgeComponent);
                };

                it["gets entities for key"] = () => {
                    var entities = indexer.GetEntities(name);
                    entities.Count.should_be(2);
                    entities.should_contain(entity1);
                    entities.should_contain(entity2);
                };

                it["retains entity"] = () => {
                    entity1.retainCount.should_be(3); // Context, Group, EntityIndex
                    entity2.retainCount.should_be(3); // Context, Group, EntityIndex
                };

                it["has existing entities"] = () => {
                    var newIndex = new MutiEntityIndexer<TestEntity, string>("TestIndex", group, (e, c) => {
                        var nameAge = c as NameAgeComponent;
                        return nameAge != null
                            ? nameAge.name
                            : ((NameAgeComponent)e.GetComponent(CID.ComponentA)).name;
                    });
                    newIndex.GetEntities(name).Count.should_be(2);
                };

                it["releases and removes entity from index when component gets removed"] = () => {
                    entity1.RemoveComponent(CID.ComponentA);
                    indexer.GetEntities(name).Count.should_be(1);
                    entity1.retainCount.should_be(1); // Context
                };

                it["can ToString"] = () => {
                    indexer.ToString().should_be("EntityIndex(TestIndex)");
                };

                context["when deactivated"] = () => {

                    before = () => {
                        indexer.Deactivate();
                    };

                    it["clears index and releases entity"] = () => {
                        indexer.GetEntities(name).should_be_empty();
                        entity1.retainCount.should_be(2); // Context, Group
                        entity2.retainCount.should_be(2); // Context, Group
                    };

                    it["doesn't add entities anymore"] = () => {
                        ctx.CreateEntity().AddComponent(CID.ComponentA, nameAgeComponent);
                        indexer.GetEntities(name).should_be_empty();
                    };

                    context["when activated"] = () => {

                        before = () => {
                            indexer.Activate();
                        };

                        it["has existing entities"] = () => {
                            var entities = indexer.GetEntities(name);
                            entities.Count.should_be(2);
                            entities.should_contain(entity1);
                            entities.should_contain(entity2);
                        };

                        it["adds new entities"] = () => {
                            var entity3 = ctx.CreateEntity();
                            entity3.AddComponent(CID.ComponentA, nameAgeComponent);

                            var entities = indexer.GetEntities(name);
                            entities.Count.should_be(3);
                            entities.should_contain(entity1);
                            entities.should_contain(entity2);
                            entities.should_contain(entity3);
                        };
                    };
                };
            };
        };

        context["multiple keys"] = () => {

            MutiEntityIndexer<TestEntity, string> indexer = null;
            IContext<TestEntity> ctx = null;
            IGroup<TestEntity> group = null;
            TestEntity entity1 = null;
            TestEntity entity2 = null;

            before = () => {
                ctx = new MyTestContext();
                group = ctx.GetGroup(Matcher<TestEntity>.CreateAllOf(CID.ComponentA));
                indexer = new MutiEntityIndexer<TestEntity, string>("TestIndex", group, (e, c) => {
                    return e == entity1
                        ? new [] { "1", "2" }
                        : new [] { "2", "3" };
                });
            };

            context["when entity for key exists"] = () => {

                before = () => {
                    entity1 = ctx.CreateEntity();
                    entity1.AddComponentA();
                    entity2 = ctx.CreateEntity();
                    entity2.AddComponentA();
                };

                it["retains entity"] = () => {
                    entity1.retainCount.should_be(3);
                    entity2.retainCount.should_be(3);

                    var safeAerc1 = entity1.aerc as SafeAERC;
                    if (safeAerc1 != null) {
                        safeAerc1.owners.should_contain(indexer);
                    }

                    var safeAerc2 = entity1.aerc as SafeAERC;
                    if (safeAerc2 != null) {
                        safeAerc2.owners.should_contain(indexer);
                    }
                };

                it["has entity"] = () => {
                    indexer.GetEntities("1").Count.should_be(1);
                    indexer.GetEntities("2").Count.should_be(2);
                    indexer.GetEntities("3").Count.should_be(1);
                };

                it["gets entity for key"] = () => {
                    indexer.GetEntities("1").First().should_be_same(entity1);
                    indexer.GetEntities("2").should_contain(entity1);
                    indexer.GetEntities("2").should_contain(entity2);
                    indexer.GetEntities("3").First().should_be_same(entity2);
                };

                it["releases and removes entity from index when component gets removed"] = () => {
                    entity1.RemoveComponent(CID.ComponentA);
                    indexer.GetEntities("1").Count.should_be(0);
                    indexer.GetEntities("2").Count.should_be(1);
                    indexer.GetEntities("3").Count.should_be(1);

                    entity1.retainCount.should_be(1);
                    entity2.retainCount.should_be(3);

                    var safeAerc1 = entity1.aerc as SafeAERC;
                    if (safeAerc1 != null) {
                        safeAerc1.owners.should_not_contain(indexer);
                    }

                    var safeAerc2 = entity2.aerc as SafeAERC;
                    if (safeAerc2 != null) {
                        safeAerc2.owners.should_contain(indexer);
                    }
                };

                it["has existing entities"] = () => {
                    indexer.Deactivate();
                    indexer.Activate();
                    indexer.GetEntities("1").First().should_be_same(entity1);
                    indexer.GetEntities("2").should_contain(entity1);
                    indexer.GetEntities("2").should_contain(entity2);
                    indexer.GetEntities("3").First().should_be_same(entity2);
                };
            };
        };
    }

    void when_index_multiple_components() {

        #pragma warning disable
        MutiEntityIndexer<TestEntity, string> indexer = null;
        IContext<TestEntity> ctx = null;
        IGroup<TestEntity> group = null;

        before = () => {
            ctx = new MyTestContext();
        };

        it["gets last component that triggered adding entity to group"] = () => {

            IComponent receivedComponent = null;

            group = ctx.GetGroup(Matcher<TestEntity>.CreateAllOf(CID.ComponentA, CID.ComponentB));
            indexer = new MutiEntityIndexer<TestEntity, string>("TestIndex", group, (e, c) => {
                receivedComponent = c;
                return ((NameAgeComponent)c).name;
            });

            var nameAgeComponent1 = new NameAgeComponent();
            nameAgeComponent1.name = "Max";

            var nameAgeComponent2 = new NameAgeComponent();
            nameAgeComponent2.name = "Jack";

            var entity = ctx.CreateEntity();
            entity.AddComponent(CID.ComponentA, nameAgeComponent1);
            entity.AddComponent(CID.ComponentB, nameAgeComponent2);

            receivedComponent.should_be_same(nameAgeComponent2);
        };

        it["works with NoneOf"] = () => {

            var receivedComponents = new List<IComponent>();

            var nameAgeComponent1 = new NameAgeComponent();
            nameAgeComponent1.name = "Max";

            var nameAgeComponent2 = new NameAgeComponent();
            nameAgeComponent2.name = "Jack";

            group = ctx.GetGroup(Matcher<TestEntity>.CreateAllOf(CID.ComponentA).NoneOf(CID.ComponentB));
            indexer = new MutiEntityIndexer<TestEntity, string>("TestIndex", group, (e, c) => {
                receivedComponents.Add(c);

                if (c == nameAgeComponent1) {
                    return ((NameAgeComponent)c).name;
                }

                return ((NameAgeComponent)e.GetComponent(CID.ComponentA)).name;
            });

            var entity = ctx.CreateEntity();
            entity.AddComponent(CID.ComponentA, nameAgeComponent1);
            entity.AddComponent(CID.ComponentB, nameAgeComponent2);

            receivedComponents.Count.should_be(2);
            receivedComponents[0].should_be(nameAgeComponent1);
            receivedComponents[1].should_be(nameAgeComponent2);
        };
    }
}
