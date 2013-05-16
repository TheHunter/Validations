namespace Validations
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOperationInfo
        : IOperation
    {
        /// <summary>
        /// A message which is used by operation result when the calling operation is failed.
        /// </summary>
        string MessageOnError { get; }
    }
}
