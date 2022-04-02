using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Frenchex.Dev.Dotnet.Docker.Compose.Lib.DependencyInjection;
using Frenchex.Dev.Dotnet.Docker.Compose.Lib.Domain.Commands.Init;
using Frenchex.Dev.Dotnet.Docker.Compose.Lib.Domain.Commands.Ps;
using Frenchex.Dev.Dotnet.Docker.Compose.Lib.Domain.Declarations;
using Frenchex.Dev.Dotnet.UnitTesting.Lib.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Frenchex.Dev.Dotnet.Docker.Compose.Lib.Tests.Domain;

[TestClass]
public class TestCompleteWorkflow
{
    private readonly UnitTest _unitTest;

    public TestCompleteWorkflow()
    {
        _unitTest = new UnitTest(
            builder => { },
            (services, configuration) =>
            {
                services.AddScoped<IConfiguration>((_) => configuration);

                ServicesConfigurator
                    .ConfigureServices(services)
                    ;
            },
            (services, configuration) => { }
        );
    }


    [TestMethod]
    [DynamicData(nameof(Test_Data), DynamicDataSourceType.Method)]
    public void Test(DockerComposeDeclaration declaration)
    {
        _unitTest.RunAsync(
            (services, configuration) => Task.CompletedTask,
            async (services, configuration) =>
            {
                var testDir = Path.Join(Path.GetTempPath(), Path.GetRandomFileName());

                var initCommand = services.GetRequiredService<IInitCommand>();
                await initCommand.ExecuteAsync(new InitCommandRequest()
                {
                    Declaration = declaration,
                    WorkingDirectory = testDir
                });

                var psCommand = services.GetRequiredService<IPsCommand>();
                var psProcess = psCommand.Execute(new PsCommandRequest()
                {
                    WorkingDirectory = testDir,
                    TimeoutMs = (int) TimeSpan.FromMinutes(1).TotalMilliseconds
                });
                
                await psProcess.Result.WaitForCompleteExit;
                
                Assert.AreEqual(0, psProcess.Result.ExitCode);
            },
            (services, configuration) => Task.CompletedTask
        );
    }

    public static IEnumerable<object[]> Test_Data()
    {
        yield return new object[]
        {
            BuildNewDockerComposeDeclaration()
        };
    }

    public static DockerComposeDeclaration BuildNewDockerComposeDeclaration()
    {
        return new DockerComposeDeclaration()
        {
            Version = "3.9",
            Services = new Dictionary<string, DockerComposeServiceDeclaration>()
            {
                {
                    "redis", new DockerComposeServiceDeclaration()
                    {
                        Image = "redis:alpine",
                        Ports = new[] {"6379"},
                        Networks = new[] {"frontend"},
                        Deploy = new DockerComposeServiceDeployDeclaration()
                        {
                            Replicas = 2,
                            UpdateConfig = new DockerComposeServiceUpdateConfigDeclaration()
                            {
                                Parallelism = 2,
                                Delay = "10s"
                            },
                            RestartPolicy = new DockerComposeServiceRestartPolicyDeclaration()
                            {
                                Condition = "on-failure"
                            }
                        }
                    }
                },
                {
                    "db", new DockerComposeServiceDeclaration()
                    {
                        Image = "postgres:9.4",
                        Volumes = new[] {"db-data:/var/lib/postgresql/data"},
                        Networks = new[] {"backend"},
                        Deploy = new DockerComposeServiceDeployDeclaration()
                        {
                            Placement = new DockerComposeServicePlacementDeclaration()
                            {
                                MaxReplicasPerNode = 1,
                                Constraints = new[] {"node.role==manager"}
                            }
                        }
                    }
                },
                {
                    "vote", new DockerComposeServiceDeclaration()
                    {
                        Image = "dockersamples/examplevotingapp_vote:before",
                        Ports = new string[] {"5000:80"},
                        Networks = new[] {"frontend"},
                        DependsOn = new[] {"redis"},
                        Deploy = new DockerComposeServiceDeployDeclaration()
                        {
                            Replicas = 2,
                            UpdateConfig = new DockerComposeServiceUpdateConfigDeclaration()
                            {
                                Parallelism = 2
                            },
                            RestartPolicy = new DockerComposeServiceRestartPolicyDeclaration()
                            {
                                Condition = "on-failure"
                            }
                        }
                    }
                },
                {
                    "result", new DockerComposeServiceDeclaration()
                    {
                        Image = "dockersamples/examplevotingapp_result:before",
                        Ports = new[] {"5001:80"},
                        Networks = new[] {"backend"},
                        DependsOn = new[] {"db"},
                        Deploy = new DockerComposeServiceDeployDeclaration()
                        {
                            Replicas = 1,
                            UpdateConfig = new DockerComposeServiceUpdateConfigDeclaration()
                            {
                                Parallelism = 2,
                                Delay = "10s"
                            },
                            RestartPolicy = new DockerComposeServiceRestartPolicyDeclaration()
                            {
                                Condition = "on-failure"
                            }
                        }
                    }
                },
                {
                    "worker", new DockerComposeServiceDeclaration()
                    {
                        Image = "dockersamples/examplevotingapp_worker",
                        Networks = new[] {"frontend", "backend"},
                        Deploy = new DockerComposeServiceDeployDeclaration()
                        {
                            Mode = "replicated",
                            Replicas = 1,
                            Labels = new string[] {"APP=VOTING"},
                            RestartPolicy = new DockerComposeServiceRestartPolicyDeclaration()
                            {
                                Condition = "on-failure",
                                Delay = "10s",
                                MaxAttempts = 3,
                                Window = "120s"
                            },
                            Placement = new DockerComposeServicePlacementDeclaration()
                            {
                                Constraints = new[] {"node.role==manager"}
                            }
                        }
                    }
                },
                {
                    "visualizer", new DockerComposeServiceDeclaration()
                    {
                        Image = "dockersamples/visualizer:stable",
                        Ports = new[] {"8080:8080"},
                        StopGracePeriod = "1m30s",
                        Volumes = new[] {"/var/run/docker.sock:/var/run/docker.sock"},
                        Deploy = new DockerComposeServiceDeployDeclaration()
                        {
                            Placement = new DockerComposeServicePlacementDeclaration()
                            {
                                Constraints = new[] {"node.role==manager"}
                            }
                        }
                    }
                }
            },
            Networks = new Dictionary<string, DockerComposeNetworkDeclaration>()
            {
                {
                    "frontend", new DockerComposeNetworkDeclaration() { }
                },
                {
                    "backend", new DockerComposeNetworkDeclaration() { }
                }
            },
            Volumes = new Dictionary<string, DockerComposeVolumeDeclaration>()
            {
                {
                    "db-data", new DockerComposeVolumeDeclaration() { }
                }
            }
        };
    }
}