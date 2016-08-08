using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawIt
{
    public partial class NewDrawing : Form
    {
        public double Ratio { get; set; }
        public string Unit { get; set; }

        public Drawing Document;
        public NewDrawing(Drawing previousDrawing)
        {
            InitializeComponent();

            if (previousDrawing == null)
            {
                Ratio = 0.5;
                Unit = "ft";
            }
            else
            {
                Ratio = previousDrawing.ConversionRatio;
                Unit = previousDrawing.Unit;
            }

            txtRatio.DataBindings.Add("Text", this, "Ratio", false, DataSourceUpdateMode.OnPropertyChanged);
            txtUnit.DataBindings.Add("Text", this, "Unit", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Document = new Drawing(Ratio, Unit);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
