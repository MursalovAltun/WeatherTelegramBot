using AutoMapper.Configuration;

namespace Common.WebApiCore.Setup
{
    public static class AutoMapperConfig
    {
        public static void Configure(MapperConfigurationExpression config)
        {
            config.AllowNullCollections = true;
        }
    }
}