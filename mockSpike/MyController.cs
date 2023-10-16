using mockSpike.Domain;

namespace mockSpike;

public class MyController
{
    private readonly IMyService _myService;

    public MyController(IMyService myService)
    {
        _myService = myService;
    }

    public int Action(int a, int b)
    {
        return _myService.Sum(a, b);
    }
}