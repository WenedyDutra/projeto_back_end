using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectEllevo.API.Models
{
    public class mongoDbdatabaseSettings : ImongoDbdatabaseSettings
    {
        public string UserCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string TaskCollectionName { get; set; }
    }
        public interface ImongoDbdatabaseSettings 

    {
        public string UserCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string TaskCollectionName { get; set; }
        }
}

