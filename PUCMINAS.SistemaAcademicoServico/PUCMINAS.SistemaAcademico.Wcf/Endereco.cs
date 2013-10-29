using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System;


namespace PUCMINAS.SistemaAcademico.Wcf
{
    public class Endereco : IEndereco
    {

        public bool InserirEndereco(EnderecoObj obj)
        {
            enderecoList.Add(obj);
            return true;
        }

        public List<EnderecoObj> BuscaTodosEnderecos()
        {
            return enderecoList;

        }

        public bool DeletarEndereco(int End)
        {
            var item = enderecoList.First(x => x.EnderecoID == End);

            enderecoList.Remove(item);
            return true;
        }

        public bool AtualizarEndereco(EnderecoObj obj)
        {
            var list = enderecoList;
            enderecoList.Where(p => p.EnderecoID == obj.EnderecoID).Update(p => p.Cep = obj.Cep);
            return true;
        }


        public static List<EnderecoObj> enderecoList = new List<EnderecoObj>()
         {
        new EnderecoObj {EnderecoID = 1, Cep="30110921", Endereco="Logradouro: Edifício Life Center <br/> Bairro: Funcionários <br/> Cidade: Belo Horizonte <br/> Estado: MG" },
        new EnderecoObj {EnderecoID = 2, Cep="30110027", Endereco="Logradouro: Avenida do Contorno - de 4303 a 4757 - lado  <br/> Bairro: Funcionários <br/> Cidade: Belo Horizonte <br/> Estado: MG" },
        new EnderecoObj {EnderecoID = 3, Cep="30430110", Endereco="Logradouro: Rua André Cavalcanti - lado par <br/> Bairro: Gutierrez <br/> Cidade:Belo Horizonte <br/> Estado: MG" }
         };


        public string buscaCep(string cep)
        {
            HttpWebRequest requisicao = (HttpWebRequest)WebRequest.Create("http://www.buscacep.correios.com.br/servicos/dnec/consultaLogradouroAction.do?Metodo=listaLogradouro&CEP=" + cep + "&TipoConsulta=cep");
            HttpWebResponse resposta = (HttpWebResponse)requisicao.GetResponse();
            int cont;
            byte[] buffer = new byte[1000];
            StringBuilder sb = new StringBuilder();
            string temp;
            Stream stream = resposta.GetResponseStream();

            do
            {
                cont = stream.Read(buffer, 0, buffer.Length);
                temp = Encoding.Default.GetString(buffer, 0, cont).Trim();
                sb.Append(temp);
            }
            while (cont > 0);

            string pagina = sb.ToString();

            if (pagina.IndexOf("<font color=\"black\">CEP NAO ENCONTRADO</font>") >= 0)
            {
                return "<b style=\"color:red\">CEP não localizado.</b>";
            }
            else
            {
                string logradouro = string.Empty, bairro = string.Empty, cidade = string.Empty, estado = string.Empty, resultado = string.Empty;

                logradouro = Regex.Match(pagina, "<td width=\"268\" style=\"padding: 2px\">(.*)</td>").Groups[1].Value;

                bairro = Regex.Matches(pagina, "<td width=\"140\" style=\"padding: 2px\">(.*)</td>")[0].Groups[1].Value;

                cidade = Regex.Matches(pagina, "<td width=\"140\" style=\"padding: 2px\">(.*)</td>")[1].Groups[1].Value;
                if (Regex.Match(pagina, "<td width=\"25\" style=\"padding: 2px\">(.*)</td>").Groups[1] != null)
                    estado = Regex.Match(pagina, "<td width=\"25\" style=\"padding: 2px\">(.*)</td>").Groups[1].Value;

                resultado = String.Format("Logradouro: {0}, Bairro: {1}, Cidade: {2}, Estado: {3}",
                   logradouro, bairro, cidade, estado);

                return resultado;
            }
        }


    }

    public static class LinqUpdates
    {

        public static void Update<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
                action(item);
        }

    }
}
