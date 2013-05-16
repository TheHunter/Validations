namespace Validations
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOperation
    {
        /// <summary>
        /// A label which identifies the calling operation.
        /// </summary>
        string Target { get; }
    }
}
