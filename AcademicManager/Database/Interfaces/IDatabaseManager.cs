namespace Database.Interfaces
{
    public interface IDatabaseManager<out T> where T:IDatabase
    {
        /// <summary>
        ///     Open an existing database.
        /// </summary>
        /// <param name="connectionString">
        ///     The "connection string" to an existing database. Basically the file path to the database
        ///     folder.
        /// </param>
        /// <returns>An instance of IDatabase</returns>
        T Open(string connectionString);

        /// <summary>
        ///     Creates a new database at the provided location, with the provided name
        /// </summary>
        /// <param name="connectionString">The file path where the database folder will be created.</param>
        /// <param name="name">The name of the database and the folder that will be created for it.</param>
        /// <returns>An instance of IDatabase</returns>
        T Create(string connectionString, string name);
    }
}