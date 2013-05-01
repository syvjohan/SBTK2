using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SBTK2
{
    public partial class FormTextureAtlas : Form
    {
        TextureListManager textureListManager = new TextureListManager();

        private Bitmap selectedImage;

        public FormTextureAtlas()
        {
            InitializeComponent();
        }

        private void btnAddImage_Click(object sender, EventArgs e)
        {
             textureListManager.OpenImage();
             LoadImages();
            
        }

        /// <summary>
        /// Loads the images from textureListManager's List, and imputs them into the listview.
        /// </summary>
        public void LoadImages()
        {
            //Clears the list of old data
            listViewAddedTextures.Items.Clear();
            loadedImages.Images.Clear();
            

            for (int i = 0; i < textureListManager.NrOfImages; i++)
            {
                //Adds the image from the textureListManager at the specific index to the imageList
                loadedImages.Images.Add(textureListManager.GetImageAtIndex(i));

                //Creates a temporary ListViewItem to hold the image index
                ListViewItem item = new ListViewItem();

                //Sets the image index
                item.ImageIndex = i;

                //Adds the image at the image index to the listview
                listViewAddedTextures.Items.Add(item);
            }
            //Updates
            listViewAddedTextures.Update();
        }

        // shows a image in the panelCutTexture when the user select a image in the listViewAddedTexture.
        private void listViewAddedTextures_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedImageIndex = listViewAddedTextures.SelectedItems[0].ImageIndex;
            DrawSelectedTexture(selectedImageIndex);
        }

        // Draw  image in a panel.
        private void DrawSelectedTexture(int index)
        {
            using (Graphics graphics = panelCutTexture.CreateGraphics())
            {

                panelCutTexture.BackgroundImage = textureListManager.GetImageAtIndex(index);
                selectedImage = (Bitmap)panelCutTexture.BackgroundImage;

            }
        }
    }
}
