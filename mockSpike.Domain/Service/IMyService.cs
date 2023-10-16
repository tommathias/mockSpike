using mockSpike.Domain.Models;

namespace mockSpike.Domain.Service;

public interface IMyService
{
    /// <summary>
    /// Sums given numbers
    /// </summary>
    /// <param name="a">an integer</param>
    /// <param name="b">an integer</param>
    /// <returns>Sum of a+b</returns>
    int Sum(int a, int b);

    /// <summary>
    /// Translates given MyModel into a new YourModel
    /// </summary>
    /// <param name="model">MyModel to be translated into YourModel</param>
    /// <returns>YourModel populated by MyModel</returns>
    YourModel Translate(MyModel model);
    
    /// <summary>
    /// Sets given MyModel.MyProperty to 27
    /// </summary>
    /// <param name="model">MyModel to have MyProperty changed</param>
    /// <returns>Given MyModel</returns>
    MyModel Mutate(MyModel model);
    
    /// <summary>
    /// Creates a new MyModel and populates with values from model
    /// </summary>
    /// <param name="model">MyModel to be duplicated</param>
    /// <returns>New MyModel with values from model(shallow clone by val)</returns>
    MyModel Duplicate(MyModel model);
    
    /// <summary>
    /// Does nothing. We all need a rest sometimes.
    /// </summary>
    /// <param name="model">MyModel to be left alone.</param>
    /// <returns>model</returns>
    MyModel Vegetate(MyModel model);
}