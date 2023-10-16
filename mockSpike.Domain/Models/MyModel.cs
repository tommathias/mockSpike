namespace mockSpike.Domain.Models;

public class MyModel
{
    public MyModel()
    {
    }
    public MyModel(MyModel model)
    {
        this.MyProperty = model.MyProperty;
    }
    public int MyProperty { get; set; }
}