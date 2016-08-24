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
