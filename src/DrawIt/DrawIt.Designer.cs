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
            System.Windows.Forms.Label lblVertical;
            System.Windows.Forms.Label lblZoom;
            this.drawSurface = new System.Windows.Forms.Panel();
            this.btnNewSegment = new System.Windows.Forms.Button();
            this.rtbDraw = new System.Windows.Forms.RadioButton();
            this.rtbMeasure = new System.Windows.Forms.RadioButton();
            this.cboVerticalAlignment = new System.Windows.Forms.ComboBox();
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
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grpDraw = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grpMeasure = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblMeasureColor = new System.Windows.Forms.Label();
            this.rbMove = new System.Windows.Forms.RadioButton();
            lblHorizontal = new System.Windows.Forms.Label();
            lblVertical = new System.Windows.Forms.Label();
            lblZoom = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tbZoom)).BeginInit();
            this.stsStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupDrawWidth)).BeginInit();
            this.mnuMainMenu.SuspendLayout();
            this.grpDraw.SuspendLayout();
            this.grpMeasure.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHorizontal
            // 
            lblHorizontal.AutoSize = true;
            lblHorizontal.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblHorizontal.Location = new System.Drawing.Point(87, 21);
            lblHorizontal.Name = "lblHorizontal";
            lblHorizontal.Size = new System.Drawing.Size(65, 15);
            lblHorizontal.TabIndex = 10;
            lblHorizontal.Text = "Horizontal";
            // 
            // lblVertical
            // 
            lblVertical.AutoSize = true;
            lblVertical.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblVertical.Location = new System.Drawing.Point(227, 21);
            lblVertical.Name = "lblVertical";
            lblVertical.Size = new System.Drawing.Size(48, 15);
            lblVertical.TabIndex = 11;
            lblVertical.Text = "Vertical";
            // 
            // lblZoom
            // 
            lblZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            lblZoom.AutoSize = true;
            lblZoom.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            lblZoom.Location = new System.Drawing.Point(630, 4);
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
            this.drawSurface.Size = new System.Drawing.Size(750, 287);
            this.drawSurface.TabIndex = 2;
            this.drawSurface.Paint += new System.Windows.Forms.PaintEventHandler(this.drawSurface_Paint);
            this.drawSurface.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drawSurface_MouseDown);
            this.drawSurface.MouseEnter += new System.EventHandler(this.drawSurface_MouseEnter);
            this.drawSurface.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drawSurface_MouseMove);
            this.drawSurface.MouseUp += new System.Windows.Forms.MouseEventHandler(this.drawSurface_MouseUp);
            // 
            // btnNewSegment
            // 
            this.btnNewSegment.Location = new System.Drawing.Point(207, 17);
            this.btnNewSegment.Name = "btnNewSegment";
            this.btnNewSegment.Size = new System.Drawing.Size(73, 23);
            this.btnNewSegment.TabIndex = 3;
            this.btnNewSegment.Text = "New Line";
            this.btnNewSegment.UseVisualStyleBackColor = true;
            this.btnNewSegment.Click += new System.EventHandler(this.btnNewSegment_Click);
            // 
            // rtbDraw
            // 
            this.rtbDraw.AutoSize = true;
            this.rtbDraw.Checked = true;
            this.rtbDraw.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbDraw.Location = new System.Drawing.Point(12, 35);
            this.rtbDraw.Name = "rtbDraw";
            this.rtbDraw.Size = new System.Drawing.Size(70, 27);
            this.rtbDraw.TabIndex = 1;
            this.rtbDraw.TabStop = true;
            this.rtbDraw.Text = "Draw";
            this.rtbDraw.UseVisualStyleBackColor = true;
            this.rtbDraw.CheckedChanged += new System.EventHandler(this.rtbDraw_CheckedChanged);
            // 
            // rtbMeasure
            // 
            this.rtbMeasure.AutoSize = true;
            this.rtbMeasure.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbMeasure.Location = new System.Drawing.Point(82, 35);
            this.rtbMeasure.Name = "rtbMeasure";
            this.rtbMeasure.Size = new System.Drawing.Size(96, 27);
            this.rtbMeasure.TabIndex = 2;
            this.rtbMeasure.Text = "Measure";
            this.rtbMeasure.UseVisualStyleBackColor = true;
            this.rtbMeasure.CheckedChanged += new System.EventHandler(this.rtbDraw_CheckedChanged);
            // 
            // cboVerticalAlignment
            // 
            this.cboVerticalAlignment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVerticalAlignment.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboVerticalAlignment.FormattingEnabled = true;
            this.cboVerticalAlignment.Location = new System.Drawing.Point(274, 17);
            this.cboVerticalAlignment.Name = "cboVerticalAlignment";
            this.cboVerticalAlignment.Size = new System.Drawing.Size(60, 23);
            this.cboVerticalAlignment.TabIndex = 8;
            // 
            // cboHorizontalAlignment
            // 
            this.cboHorizontalAlignment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHorizontalAlignment.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboHorizontalAlignment.FormattingEnabled = true;
            this.cboHorizontalAlignment.Location = new System.Drawing.Point(154, 17);
            this.cboHorizontalAlignment.Name = "cboHorizontalAlignment";
            this.cboHorizontalAlignment.Size = new System.Drawing.Size(60, 23);
            this.cboHorizontalAlignment.TabIndex = 9;
            // 
            // tbZoom
            // 
            this.tbZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbZoom.AutoSize = false;
            this.tbZoom.BackColor = System.Drawing.SystemColors.Control;
            this.tbZoom.LargeChange = 10;
            this.tbZoom.Location = new System.Drawing.Point(667, 0);
            this.tbZoom.Maximum = 50;
            this.tbZoom.Minimum = 5;
            this.tbZoom.Name = "tbZoom";
            this.tbZoom.Size = new System.Drawing.Size(83, 24);
            this.tbZoom.SmallChange = 5;
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
            this.stsStatus.Size = new System.Drawing.Size(750, 25);
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
            this.stsPosition.Size = new System.Drawing.Size(275, 20);
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
            this.rtbDelete.AutoSize = true;
            this.rtbDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rtbDelete.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbDelete.Location = new System.Drawing.Point(178, 35);
            this.rtbDelete.Name = "rtbDelete";
            this.rtbDelete.Size = new System.Drawing.Size(83, 28);
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
            this.nupDrawWidth.Location = new System.Drawing.Point(113, 17);
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
            this.helpToolStripMenuItem});
            this.mnuMainMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMainMenu.Name = "mnuMainMenu";
            this.mnuMainMenu.Size = new System.Drawing.Size(750, 24);
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
            this.grpDraw.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDraw.Controls.Add(this.label2);
            this.grpDraw.Controls.Add(this.label1);
            this.grpDraw.Controls.Add(this.btnNewSegment);
            this.grpDraw.Controls.Add(this.lblDrawColor);
            this.grpDraw.Controls.Add(this.nupDrawWidth);
            this.grpDraw.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDraw.Location = new System.Drawing.Point(353, 24);
            this.grpDraw.Name = "grpDraw";
            this.grpDraw.Size = new System.Drawing.Size(391, 49);
            this.grpDraw.TabIndex = 24;
            this.grpDraw.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(80, 21);
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
            // grpMeasure
            // 
            this.grpMeasure.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpMeasure.Controls.Add(this.label3);
            this.grpMeasure.Controls.Add(this.lblMeasureColor);
            this.grpMeasure.Controls.Add(this.cboHorizontalAlignment);
            this.grpMeasure.Controls.Add(this.cboVerticalAlignment);
            this.grpMeasure.Controls.Add(lblHorizontal);
            this.grpMeasure.Controls.Add(lblVertical);
            this.grpMeasure.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.grpMeasure.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpMeasure.Location = new System.Drawing.Point(353, 24);
            this.grpMeasure.Name = "grpMeasure";
            this.grpMeasure.Size = new System.Drawing.Size(391, 49);
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
            // rbMove
            // 
            this.rbMove.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbMove.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbMove.Location = new System.Drawing.Point(261, 35);
            this.rbMove.Name = "rbMove";
            this.rbMove.Size = new System.Drawing.Size(78, 28);
            this.rbMove.TabIndex = 4;
            this.rbMove.Text = "Move";
            this.rbMove.UseVisualStyleBackColor = true;
            this.rbMove.CheckedChanged += new System.EventHandler(this.rtbDraw_CheckedChanged);
            // 
            // DrawIt
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 391);
            this.Controls.Add(this.tbZoom);
            this.Controls.Add(lblZoom);
            this.Controls.Add(this.rtbDelete);
            this.Controls.Add(this.stsStatus);
            this.Controls.Add(this.mnuMainMenu);
            this.Controls.Add(this.rtbMeasure);
            this.Controls.Add(this.rtbDraw);
            this.Controls.Add(this.drawSurface);
            this.Controls.Add(this.rbMove);
            this.Controls.Add(this.grpMeasure);
            this.Controls.Add(this.grpDraw);
            this.KeyPreview = true;
            this.MainMenuStrip = this.mnuMainMenu;
            this.Name = "DrawIt";
            this.Text = "DrawIt!";
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
            this.grpMeasure.ResumeLayout(false);
            this.grpMeasure.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel drawSurface;
        private System.Windows.Forms.Button btnNewSegment;
        private System.Windows.Forms.RadioButton rtbDraw;
        private System.Windows.Forms.RadioButton rtbMeasure;
        private System.Windows.Forms.ComboBox cboVerticalAlignment;
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
        private System.Windows.Forms.RadioButton rbMove;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jpegToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bmpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pngToolStripMenuItem;
        private System.Windows.Forms.Label label3;
    }
}

