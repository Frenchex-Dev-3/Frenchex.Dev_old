﻿using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#pragma warning disable CS1998

namespace Frenchex.Dev.Dotnet.UnitTesting.Lib.Domain;

public class UnitTest
{
    private readonly Action<IServiceProvider, IConfigurationRoot>? _assertAction;

    private readonly Func<IServiceProvider, IConfigurationRoot, Task>? _assertFunc;
    private readonly Action<IConfigurationBuilder>? _configureConfigurationAction;

    private readonly Func<IConfigurationBuilder, Task>? _configureConfigurationFunc;
    private readonly Action<IServiceCollection, IConfigurationRoot>? _configureMocksAction;

    private readonly Func<IServiceCollection, IConfigurationRoot, Task>? _configureMocksFunc;
    private readonly Action<IServiceCollection, IConfigurationRoot>? _configureServicesAction;

    private readonly Func<IServiceCollection, IConfigurationRoot, Task>? _configureServicesFunc;
    private readonly Action<IServiceProvider, IConfigurationRoot>? _executeAction;

    private readonly Func<IServiceProvider, IConfigurationRoot, Task>? _executeFunc;
    private readonly Action<IServiceProvider, IConfigurationRoot>? _prepareAction;

    private readonly Func<IServiceProvider, IConfigurationRoot, Task>? _prepareFunc;
    private bool _openVsCode;

    private List<string> _openVsCodePath = new List<string>();

    public UnitTest(
        Action<IConfigurationBuilder> configureConfigurationAction,
        Action<IServiceCollection, IConfigurationRoot> configureServicesAction,
        Action<IServiceCollection, IConfigurationRoot> configureMocksAction,
        Action<IServiceProvider, IConfigurationRoot> prepareAction,
        Action<IServiceProvider, IConfigurationRoot> executeAction,
        Action<IServiceProvider, IConfigurationRoot> assertAction
    )
    {
        _configureConfigurationAction = configureConfigurationAction;
        _configureServicesAction = configureServicesAction;
        _configureMocksAction = configureMocksAction;
        _prepareAction = prepareAction;
        _executeAction = executeAction;
        _assertAction = assertAction;
    }

    public UnitTest(
        Action<IConfigurationBuilder> configureConfigurationAction,
        Action<IServiceCollection, IConfigurationRoot> configureServicesAction,
        Action<IServiceCollection, IConfigurationRoot> configureMocksAction
    )
    {
        _configureConfigurationAction = configureConfigurationAction;
        _configureServicesAction = configureServicesAction;
        _configureMocksAction = configureMocksAction;
    }

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

    public UnitTest(
        Func<IConfigurationBuilder, Task> configureConfigurationFunc,
        Func<IServiceCollection, IConfigurationRoot, Task> configureServicesFunc,
        Func<IServiceCollection, IConfigurationRoot, Task> configureMocksFunc
    )
    {
        _configureServicesFunc = configureServicesFunc;
        _configureConfigurationFunc = configureConfigurationFunc;
        _configureMocksFunc = configureMocksFunc;
    }

    public UnitTest OpenVsCode(string[] path, bool open = true)
    {
        _openVsCodePath.AddRange(path);
        _openVsCode = open;

        return this;
    }

    public void Run()
    {
        if (null == _prepareAction)
            throw new ArgumentNullException(nameof(_prepareAction));

        if (null == _executeAction)
            throw new ArgumentNullException(nameof(_executeAction));

        if (null == _assertAction)
            throw new ArgumentNullException(nameof(_assertAction));

        RunInternalTask(
            _prepareAction, _executeAction, _assertAction);
    }

    public void Run(
        Action<IServiceProvider, IConfigurationRoot> prepareAction,
        Action<IServiceProvider, IConfigurationRoot> executeAction,
        Action<IServiceProvider, IConfigurationRoot> assertAction
    )
    {
        RunInternalTask(
            prepareAction, executeAction, assertAction);
    }

    private void RunInternalTask(
        Action<IServiceProvider, IConfigurationRoot> prepareAction,
        Action<IServiceProvider, IConfigurationRoot> executeAction,
        Action<IServiceProvider, IConfigurationRoot> assertAction
    )
    {
        RunAsync(
                async (provider, root) => { prepareAction(provider, root); },
                async (provider, root) => { executeAction(provider, root); },
                async (provider, root) => { assertAction(provider, root); }
            )
            .GetAwaiter()
            .GetResult();
    }

    public async Task RunAsync(
        Func<IServiceProvider, IConfigurationRoot, Task> prepareFunc,
        Func<IServiceProvider, IConfigurationRoot, Task> executeFunc,
        Func<IServiceProvider, IConfigurationRoot, Task>? assertFunc = null
    )
    {
        await RunInternalTaskAsync(
            async configuration =>
            {
                if (null != _configureConfigurationFunc)
                    await _configureConfigurationFunc(configuration);
                else
                    _configureConfigurationAction?.Invoke(configuration);
            },
            async (services, configuration) =>
            {
                if (null != _configureServicesFunc)
                    await _configureServicesFunc(services, configuration);
                else
                    _configureServicesAction?.Invoke(services, configuration);
            },
            async (services, configuration) =>
            {
                if (null != _configureMocksFunc)
                    await _configureMocksFunc(services, configuration);
                else
                    _configureMocksAction?.Invoke(services, configuration);
            },
            prepareFunc,
            executeFunc,
            assertFunc
        );
    }

    public async Task RunAsync()
    {
        if (null == _prepareFunc)
            throw new ArgumentNullException(nameof(_prepareFunc));

        if (null == _executeFunc)
            throw new ArgumentNullException(nameof(_executeFunc));

        if (null == _assertFunc)
            throw new ArgumentNullException(nameof(_prepareFunc));

        await RunInternalTaskAsync(
            _configureConfigurationFunc!,
            _configureServicesFunc!,
            _configureMocksFunc!,
            _prepareFunc,
            _executeFunc,
            _assertFunc
        );
    }

    private async Task RunInternalTaskAsync(
        Func<IConfigurationBuilder, Task> configureConfigurationFunc,
        Func<IServiceCollection, IConfigurationRoot, Task> configureServicesFunc,
        Func<IServiceCollection, IConfigurationRoot, Task> configureMocksFunc,
        Func<IServiceProvider, IConfigurationRoot, Task> prepareFunc,
        Func<IServiceProvider, IConfigurationRoot, Task> executeFunc,
        Func<IServiceProvider, IConfigurationRoot, Task>? assertFunc
    )
    {
        if (null == configureConfigurationFunc)
            throw new ArgumentNullException(nameof(configureConfigurationFunc));

        if (null == configureServicesFunc)
            throw new ArgumentNullException(nameof(configureServicesFunc));

        if (null == configureMocksFunc)
            throw new ArgumentNullException(nameof(configureMocksFunc));

        var configurationBuilder = new ConfigurationBuilder();

        await configureConfigurationFunc(configurationBuilder);

        var configuration = configurationBuilder.Build();

        var services = new ServiceCollection();

        services.AddSingleton(configuration);

        await configureServicesFunc(services, configuration);
        await configureMocksFunc(services, configuration);

        var di = services.BuildServiceProvider();
        var scope = di.CreateAsyncScope();
        var scopedDi = scope.ServiceProvider;

        List<Process> processes = new List<Process>();

        if (_openVsCode)
        {
            foreach (var vspath in _openVsCodePath)
            {
                Process process = Process.Start("C:\\Program Files\\Microsoft VS Code\\Code.exe", "-n " + vspath);
                processes.Add(process);
            }
        }

        await prepareFunc(scopedDi, configuration);
        await executeFunc(scopedDi, configuration);

        // sometimes the executeFunc makes asserts 
        // because we have not yet decomposed enough our tests
        // in the executeFunc, we are asserting parsing and execution

        if (null != assertFunc)
        {
            await assertFunc(scopedDi, configuration);
        }

        await scope.DisposeAsync();

        if (_openVsCode && processes.Any())
            foreach (var pro in processes)
            {
                pro.Kill();
            }

        foreach (var vspath in _openVsCodePath.Where(vspath => Directory.Exists(vspath)))
        {
            Directory.Delete(vspath, true);
        }
    }
}