namespace Server
{
    //these data is secrets and should be stored in the app settings or securly in the server
    public static class Constants
    {
        public const string Issuer = Audiance;
        public const string Audiance = "https://localhost:44382/";
            public const string Secret = "not_too_short_secret_otherwise_it_might_error";
    }
}
