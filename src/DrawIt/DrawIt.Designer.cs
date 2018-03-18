namespace DrawIt
{
    partial class DrawIt
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label lblHorizontal;
            System.Windows.Forms.Label lblZoom;
            this.drawSurface = new System.Windows.Forms.Panel();
            this.btnNewSegment = new System.Windows.Forms.Button();
            this.rtbDraw = new System.Windows.Forms.RadioButton();
            this.rtbMeasure = new System.Windows.Forms.RadioButton();
            this.cboHorizontalAlignment = new System.Windows.Forms.ComboBox();
            this.tbZoom = new System.Windows.Forms.TrackBar();
            this.stsStatus = new System.Windows.Forms.StatusStrip();
            this.stsDocData = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsPosition = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssActiveStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.rtbDelete = new System.Windows.Forms.RadioButton();
            this.lblDrawColor = new System.Windows.Forms.Label();
            this.nupDrawWidth = new System.Windows.Forms.NumericUpDown();
            this.mnuMainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jpegToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bmpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grpDraw = new System.Windows.Forms.GroupBox();
            this.lblNupArcDescription = new System.Windows.Forms.Label();
            this.lblNupArcUnits = new System.Windows.Forms.Label();
            this.nupArcSize = new System.Windows.Forms.NumericUpDown();
            this.cboDrawElements = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grpText = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblFont = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTextToDraw = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTextColor = new System.Windows.Forms.Label();
            this.grpMeasure = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblMeasureColor = new System.Windows.Forms.Label();
            this.rtbMove = new System.Windows.Forms.RadioButton();
            this.rtbText = new System.Windows.Forms.RadioButton();
            this.rtbImage = new System.Windows.Forms.RadioButton();
            this.grpImage = new System.Windows.Forms.GroupBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtImagePath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            lblHorizontal = new System.Windows.Forms.Label();
            lblZoom = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tbZoom)).BeginInit();
            this.stsStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupDrawWidth)).BeginInit();
            this.mnuMainMenu.SuspendLayout();
            this.grpDraw.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupArcSize)).BeginInit();
            this.grpText.SuspendLayout();
            this.grpMeasure.SuspendLayout();
            this.grpImage.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHorizontal
            // 
            lblHorizontal.AutoSize = true;
            lblHorizontal.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblHorizontal.Location = new System.Drawing.Point(87, 21);
            lblHorizontal.Name = "lblHorizontal";
            lblHorizontal.Size = new System.Drawing.Size(86, 15);
            lblHorizontal.TabIndex = 10;
            lblHorizontal.Text = "Text alignment";
            // 
            // lblZoom
            // 
            lblZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            lblZoom.AutoSize = true;
            lblZoom.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            lblZoom.Location = new System.Drawing.Point(729, 4);
            lblZoom.Name = "lblZoom";
            lblZoom.Size = new System.Drawing.Size(34, 13);
            lblZoom.TabIndex = 14;
            lblZoom.Text = "Zoom";
            // 
            // drawSurface
            // 
            this.drawSurface.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.drawSurface.AutoScroll = true;
            this.drawSurface.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.drawSurface.Cursor = System.Windows.Forms.Cursors.Cross;
            this.drawSurface.Location = new System.Drawing.Point(0, 79);
            this.drawSurface.Name = "drawSurface";
            this.drawSurface.Size = new System.Drawing.Size(849, 287);
            this.drawSurface.TabIndex = 2;
            this.drawSurface.Paint += new System.Windows.Forms.PaintEventHandler(this.drawSurface_Paint);
            this.drawSurface.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drawSurface_MouseDown);
            this.drawSurface.MouseEnter += new System.EventHandler(this.drawSurface_MouseEnter);
            this.drawSurface.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drawSurface_MouseMove);
            this.drawSurface.MouseUp += new System.Windows.Forms.MouseEventHandler(this.drawSurface_MouseUp);
            // 
            // btnNewSegment
            // 
            this.btnNewSegment.Location = new System.Drawing.Point(351, 17);
            this.btnNewSegment.Name = "btnNewSegment";
            this.btnNewSegment.Size = new System.Drawing.Size(43, 23);
            this.btnNewSegment.TabIndex = 3;
            this.btnNewSegment.Text = "New";
            this.btnNewSegment.UseVisualStyleBackColor = true;
            this.btnNewSegment.Click += new System.EventHandler(this.btnNewSegment_Click);
            // 
            // rtbDraw
            // 
            this.rtbDraw.Checked = true;
            this.rtbDraw.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbDraw.Location = new System.Drawing.Point(11, 39);
            this.rtbDraw.Name = "rtbDraw";
            this.rtbDraw.Size = new System.Drawing.Size(78, 28);
            this.rtbDraw.TabIndex = 1;
            this.rtbDraw.TabStop = true;
            this.rtbDraw.Text = "Draw";
            this.rtbDraw.UseVisualStyleBackColor = true;
            this.rtbDraw.CheckedChanged += new System.EventHandler(this.rtbDraw_CheckedChanged);
            // 
            // rtbMeasure
            // 
            this.rtbMeasure.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbMeasure.Location = new System.Drawing.Point(76, 39);
            this.rtbMeasure.Name = "rtbMeasure";
            this.rtbMeasure.Size = new System.Drawing.Size(97, 28);
            this.rtbMeasure.TabIndex = 2;
            this.rtbMeasure.Text = "Measure";
            this.rtbMeasure.UseVisualStyleBackColor = true;
            this.rtbMeasure.CheckedChanged += new System.EventHandler(this.rtbDraw_CheckedChanged);
            // 
            // cboHorizontalAlignment
            // 
            this.cboHorizontalAlignment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHorizontalAlignment.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboHorizontalAlignment.FormattingEnabled = true;
            this.cboHorizontalAlignment.Location = new System.Drawing.Point(179, 16);
            this.cboHorizontalAlignment.Name = "cboHorizontalAlignment";
            this.cboHorizontalAlignment.Size = new System.Drawing.Size(76, 23);
            this.cboHorizontalAlignment.TabIndex = 9;
            // 
            // tbZoom
            // 
            this.tbZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbZoom.AutoSize = false;
            this.tbZoom.BackColor = System.Drawing.SystemColors.Control;
            this.tbZoom.Location = new System.Drawing.Point(766, 0);
            this.tbZoom.Maximum = 50;
            this.tbZoom.Minimum = 1;
            this.tbZoom.Name = "tbZoom";
            this.tbZoom.Size = new System.Drawing.Size(83, 24);
            this.tbZoom.TabIndex = 12;
            this.tbZoom.TickFrequency = 5;
            this.tbZoom.Value = 15;
            this.tbZoom.Scroll += new System.EventHandler(this.tbZoom_Scroll);
            // 
            // stsStatus
            // 
            this.stsStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stsDocData,
            this.stsPosition,
            this.tssActiveStatus});
            this.stsStatus.Location = new System.Drawing.Point(0, 366);
            this.stsStatus.Name = "stsStatus";
            this.stsStatus.Size = new System.Drawing.Size(849, 25);
            this.stsStatus.TabIndex = 13;
            // 
            // stsDocData
            // 
            this.stsDocData.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsDocData.Name = "stsDocData";
            this.stsDocData.Size = new System.Drawing.Size(4, 20);
            // 
            // stsPosition
            // 
            this.stsPosition.AutoSize = false;
            this.stsPosition.Name = "stsPosition";
            this.stsPosition.Size = new System.Drawing.Size(375, 20);
            // 
            // tssActiveStatus
            // 
            this.tssActiveStatus.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tssActiveStatus.Name = "tssActiveStatus";
            this.tssActiveStatus.Size = new System.Drawing.Size(4, 20);
            this.tssActiveStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rtbDelete
            // 
            this.rtbDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rtbDelete.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbDelete.Location = new System.Drawing.Point(170, 39);
            this.rtbDelete.Name = "rtbDelete";
            this.rtbDelete.Size = new System.Drawing.Size(70, 28);
            this.rtbDelete.TabIndex = 3;
            this.rtbDelete.Text = "Delete";
            this.rtbDelete.UseVisualStyleBackColor = true;
            this.rtbDelete.CheckedChanged += new System.EventHandler(this.rtbDraw_CheckedChanged);
            // 
            // lblDrawColor
            // 
            this.lblDrawColor.BackColor = System.Drawing.Color.Black;
            this.lblDrawColor.Location = new System.Drawing.Point(50, 18);
            this.lblDrawColor.Name = "lblDrawColor";
            this.lblDrawColor.Size = new System.Drawing.Size(20, 20);
            this.lblDrawColor.TabIndex = 19;
            this.lblDrawColor.Click += new System.EventHandler(this.PickColorLabel_Click);
            // 
            // nupDrawWidth
            // 
            this.nupDrawWidth.Location = new System.Drawing.Point(108, 17);
            this.nupDrawWidth.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nupDrawWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupDrawWidth.Name = "nupDrawWidth";
            this.nupDrawWidth.Size = new System.Drawing.Size(33, 23);
            this.nupDrawWidth.TabIndex = 20;
            this.nupDrawWidth.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // mnuMainMenu
            // 
            this.mnuMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mnuMainMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMainMenu.Name = "mnuMainMenu";
            this.mnuMainMenu.Size = new System.Drawing.Size(849, 24);
            this.mnuMainMenu.TabIndex = 23;
            this.mnuMainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.newToolStripMenuItem.Text = "New Drawing";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.openToolStripMenuItem.Text = "Open Drawing";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.saveToolStripMenuItem.Text = "Save Drawing";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(190, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.jpegToolStripMenuItem,
            this.bmpToolStripMenuItem,
            this.pngToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // jpegToolStripMenuItem
            // 
            this.jpegToolStripMenuItem.Name = "jpegToolStripMenuItem";
            this.jpegToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.J)));
            this.jpegToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.jpegToolStripMenuItem.Text = "Jpeg";
            this.jpegToolStripMenuItem.Click += new System.EventHandler(this.jpegToolStripMenuItem_Click);
            // 
            // bmpToolStripMenuItem
            // 
            this.bmpToolStripMenuItem.Name = "bmpToolStripMenuItem";
            this.bmpToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.bmpToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.bmpToolStripMenuItem.Text = "Bmp";
            this.bmpToolStripMenuItem.Click += new System.EventHandler(this.bmpToolStripMenuItem_Click);
            // 
            // pngToolStripMenuItem
            // 
            this.pngToolStripMenuItem.Name = "pngToolStripMenuItem";
            this.pngToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.pngToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.pngToolStripMenuItem.Text = "Png";
            this.pngToolStripMenuItem.Click += new System.EventHandler(this.pngToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // grpDraw
            // 
            this.grpDraw.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDraw.Controls.Add(this.lblNupArcDescription);
            this.grpDraw.Controls.Add(this.lblNupArcUnits);
            this.grpDraw.Controls.Add(this.nupArcSize);
            this.grpDraw.Controls.Add(this.cboDrawElements);
            this.grpDraw.Controls.Add(this.label2);
            this.grpDraw.Controls.Add(this.label1);
            this.grpDraw.Controls.Add(this.btnNewSegment);
            this.grpDraw.Controls.Add(this.lblDrawColor);
            this.grpDraw.Controls.Add(this.nupDrawWidth);
            this.grpDraw.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDraw.Location = new System.Drawing.Point(437, 28);
            this.grpDraw.Name = "grpDraw";
            this.grpDraw.Size = new System.Drawing.Size(406, 49);
            this.grpDraw.TabIndex = 24;
            this.grpDraw.TabStop = false;
            // 
            // lblNupArcDescription
            // 
            this.lblNupArcDescription.AutoSize = true;
            this.lblNupArcDescription.Location = new System.Drawing.Point(236, 21);
            this.lblNupArcDescription.Name = "lblNupArcDescription";
            this.lblNupArcDescription.Size = new System.Drawing.Size(45, 15);
            this.lblNupArcDescription.TabIndex = 26;
            this.lblNupArcDescription.Text = "Radius";
            this.lblNupArcDescription.Visible = false;
            // 
            // lblNupArcUnits
            // 
            this.lblNupArcUnits.AutoSize = true;
            this.lblNupArcUnits.Location = new System.Drawing.Point(317, 21);
            this.lblNupArcUnits.Name = "lblNupArcUnits";
            this.lblNupArcUnits.Size = new System.Drawing.Size(18, 15);
            this.lblNupArcUnits.TabIndex = 25;
            this.lblNupArcUnits.Text = "in";
            this.lblNupArcUnits.Visible = false;
            // 
            // nupArcSize
            // 
            this.nupArcSize.Location = new System.Drawing.Point(281, 17);
            this.nupArcSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupArcSize.Name = "nupArcSize";
            this.nupArcSize.Size = new System.Drawing.Size(36, 23);
            this.nupArcSize.TabIndex = 24;
            this.nupArcSize.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nupArcSize.Visible = false;
            // 
            // cboDrawElements
            // 
            this.cboDrawElements.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDrawElements.FormattingEnabled = true;
            this.cboDrawElements.Location = new System.Drawing.Point(151, 17);
            this.cboDrawElements.Name = "cboDrawElements";
            this.cboDrawElements.Size = new System.Drawing.Size(77, 23);
            this.cboDrawElements.TabIndex = 23;
            this.cboDrawElements.SelectedValueChanged += new System.EventHandler(this.cboDrawElements_SelectedValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(75, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 15);
            this.label2.TabIndex = 22;
            this.label2.Text = "Size";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 21;
            this.label1.Text = "Color";
            // 
            // grpText
            // 
            this.grpText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpText.Controls.Add(this.label4);
            this.grpText.Controls.Add(this.lblFont);
            this.grpText.Controls.Add(this.label7);
            this.grpText.Controls.Add(this.txtTextToDraw);
            this.grpText.Controls.Add(this.label5);
            this.grpText.Controls.Add(this.lblTextColor);
            this.grpText.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpText.Location = new System.Drawing.Point(435, 28);
            this.grpText.Name = "grpText";
            this.grpText.Size = new System.Drawing.Size(406, 49);
            this.grpText.TabIndex = 27;
            this.grpText.TabStop = false;
            this.grpText.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(234, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 15);
            this.label4.TabIndex = 26;
            this.label4.Text = "Font";
            // 
            // lblFont
            // 
            this.lblFont.AutoSize = true;
            this.lblFont.Location = new System.Drawing.Point(271, 21);
            this.lblFont.Name = "lblFont";
            this.lblFont.Size = new System.Drawing.Size(120, 15);
            this.lblFont.TabIndex = 25;
            this.lblFont.Text = "Calibri, 9.75 Regular";
            this.lblFont.Click += new System.EventHandler(this.lblFont_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(87, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 15);
            this.label7.TabIndex = 24;
            this.label7.Text = "Text";
            // 
            // txtTextToDraw
            // 
            this.txtTextToDraw.Location = new System.Drawing.Point(121, 18);
            this.txtTextToDraw.Name = "txtTextToDraw";
            this.txtTextToDraw.Size = new System.Drawing.Size(100, 23);
            this.txtTextToDraw.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 15);
            this.label5.TabIndex = 21;
            this.label5.Text = "Color";
            // 
            // lblTextColor
            // 
            this.lblTextColor.BackColor = System.Drawing.Color.Black;
            this.lblTextColor.Location = new System.Drawing.Point(50, 18);
            this.lblTextColor.Name = "lblTextColor";
            this.lblTextColor.Size = new System.Drawing.Size(20, 20);
            this.lblTextColor.TabIndex = 19;
            this.lblTextColor.Click += new System.EventHandler(this.PickColorLabel_Click);
            // 
            // grpMeasure
            // 
            this.grpMeasure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpMeasure.Controls.Add(this.label3);
            this.grpMeasure.Controls.Add(this.lblMeasureColor);
            this.grpMeasure.Controls.Add(this.cboHorizontalAlignment);
            this.grpMeasure.Controls.Add(lblHorizontal);
            this.grpMeasure.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpMeasure.Location = new System.Drawing.Point(437, 28);
            this.grpMeasure.Name = "grpMeasure";
            this.grpMeasure.Size = new System.Drawing.Size(406, 49);
            this.grpMeasure.TabIndex = 25;
            this.grpMeasure.TabStop = false;
            this.grpMeasure.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 22;
            this.label3.Text = "Color";
            // 
            // lblMeasureColor
            // 
            this.lblMeasureColor.BackColor = System.Drawing.Color.Green;
            this.lblMeasureColor.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMeasureColor.Location = new System.Drawing.Point(50, 18);
            this.lblMeasureColor.Name = "lblMeasureColor";
            this.lblMeasureColor.Size = new System.Drawing.Size(20, 20);
            this.lblMeasureColor.TabIndex = 20;
            this.lblMeasureColor.Click += new System.EventHandler(this.PickColorLabel_Click);
            // 
            // rtbMove
            // 
            this.rtbMove.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rtbMove.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbMove.Location = new System.Drawing.Point(245, 39);
            this.rtbMove.Name = "rtbMove";
            this.rtbMove.Size = new System.Drawing.Size(68, 28);
            this.rtbMove.TabIndex = 4;
            this.rtbMove.Text = "Move";
            this.rtbMove.UseVisualStyleBackColor = true;
            this.rtbMove.CheckedChanged += new System.EventHandler(this.rtbDraw_CheckedChanged);
            // 
            // rtbText
            // 
            this.rtbText.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rtbText.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbText.Location = new System.Drawing.Point(314, 39);
            this.rtbText.Name = "rtbText";
            this.rtbText.Size = new System.Drawing.Size(51, 28);
            this.rtbText.TabIndex = 26;
            this.rtbText.Text = "Text";
            this.rtbText.UseVisualStyleBackColor = true;
            this.rtbText.CheckedChanged += new System.EventHandler(this.rtbDraw_CheckedChanged);
            // 
            // rtbImage
            // 
            this.rtbImage.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rtbImage.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbImage.Location = new System.Drawing.Point(365, 39);
            this.rtbImage.Name = "rtbImage";
            this.rtbImage.Size = new System.Drawing.Size(66, 28);
            this.rtbImage.TabIndex = 28;
            this.rtbImage.Text = "Image";
            this.rtbImage.UseVisualStyleBackColor = true;
            this.rtbImage.CheckedChanged += new System.EventHandler(this.rtbDraw_CheckedChanged);
            // 
            // grpImage
            // 
            this.grpImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpImage.Controls.Add(this.btnBrowse);
            this.grpImage.Controls.Add(this.txtImagePath);
            this.grpImage.Controls.Add(this.label6);
            this.grpImage.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpImage.Location = new System.Drawing.Point(437, 28);
            this.grpImage.Name = "grpImage";
            this.grpImage.Size = new System.Drawing.Size(406, 49);
            this.grpImage.TabIndex = 28;
            this.grpImage.TabStop = false;
            this.grpImage.Visible = false;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(337, 19);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(63, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtImagePath
            // 
            this.txtImagePath.Location = new System.Drawing.Point(55, 19);
            this.txtImagePath.Name = "txtImagePath";
            this.txtImagePath.ReadOnly = true;
            this.txtImagePath.Size = new System.Drawing.Size(276, 23);
            this.txtImagePath.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "Path";
            // 
            // DrawIt
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 391);
            this.Controls.Add(this.grpMeasure);
            this.Controls.Add(this.grpText);
            this.Controls.Add(this.grpImage);
            this.Controls.Add(this.rtbImage);
            this.Controls.Add(this.rtbText);
            this.Controls.Add(this.tbZoom);
            this.Controls.Add(lblZoom);
            this.Controls.Add(this.rtbDelete);
            this.Controls.Add(this.stsStatus);
            this.Controls.Add(this.mnuMainMenu);
            this.Controls.Add(this.rtbMeasure);
            this.Controls.Add(this.rtbDraw);
            this.Controls.Add(this.drawSurface);
            this.Controls.Add(this.rtbMove);
            this.Controls.Add(this.grpDraw);
            this.KeyPreview = true;
            this.MainMenuStrip = this.mnuMainMenu;
            this.Name = "DrawIt";
            this.Text = "DrawIt!";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DrawIt_FormClosing);
            this.Load += new System.EventHandler(this.DrawIt_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.DrawIt_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.DrawIt_DragEnter);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DrawIt_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.tbZoom)).EndInit();
            this.stsStatus.ResumeLayout(false);
            this.stsStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupDrawWidth)).EndInit();
            this.mnuMainMenu.ResumeLayout(false);
            this.mnuMainMenu.PerformLayout();
            this.grpDraw.ResumeLayout(false);
            this.grpDraw.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupArcSize)).EndInit();
            this.grpText.ResumeLayout(false);
            this.grpText.PerformLayout();
            this.grpMeasure.ResumeLayout(false);
            this.grpMeasure.PerformLayout();
            this.grpImage.ResumeLayout(false);
            this.grpImage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel drawSurface;
        private System.Windows.Forms.Button btnNewSegment;
        private System.Windows.Forms.RadioButton rtbDraw;
        private System.Windows.Forms.RadioButton rtbMeasure;
        private System.Windows.Forms.ComboBox cboHorizontalAlignment;
        private System.Windows.Forms.TrackBar tbZoom;
        private System.Windows.Forms.StatusStrip stsStatus;
        private System.Windows.Forms.ToolStripStatusLabel stsPosition;
        private System.Windows.Forms.RadioButton rtbDelete;
        private System.Windows.Forms.Label lblDrawColor;
        private System.Windows.Forms.NumericUpDown nupDrawWidth;
        private System.Windows.Forms.MenuStrip mnuMainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.GroupBox grpDraw;
        private System.Windows.Forms.GroupBox grpMeasure;
        private System.Windows.Forms.ToolStripStatusLabel stsDocData;
        private System.Windows.Forms.Label lblMeasureColor;
        private System.Windows.Forms.ToolStripStatusLabel tssActiveStatus;
        private System.Windows.Forms.RadioButton rtbMove;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jpegToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bmpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pngToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboDrawElements;
        private System.Windows.Forms.RadioButton rtbText;
        private System.Windows.Forms.GroupBox grpText;
        private System.Windows.Forms.TextBox txtTextToDraw;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTextColor;
        private System.Windows.Forms.Label lblFont;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nupArcSize;
        private System.Windows.Forms.Label lblNupArcUnits;
        private System.Windows.Forms.Label lblNupArcDescription;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.RadioButton rtbImage;
        private System.Windows.Forms.GroupBox grpImage;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtImagePath;
        private System.Windows.Forms.Label label6;
    }
}

