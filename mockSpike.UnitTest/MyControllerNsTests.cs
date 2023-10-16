using NSubstitute;
using mockSpike.Domain.Models;
using mockSpike.Domain.Service;

namespace mockSpike.UnitTest;

public class MyControllerNsTests
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
    [TestCase(1, 2, 3)]
    [TestCase(100, 200, 0)]
    public void SumAction_ForPositiveIntegers_ReturnsSum(int a, int b, int expected)
    {
        //arrange
        var substituteMyService = Substitute.For<IMyService>();

        //N.B. .Returns() is greedy and will return early at the first match, not the most specific match.
        //.Returns() are added in a stack (LIFO), order the arrange block accordingly.
        substituteMyService.Sum(default, default).Returns(0);
        substituteMyService.Sum(1, 2).Returns(3);
        var myControllerUnderTest = new MyController(substituteMyService);

        //act
        var actual = myControllerUnderTest.SumAction(a, b);

        //assert
        actual.Should().Be(expected);
    }

    [TestCase(1, 2)]
    [TestCase(100, 200)]
    public void SumAction_ForPositiveIntegers_ArgsArePassedToService(int a, int b)
    {
        //arrange
        var substituteMyService = Substitute.For<IMyService>();

        //N.B. .Returns() is greedy and will return early at the first match, not the most specific match.
        //.Returns() are added in a stack (LIFO), order the arrange block accordingly.
        substituteMyService.Sum(default, default).Returns(0);
        substituteMyService.Sum(1, 2).Returns(3);
        var myControllerUnderTest = new MyController(substituteMyService);

        //act
        var actual = myControllerUnderTest.SumAction(a, b);

        //assert
        substituteMyService.Received().Sum(a, b);
    }

    [Test]
    public void SumAction_CallingUnconfiguredFake_DoesNotThrow()
    {
        //arrange
        var substituteMyService = Substitute.For<IMyService>();
        var myControllerUnderTest = new MyController(substituteMyService);

        //act
        var actual = myControllerUnderTest.SumAction(1, 2);

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
        var substituteMyService = Substitute.For<IMyService>();
        var myControllerUnderTest = new MyController(substituteMyService);
        var myModel = new MyModel();
        
        //act
        var actual = myControllerUnderTest.VegetateAction(myModel);
        
        //assert
        substituteMyService.Received().Vegetate(myModel);
    }

    /// <summary>
    /// This is almost a different type of bad test, it could test whether the Fake Framework has implemented the domain correctly.
    /// We're using it to exercise NSubstitute, so allow weak/anti-pattern.
    /// </summary>
    [Test]
    public void VegetateAction_CalledForModel_AllowsFluentReferenceAssertion()
    {
        //arrange
        var substituteMyService = Substitute.For<IMyService>();
        var myModel = new MyModel();
        
        //see: https://nsubstitute.github.io/help/return-from-function/
        substituteMyService.Vegetate(Arg.Any<MyModel>())
            .ReturnsForAnyArgs(m => m.Arg<MyModel>() );
        var myControllerUnderTest = new MyController(substituteMyService);
        
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
        var substituteMyService = Substitute.For<IMyService>();
        var myModel = new MyModel();
        
        //see: https://nsubstitute.github.io/help/return-from-function/
        substituteMyService.Vegetate(Arg.Any<MyModel>())
            .ReturnsForAnyArgs(m => m.Arg<MyModel>() );
        var myControllerUnderTest = new MyController(substituteMyService);
        
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
        var substituteMyService = Substitute.For<IMyService>();
        var myModel = new MyModel
        {
            MyProperty = 400
        };
        var otherModel = new MyModel
        {
            MyProperty = 400
        };

        //see: https://nsubstitute.github.io/help/return-from-function/
        substituteMyService.Duplicate(Arg.Any<MyModel>())
            .Returns(otherModel);
        var myControllerUnderTest = new MyController(substituteMyService);
        
        //act
        var actual = myControllerUnderTest.DuplicateAction(myModel);
        
        //assert
        actual.Should().NotBeSameAs(myModel);
        actual.Should().BeSameAs(otherModel);
        
        actual.Should().BeEquivalentTo(otherModel);
    }
}