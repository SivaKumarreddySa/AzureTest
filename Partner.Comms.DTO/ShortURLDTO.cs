using System;
using System.Collections.Generic;
using System.Text;
using Partner.Comms.Domain.Docket;

namespace Partner.Comms.DTO
{
    [Serializable]
   public class ShortURLRequestDTO
    {
        public string originalUrl { get; set; }
    }

    [Serializable]
    public class ShortURLResponseDTO
    {
        public string _Id { get; set; }

        public string OriginalUrl { get; set; }

        public string urlcode { get; set; }

        public string ShortUrl { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public string _V { get; set; }
    }
}
