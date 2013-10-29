using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PUCMINAS.SistemaAcademico.Wcf
{
    [ServiceContract]
    public interface IEndereco
    {
        [OperationContract]
        string buscaCep(string cep);

        [OperationContract]
        bool InserirEndereco(EnderecoObj obj);

        [OperationContract]
        List<EnderecoObj> BuscaTodosEnderecos();


        [OperationContract]
        bool DeletarEndereco(int idEndereco);


        [OperationContract]
        bool AtualizarEndereco(EnderecoObj obj);
    }
}
