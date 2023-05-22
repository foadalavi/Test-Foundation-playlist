using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseProject.Test.Infrastructure
{
    public sealed class MsSqlContainer : DockerContainer
    {
        private readonly MsSqlConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="MsSqlContainer" /> class.
        /// </summary>
        /// <param name="configuration">The container configuration.</param>
        /// <param name="logger">The logger.</param>
        public MsSqlContainer(MsSqlConfiguration configuration, ILogger logger)
            : base(configuration, logger)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Gets the MsSql connection string.
        /// </summary>
        /// <returns>The MsSql connection string.</returns>
        public string GetConnectionString()
        {
            var properties = new Dictionary<string, string>();
            properties.Add("Server", Hostname + "," + GetMappedPublicPort(MsSqlBuilder.MsSqlPort));
            properties.Add("Database", _configuration.Database);
            properties.Add("User Id", _configuration.Username);
            properties.Add("Password", _configuration.Password);
            properties.Add("TrustServerCertificate", bool.TrueString);
            return string.Join(";", properties.Select(property => string.Join("=", property.Key, property.Value)));
        }

        /// <summary>
        /// Executes the SQL script in the MsSql container.
        /// </summary>
        /// <param name="scriptContent">The content of the SQL script to execute.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Task that completes when the SQL script has been executed.</returns>
        public async Task<ExecResult> ExecScriptAsync(string scriptContent, CancellationToken ct = default)
        {
            var scriptFilePath = string.Join("/", string.Empty, "tmp", Guid.NewGuid().ToString("D"), Path.GetRandomFileName());

            await CopyFileAsync(scriptFilePath, Encoding.Default.GetBytes(scriptContent), 493, 0, 0, ct)
                .ConfigureAwait(false);

            return await ExecAsync(new[] { "/opt/mssql-tools/bin/sqlcmd", "-b", "-r", "1", "-U", _configuration.InitialUsername, "-P", _configuration.InitialPassword, "-i", scriptFilePath }, ct)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Start the containre and executes the initial SQL script in the MsSql container.
        /// </summary>
        /// <param name="initialScript">The content of the initial SQL script to execute.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns></returns>
        public async Task StartAsync(string initialScript = null, CancellationToken ct = default)
        {
            await StartAsync(ct);
            if (!string.IsNullOrEmpty(initialScript))
            {
                await ExecScriptAsync(initialScript, ct);
            }
        }

        /// <summary>
        /// Start the containre and create DataBase and login.
        /// </summary>
        /// <param name="ct">Cancellation token.</param>
        /// <returns></returns>
        public override async Task StartAsync(CancellationToken ct = default)
        {
            await base.StartAsync(ct);
            var script = $"""
            Create database [{_configuration.Database}];
            CREATE LOGIN [{_configuration.Username}] WITH PASSWORD = '{_configuration.Password}';
            ALTER SERVER ROLE [sysadmin] ADD MEMBER [{_configuration.Username}];
            go
            USE [{_configuration.Database}];
            CREATE USER [{_configuration.Username}] FOR LOGIN [{_configuration.Username}];
            ALTER USER [{_configuration.Username}] WITH DEFAULT_SCHEMA=[dbo];
            ALTER ROLE [db_owner] ADD MEMBER [{_configuration.Username}];
            """;
            await ExecScriptAsync(script, ct);
        }
    }
}
