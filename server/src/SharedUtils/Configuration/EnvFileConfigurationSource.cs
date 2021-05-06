using Microsoft.Extensions.Configuration;
namespace SharedUtils.Configuration
{
    public class EnvFileConfigurationSource : IConfigurationSource
    {
        public EnvFileConfigurationSource(string envFilePath)
        {
            EnvFilePath = envFilePath;
        }
        public string EnvFilePath { get; }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new EnvFileConfigurationProvider(EnvFilePath);
        }
    }
}