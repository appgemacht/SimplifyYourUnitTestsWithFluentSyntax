# Simplify your Unit Tests with Fluent Syntax

This repo contains sample projects used in my new blog post about using the Builder pattern in automated software tests.

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

## Advantages of Fluent Syntax
* Usage of the productive code can be different in tests.
* Readability of test code is as important as of productive code.
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

## Disadvantages
* Good fluent APIs take a while to build.
* Coming next...

## Pay attention!
* Do not hide any values, that are important to the test (e.g. ``level: GetLevel()``)
* Coming next...

## How to
* Coming next...
