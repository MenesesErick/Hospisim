// Em: Hospisim/Data/HospisimContextFactory.cs

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

// O namespace DEVE ser exatamente este
namespace Hospisim.Data
{
    // A assinatura da classe e a interface implementada DEVEM ser exatamente estas
    public class HospisimContextFactory : IDesignTimeDbContextFactory<HospisimContext>
    {
        public HospisimContext CreateDbContext(string[] args)
        {
            // Este método encontra o appsettings.json, lê a connection string
            // e cria o contexto, evitando o erro de DI.
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<HospisimContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new HospisimContext(optionsBuilder.Options);
        }
    }
}