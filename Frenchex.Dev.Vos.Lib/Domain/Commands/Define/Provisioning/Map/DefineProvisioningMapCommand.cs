namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Define.Provisioning.Map
{
    public class DefineProvisioningMapCommand : IDefineProvisioningMapCommand
    {
        private readonly IDefineProvisioningMapCommandResponseBuilderFactory _responseBuilderFactory;

        public DefineProvisioningMapCommand(
            IDefineProvisioningMapCommandResponseBuilderFactory responseBuilderFactory
        )
        {
            _responseBuilderFactory = responseBuilderFactory;
        }

#pragma warning disable CS1998 // Cette méthode async n'a pas d'opérateur 'await' et elle s'exécutera de façon synchrone
        public async Task<IDefineProvisioningMapCommandResponse> Execute(IDefineProvisioningMapCommandRequest request)
#pragma warning restore CS1998 // Cette méthode async n'a pas d'opérateur 'await' et elle s'exécutera de façon synchrone
        {
            return _responseBuilderFactory.Factory().Build();
        }
    }
}
