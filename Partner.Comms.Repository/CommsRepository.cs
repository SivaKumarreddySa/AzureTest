using Partner.Comms.Common;
using Partner.Comms.DTO;
using Partner.Comms.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Partner.Comms.Repository
{
    public interface ICommsRepository
    {
    }

    public class CommsRepository : ICommsRepository
    {
        private string _serviceProvider { get; set; }

        public CommsRepository()
        {
            _serviceProvider = Environment.GetEnvironmentVariable("ServiceProvider");
        }
    }
}