using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PUCMINAS.SistemaAcademico.Aplicacao.SistemaAcademicoServico;

namespace PUCMINAS.SistemaAcademico.Aplicacao
{
    public partial class Endereco : System.Web.UI.Page
    {
        EnderecoClient proxy;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    proxy = new EnderecoClient();
                    GridView1.DataSource = proxy.BuscaTodosEnderecos();
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                RegistraErro(ex.Message);
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                proxy = new EnderecoClient();
                EnderecoObj objcust =
                new EnderecoObj()
                {
                    EnderecoID = 4,
                    Cep = txtCep.Text,
                    Endereco = txtEndereco.Text
                };

                proxy.InserirEndereco(objcust);

                GridView1.DataSource = proxy.BuscaTodosEnderecos();
                GridView1.DataBind();
                Label1.Text = "Record Saved Successfully";

            }
            catch (Exception ex)
            {
                RegistraErro(ex.Message);

            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int userid = Convert.ToInt32(GridView1.DataKeys
            [e.RowIndex].Values["EnderecoID"].ToString());
            proxy = new EnderecoClient();

            bool check = proxy.DeletarEndereco(userid);
            Label1.Text = "Registros deletados com sucesso";
            GridView1.DataSource = proxy.BuscaTodosEnderecos();
            GridView1.DataBind();
        }

        protected void txtCep_TextChanged(object sender, EventArgs e)
        {
            try
            {

                proxy = new EnderecoClient();

                txtEndereco.Text = proxy.buscaCep(txtCep.Text);
            }
            catch (Exception ex)
            {
                RegistraErro(ex.Message);
            }
        }

        private void RegistraErro(string mensagem)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Ocorreu um erro na aplicação + " + mensagem + "');", true);
        }

        //private void regerr(Exception ex)
        //{
        //    List<string> errs = new List<string>();

        //    errs.Add(ex.Message);

        //    Exception iex;

        //    iex = ex;

        //    while (ex.InnerException != null) {
        //        errs.Add(ex.Message);
        //        iex = iex.InnerException;
        //    }

        //    RegistraErro(errs.ToString());
        //}


    }
}