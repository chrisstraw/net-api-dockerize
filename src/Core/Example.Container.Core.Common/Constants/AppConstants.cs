namespace Example.Container.Core
{
    public static class AppConstants
    {


        public static class ConnectionStrings
        {
            public const string DbConnectionName = "DbConnection";
        }


        public static class DefaultData
        {
            public static readonly DateTimeOffset DefaultEffectiveDate = new DateTimeOffset(1980, 1, 1, 0, 0, 0, 0, DateTimeOffset.UtcNow.Offset);
        }

    }
}
