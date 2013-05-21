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
        private int recPositionX;
        private int recPositionY;
        private bool mouseDrawRec = false;// Flagg for controlling the dragging in the panels
        private bool draggingRec = false;// Flagg for controlling the dragging in the panels 
        private Graphics Panelgraphics { get; set; }
        private Bitmap selectedImage;
        private Bitmap drawImage;
        Pen pen = new Pen(Color.Black, 2);

        // ReDraw the rectangle
        private Bitmap canvas;
        private int panelSizeX = 1024;
        private int panelSizeY = 1024;

        // Predefined rectangle
        Rectangle predefinedRectangle;

        // Rectangles for the panelTextureCollector
        List<TextureRect> textureClips = new List<TextureRect>();

        // Mouse Move
        int X;
        int Y;
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
                Brush brush = new SolidBrush(Color.White);
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
            Panelgraphics.DrawRectangle(pen, X, Y, predefinedRectangle.Width, predefinedRectangle.Height);

        }

        /// <summary>
        ///  Drawing the cutted rectangle from panelCutTexture into the panelTextureCollector.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReDrawRectangle(object sender, PaintEventArgs e)
        {

            // casta from Image to canvas (bitmap)
            Panelgraphics = Graphics.FromImage((Image)canvas);

            Brush brush = new SolidBrush(Color.Transparent);
            Panelgraphics.FillRectangle(brush, new Rectangle(0, 0, panelSizeX, panelSizeY));

            Panelgraphics = panelTextureCollector.CreateGraphics();
            Panelgraphics.DrawImage(canvas, new Point(0, 0));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelCutTexture_MouseDown(object sender, MouseEventArgs e)
        {
            if (cuttingRectangle != null && drawImage != null && mouseDrawRec == true)
            {
                if (e.X >= recPositionX && e.X <= (recPositionX + cuttingRectangle.Width) &&
                    e.Y >= recPositionY && e.Y <= (recPositionY + cuttingRectangle.Height))
                {
                    draggingRec = true;
                    if (drawImage != null)
                    {
                        DoDragDrop(drawImage, DragDropEffects.Copy);
                    }
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
        private void DrawRectangle(MouseEventArgs e)
        {
                if (panelCutTexture.BackgroundImage != null)
                {
                    this.Refresh();

                    int width = e.X - recPositionX;
                    int height = e.Y - recPositionY;

                    cuttingRectangle = new Rectangle(recPositionX,
                        recPositionY,
                        width,
                        height);

                    Panelgraphics = panelCutTexture.CreateGraphics();
                    Panelgraphics.DrawRectangle(pen, cuttingRectangle);
                }

        }

        private void panelCutTexture_MouseMove(object sender, MouseEventArgs e)
        {
            mousepoint = new Point(e.X, e.Y);
            
            if(e.Button != MouseButtons.Left) return;
            
            if (RectsIntersect(mousepoint, predefinedRectangle))
            {
                MovingRec(e); 
            }
            //else
            //{
            //    Panelgraphics.Dispose();
            //    DrawRectangle(e);
            //}
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

                textureRect = new TextureRect(selectedImage, cuttingRectangle, new Point(recPositionX, recPositionY));
                textureClips.Add(textureRect);
                panelTextureCollector.Refresh();
            }
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

        private void panelTextureCollector_Click(object sender, EventArgs e)
        {
            if (drawImage != null)
            {
                Panelgraphics = panelTextureCollector.CreateGraphics();
                foreach (TextureRect tr in textureClips)
                {
                    Panelgraphics.DrawImage(
                      tr.SourceImage, //.Crop(tr.Clip.Top,tr.Clip.Left,tr.Clip.Width,tr.Clip.Height),
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
            panelCutTexture.Clear();

            predefinedRectangle = ((CustomRectangleRule)cmb.SelectedItem).rec;

            Panelgraphics = panelCutTexture.CreateGraphics();
            Panelgraphics.DrawRectangle(pen, predefinedRectangle);
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

        private void MovingRec(MouseEventArgs e)
        {
            // For the rectangle move.
                X = e.X;
                Y = e.Y;

                if (X > drawImage.Width)
                {
                    X = drawImage.Width - (predefinedRectangle.Right);
                }
                if (X < drawImage.Width)
                {
                    X = drawImage.Width + (predefinedRectangle.Left);
                }
                if (Y < drawImage.Height)
                {
                    Y = drawImage.Height - (predefinedRectangle.Top);
                }
                if (Y > drawImage.Height)
                {
                    Y = drawImage.Height + (predefinedRectangle.Bottom);
                }

                panelCutTexture.Invalidate();
        }

        private void btnCleanPanel_Click(object sender, EventArgs e)
        {
            textureClips.Clear();
            panelTextureCollector.Clear();
        }

        private void btnDeleteRectangels_Click(object sender, EventArgs e)
        {
            cmb.SelectedIndex = 0;

            Panelgraphics.Dispose();
            panelCutTexture.Clear();
            listViewAddedTextures.SelectedIndexChanged += new EventHandler(listViewAddedTextures_SelectedIndexChanged);
        }

    }
}

// Delete button, one key event in the form for everything or many key event for deleting objects ?
// Make it possible to dragging Rectangle with the DraggingRec method if it´s possible with only one method.

