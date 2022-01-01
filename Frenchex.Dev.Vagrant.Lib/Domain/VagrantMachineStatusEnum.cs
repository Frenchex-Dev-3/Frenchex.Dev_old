namespace Frenchex.Dev.Vagrant.Lib.Domain;

public enum VagrantMachineStatusEnum
{
    NotCreated,
    Aborted,
    Running,
    Stopped,
    Suspended
}

public class Statuses
{
    public static readonly string NotCreated = "not created";
    public static readonly string Running = "running";
    public static readonly string Aborted = "aborted";
    public static readonly string Suspended = "suspended";
    public static readonly string Stopped = "stopped";

    public string ToString(VagrantMachineStatusEnum status)
    {
        if (status == VagrantMachineStatusEnum.NotCreated)
            return Statuses.NotCreated;

        if (status == VagrantMachineStatusEnum.Running)
            return Statuses.Running;

        if (status == VagrantMachineStatusEnum.Aborted)
            return Statuses.Aborted;

        if (status == VagrantMachineStatusEnum.Stopped)
            return Statuses.Stopped;

        if (status == VagrantMachineStatusEnum.Suspended)
            return Statuses.Suspended;

        throw new InvalidOperationException("unknown enum status value");
    }

    public VagrantMachineStatusEnum ToEnum(string status)
    {
        if (status == Statuses.NotCreated)
            return VagrantMachineStatusEnum.NotCreated;

        if (status == Statuses.Running)
            return VagrantMachineStatusEnum.Running;

        if (status == Statuses.Aborted)
            return VagrantMachineStatusEnum.Aborted;

        if (status == Statuses.Stopped)
            return VagrantMachineStatusEnum.Stopped;

        if (status == Statuses.Suspended)
            return VagrantMachineStatusEnum.Suspended;

        throw new InvalidOperationException("unknown string status value");
    }
}