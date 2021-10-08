using System.ComponentModel.DataAnnotations;

namespace Partner.Comms.Common
{
    public class Enums
    {

        public enum KeyVaultEmailConfigurations
        {

            EmailAPIKey,
        }

        public enum KeyVaultSMSConfigurations
        {
            SMSAPIKey,
        }

        public enum KeyVaultPaylinkConfigurations
        {
            ShortURLAPIKey,

        }

        public enum Client
        {
            APIMClient
        }

        public enum Status
        {
            PROCESSING,
            COMPLETED,
            CREATED,
            DELETED,
            UPDATED,
            RECEIVED,
            SENT
        }
        public enum ContentType
        {
            [Display(Name = "json", Description = "application/json")]
            JSON
        }
        public enum KeyVaultKeyNames
        {
            ShortURLAPIMKey
        }

    }
}
