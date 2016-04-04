﻿namespace Database.Implementations.Internal.Domain
{
    using System.Collections.Generic;

    public class Row
    {
        /// <summary>
        /// Key = column name
        /// Value = value for the column
        /// </summary>
        public Dictionary<string, string> Values { get; set; }
    }
}