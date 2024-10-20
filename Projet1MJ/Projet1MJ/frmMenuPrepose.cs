﻿using System;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void déconnexionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConnexion fConnexion = new frmConnexion();
            this.Hide();
            fConnexion.ShowDialog();
            this.Close();
        }

        private void gérerLesClientsEtLeursInvitésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGestionClientInvite fGestionClientInvite = new frmGestionClientInvite();
            this.Hide();
            fGestionClientInvite.ShowDialog();
            this.Show();
        }
    }
}
