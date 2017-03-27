using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawIt
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();

            if (ApplicationDeployment.IsNetworkDeployed)
                lblAppVersion.Text = $"Version {ApplicationDeployment.CurrentDeployment.CurrentVersion}";
            else
                lblAppVersion.Text = "Version <local>";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            Process.Start("mailto:" + lnkEmail.Text);
        }
    }
}
