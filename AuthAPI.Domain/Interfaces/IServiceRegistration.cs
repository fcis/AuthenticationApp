using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Domain.Interfaces
{
    public interface IServiceRegistration
    {
        IServiceCollection RegisterServices(IServiceCollection services, IConfiguration configuration);
    }
}
