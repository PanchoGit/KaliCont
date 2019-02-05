namespace Kali.Security
{
    public class TokenSetting
    {
        public string SecretKey { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int ExpirationDays { get; set; }

        public string DefaultUserName { get; set; }

        public string DefaultPassword { get; set; }
    }
}
