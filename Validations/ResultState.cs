namespace Validations
{
    /// <summary>
    /// A set states of customizing results, used for Validators.
    /// </summary>
    public enum ResultState
    {
        /// <summary>
        /// Indicates Result has exceeded successfully.
        /// </summary>
        Successfully = 0,

        /// <summary>
        /// Indicates Result has errors.
        /// </summary>
        Unsuccessfully = 1,

        /// <summary>
        /// Indicates there's no Result to qualify.
        /// </summary>
        NoDeterminated = 2,

        /// <summary>
        /// Indicates a runtime error when Validator tried to get result.
        /// </summary>
        RuntimeError = 3
    }
}
