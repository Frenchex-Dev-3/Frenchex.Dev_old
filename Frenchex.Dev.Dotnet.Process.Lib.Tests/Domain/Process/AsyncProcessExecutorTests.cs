using System.IO;
using System.Threading.Tasks;
using Frenchex.Dev.Dotnet.Process.Lib.DependencyInjection;
using Frenchex.Dev.Dotnet.Process.Lib.Domain.ProcessBuilder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Frenchex.Dev.Dotnet.Process.Lib.Tests.Domain.Process;

[TestClass]
public class AsyncProcessExecutorTests
{
    private IProcessBuilder? sut;

    [TestInitialize]
    public void Setup()
    {
        var services = new ServiceCollection();

        ServicesConfiguration
            .ConfigureServices(services);

        var di = services.BuildServiceProvider();

        sut = di.GetService<IProcessBuilder>();
    }

    [DataTestMethod]
    [DataRow(
        /** binary                  **/ "dotnet",
        /** arguments               **/ "--help",
        /** timeoutInMiliSeconds    **/ 10000
    )]
    public async Task Test_Can_Execute_Async(
        string binary,
        string arguments,
        int timeoutInMiliSeconds
    )
    {
        var workingDirectory = Path.Join(Path.GetTempPath(), Path.GetRandomFileName());

        Directory.CreateDirectory(workingDirectory);

        Assert.IsNotNull(sut);

        var process = sut.Build(new ProcessBuildingParameters(
            binary,
            arguments,
            workingDirectory,
            timeoutInMiliSeconds,
            false,
            true,
            true,
            true,
            true
        ));

        var processExecution = process.Start();

        var outputStream = new MemoryStream();
        var outputStreamWriter = new StreamWriter(outputStream);

        processExecution.Process.OutputDataReceived += async (s, e) =>
        {
            if (e.Data != null) await outputStreamWriter.WriteLineAsync(e.Data);
        };

        Assert.IsNotNull(processExecution.WaitForCompleteExit);

        await processExecution.WaitForCompleteExit;

        Assert.IsTrue(processExecution.ExitCode == 0);
        Assert.IsTrue(processExecution.Completed);

        await outputStreamWriter.FlushAsync();
        outputStream.Position = 0;
        var outputReader = new StreamReader(outputStream);
        var output = await outputReader.ReadToEndAsync();
        Assert.IsTrue(!string.IsNullOrEmpty(output));

        Directory.Delete(workingDirectory, true);
    }
}