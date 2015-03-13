namespace Validations
{
    /// <summary>
    /// The Validations namespace.
    /// </summary>
    public interface IOperation
    {
        /// <summary>
        /// Gets the label which identifies the calling operation.
        /// </summary>
        /// <value>The target.</value>
        string Target { get; }
    }
}
