using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace PUCMINAS.SistemaAcademico.Wcf
{
    [DataContract]  
    public class EnderecoObj
    {
        [DataMember]
        public int EnderecoID;

        [DataMember]
        public string Cep;

        [DataMember]
        public string Endereco;
    }
}
