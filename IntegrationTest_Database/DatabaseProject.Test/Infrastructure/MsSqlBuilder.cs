#region Assembly Testcontainers.MsSql, Version=3.1.0.0, Culture=neutral, PublicKeyToken=e4b565b6322a8e33
// C:\Users\foada\.nuget\packages\testcontainers.mssql\3.1.0\lib\netstandard2.1\Testcontainers.MsSql.dll
// Decompiled with ICSharpCode.Decompiler 7.1.0.6543
#endregion

using System.Threading.Tasks;
using Docker.DotNet.Models;
using DotNet.Testcontainers;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;

namespace DatabaseProject.Test.Infrastructure
{
    public sealed class MsSqlBuilder : ContainerBuilder<MsSqlBuilder, MsSqlContainer, MsSqlConfiguration>
    {
        //
        // Remarks:
        //     Uses the sqlcmd utility scripting variables to detect readiness of the MsSql
        //     container: https://learn.microsoft.com/en-us/sql/tools/sqlcmd/sqlcmd-utility?view=sql-server-linux-ver15#sqlcmd-scripting-variables.
        private sealed class WaitUntil : IWaitUntil
        {
            private readonly string[] _command = new string[3] { "/opt/mssql-tools/bin/sqlcmd", "-Q", "SELECT 1;" };

            public async Task<bool> UntilAsync(IContainer container)
            {
                return 0L.Equals((await container.ExecAsync(_command).ConfigureAwait(continueOnCapturedContext: false)).ExitCode);
            }
        }

        public const string MsSqlImage = "mcr.microsoft.com/mssql/server:2019-CU18-ubuntu-20.04";

        public const ushort MsSqlPort = 1433;

        public const string DefaultDatabase = "master";

        public const string DefaultUsername = "sa";

        public const string DefaultPassword = "yourStrong(!)Password";

        protected override MsSqlConfiguration DockerResourceConfiguration { get; }

        //
        // Summary:
        //     Initializes a new instance of the Testcontainers.MsSql.MsSqlBuilder class.
        public MsSqlBuilder()
            : this(new MsSqlConfiguration(DefaultDatabase, DefaultUsername, DefaultPassword, MsSqlPort))
        {
            DockerResourceConfiguration = Init().DockerResourceConfiguration;
        }

        //
        // Summary:
        //     Initializes a new instance of the Testcontainers.MsSql.MsSqlBuilder class.
        //
        // Parameters:
        //   resourceConfiguration:
        //     The Docker resource configuration.
        private MsSqlBuilder(MsSqlConfiguration resourceConfiguration)
            : base(resourceConfiguration)
        {
            DockerResourceConfiguration = resourceConfiguration;
        }

        //
        // Summary:
        //     Sets the MsSql password.
        //
        // Parameters:
        //   password:
        //     The MsSql password.
        //
        // Returns:
        //     A configured instance of Testcontainers.MsSql.MsSqlBuilder.
        public MsSqlBuilder WithPassword(string password)
        {
            return Merge(DockerResourceConfiguration, new MsSqlConfiguration() { Password = password }).WithEnvironment("MSSQL_SA_PASSWORD", DefaultPassword).WithEnvironment("SQLCMDPASSWORD", DefaultPassword);
        }

        /// <inheritdoc />
        public override MsSqlContainer Build()
        {
            Validate();
            return new MsSqlContainer(DockerResourceConfiguration, TestcontainersSettings.Logger);
        }

        /// <inheritdoc />
        protected override MsSqlBuilder Init()
        {
            return base.Init().WithImage(MsSqlImage).WithPortBinding(1433, assignRandomHostPort: true)
                .WithEnvironment("ACCEPT_EULA", "Y")
                .WithDatabase(DefaultDatabase)
                .WithUsername(DefaultUsername)
                .WithPassword(DefaultPassword)
                .WithWaitStrategy(Wait.ForUnixContainer().AddCustomWaitStrategy(new WaitUntil()));
        }

        /// <inheritdoc />
        protected override void Validate()
        {
            base.Validate();
            Guard.ArgumentInfo<string> argument = Guard.Argument(DockerResourceConfiguration.Password, "Password");
            _ = ref argument.NotNull().NotEmpty();
        }

        /// <inheritdoc />
        protected override MsSqlBuilder Clone(IResourceConfiguration<CreateContainerParameters> resourceConfiguration)
        {
            return Merge(DockerResourceConfiguration, new MsSqlConfiguration(resourceConfiguration));
        }

        /// <inheritdoc />
        protected override MsSqlBuilder Clone(IContainerConfiguration resourceConfiguration)
        {
            return Merge(DockerResourceConfiguration, new MsSqlConfiguration(resourceConfiguration));
        }

        /// <inheritdoc />
        protected override MsSqlBuilder Merge(MsSqlConfiguration oldValue, MsSqlConfiguration newValue)
        {
            return new MsSqlBuilder(new MsSqlConfiguration(oldValue, newValue));
        }

        //
        // Summary:
        //     Sets the MsSql database.
        //
        // Parameters:
        //   database:
        //     The MsSql database.
        //
        // Returns:
        //     A configured instance of Testcontainers.MsSql.MsSqlBuilder.
        //
        // Remarks:
        //     The Docker image does not allow to configure the database.
        public MsSqlBuilder WithDatabase(string database)
        {
            return Merge(DockerResourceConfiguration, new MsSqlConfiguration() { Database = database }).WithEnvironment("SQLCMDDBNAME", DefaultDatabase);
        }

        //
        // Summary:
        //     Sets the MsSql username.
        //
        // Parameters:
        //   username:
        //     The MsSql username.
        //
        // Returns:
        //     A configured instance of Testcontainers.MsSql.MsSqlBuilder.
        //
        // Remarks:
        //     The Docker image does not allow to configure the username.
        public MsSqlBuilder WithUsername(string username)
        {
            return Merge(DockerResourceConfiguration, new MsSqlConfiguration() { Username = username }).WithEnvironment("SQLCMDUSER", DefaultUsername);
        }

        public MsSqlBuilder WithRestore(string backUpFilePath)
        {
            return Merge(DockerResourceConfiguration, new MsSqlConfiguration() { BackUpFilePath = backUpFilePath });
        }
    }
}
