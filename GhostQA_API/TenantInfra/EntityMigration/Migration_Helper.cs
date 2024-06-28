using GhostQA_API.DBContext;
using GhostQA_API.TenantInfra.EntityModal;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;


namespace GhostQA_API.TenantInfra.EntityMigration;
public class Migration_Helper
{
    private readonly IConfiguration _configuration;
    private readonly IServiceProvider _serviceProvider;

    public Migration_Helper(IConfiguration configuration, IServiceProvider serviceProvider)
    {
        _configuration = configuration;
        _serviceProvider = serviceProvider;
    }

    public async Task<string> RunMigrations(Dto_Tenant _tenant)
    {
        try
        {
            var connectionString = _configuration.GetConnectionString("AppDBContextConnection");
            string updatedConnectionString = UpdateConnectionString(connectionString, "Initial Catalog", _tenant.TenantName);
            string SqlFilePath = @"~\SqlScript\AllGhostQA_SP.sql";

            var optionsBuilder = new DbContextOptionsBuilder<AppDBContext>();
            optionsBuilder.UseSqlServer(updatedConnectionString);

            using (var context = new AppDBContext(optionsBuilder.Options))
            {
                try
                {
                    context.Database.EnsureCreated(); // Creates the database if it does not exist
                    return ($"Migrtion completed Successfully");
                    //if (!File.Exists(SqlFilePath))
                    //{
                    //    return ("SQL file not found.");
                    //}

                    //string sql;
                    //try
                    //{
                    //    sql = await File.ReadAllTextAsync(SqlFilePath);
                    //}
                    //catch (Exception ex)
                    //{
                    //    return ($"Error reading SQL file: {ex.Message}");
                    //}

                    //try
                    //{
                    //    await context.Database.ExecuteSqlRawAsync(sql);
                    //    return ("SQL executed successfully.");
                    //}
                    //catch (Exception ex)
                    //{
                    //    return ($"Error executing SQL: {ex.Message}");
                    //}
                }
                catch (Exception ex)
                {
                    return ex.Message.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            var logger = _serviceProvider.GetRequiredService<ILogger<Migration_Helper>>();
            logger.LogError(ex, "An error occurred while migrating or initializing the database.");
            return ex.Message.ToString();
        }
    }

    // Method to update a parameter value in a connection string
    static string UpdateConnectionString(string connectionString, string key, string value)
    {
        // Parse the existing connection string
        var builder = new SqlConnectionStringBuilder(connectionString);

        // Update the parameter value
        if (builder.ContainsKey(key))
        {
            builder[key] = value;
        }
        else
        {
            // Handle case where key doesn't exist (optional)
            throw new ArgumentException($"Connection string does not contain key '{key}'");
        }

        // Reconstruct and return the updated connection string
        return builder.ConnectionString;
    }
}