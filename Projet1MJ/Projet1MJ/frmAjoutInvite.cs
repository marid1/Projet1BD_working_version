using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projet1MJ
{
    public partial class frmAjoutInvite : Form
    {
        public BDB56Projet1MJDataSet.inviteRow unInvite;

        public frmAjoutInvite()
        {
            InitializeComponent();
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            if (tbNom.Text.Trim() == "")
                errMessage.SetError(tbNom, "Le nom ne peut pas être vide");
            else
            {
                unInvite.nomPrenomInvite = tbNom.Text.Trim();
                this.Close();
            }
        }
    }
}
