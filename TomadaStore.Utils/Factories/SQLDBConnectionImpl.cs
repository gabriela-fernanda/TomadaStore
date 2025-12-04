using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomadaStore.Utils.Factories.Interfaces;

namespace TomadaStore.Utils.Factories
{
    internal class SQLDBConnectionImpl : IDBConnection
    {
        private readonly string _connectionString;
        private readonly IConfiguration configuration;

        public SQLDBConnectionImpl()
        {
            _connectionString = configuration.GetConnectionString("SqlServer");
        }

        public string ConnectionString()
        {
            return _connectionString;
        }
    }
}
