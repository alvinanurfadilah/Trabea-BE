using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabeaDataAccess.Models;

namespace TrabeaDataAccess;
public class Dependencies
{
    public static void ConfigureService(IConfiguration configuration, IServiceCollection services)
    {
        services.AddDbContext<TrabeaContext>(option => option.UseSqlServer(configuration.GetConnectionString("TrabeaConnection")));
    }
}
