namespace Tester.src.Common.Result
{
    /// <summary>
    /// An interface that represents the results produced by each task
    /// </summary>
    interface IDataSet
    {
        string GetName();

        string Serialize();
    }
}
