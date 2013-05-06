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
            this.panelCutTexture = new System.Windows.Forms.Panel();
            this.btnAddImage = new System.Windows.Forms.Button();
            this.listViewAddedTextures = new System.Windows.Forms.ListView();
            this.loadedImages = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelTextureCollector = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panelCutTexture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCutTexture
            // 
            this.panelCutTexture.AutoScroll = true;
            this.panelCutTexture.AutoScrollMinSize = new System.Drawing.Size(1024, 1024);
            this.panelCutTexture.AutoSize = true;
            this.panelCutTexture.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelCutTexture.BackColor = System.Drawing.Color.Red;
            this.panelCutTexture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelCutTexture.Controls.Add(this.flowLayoutPanel1);
            this.panelCutTexture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCutTexture.ForeColor = System.Drawing.Color.Black;
            this.panelCutTexture.Location = new System.Drawing.Point(0, 0);
            this.panelCutTexture.Name = "panelCutTexture";
            this.panelCutTexture.Size = new System.Drawing.Size(199, 391);
            this.panelCutTexture.TabIndex = 1;
            this.panelCutTexture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelCutTexture_MouseDown);
            this.panelCutTexture.MouseEnter += new System.EventHandler(this.panelCutTexture_MouseEnter);
            this.panelCutTexture.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelCutTexture_MouseMove);
            this.panelCutTexture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelCutTexture_MouseUp);
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
            this.listViewAddedTextures.Size = new System.Drawing.Size(187, 391);
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
            this.splitContainer1.Size = new System.Drawing.Size(702, 391);
            this.splitContainer1.SplitterDistance = 199;
            this.splitContainer1.TabIndex = 3;
            // 
            // panelTextureCollector
            // 
            this.panelTextureCollector.AutoScroll = true;
            this.panelTextureCollector.AutoScrollMinSize = new System.Drawing.Size(1024, 1024);
            this.panelTextureCollector.AutoSize = true;
            this.panelTextureCollector.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelTextureCollector.BackColor = System.Drawing.Color.Teal;
            this.panelTextureCollector.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelTextureCollector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTextureCollector.Location = new System.Drawing.Point(0, 0);
            this.panelTextureCollector.Name = "panelTextureCollector";
            this.panelTextureCollector.Size = new System.Drawing.Size(499, 391);
            this.panelTextureCollector.TabIndex = 0;
            this.panelTextureCollector.Click += new System.EventHandler(this.panelTextureCollector_Click);
            this.panelTextureCollector.MouseEnter += new System.EventHandler(this.panelTextureCollector_MouseEnter);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(1, -2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(0, 0);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // FormTextureAtlas
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(993, 556);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnAddImage);
            this.Controls.Add(this.listViewAddedTextures);
            this.Name = "FormTextureAtlas";
            this.Text = "CreateTextureAtlas";
            this.panelCutTexture.ResumeLayout(false);
            this.panelCutTexture.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelCutTexture;
        private System.Windows.Forms.Button btnAddImage;
        private System.Windows.Forms.ListView listViewAddedTextures;
        private System.Windows.Forms.ImageList loadedImages;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panelTextureCollector;
    }
}

