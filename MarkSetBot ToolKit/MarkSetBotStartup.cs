using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shiny;
namespace MarkSetBot_ToolKit
{
    public class MarkSetBotStartup :ShinyStartup
    {
        public MarkSetBotStartup()
        {

        }

        public override void ConfigureLogging(ILoggingBuilder builder, IPlatform platform)
        {
            
        }


        public override void ConfigureServices(IServiceCollection services, IPlatform platform)
        {
            services.UseBleClient();
        }
    }
}
