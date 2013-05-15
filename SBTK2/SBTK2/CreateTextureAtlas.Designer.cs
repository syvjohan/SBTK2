namespace SBTK2
{
    partial class FormTextureAtlas
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
            this.components = new System.ComponentModel.Container();
            this.btnAddImage = new System.Windows.Forms.Button();
            this.listViewAddedTextures = new System.Windows.Forms.ListView();
            this.loadedImages = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelTextureCollector = new SBTK2.CustomPanel();
            this.cmb = new System.Windows.Forms.ComboBox();
            this.panelCutTexture = new SBTK2.CustomPanel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddImage
            // 
            this.btnAddImage.Location = new System.Drawing.Point(21, 12);
            this.btnAddImage.Name = "btnAddImage";
            this.btnAddImage.Size = new System.Drawing.Size(105, 43);
            this.btnAddImage.TabIndex = 2;
            this.btnAddImage.Text = "Add Texture";
            this.btnAddImage.UseVisualStyleBackColor = true;
            this.btnAddImage.Click += new System.EventHandler(this.btnAddImage_Click);
            // 
            // listViewAddedTextures
            // 
            this.listViewAddedTextures.AllowDrop = true;
            this.listViewAddedTextures.LargeImageList = this.loadedImages;
            this.listViewAddedTextures.Location = new System.Drawing.Point(21, 70);
            this.listViewAddedTextures.Name = "listViewAddedTextures";
            this.listViewAddedTextures.Size = new System.Drawing.Size(187, 526);
            this.listViewAddedTextures.TabIndex = 0;
            this.listViewAddedTextures.UseCompatibleStateImageBehavior = false;
            this.listViewAddedTextures.SelectedIndexChanged += new System.EventHandler(this.listViewAddedTextures_SelectedIndexChanged);
            this.listViewAddedTextures.DragDrop += new System.Windows.Forms.DragEventHandler(this.listViewAddedTexture_DragDrop);
            this.listViewAddedTextures.DragEnter += new System.Windows.Forms.DragEventHandler(this.listViewAddedTexture_DragEnter);
            this.listViewAddedTextures.DragLeave += new System.EventHandler(this.listViewAddedTexture_DragLeave);
            // 
            // loadedImages
            // 
            this.loadedImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.loadedImages.ImageSize = new System.Drawing.Size(32, 32);
            this.loadedImages.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(230, 70);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panelCutTexture);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panelTextureCollector);
            this.splitContainer1.Size = new System.Drawing.Size(911, 526);
            this.splitContainer1.SplitterDistance = 258;
            this.splitContainer1.TabIndex = 3;
            // 
            // panelTextureCollector
            // 
            this.panelTextureCollector.AutoScroll = true;
            this.panelTextureCollector.AutoScrollMinSize = new System.Drawing.Size(1024, 1024);
            this.panelTextureCollector.AutoSize = true;
            this.panelTextureCollector.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelTextureCollector.BackColor = System.Drawing.Color.White;
            this.panelTextureCollector.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelTextureCollector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTextureCollector.Location = new System.Drawing.Point(0, 0);
            this.panelTextureCollector.Name = "panelTextureCollector";
            this.panelTextureCollector.Size = new System.Drawing.Size(649, 526);
            this.panelTextureCollector.TabIndex = 0;
            this.panelTextureCollector.Click += new System.EventHandler(this.panelTextureCollector_Click);
            this.panelTextureCollector.MouseEnter += new System.EventHandler(this.panelTextureCollector_MouseEnter);
            // 
            // cmb
            // 
            this.cmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb.FormattingEnabled = true;
            this.cmb.Location = new System.Drawing.Point(230, 24);
            this.cmb.Name = "cmb";
            this.cmb.Size = new System.Drawing.Size(258, 23);
            this.cmb.TabIndex = 4;
            this.cmb.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);
            // 
            // panelCutTexture
            // 
            this.panelCutTexture.AutoScroll = true;
            this.panelCutTexture.AutoScrollMinSize = new System.Drawing.Size(1024, 1024);
            this.panelCutTexture.AutoSize = true;
            this.panelCutTexture.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelCutTexture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelCutTexture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCutTexture.Location = new System.Drawing.Point(0, 0);
            this.panelCutTexture.Name = "panelCutTexture";
            this.panelCutTexture.Size = new System.Drawing.Size(258, 526);
            this.panelCutTexture.TabIndex = 0;
            this.panelCutTexture.Paint += new System.Windows.Forms.PaintEventHandler(this.panelCutTexture_Paint);
            this.panelCutTexture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelCutTexture_MouseDown);
            this.panelCutTexture.MouseEnter += new System.EventHandler(this.panelCutTexture_MouseEnter);
            this.panelCutTexture.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelCutTexture_MouseMove);
            this.panelCutTexture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelCutTexture_MouseUp);
            // 
            // FormTextureAtlas
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1180, 618);
            this.Controls.Add(this.cmb);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnAddImage);
            this.Controls.Add(this.listViewAddedTextures);
            this.Name = "FormTextureAtlas";
            this.Text = "CreateTextureAtlas";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAddImage;
        private System.Windows.Forms.ListView listViewAddedTextures;
        private System.Windows.Forms.ImageList loadedImages;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panelTextureCollector;
        private System.Windows.Forms.ComboBox cmb;
        private CustomPanel panelCutTexture;
    }
}

