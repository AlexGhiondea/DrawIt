using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawIt
{
    public partial class Options : Form
    {
        public string HeaderText { get; set; }
        public int LogoHeight { get; set; }
        public string Unit { get; set; }

        private string EncodedLogo;
        private Font HeaderFont;

        public Options()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveSettings();
            Close();
        }

        private void SaveSettings()
        {
            Configuration.SetSetting(Constants.Application.Header.Text, HeaderText);
            Configuration.SetSetting(Constants.Application.Logo.Image, EncodedLogo);
            Configuration.SetSetting(Constants.Application.Logo.Height, LogoHeight.ToString());

            Configuration.SetSetting(Constants.Application.Header.TextColor, _backColor.ToArgb().ToString());
            Configuration.SetSetting(Constants.Application.Header.FontName, HeaderFont.Name);
            Configuration.SetSetting(Constants.Application.Header.FontSize, HeaderFont.Size.ToString());
            Configuration.SetSetting(Constants.Application.Header.FontStyle, HeaderFont.Style.ToString());

            Configuration.SetSetting(Constants.Document.MeasurementUnit, Unit);

            Configuration.SaveToDisk();
        }

        private Color _backColor;

        private void Options_Load(object sender, EventArgs e)
        {
            HeaderText = Configuration.GetSetting(Constants.Application.Header.Text) ?? string.Empty;
            EncodedLogo = Configuration.GetSetting(Constants.Application.Logo.Image) ?? string.Empty;
            LogoHeight = Configuration.GetSettingOrDefault(Constants.Application.Logo.Height, int.TryParse, Constants.Application.Defaults.LogoHeight);
            Unit = Configuration.GetSetting(Constants.Document.MeasurementUnit) ?? Constants.Document.Defaults.MeasurementUnitDefault;

            // Header Text
            _backColor = Color.FromArgb(Configuration.GetSettingOrDefault<int>(Constants.Application.Header.TextColor, int.TryParse, Constants.Measurement.Defaults.Black));

            HeaderFont = FontHelpers.GetHeaderFont();
            lblFont.Font = HeaderFont;
            lblFont.ForeColor = _backColor;
            //lblFont.Text = string.Format("{0}, {1} {2}", HeaderFont.Name, HeaderFont.Size, HeaderFont.Style);

            txtHeaderText.DataBindings.Add("Text", this, "HeaderText");
            nupLogoHeight.DataBindings.Add("Value", this, "LogoHeight");
            txtUnit.DataBindings.Add("Text", this, "Unit");

            if (!string.IsNullOrEmpty(EncodedLogo))
                LoadImageFromEncodedString(Convert.FromBase64String(EncodedLogo));
        }

        private void LoadImageFromEncodedString(byte[] imageBytes)
        {
            MemoryStream ms = new MemoryStream(imageBytes);
            Bitmap bmp = new Bitmap(ms);

            // dispose of the previous image.
            if (pbLogo.Image != null)
                pbLogo.Image.Dispose();

            pbLogo.Image = bmp;

            // store the encoded image
            EncodedLogo = Convert.ToBase64String(imageBytes);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSelectLogo_Click(object sender, EventArgs e)
        {
            // choose another image

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.CheckFileExists = true;
                ofd.CheckPathExists = true;
                ofd.Filter = "Image files|*.jpg;*.bmp;*.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    LoadImageFromEncodedString(File.ReadAllBytes(ofd.FileName));
                    nupLogoHeight.Value = Constants.Application.Defaults.LogoHeight;
                    LogoHeight = Constants.Application.Defaults.LogoHeight;
                }
            }
        }

        private void btnRemoveLogo_Click(object sender, EventArgs e)
        {
            RemoveLogo();
        }

        private void RemoveLogo()
        {
            if (pbLogo.Image != null)
                pbLogo.Image.Dispose();

            pbLogo.Image = null;

            EncodedLogo = string.Empty;
        }

        private void lblTextColor_Click(object sender, EventArgs e)
        {

        }

        private void btnRemoveHeader_Click(object sender, EventArgs e)
        {
            HeaderText = string.Empty;
            txtHeaderText.Text = string.Empty;
            LogoHeight = 0;
            nupLogoHeight.Value = 0;

            RemoveLogo();
        }

        private void btnChangeFont_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = HeaderFont;
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                HeaderFont = fd.Font;
                //lblFont.Text = string.Format("{0}, {1} {2}", HeaderFont.Name, HeaderFont.Size, HeaderFont.Style);

                lblFont.Font = HeaderFont;
            }
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = _backColor;
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _backColor = cd.Color;
                lblFont.ForeColor = _backColor;
            }
        }
    }
}
