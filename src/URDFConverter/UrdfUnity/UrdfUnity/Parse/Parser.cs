namespace UrdfUnity.Parse
{
    /// <summary>
    /// Defines the generic interface implemented by utility parsing classes.
    /// </summary>
    /// <typeparam name="T">The object type being parsed and returned</typeparam>
    /// <typeparam name="E">The type of input element being parsed from</typeparam>
    public interface Parser<T, E>
    {
        /// <summary>
        /// Parses the provided input and returns the parsed object.
        /// </summary>
        /// <param name="input">The input that will be parsed</param>
        /// <returns>The object that was parsed from the input element</returns>
        T Parse(E input);
    }
}
