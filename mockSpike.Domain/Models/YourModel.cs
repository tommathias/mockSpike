namespace mockSpike.Domain.Models;

public class YourModel
{
    public YourModel(MyModel model)
    {
        this.YourProperty = model.MyProperty;
    }
    
    public int YourProperty { get; set; }
}