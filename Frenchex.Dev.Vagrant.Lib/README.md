## Vagrant [WIP]

`vagrant`'s `devx` subcommand provides helpful `vagrant` CLI wrapping commands.

For example, when using Vagrant, your folder containing your Vagrantfile will be used as the name to prefix your
machines.

These little wrappers are helping managing this for you plus more.

```
devx vagrant --help
```

### Commands implemented

All Vagrant commands are implemented by respecting following conventions :

* Naming

The name of the Vagrant instance is the name of the project holding the Vagrantfile.

* `devx vagrant` subcommands which are using naming patterns are giving you ability to "forget" this.

There is also a subcommand `name` which outputs the name of the machines matching naming pattern.

* `devx vagrant` subcommands are providing pretty much same --options as wrapped `vagrant`.

* Timeouts and such timing options are represented as strings using human-readable patterns like "10s".

#### Init

```powershell
devx vagrant init --code [or --vs]
```

Will create a new

#### Destroy

Will destroy all vagrant machines named following given name pattern in your current directory

```powershell
devx vagrant destroy my-vagrant-machine-[1-15] --force --timeout 0 [--path c:\\somewhere\\else]
```

#### Halt

```powershell
devx vagrant halt my-vagrant-machine-[1-15] --force --gracefull-timeout 60s [--path c:\\somewhere\\else]
```

#### SSH

This little wrapper provides you a great utility to send an ssh command to many machines at once.

```powershell
devx vagrant ssh my-vagrant-machine-[0-14] --command "echo hostnames"
```

Output will be managed and each line will be formatted following pattern given using the `--log-output-format` option
with default to `[timestamp UTC][hostname]`.

#### SSH Config

Will add or update machines following naming pattern to your user's .ssh/config file.

```powershell
devx vagrant ssh-config my-vagrant-machine-[0-14] --to-ssh-config --user
```

#### Up

```powershell
devx vagrant ssh-config my-vagrant-machine-[0-14] --to-ssh-config --user
```