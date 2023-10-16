using mockSpike.Domain.Models;

namespace mockSpike.Domain.Service;

public class MyService : IMyService
{
    public int Sum(int a, int b)
    {
        return a + b;
    }

    public YourModel Translate(MyModel model)
    {
        return new YourModel(model);
    }

    public MyModel Mutate(MyModel model)
    {
        model.MyProperty = 27; // lucky number

        return model;
    }

    public MyModel Duplicate(MyModel model)
    {
        var result = new MyModel(model);
        return result;
    }

    public MyModel Vegetate(MyModel model)
    {
        return model;
    }
}