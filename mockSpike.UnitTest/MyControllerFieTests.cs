using FakeItEasy;
using mockSpike.Domain;

namespace mockSpike.UnitTest;

public class MyControllerFieTests
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
        var fakeMyService = A.Fake<IMyService>();
        
        //N.B. A.CallTo() with A<int>.Ignored is greedy and will return early at the first match, not the most specific match.
        //A.CallTo()s are added in a stack (LIFO), order the arrange block accordingly.
        A.CallTo(() => fakeMyService.Sum(A<int>.Ignored, A<int>.Ignored)).Returns(0);
        A.CallTo(() => fakeMyService.Sum(1, 2)).Returns(3);
        var myControllerUnderTest = new MyController(fakeMyService);
        
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
        var fakeMyService = A.Fake<IMyService>();
        
        //N.B. A.CallTo() with A<int>.Ignored is greedy and will return early at the first match, not the most specific match.
        //A.CallTo()s are added in a stack (LIFO), order the arrange block accordingly.
        A.CallTo(() => fakeMyService.Sum(A<int>.Ignored, A<int>.Ignored)).Returns(0);
        A.CallTo(() => fakeMyService.Sum(1, 2)).Returns(3);
        var myControllerUnderTest = new MyController(fakeMyService);
        
        //act
        var actual = myControllerUnderTest.Action(a, b);
        
        //assert
        A.CallTo(() => fakeMyService.Sum(a, b)).MustHaveHappened();
    }
}