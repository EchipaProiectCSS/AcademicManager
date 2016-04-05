namespace Database.Implementations.Internal.Utility
{
    public static class Instructions
    {
        public static readonly string CreateTable = "create table";
        public static readonly string CreateDatabase = "create database";
        public static readonly string Go = "go";
        public static readonly string Use = "use";
        public static readonly string CreateSchema = "create schema";
        public static readonly string PrimaryKey = "pk";
        public static readonly string InsertInto = "insert into";
        public static readonly string Values = "values";
        public static readonly string Select = "select";
        public static readonly string From = "from";
        public static readonly string Where = "where";
        public static readonly string Update = "update";
        public static readonly string Set = "set";
        public static readonly string Delete = "delete";
        public static readonly string DropTable = "drop table";
        public static readonly string DropSchema = "drop schema";
        public static readonly string DropDatabase = "drop database";
        public static readonly char StatementTerminator = ';';
    }
}