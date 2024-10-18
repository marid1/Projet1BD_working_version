using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projet1MJ
{
    public partial class frmAjoutClient : Form
    {
        public BDB56Projet1MJDataSet.clientRow unClient;

        public frmAjoutClient()
        {
            InitializeComponent();
        }

        private void frmAjoutClient_Load(object sender, EventArgs e)
        {
            tbDate.Text = DateTime.Now.ToString();
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            if (tbNom.Text.Trim() == "")
                errMessage.SetError(tbNom, "Le nom ne peut pas être vide");
            else if (tbPrenom.Text.Trim() == "")
                errMessage.SetError(tbPrenom, "Le prénom ne peut pas être vide");
            else if (tbVille.Text.Trim() == "")
                errMessage.SetError(tbVille, "La ville ne peut pas être vide");
            else if (tbPays.Text.Trim() == "")
                errMessage.SetError(tbPays, "Le pays ne peut pas être vide");
            else if (tbAdresse.Text.Trim() == "")
                errMessage.SetError(tbAdresse, "L'adresse ne peut pas être vide");
            else if (tbCodePostal.Text.Trim() == "")
                errMessage.SetError(tbCodePostal, "Le code postal ne peut pas être vide");
            else
            {
                unClient.nomClient = tbNom.Text.Trim();
                unClient.prenomClient = tbPrenom.Text.Trim();
                unClient.villeClient = tbVille.Text.Trim(); ;
                unClient.paysClient = tbPays.Text.Trim(); ;
                unClient.adresseClient = tbAdresse.Text.Trim(); ;
                unClient.codePostalClient = tbCodePostal.Text.Trim(); ;
                unClient.dateInscriptionClient = Convert.ToDateTime(tbDate.Text);

                this.Close();
            }
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
