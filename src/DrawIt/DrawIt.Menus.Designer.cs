using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace DrawIt
{
    public partial class DrawIt
    {
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowNewDocumentDialog();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveProject();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowOpenFileDialog();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_drawing.HasChanges())
            {
                if (MessageBox.Show("Exit without saving changes?", "Are you sure?", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
                {
                    return;
                }
            }
            this.Close();
        }

        private void jpegToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportImage(ImageFormat.Jpeg);
        }

        private void bmpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportImage(ImageFormat.Bmp);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About a = new About();
            a.ShowDialog(this);
        }

        private void pngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportImage(ImageFormat.Png);
        }

    }
}
