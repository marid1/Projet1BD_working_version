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
    public partial class frmGestionClientInvite : Form
    {
        private bool isDeleting = false;

        public frmGestionClientInvite()
        {
            InitializeComponent();
        }

        private void clientBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.clientBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.bDB56Projet1MJDataSet);

        }

        private void frmGestionClientInvite_Load(object sender, EventArgs e)
        {
            // TODO: cette ligne de code charge les données dans la table 'bDB56Projet1MJDataSet.invite'. Vous pouvez la déplacer ou la supprimer selon les besoins.
            this.inviteTableAdapter.Fill(this.bDB56Projet1MJDataSet.invite);
            // TODO: cette ligne de code charge les données dans la table 'bDB56Projet1MJDataSet.client'. Vous pouvez la déplacer ou la supprimer selon les besoins.
            this.clientTableAdapter.Fill(this.bDB56Projet1MJDataSet.client);

        }

        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.clientBindingSource.EndEdit();
            this.clientTableAdapter.Update(this.bDB56Projet1MJDataSet.client);
            MessageBox.Show("Les modifications ont été enregistrées dans la base de données.", " Enregistrement des données", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            bDB56Projet1MJDataSet.client.RejectChanges();
            clientBindingSource.ResetBindings(false);
            MessageBox.Show("Les modifications depuis le dernier enregistrement ont été annulées.", "Annulation des modifications", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnPremier_Click(object sender, EventArgs e)
        {
            clientBindingSource.MoveFirst();
        }

        private void btnPrecedent_Click(object sender, EventArgs e)
        {
            clientBindingSource.MovePrevious();
        }

        private void btnSuivant_Click(object sender, EventArgs e)
        {
            clientBindingSource.MoveNext();
        }

        private void btnDernier_Click(object sender, EventArgs e)
        {
            clientBindingSource.MoveLast();
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnActualiser_Click(object sender, EventArgs e)
        {
            clientBindingSource.ResetCurrentItem();
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            BDB56Projet1MJDataSet.clientRow unClient = bDB56Projet1MJDataSet.client.NewclientRow();

            decimal noClientMax = 0;
            foreach (BDB56Projet1MJDataSet.clientRow uneLigne in bDB56Projet1MJDataSet.client.Rows)
                if (uneLigne.noClient > noClientMax) noClientMax = uneLigne.noClient;

            unClient.noClient = (int)(noClientMax + 10);

            unClient.nomClient = string.Empty;
            unClient.prenomClient = string.Empty;
            unClient.villeClient = string.Empty;
            unClient.paysClient = string.Empty;
            unClient.adresseClient = string.Empty;
            unClient.codePostalClient = string.Empty;
            unClient.dateInscriptionClient = DateTime.Now;

            frmAjoutClient frmClient = new frmAjoutClient();
            frmClient.unClient = unClient;
            frmClient.ShowDialog();

            if (!string.IsNullOrEmpty(unClient.nomClient) || !string.IsNullOrEmpty(unClient.prenomClient) ||
                !string.IsNullOrEmpty(unClient.villeClient) || !string.IsNullOrEmpty(unClient.paysClient) ||
                !string.IsNullOrEmpty(unClient.adresseClient) || !string.IsNullOrEmpty(unClient.codePostalClient))
            {
                bDB56Projet1MJDataSet.client.AddclientRow(unClient);
                clientBindingSource.MoveLast();
                MessageBox.Show("Le client " + unClient.noClient.ToString() + " a été ajouté.", "Ajout d'un client", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {

        }

        private void clientDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            clientDataGridView.Rows[e.RowIndex].ErrorText =
                "Le type de données de " + clientDataGridView.Columns[e.ColumnIndex].HeaderText + " n'est pas valide.";
            e.Cancel = true;
        }

        private void clientDataGridView_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (isDeleting)
                return;

            string messageErreur = string.Empty;
            string strNom = clientDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
            string strPrenom = clientDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
            string strVille = clientDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
            string strPays = clientDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
            string strAdresse = clientDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
            string strCodePostal = clientDataGridView.Rows[e.RowIndex].Cells[6].Value.ToString();

            if (string.IsNullOrEmpty(strNom) || string.IsNullOrEmpty(strPrenom) ||
                string.IsNullOrEmpty(strVille) || string.IsNullOrEmpty(strPays) ||
                string.IsNullOrEmpty(strAdresse) || string.IsNullOrEmpty(strCodePostal))
                messageErreur = "Aucun champ ne doit être vide";

            clientDataGridView.Rows[e.RowIndex].ErrorText = messageErreur;
            if (messageErreur != String.Empty)
                e.Cancel = true;
        }

        private void btnPremierInvite_Click(object sender, EventArgs e)
        {
            inviteBindingSource.MoveFirst();
        }

        private void btnPrecedentInvite_Click(object sender, EventArgs e)
        {
            inviteBindingSource.MovePrevious();
        }

        private void btnSuivantInvite_Click(object sender, EventArgs e)
        {
            inviteBindingSource.MoveNext();
        }

        private void btnDernierInvite_Click(object sender, EventArgs e)
        {
            inviteBindingSource.MoveLast();
        }

        private void btnAjouterInvite_Click(object sender, EventArgs e)
        {
            if (clientDataGridView.CurrentRow == null)
            {
                MessageBox.Show("Veuillez sélectionner un client.", "Erreur", MessageBoxButtons.OK);
                return;
            }

            int noClient = (int)clientDataGridView.CurrentRow.Cells[0].Value;

            int noInvite = noClient + 1;

            var invitesExistants = bDB56Projet1MJDataSet.invite
                .Where(i => i.noClient == noClient)
                .Select(i => i.noInvite)
                .ToList();

            // Trouvez le premier numéro disponible
            for (int i = 0; i < 9; i++)
            {
                if (!invitesExistants.Contains(noClient + 1 + i))
                {
                    noInvite = noClient + 1 + i;
                    break;
                }
            }

            BDB56Projet1MJDataSet.inviteRow unInvite = bDB56Projet1MJDataSet.invite.NewinviteRow();

            unInvite.noClient = noClient;
            unInvite.noInvite = noInvite;
            unInvite.nomPrenomInvite = string.Empty;            

            frmAjoutInvite frmInvite = new frmAjoutInvite();
            frmInvite.unInvite = unInvite;
            frmInvite.ShowDialog();

            if (!string.IsNullOrEmpty(unInvite.nomPrenomInvite))
            {
                bDB56Projet1MJDataSet.invite.AddinviteRow(unInvite);
                inviteBindingSource.MoveLast();
                MessageBox.Show("L'invité " + unInvite.noInvite.ToString() + " a été ajouté.", "Ajout d'un invité", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void btnSupprimerInvite_Click(object sender, EventArgs e)
        {

        }

        private void btnActualiserInvite_Click(object sender, EventArgs e)
        {
            inviteBindingSource.ResetCurrentItem();
        }

        private void inviteDataGridView_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (isDeleting)
                return;

            string messageErreur = string.Empty;
            string strNom = inviteDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();

            if (string.IsNullOrEmpty(strNom))
                messageErreur = "Aucun champ ne doit être vide";

            inviteDataGridView.Rows[e.RowIndex].ErrorText = messageErreur;
            if (messageErreur != String.Empty)
                e.Cancel = true;
        }

        private void inviteDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            inviteDataGridView.Rows[e.RowIndex].ErrorText =
                "Le type de données de " + inviteDataGridView.Columns[e.ColumnIndex].HeaderText + " n'est pas valide.";
            e.Cancel = true;
        }
    }
}
