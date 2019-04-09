namespace D2CFL.Database.Extensions
{
    internal static class MsSqlExtensions
    {
        internal static class Schema
        {
            public const string SchemaName = "SchemaName";
        }

        internal static class Functions
        {
            public const string NewSequentialId = "newsequentialid()";
            public const string GetUtcDate = "getutcdate()";
        }

        internal static class ColumnTypes
        {
            public const string NVarChar = "nvarchar";

            public const string Money = "decimal(18,4)";
            public const string DateTime = "datetime2";
            public const string Date = "date";

            public static string GetNVarCharWithSpecifiedLength(int length = 255)
            {
                return $"{(object)"nvarchar"}({(object)length})";
            }
        }

        internal static class ColumnLengths
        {
            public const int DefaultNVarChar = 255;
            public const int UniqueName = 50;
        }
    }
}
