Simplify.Your(Automated.Tests).With(Fluent.Syntax)
============================================

This repo contains sample projects used in my new blog post
about using the Builder pattern in automated software tests.

## Example
Classic:
```c#
[Fact]
public void FindRoomForFriendsMeetup_WhenOnlyOneLargestRoomExist_ThenReturnLargestRoom2()
{
    // Arrange
    House house = new House
    {
        Floors = new[]
        {
            new Floor { Level = 1, Rooms = new[]
            {
                new Room("Kitchen", size: 10, roomNr:2, numberOfWallSockets:4, numberOfWaterSupplies:2, color:"Black", renovatedDate:new DateTime(2009, 06, 01)),
                new Room("Living Room", size: 40, roomNr:1, numberOfWallSockets:4, numberOfWaterSupplies:0, color:"White", renovatedDate:new DateTime(2020, 06, 01))
            }.ToList()},
            new Floor { Level = 2, Rooms = new[]
            {
                new Room("Bathroom", size: 5, roomNr:2, numberOfWallSockets:2, numberOfWaterSupplies:3, color:"Blue", renovatedDate:new DateTime(2012, 06, 01)),
                new Room("Bedroom", size: 10, roomNr:23, numberOfWallSockets:2, numberOfWaterSupplies:0, color:"Green", renovatedDate:new DateTime(2014, 06, 01))
            }.ToList()},
        }.ToList(),
        Garage = new Garage(),
        Pool = new Pool(),
        Garden = new Garden()
    };

    // Act
    (int level, string roomName) = new RoomFinder(house).FindRoomForFriendsMeetup();

    // Assert
    level.Should().Be(1);
    roomName.Should().Be("Living Room");
}
```
With Fluent Syntax:
```c#
[Fact]
public void FindRoomForFriendsMeetup_WhenOnlyOneLargestRoomExist_ThenReturnLargestRoom()
{
    // Arrange
    House house = TestHouse.Create().WithFloors(
            TestFloor.Create(level: 1).WithRoom("Kitchen", size: 10).WithRoom("Living Room", size: 40),
            TestFloor.Create(level: 2).WithRoom("Bathroom", size: 5).WithRoom("Bedroom", size: 10))
        .WithGarage().WithPool().WithGarden();

    // Act
    (int level, string roomName) = new RoomFinder(house).FindRoomForFriendsMeetup();

    // Assert
    level.Should().Be(1);
    roomName.Should().Be("Living Room");
}
```

## What do I mean with *"Fluent Syntax"*?
* Also named as [Fluent interface](https://en.wikipedia.org/wiki/Fluent_interface)
* An API using pattern like [method chaining](https://en.wikipedia.org/wiki/Method_chaining),
  the [builder pattern](https://en.wikipedia.org/wiki/Builder_pattern),
* For example, each method returns `this` or some other context which can be used for subsequent method calls.
* The goal is to write in natural human language or make the code more
  [domain-specific (DSL)](https://en.wikipedia.org/wiki/Domain-specific_language).
* Another goal is to decouple the creation of objects/data from the object itself.
* Popular libraries using it: C# LINQ/FluentAssertions, Java Steam API/AssertJ, Javascript jQuery/Jasmine...
* In this post, I'm also mentioning some other best practices for tests without Fluent Syntax.

## The Principle of Chekhov's Gun
> Remove everything that has no relevance to the story. If you say in the first chapter that there is
  a rifle hanging on the wall, in the second or third chapter it absolutely must go off. If it’s not going to be fired,
  it shouldn’t be hanging there.

What it means for automated software tests:
  * Everything within the body of a test should be important to the test
  (e.g. room name, `size` and `level`)
  * All visible values or method calls are necessary and should influence the execution path being tested.
  * Everything else will make the tests noisy and hard to understand.
  (e.g. `roomNr`, `renovatedDate`)
  * BUT: Do not hide any values, that are important to the test (e.g. ``level: GetLevel()``)

## Advantages of Fluent Syntax
* Usage of the productive code can be different in tests, because you need to mock/fake some dependencies.
* A test API helps as abstraction layer to avoid changes when you're refactoring the productive code.
* Readability of test code is as important as of productive code.
* Reduces the lines of code per test (our team's convention is max. 1 screen height/test)
* Using a domain-specific human language, your tests could look like "coded" acceptance criteria.
* It's easier for you and your team members (maybe even with a stakeholder) to discuss the expected
  software behaviour by going through your tests.
* Your tests could be the living documentation of your system
  ([Specification by example](https://en.wikipedia.org/wiki/Specification_by_example))
* Fluent tests support the approach of
  [Behaviour Driven Development](https://de.wikipedia.org/wiki/Behavior_Driven_Development) or
  [Acceptance test–driven development (ATDD)](https://en.wikipedia.org/wiki/Acceptance_test%E2%80%93driven_development).
  For example, the implementation of Given/When/Then steps could be written in place with the step definition
```c#
public class SalaryCalculationFeature
{
    [Scenario]
    public void CalculateSalary(Employee employee, Company company, double salary)
    {
        "Given a company working Mo-Fr between 08:00-12:00 and 13:00-17:00"
            .x(() => company = TestCompany.Working()
                .On(DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday)
                .Daily.From(08, 00).To(12,00)
                .Daily.From(13, 00).To(17,00).Company);

        "And an employee payed 30$ per hour"
            .x(() => employee = TestEmployee.Create()
                .WithContract(TestEmployeeContract.For(company).Earning(30).Per(TimeSpan.FromHours(1))));

        "When the employee has worked November 2021"
            .x(() => salary = SalaryCalculator.CalculateSalary(employee, DateTime.Parse("2021-11-01"), DateTime.Parse("2021-12-01")));

        "Then his salary is "
            .x(() => salary.Should().Be(5280));
    }
}
  ```

## How to write better tests
The following code snippets are very abstract and small for better understanding.
In real situations your productive classes will have probably
* dozens of properties
* large constructors
* aggregations
* hierarchical structure
* primary/foreign keys or navigation properties for O/R mapping
* and so on...

I will show you the patterns based on a simple productive DTO-styled class `Foo`:
```c#
namespace FooProject.FeatureX;
public class Foo
{
    public Foo() { }
    public Foo(string property1, int property2, double property3)
    {
        Property1 = property1;
        Property2 = property2;
        Property3 = property3;
    }

    public string Property1 { get; set; }
    public int Property2 { get; set; }
    public double Property3 { get; set; }
    public List<Bar> Bars { get; set; } = new List<Bar>();
}
```

### Begin with Static Methods and Classes
The very first step to improve your tests is NOT to build a fully extensible, human friendly and fancy fluent API!

I used to add a simple static helper method with required or optional parameters in the test class itself,
only if more than 3 test cases use it:
```c#
private Foo CreateFoo(string property1, int property2, double property3 = 12.34) => { ... }
  ```

Then, if more than 3 test classes/fixtures need to set up the same productive class,
* I move the test class helper methods into a static test helper class.
* I prefer to name the test helper class after the productive class with a prefix ``Test``.
* I place the test helper classes in the corresponding .NET test project/namespace for each productive class.
In this way, the dependencies of all test helper classes and all productive classes are similar.
```c#
namespace FooProject.Tests.FeatureX;
public static class TestFoo { ... }
 ```

### Continue with Static Factory Methods
When you created your new test helper classes, you can use static factory methods
* to init your productive class with some meaningful sample values from productive scenarios.
* Make some method properties optional, when they aren't relevant in your tests
* Do not introduce properties, if they are never or rarely relevant in tests.
* Instead, you could use the test helper and set the special property afterwards (if productive class is mutable).
```c#
public static class TestFoo
{
    public const double Property3Default = 1.23; // frequently used sample values as public constants
    public static Foo Create() => Create("A"); // shortcut for tests where all properties are irrelevant
    public static Foo Create(string property1, int? optionalProperty2 = null)
    {
        return new Foo
        {
            Property1 = property1,
            Property2 = optionalProperty2 ?? 1, // default value can be defined here or as const
            Property3 = Property3Default // not relevant in tests (yet)
        };
    }
}
```

### Now, build your Fluent API (using Extension Methods in C#)
As soon as you have too many factory methods with too much code duplication in your test class,
or when need more flexibility in your tests, then you finally could write some fluent syntax.

Usually, this is done with a *Builder Pattern* by setting some properties on the productive class
and returning the builder instance (this). But C# has a nice feature named
[Extension Methods](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods)
so that you can return the productive class itself and keep the test helper class static:

```c#
public static class TestFoo
{
    private static int _property1Counter = 1;

    // some properties need to be unique or random for each test
    public static string CreateParam1() => $"Property1-{_property1Counter++}";

    public static Foo Create() => new Foo().WithPropertyGroup1().WithPropertyGroup2(123);

    // split into independent groups when properties are often configured/omitted together
    public static Foo WithPropertyGroup1(this Foo foo, string property1 = null)
    {
        foo.Property1 = property1 ?? CreateParam1();
        return foo;
    }

    public static Foo WithPropertyGroup2(this Foo foo, int property2, double? optionalProperty3 = null)
    {
        foo.Property2 = property2;
        foo.Property3 = optionalProperty3 ?? TestFoo.Property3Default;
        return foo;
    }

    // shortcuts for frequently used variants
    public static Foo CreateAsVariantX() => Create().WithPropertyGroup1("X").WithPropertyGroup2(123, 12.34);
    public static Foo CreateAsVariantY() => Create().WithPropertyGroup1("Y").WithPropertyGroup2(234);
}
```

### Or use the Builder Pattern
The Builder Pattern is an alternative to C# extension methods
* if your productive class is (partly) immutable
* if all dependencies/sample data need to be setup before instantiating the productive class
* if you want to ensure all property values are configured before instantiating the productive class.
* Unfortunately, this pattern requires some extra characters for the final call to Build().
* You can combine the Builder pattern with Static Factory Methods and C# extension methods.
```c#
public class TestFoo
{
    public const double Property3Default = 1.23; // frequently used sample values as public constants

    private string _property1 = "A";
    private int _property2 = 1;
    private double _property3 = Property3Default;

    public TestFoo WithPropertyGroup1(string property1)
    {
        _property1 = property1;
        return this;
    }

    public TestFoo WithPropertyGroup2(int property2, double? optionalProperty3 = null)
    {
        _property2 = property2;
        _property3 = optionalProperty3 ?? Property3Default;
        return this;
    }

    // shortcuts for frequently used variants
    public static Foo AsVariantX() => new TestFoo().WithPropertyGroup1("X").WithPropertyGroup2(123, 12.34).Build();
    public static Foo AsVariantY() => new TestFoo().WithPropertyGroup1("Y").WithPropertyGroup2(234).Build();

    // the builder syntax is useful when productive class (partly) immutable
    public Foo Build() => new Foo(_property1, _property2, _property3);
}
```

### Combine multiple test helpers
As productive classes can depend on other productive classes e.g. by aggregation,
you can reuse other test helper classes in your actual test helper class:
```c#
public static class TestFoo
{
    public static Foo AddBar(this Foo foo, Bar? bar = null)
    {
        foo.Bars.Add(bar ?? TestBar.Create());
        return foo;
    }
}
```

```c#
var foo = TestFoo.Create()
    .WithPropertyGroup1("Y").WithPropertyGroup2(234)
    .AddBar(TestBar.Create().WithBarProperty("B"));
```
### Nested Fluent Builders
You can integrate dependent builders so that the method chaining isn't interrupted by calls to other builders:
```c#
public class TestFoo
{
    private readonly List<Bar> _bars = new();

    // continuous method chaining
    public TestBar AddBar() => new TestBar(this);

    // interrupted method chaining
    public TestFoo AddBar(Bar bar)
    {
        _bars.Add(bar);
        return this;
    }

    public Foo Build() => new Foo(_property1, _property2, _property3) { Bar = Bars };
}
```
```c#
var foo = new TestFoo()
    .WithPropertyGroup1("Y").WithPropertyGroup2(234)
    .AddBar().WithBarProperty("B").Add() // continuous
    .AddBar(new TestBar().WithBarProperty("B").Build()) // interrupted
    .Build();
```

## Use Fluent Syntax as much as necessary, as little as possible
* Good fluent APIs take a while to build.
* Too complex test helpers can lead to bugs in your test code.
* Try to improve the class design of your productive classes so that you don't even need test helpers:
  * In your productive classes, use appropriate constructors or factory methods.
  * Validate parameter values and ensure the object is always consistent.
  * Make properties immutable if possible.
* If your productive classes are only mutable bags with getters and setters, maybe named "DTOs":
  * See [AnemicDomainModel](https://martinfowler.com/bliki/AnemicDomainModel.html).
  * Put more behaviour into your domain objects.
  * Prefer object-oriented over procedural style.
* If refactoring your (legacy) productive classes takes too much time, you can create test helpers first
  and refactor the productive API step by step afterwards.
* Keep in mind, that one goal of Test First/TDD is to improve the design of your classes.
  With Fluent Syntax, maybe you're only improving the design of your Fluent API.
