namespace GradeApi.Persistence.Extensions
{
    using System.Collections.Generic;
    using System;
    using System.Data.Common;

    public static class DbCommandExtensions
    {
        /// <summary>
        /// Extension method to load from a commad <see cref="DbCommand"/>.
        /// </summary>
        /// <typeparam name="TResult">Type of the item.</typeparam>
        /// <param name="dbCommand">Db Command.</param>
        /// <param name="creator">Method to create the result.</param>
        /// <returns>A collection of items.</returns>
        public static IEnumerable<TResult> LoadMany<TResult>(this DbCommand dbCommand, Func<DbDataReader, TResult> creator)
        {
            using DbDataReader reader = dbCommand.ExecuteReader();

            while (reader.Read())
            {
                yield return creator(reader);
            }
        }
    }
}
