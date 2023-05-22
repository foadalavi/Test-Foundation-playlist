using Docker.DotNet.Models;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseProject.Test.Infrastructure
{
    public sealed class MsSqlConfiguration : ContainerConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MsSqlConfiguration" /> class.
        /// </summary>
        /// <param name="database">The MsSql database.</param>
        /// <param name="username">The MsSql username.</param>
        /// <param name="password">The MsSql password.</param>
        public MsSqlConfiguration(
            string database = null,
            string username = null,
            string password = null)
        {
            Database = database;
            Username = username;
            Password = password;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsSqlConfiguration" /> class.
        /// </summary>
        /// <param name="database">The MsSql initial database.</param>
        /// <param name="username">The MsSql initial username.</param>
        /// <param name="password">The MsSql initial password.</param>
        public MsSqlConfiguration(
            string database,
            string username,
            string password,
            ushort port)
        {
            InitialDatabase = database;
            InitialUsername = username;
            InitialPassword = password;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsSqlConfiguration" /> class.
        /// </summary>
        /// <param name="resourceConfiguration">The Docker resource configuration.</param>
        public MsSqlConfiguration(IResourceConfiguration<CreateContainerParameters> resourceConfiguration)
            : base(resourceConfiguration)
        {
            // Passes the configuration upwards to the base implementations to create an updated immutable copy.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsSqlConfiguration" /> class.
        /// </summary>
        /// <param name="resourceConfiguration">The Docker resource configuration.</param>
        public MsSqlConfiguration(IContainerConfiguration resourceConfiguration)
            : base(resourceConfiguration)
        {
            // Passes the configuration upwards to the base implementations to create an updated immutable copy.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsSqlConfiguration" /> class.
        /// </summary>
        /// <param name="resourceConfiguration">The Docker resource configuration.</param>
        public MsSqlConfiguration(MsSqlConfiguration resourceConfiguration)
            : this(new MsSqlConfiguration(), resourceConfiguration)
        {
            // Passes the configuration upwards to the base implementations to create an updated immutable copy.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MsSqlConfiguration" /> class.
        /// </summary>
        /// <param name="oldValue">The old Docker resource configuration.</param>
        /// <param name="newValue">The new Docker resource configuration.</param>
        public MsSqlConfiguration(MsSqlConfiguration oldValue, MsSqlConfiguration newValue)
            : base(oldValue, newValue)
        {
            Database = BuildConfiguration.Combine(oldValue.Database, newValue.Database);
            Username = BuildConfiguration.Combine(oldValue.Username, newValue.Username);
            Password = BuildConfiguration.Combine(oldValue.Password, newValue.Password);
            InitialDatabase = BuildConfiguration.Combine(oldValue.InitialDatabase, newValue.InitialDatabase);
            InitialUsername = BuildConfiguration.Combine(oldValue.InitialUsername, newValue.InitialUsername);
            InitialPassword = BuildConfiguration.Combine(oldValue.InitialPassword, newValue.InitialPassword);
        }

        /// <summary>
        /// Gets the MsSql database.
        /// </summary>
        public string Database { get; }

        /// <summary>
        /// Gets the MsSql username.
        /// </summary>
        public string Username { get; }

        /// <summary>
        /// Gets the MsSql password.
        /// </summary>
        public string Password { get; }

        /// <summary>
        /// Gets the MsSql initial database.
        /// </summary>
        public string InitialDatabase { get; }

        /// <summary>
        /// Gets the MsSql initial username.
        /// </summary>
        public string InitialUsername { get; }

        /// <summary>
        /// Gets the MsSql initial password.
        /// </summary>
        public string InitialPassword { get; }
    }
}
