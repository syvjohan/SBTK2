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
            this.panelTextureCollector = new System.Windows.Forms.Panel();
            this.panelCutTexture = new System.Windows.Forms.Panel();
            this.btnAddImage = new System.Windows.Forms.Button();
            this.listViewAddedTextures = new System.Windows.Forms.ListView();
            this.loadedImages = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // panelTextureCollector
            // 
            this.panelTextureCollector.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelTextureCollector.BackColor = System.Drawing.Color.Teal;
            this.panelTextureCollector.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelTextureCollector.Location = new System.Drawing.Point(542, 70);
            this.panelTextureCollector.Name = "panelTextureCollector";
            this.panelTextureCollector.Size = new System.Drawing.Size(413, 391);
            this.panelTextureCollector.TabIndex = 0;
            // 
            // panelCutTexture
            // 
            this.panelCutTexture.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelCutTexture.BackColor = System.Drawing.Color.Red;
            this.panelCutTexture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelCutTexture.ForeColor = System.Drawing.Color.Black;
            this.panelCutTexture.Location = new System.Drawing.Point(237, 70);
            this.panelCutTexture.Name = "panelCutTexture";
            this.panelCutTexture.Size = new System.Drawing.Size(299, 391);
            this.panelCutTexture.TabIndex = 1;
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
            // 
            // loadedImages
            // 
            this.loadedImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.loadedImages.ImageSize = new System.Drawing.Size(32, 32);
            this.loadedImages.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // FormTextureAtlas
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(993, 556);
            this.Controls.Add(this.btnAddImage);
            this.Controls.Add(this.listViewAddedTextures);
            this.Controls.Add(this.panelCutTexture);
            this.Controls.Add(this.panelTextureCollector);
            this.Name = "FormTextureAtlas";
            this.Text = "CreateTextureAtlas";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTextureCollector;
        private System.Windows.Forms.Panel panelCutTexture;
        private System.Windows.Forms.Button btnAddImage;
        private System.Windows.Forms.ListView listViewAddedTextures;
        private System.Windows.Forms.ImageList loadedImages;
    }
}

