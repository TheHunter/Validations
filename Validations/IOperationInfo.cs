namespace Validations
{
    /// <summary>
    /// The Validations namespace.
    /// </summary>
    public interface IOperationInfo
        : IOperation
    {
        /// <summary>
        /// Gets a message which is used by operation result when the calling operation is failed.
        /// </summary>
        string MessageOnError { get; }
    }
}
