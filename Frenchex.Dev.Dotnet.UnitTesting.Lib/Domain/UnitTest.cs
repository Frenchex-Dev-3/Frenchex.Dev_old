using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#pragma warning disable CS1998

namespace Frenchex.Dev.Dotnet.UnitTesting.Lib.Domain;

public class UnitTest
{
    private readonly Action<IConfigurationBuilder> _configureConfigurationFunc;
    private readonly Action<IServiceCollection, IConfigurationRoot>? _configureMocksFunc;
    private readonly Action<IServiceCollection, IConfigurationRoot>? _configureServicesFunc;
    private bool _openVsCode;
    private string _openVsCodePath;


    public UnitTest(
        Action<IConfigurationBuilder> configureConfigurationFunc,
        Action<IServiceCollection, IConfigurationRoot>? configureServicesFunc,
        Action<IServiceCollection, IConfigurationRoot>? configureMocksFunc
    )
    {
        _configureConfigurationFunc = configureConfigurationFunc;
        _configureServicesFunc = configureServicesFunc;
        _configureMocksFunc = configureMocksFunc;
    }

    public IServiceProvider? Provider { get; protected set; }
    public AsyncServiceScope? AsyncScope { get; protected set; }
    public IConfigurationRoot? Configuration { get; protected set; }


    public async Task RunAsync(
        Func<IServiceProvider, IConfigurationRoot, Task> prepareFunc,
        Func<IServiceProvider, IConfigurationRoot, Task> executeFunc,
        Func<IServiceProvider, IConfigurationRoot, Task>? assertFunc = null
    )
    {
        await RunInternalTaskAsync(
            prepareFunc,
            executeFunc,
            assertFunc
        );
    }

    private async Task RunInternalTaskAsync(
        Func<IServiceProvider, IConfigurationRoot, Task> prepareFunc,
        Func<IServiceProvider, IConfigurationRoot, Task> executeFunc,
        Func<IServiceProvider, IConfigurationRoot, Task>? assertFunc
    )
    {
        await Build();
        if (prepareFunc == null) throw new ArgumentNullException(nameof(prepareFunc));
        if (executeFunc == null) throw new ArgumentNullException(nameof(executeFunc));

        if (Provider != null)
        {
            Process vsCodeProcess = null;

            if (_openVsCode)
                vsCodeProcess = Process.Start("C:\\Program Files\\Microsoft VS Code\\Code.exe",
                    "-n " + _openVsCodePath);

            try
            {
                await prepareFunc(Provider, Configuration);
                await executeFunc(Provider, Configuration);

                // sometimes the executeFunc makes asserts 
                // because we have not yet decomposed enough our tests
                // in the executeFunc, we are asserting parsing and execution

                if (null != assertFunc)
                    await assertFunc(Provider, Configuration);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            
            vsCodeProcess?.Kill();
        }

        if (AsyncScope.HasValue) await AsyncScope.Value.DisposeAsync();
    }

    public async Task Build()
    {
        if (null == _configureConfigurationFunc)
            throw new ArgumentNullException(nameof(_configureConfigurationFunc));

        if (null == _configureServicesFunc)
            throw new ArgumentNullException(nameof(_configureServicesFunc));

        if (null == _configureMocksFunc)
            throw new ArgumentNullException(nameof(_configureMocksFunc));

        var configurationBuilder = new ConfigurationBuilder();

        _configureConfigurationFunc(configurationBuilder);

        var configuration = configurationBuilder.Build();

        var services = new ServiceCollection();

        services.AddSingleton(configuration);

        _configureServicesFunc(services, configuration);
        _configureMocksFunc(services, configuration);

        var di = services.BuildServiceProvider();
        AsyncScope = di.CreateAsyncScope();
        Provider = AsyncScope.Value.ServiceProvider;
        Configuration = configuration;
    }

    public UnitTest OpenVsCode(string path, bool open = true)
    {
        _openVsCodePath = path;
        _openVsCode = open;

        return this;
    }
}