using FakeItEasy;
using mockSpike.Domain.Models;
using mockSpike.Domain.Service;

namespace mockSpike.UnitTest;

public class MyControllerFieTests
{
    [SetUp]
    public void Setup()
    {
    }

    #region SumAction
    /// <summary>
    /// This is a bad unit test, it's reimplementing and testing the domain logic, not the controller specific logic.
    /// In this format to exercise FluentAssertions and parametrised tests
    /// </summary>
    [TestCase(1,2,3)]
    [TestCase(100,200,0)]
    public void SumAction_ForPositiveIntegers_ReturnsSum(int a, int b, int expected)
    {
        //arrange
        var fakeMyService = A.Fake<IMyService>();
        
        //N.B. A.CallTo() with A<int>.Ignored is greedy and will return early at the first match, not the most specific match.
        //A.CallTo()s are added in a stack (LIFO), order the arrange block accordingly.
        A.CallTo(() => fakeMyService.Sum(A<int>.Ignored, A<int>.Ignored)).Returns(0);
        A.CallTo(() => fakeMyService.Sum(1, 2)).Returns(3);
        var myControllerUnderTest = new MyController(fakeMyService);
        
        //act
        var actual = myControllerUnderTest.SumAction(a, b);
        
        //assert
        actual.Should().Be(expected);
    }
    
    
    [TestCase(1,2)]
    [TestCase(100,200)]
    public void SumAction_ForPositiveIntegers_ArgsArePassedToService(int a, int b)
    {
        //arrange
        var fakeMyService = A.Fake<IMyService>();
        
        //N.B. A.CallTo() with A<int>.Ignored is greedy and will return early at the first match, not the most specific match.
        //A.CallTo()s are added in a stack (LIFO), order the arrange block accordingly.
        A.CallTo(() => fakeMyService.Sum(A<int>.Ignored, A<int>.Ignored)).Returns(0);
        A.CallTo(() => fakeMyService.Sum(1, 2)).Returns(3);
        var myControllerUnderTest = new MyController(fakeMyService);
        
        //act
        var actual = myControllerUnderTest.SumAction(a, b);
        
        //assert
        A.CallTo(() => fakeMyService.Sum(a, b)).MustHaveHappened();
    }
    
    [Test]
    public void SumAction_CallingUnconfiguredFake_DoesNotThrow()
    {
        //arrange
        var fakeMyService = A.Fake<IMyService>();
        var myControllerUnderTest = new MyController(fakeMyService);
        
        //act
        var actual = myControllerUnderTest.SumAction(1,2);
        
        //assert
        Assert.Pass();
        //thank you for playing wing commander
    }
    #endregion
    
    #region VegetateAction
    [Test]
    public void VegetateAction_CalledForModel_AllowsMockReferenceAssertion()
    {
        //arrange
        var fakeMyService = A.Fake<IMyService>();
        var myControllerUnderTest = new MyController(fakeMyService);
        var myModel = new MyModel();
        var aDifferentModel = new MyModel();
        
        //act
        var actual = myControllerUnderTest.VegetateAction(myModel);
        
        //assert
        A.CallTo(() => fakeMyService.Vegetate(myModel)).MustHaveHappened();
        A.CallTo(() => fakeMyService.Vegetate(aDifferentModel)).MustNotHaveHappened();
    }

    /// <summary>
    /// This is almost a different type of bad test, it could test whether the Fake Framework has implemented the domain correctly.
    /// We're using it to exercise NSubstitute, so allow weak/anti-pattern.
    /// </summary>
    [Test]
    public void VegetateAction_CalledForModel_AllowsFluentReferenceAssertion()
    {
        //arrange
        var fakeMyService = A.Fake<IMyService>();
        var myModel = new MyModel();
        
        //todo: this is the object by reference, not the argument, return argument passed in
        A.CallTo(() => fakeMyService.Vegetate(A<MyModel>.Ignored))
            .Returns(myModel);
        var myControllerUnderTest = new MyController(fakeMyService);
        
        //act
        var actual = myControllerUnderTest.VegetateAction(myModel);
        
        //assert
        actual.Should().BeSameAs(myModel);
    }
    
    /// <summary>
    /// This is almost a different type of bad test, it could test whether the Fake Framework has implemented the domain correctly.
    /// We're using it to exercise NSubstitute, so allow weak/anti-pattern.
    /// </summary>
    [Test]
    public void VegetateAction_CalledForModel_AllowsFluentEquivalentAssertion()
    {
        //arrange
        var fakeMyService = A.Fake<IMyService>();
        var myModel = new MyModel();
        
        //todo: this is the object by reference, not the argument, return argument passed in
        A.CallTo(() => fakeMyService.Vegetate(A<MyModel>.Ignored))
            .Returns(myModel);
        var myControllerUnderTest = new MyController(fakeMyService);
        
        //act
        var actual = myControllerUnderTest.VegetateAction(myModel);
        
        //assert
        actual.Should().Be(myModel);
        actual.Should().Match(ac => ac == myModel);
    }
    #endregion

    [Test]
    public void DuplicateAction_CalledForModel_AllowsFluentEquivalentAssertion()
    {
        //arrange
        var fakeMyService = A.Fake<IMyService>();
        var myModel = new MyModel
        {
            MyProperty = 400
        };
        var otherModel = new MyModel
        {
            MyProperty = 400
        };

        A.CallTo(() => fakeMyService.Duplicate(A<MyModel>.Ignored))
            .Returns(otherModel);
        var myControllerUnderTest = new MyController(fakeMyService);
        
        //act
        var actual = myControllerUnderTest.DuplicateAction(myModel);
        
        //assert
        actual.Should().NotBeSameAs(myModel);
        actual.Should().BeSameAs(otherModel);
        
        actual.Should().BeEquivalentTo(otherModel);
    }
}