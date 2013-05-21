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
        // Classes
        TextureListManager textureListManager = new TextureListManager();
        TextureRect textureRect;

        // For the rectangle 
        private Rectangle cuttingRectangle;

        private bool mouseDrawRec = false;// Flagg for controlling the dragging in the panels.

        private Graphics Panelgraphics { get; set; }

        private Bitmap selectedImage;
        private Bitmap drawImage;

        Pen pen = new Pen(Color.Black, 2); // used for drawing rectangle.
        Brush brush = new SolidBrush(Color.Transparent); // used for filling rectangle.

        // ReDraw the rectangle
        private Bitmap canvas;
        private int panelSizeX = 1024;
        private int panelSizeY = 1024;

        // Predefined rectangle
        Rectangle predefinedRectangle;

        // Rectangles for the panelTextureCollector
        List<TextureRect> textureClips = new List<TextureRect>();

        // Mouse Move
        private int recPositionX;
        private int recPositionY;
        Point mousepoint;

        public FormTextureAtlas()
        {
            InitializeComponent();
            InitializeLocalComponents();
            FillCmb();
        }

        private void InitializeLocalComponents()
        {
            // Redraw the rectangle
            canvas = new Bitmap(panelSizeX, panelSizeY);
            panelCutTexture.BackgroundImage = canvas;

            panelTextureCollector.Click += new EventHandler(panelTextureCollector_Click);

            // Use DoubbleBuffering to reduce the flickering.
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
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
            if (listViewAddedTextures.SelectedItems.Count > 0)
            {
                int selectedImageIndex = listViewAddedTextures.SelectedItems[0].ImageIndex;
                selectedImage = (Bitmap)textureListManager.GetImageAtIndex(selectedImageIndex);

                drawImage = new Bitmap(panelSizeX, panelSizeY);

                Panelgraphics = Graphics.FromImage(drawImage);
                Panelgraphics.FillRectangle(brush, new Rectangle(0, 0, drawImage.Width, drawImage.Height));

                Panelgraphics.DrawImage(textureListManager.GetImageAtIndex(selectedImageIndex), new Point(0, 0));
                panelCutTexture.Refresh();
            }
        }

        private void panelCutTexture_Paint(object sender, PaintEventArgs e)
        {
            Panelgraphics = panelCutTexture.CreateGraphics();

            if (drawImage == null)
            {
                panelCutTexture.Clear();
            }
            else
            {
                Panelgraphics.DrawImage(drawImage, new Point(0, 0));
            }

            // Graphics for drawing the dragging predefinedRectangle.
            Panelgraphics.DrawRectangle(pen, recPositionX, recPositionY, predefinedRectangle.Width, predefinedRectangle.Height);
        }

        private void panelCutTexture_MouseDown(object sender, MouseEventArgs e)
        {
            bool draggingRec = false;// Flagg for controlling the dragging in the panels.
            mousepoint = new Point(e.X, e.Y);

            if (ValidatePanel())
            {
                if(RectsIntersect(mousepoint , cuttingRectangle)) 
                {
                    draggingRec = true;
                    DoDragDrop(drawImage, DragDropEffects.Copy);
                }
                else
                {
                    mouseDrawRec = true;
                    recPositionX = e.X;
                    recPositionY = e.Y;
                    Update();
                }
            }
            else
            {
                mouseDrawRec = false;
            }
        }

        /// <summary>
        /// if mouse move is true and cutImage is not false draw a rectangle.
        /// Drawing a rectangle in a panel and get called by the the MouseMove event.
        /// </summary>
        /// <param name="e"></param>
        private void DrawingRectangle(MouseEventArgs e)
        {
            if (drawImage != null)
            {
                this.Refresh();

                int width = e.X + recPositionX;
                int height = e.Y - recPositionY;

                cuttingRectangle = new Rectangle(e.X,
                    e.Y,
                    width,
                    height);

                Panelgraphics = panelCutTexture.CreateGraphics();
                Panelgraphics.DrawRectangle(pen, cuttingRectangle);
            }
        }

        private void panelCutTexture_MouseMove(object sender, MouseEventArgs e)
        {
            mousepoint = new Point(e.X, e.Y);

            if (e.Button != MouseButtons.Left) return;

            if (RectsIntersect(mousepoint, predefinedRectangle) && drawImage != null &&  CheckRecMove(e))
            {
                MouseMove(e);
               panelCutTexture.Invalidate();
            }
            else
            {
                Panelgraphics.Dispose();
                DrawingRectangle(e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelCutTexture_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDrawRec = false;
            if (cuttingRectangle != null && selectedImage != null)
            {
                DrawTexture();
            }
        }

        /// <summary>
        ///  Draw the texture inside cuttingRectangle in the panelTextureCollector.
        /// </summary>
        private void DrawTexture()
        {
           textureRect = new TextureRect(selectedImage, cuttingRectangle, new Point(recPositionX, recPositionY));
            textureClips.Add(textureRect);
            panelTextureCollector.Refresh();
        }

        /// <summary>
        ///  Enter and call the DragEnter method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewAddedTexture_DragEnter(object sender, DragEventArgs e)
        {
            textureListManager.DragEnterGeneric(sender, e);
        }

        /// <summary>
        ///  Call DragDrop and load the image into the listView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewAddedTexture_DragDrop(object sender, DragEventArgs e)
        {
            textureListManager.DragDropFromDesktop(sender, e);
            LoadImages();
        }

        /// <summary>
        ///  Leave the DragDrop event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewAddedTexture_DragLeave(object sender, EventArgs e)
        {
            textureListManager.DragDropLeave(sender, e);
        }

        /// <summary>
        /// Drawing the cutted rectangle from panelCutTexture into the panelTextureCollector.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelTextureCollector_Click(object sender, EventArgs e)
        {
            if (drawImage != null || predefinedRectangle != null)
            {
                Panelgraphics = panelTextureCollector.CreateGraphics();
                foreach (TextureRect tr in textureClips)
                {
                    Panelgraphics.DrawImage(
                      tr.SourceImage,
                      tr.Clip,
                      new Rectangle(
                        tr.Position,
                        new Size(tr.Clip.Width, tr.Clip.Height)
                      ),
                      GraphicsUnit.Pixel
                    );
                }
            }
        }

        /// <summary>
        /// Enables scrolling (Y-axel) with the mouse wheel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelCutTexture_MouseEnter(object sender, EventArgs e)
        {
            panelCutTexture.Focus();
        }

        /// <summary>
        /// Enables scrolling (Y-axel) with the mouse wheel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelTextureCollector_MouseEnter(object sender, EventArgs e)
        {
            panelTextureCollector.Focus();
        }

        /// <summary>
        /// Text in the combobox and the rectangles (cmb).
        /// </summary>
        private void FillCmb()
        {
            cmb.Items.Add(CustomRectangleRule.rTitle);
            cmb.Items.Add(CustomRectangleRule.r16);
            cmb.Items.Add(CustomRectangleRule.r32);
            cmb.Items.Add(CustomRectangleRule.r64);
            cmb.Items.Add(CustomRectangleRule.r128);
            cmb.Items.Add(CustomRectangleRule.r256);
            cmb.SelectedIndex = 0; // sets the first item in the list as start choice (Title cmb).
        }

        /// <summary>
        /// Predefined Rectangle in the panelCutTexture.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            predefinedRectangle = ((CustomRectangleRule)cmb.SelectedItem).rec;

            Panelgraphics = panelCutTexture.CreateGraphics();
            Panelgraphics.DrawRectangle(pen, predefinedRectangle);

            DeletePredefRec();
        }

        /// <summary>
        ///  Deleting the predefined rectangle in the panelCutTexture.
        /// </summary>
        private void DeletePredefRec()
        {
            Panelgraphics.Dispose();
            panelCutTexture.Clear();
            listViewAddedTextures.SelectedIndexChanged += new EventHandler(listViewAddedTextures_SelectedIndexChanged);
        }

        private bool RectsIntersect(Point p, Rectangle preRec)
        {
            if (p.X < preRec.X)
            {
                return false;
            }
            if (p.Y < preRec.Y)
            {
                return false;
            }
            if (p.X > preRec.X + preRec.Width)
            {
                return false;
            }
            if (p.Y > preRec.Y + preRec.Height)
            {
                return false;
            }

            return true;
        }

        private void MouseMove( MouseEventArgs e)
        {
            // For the rectangle move.
            recPositionX = e.X;
            recPositionY = e.Y;
        }

        private bool CheckRecMove(MouseEventArgs e)
        {

            if (recPositionX < drawImage.Width)
            {
                return false;
               //recPositionX = drawImage.Width - (predefinedRectangle.Right);
            }
            if (recPositionX > drawImage.Width)
            {
                return false;
                //recPositionX = drawImage.Width + (predefinedRectangle.Left);
            }
            if (recPositionY < drawImage.Height)
            {
                return false;
                //recPositionY = drawImage.Height - (predefinedRectangle.Top);
            }
            if (recPositionY > drawImage.Height)
            {
                return false;
                //recPositionY = drawImage.Height + (predefinedRectangle.Bottom);
            }
            return true;

        }

        private void btnCleanPanel_Click(object sender, EventArgs e)
        {
            textureClips.Clear();
            panelTextureCollector.Clear();
        }

        private void btnDeleteRectangels_Click(object sender, EventArgs e)
        {
            cmb.SelectedIndex = 0;

            DeletePredefRec();
        }

        private bool ValidatePanel()
        {
            if (cuttingRectangle != null)
            {
                return false;
            }
            if (drawImage != null)
            {
                return false;
            }
            if (mouseDrawRec != true)
            {
                return false;
            }
            return true;
        }

        private void btnDeleteObject_Click(object sender, EventArgs e)
        {
            //foreach (TextureRect tr in textureClips)
            for ( int i = GetTextureClipAtIndex(i); i
            {
                if (RectsIntersect(MousePosition, tr.Clip))
                {
                    
                }
            }
        }

         //int selectedImageIndex = listViewAddedTextures.SelectedItems[0].ImageIndex;
         //       selectedImage = (Bitmap)textureListManager.GetImageAtIndex(selectedImageIndex);

        public TextureRect GetTextureClipAtIndex(int index)
        {
            return textureClips[index];
        }

    }
}

// Delete button, one key event in the form for everything or many key event for deleting objects ?
// Make it possible to dragging Rectangle with the DraggingRec method if it´s possible with only one method.

