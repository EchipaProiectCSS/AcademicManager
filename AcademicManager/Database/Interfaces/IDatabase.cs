namespace Database.Interfaces
{
    using Internal;

    public interface IDatabase
    {
        string ConnectionString { get; set; }

        string Name { get; set; }

        /// <summary>
        /// Load and run the script file located at the specified location. 
        /// Will throw if the script is not found or it has errors.
        /// </summary>
        /// <param name="scriptFilePath">The file path for the script to be executed.</param>
        void RunScriptFile(string scriptFilePath);

        /// <summary>
        /// Executes the provided script and does not return any value. 
        /// Recommended for scripts that don't have return values.
        /// Will throw on script errors.
        /// </summary>
        /// <param name="script">The script's body.</param>
        void Execute(string script);

        /// <summary>
        /// Executes the provided script and returns the query result, if any. 
        /// Will not return null. 
        /// Will throw on script errors.
        /// </summary>
        /// <param name="script">The script's body.</param>
        IQueryResult Query(string script);
    }
}