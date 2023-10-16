using mockSpike.Domain.Models;
using mockSpike.Domain.Service;

namespace mockSpike;

public class MyController
{
    private readonly IMyService _myService;

    public MyController(IMyService myService)
    {
        _myService = myService;
    }

    public int SumAction(int a, int b)
    {
        return _myService.Sum(a, b);
    }

    public YourModel TranslateAction(MyModel model)
    {
        return _myService.Translate(model);
    }

    public MyModel MutateAction(MyModel model)
    {
        return _myService.Mutate(model);
    }

    public MyModel DuplicateAction(MyModel model)
    {
        return _myService.Duplicate(model);
    }

    public MyModel VegetateAction(MyModel model)
    {
        return _myService.Vegetate(model);
    }
}