using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SBTK2
{
    public partial class FormTextureAtlas : Form
    {
        TextureListManager textureListManager = new TextureListManager();

        

        // For the rectangle 
        private Rectangle cuttingRectangle;
        private int recPositionX;
        private int recPositionY;
        private bool mouseDrawRec = false;
        private bool draggingRec = false;
        private Graphics Panelgraphics { get; set; }
        private Bitmap selectedImage;
        private Bitmap cutImage;

        // ReDraw the rectangle
        private Bitmap canvas;
        private int panelSizeX = 1024;
        private int panelSizeY = 1024;
        Point rectPos = new Point(0, 0);

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
        /// Loads the images from textureListManager's List, and puts them into the listview.
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

        /// <summary>
        /// shows a image in the panelCutTexture when the user select a image in the listViewAddedTexture.
        /// </summary>
        /// <param name="sender"></param>
        private void listViewAddedTextures_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedImageIndex = listViewAddedTextures.SelectedItems[0].ImageIndex;
            DrawSelectedTexture(selectedImageIndex);
        }

        /// <summary>
        /// Draw one image in a panel.
        /// </summary>
        /// <param name="index"></param>
        private void DrawSelectedTexture(int index)
        {
            using (Graphics graphics = panelCutTexture.CreateGraphics())
            {

                panelCutTexture.BackgroundImage = textureListManager.GetImageAtIndex(index);
                selectedImage = (Bitmap)panelCutTexture.BackgroundImage;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelCutTexture_MouseDown(object sender, MouseEventArgs e)
        {
            if (cuttingRectangle != null && panelCutTexture.BackgroundImage != null)
            {
                if (e.X >= recPositionX && e.X <= (recPositionX + cuttingRectangle.Width) &&
                    e.Y >= recPositionY && e.Y <= (recPositionY + cuttingRectangle.Height))
                {
                    draggingRec = true;
                    DoDragDrop(cutImage, DragDropEffects.Copy);
                }
                else
                {
                    mouseDrawRec = true;
                    recPositionX = e.X;
                    recPositionY = e.Y;
                    Update();
                }
            }
        }

        /// <summary>
        /// Drawing a rectangle in a panel and get called by the the MouseMove event.
        /// </summary>
        /// <param name="e"></param>
        private void DrawRectangle(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && mouseDrawRec == true)
            {
                Image image = null;
                image = panelCutTexture.BackgroundImage;

                if(image != null)
                {
                    this.Refresh();

                    Pen pen = new Pen(Color.Black, 2);
                    int width = e.X - recPositionX;
                    int height = e.Y - recPositionY;

                    if( width <= 1)
                    {
                        width = 1;
                    }

                    if (height <= 1)
                    {
                        height = 1;
                    }

                    cuttingRectangle = new Rectangle(recPositionX,
                        recPositionY,
                        width,
                        height);

                    Panelgraphics = panelCutTexture.CreateGraphics();
                    Panelgraphics.DrawRectangle(pen, cuttingRectangle);
                }
            }
        }

        private void panelCutTexture_MouseMove(object sender, MouseEventArgs e)
        {
            DrawRectangle(e);
        }

        /// <summary>
        ///  Clone the cuttingRectangle area and save it in the Bitmap cutImage.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelCutTexture_MouseUp(object sender, MouseEventArgs e)
        {
            if (cuttingRectangle != null && selectedImage != null)
            {
                cutImage = null;
                cutImage = selectedImage.Clone(cuttingRectangle, PixelFormat.Format32bppArgb);
            }
        }

        private void panelTextureCollector_Click(object sender, EventArgs e)
        {
            ReDrawRectangle();

            if (cutImage != null)
            {
                rectPos = Cursor.Position;
                rectPos = panelTextureCollector.PointToClient(rectPos);
                rectPos.X -= cutImage.Width / 2;
                rectPos.Y -= cutImage.Height / 2;

                Graphics graphics = Graphics.FromImage(canvas);
                graphics.DrawImage(cutImage, rectPos);
                panelTextureCollector.BackgroundImage = canvas;
                panelTextureCollector.Refresh();
            }
        }

        private void ReDrawRectangle()
        {
            canvas = new Bitmap(panelSizeX, panelSizeY);
            panelTextureCollector.BackgroundImage = canvas;

            Graphics graphicsCanvas = Graphics.FromImage((Image)canvas);

            Brush brush = new SolidBrush(Color.Transparent);
            graphicsCanvas.FillRectangle(brush, new Rectangle(0, 0, panelSizeX, panelSizeY));

            graphicsCanvas = panelTextureCollector.CreateGraphics();
            graphicsCanvas.DrawImage(canvas, new Point(0, 0));

        }

        private void listViewAddedTexture_DragEnter(object sender, DragEventArgs e)
        {
            textureListManager.DragEnterGeneric(sender, e);
        }

        private void listViewAddedTexture_DragLeave(object sender, DragEventArgs e)
        {
            textureListManager.DragDropLeave(sender, e);
        }

        private void listViewAddedTexture_DragDrop(object sender, DragEventArgs e)
        {
            textureListManager.DragDropFromDesktop(sender, e);
            LoadImages();
        }

        private void listViewAddedTexture_DragLeave(object sender, EventArgs e)
        {
            textureListManager.DragDropLeave(sender, e);
        }
    }
}
