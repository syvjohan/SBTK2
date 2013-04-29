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

        public FormTextureAtlas()
        {
            InitializeComponent();
        }

        private void btnAddImage_Click(object sender, EventArgs e)
        {
             textureListManager.OpenImage();
             LoadImages();
            
        }

        public void LoadImages()
        {
            //Clears the list of old data
            loadedImages.Images.Clear();
            listViewAddedTextures.Items.Clear();

            for (int m = 0; m < textureListManager.NrOfImages; m++)
            {
                //Adds the image from the textureListManager at the specific index to the imageList
                loadedImages.Images.Add(textureListManager.getImageAtIndex(m));

                //Creates a temporary ListViewItem to hold the image index
                ListViewItem item = new ListViewItem();

                //Sets the image index
                item.ImageIndex = m;

                //Adds the image at the image index to the listview
                listViewAddedTextures.Items.Add(item);
            }
            //Updates
            listViewAddedTextures.Update();
        }
    }
}
