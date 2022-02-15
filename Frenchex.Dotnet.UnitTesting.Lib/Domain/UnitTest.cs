using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Frenchex.Dotnet.UnitTesting.Lib.Domain
{
    public class UnitTest
    {
        private readonly Func<IServiceCollection, IConfigurationRoot, Task> _configureServicesFunc;
        private readonly Func<IConfigurationBuilder, Task> _configureConfigurationFunc;
        private readonly Func<IServiceCollection, IConfigurationRoot, Task> _configureMocksFunc;
        private readonly Func<IServiceProvider, IConfigurationRoot, Task> _prepareFunc;
        private readonly Func<IServiceProvider, IConfigurationRoot, Task> _executeFunc;
        private readonly Func<IServiceProvider, IConfigurationRoot, Task> _assertFunc;

        public UnitTest(
            Func<IConfigurationBuilder, Task> configureConfigurationFunc,
            Func<IServiceCollection, IConfigurationRoot, Task> configureServicesFunc,
            Func<IServiceCollection, IConfigurationRoot, Task> configureMocksFunc,
            Func<IServiceProvider, IConfigurationRoot, Task> prepareFunc,
            Func<IServiceProvider, IConfigurationRoot, Task> executeFunc,
            Func<IServiceProvider, IConfigurationRoot, Task> assertFunc
        )
        {
            _configureServicesFunc = configureServicesFunc;
            _configureConfigurationFunc = configureConfigurationFunc;
            _configureMocksFunc = configureMocksFunc;
            _prepareFunc = prepareFunc;
            _executeFunc = executeFunc;
            _assertFunc = assertFunc;
        }

        public async Task Run()
        {
            var configurationBuilder = new ConfigurationBuilder();

            await _configureConfigurationFunc(configurationBuilder);

            var configuration = configurationBuilder.Build();

            var services = new ServiceCollection();

            services.AddSingleton(configuration);

            await _configureServicesFunc(services, configuration);
            await _configureMocksFunc(services, configuration);

            var di = services.BuildServiceProvider();
            var scope = di.CreateAsyncScope();
            var scopedDi = scope.ServiceProvider;

            await _prepareFunc(scopedDi, configuration);
            await _executeFunc(scopedDi, configuration);
            await _assertFunc(scopedDi, configuration);

            await scope.DisposeAsync();
        }
    }
}
