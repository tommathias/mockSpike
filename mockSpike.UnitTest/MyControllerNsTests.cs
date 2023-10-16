using mockSpike.Domain;
using NSubstitute;
using NSubstitute.ReceivedExtensions;

namespace mockSpike.UnitTest;

public class MyControllerNsTests
{
    [SetUp]
    public void Setup()
    {
    }

    /// <summary>
    /// This is a bad unit test, it's reimplementing and testing the domain logic, not the controller specific logic.
    /// In this format to exercise FluentAssertions and parametrised tests
    /// </summary>
    [TestCase(1,2,3)]
    [TestCase(100,200,0)]
    public void Action_ForPositiveIntegers_ReturnsSum(int a, int b, int expected)
    {
        //arrange
        var substituteMyService = Substitute.For<IMyService>();
        
        //N.B. .Returns() is greedy and will return early at the first match, not the most specific match.
        //.Returns() are added in a stack (LIFO), order the arrange block accordingly.
        substituteMyService.Sum(default, default).Returns(0);
        substituteMyService.Sum(1, 2).Returns(3);
        var myControllerUnderTest = new MyController(substituteMyService);
        
        //act
        var actual = myControllerUnderTest.Action(a, b);
        
        //assert
        actual.Should().Match(ac => ac == expected);
    }
    
    [TestCase(1,2)]
    [TestCase(100,200)]
    public void Action_ForPositiveIntegers_ArgsArePassedToService(int a, int b)
    {
        //arrange
        var substituteMyService = Substitute.For<IMyService>();
        
        //N.B. .Returns() is greedy and will return early at the first match, not the most specific match.
        //.Returns() are added in a stack (LIFO), order the arrange block accordingly.
        substituteMyService.Sum(default, default).Returns(0);
        substituteMyService.Sum(1, 2).Returns(3);
        var myControllerUnderTest = new MyController(substituteMyService);
        
        //act
        var actual = myControllerUnderTest.Action(a, b);
        
        //assert
        substituteMyService.Received().Sum(a, b);
    }
}