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
        private bool draggingRec = false; // make it not possible to draw a rec over a empty panel.
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

        List<TextureRect> textureClips;

        //timer for the Paint events
        Timer timer;

        public FormTextureAtlas()
        {
            InitializeComponent();
            InitializeLocalComponents();
            TimerUpdate();
            FillCmb();

            textureClips = new List<TextureRect>();
        }

        private void InitializeLocalComponents()
        {
            // Redraw the rectangle
            canvas = new Bitmap(panelSizeX, panelSizeY);
            panelCutTexture.BackgroundImage = canvas;

            //draw image in panelCutTexture
            drawImage = new Bitmap(panelSizeX, panelSizeY);

            //panelCutTexture.Paint += new PaintEventHandler(panelCutTexture_Paint);
            panelTextureCollector.Click += new EventHandler(panelTextureCollector_Click);


            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();

  

        }

        /// <summary>
        /// Timer will update 60 times every second
        /// </summary>
        public void TimerUpdate()
        {
            timer = new Timer();

            timer.Tick += new EventHandler(timer_Tick);

            timer.Interval = (1000 / 60); // timer will tick 60 times every second
            timer.Enabled = true;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {

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

                Graphics g = Graphics.FromImage(drawImage);
                Brush brush = new SolidBrush(Color.White);
                g.FillRectangle(brush, new Rectangle(0, 0, drawImage.Width, drawImage.Height));

                g.DrawImage(textureListManager.GetImageAtIndex(selectedImageIndex), new Point(0, 0));
                panelCutTexture.Refresh();
            }
        }

        private void panelCutTexture_Paint(object sender, PaintEventArgs e)
        {

            // casta from Image to canvas (bitmap)
            Graphics graphics; //= Graphics.FromImage((Image)drawImage);
            graphics = panelCutTexture.CreateGraphics();
            graphics.DrawImage(drawImage, new Point(0, 0));
            
           

        }

        private void ReDrawRectangle(object sender, PaintEventArgs e)
        {

            // casta from Image to canvas (bitmap)
            Graphics graphicsCanvas = Graphics.FromImage((Image)canvas);

            Brush brush = new SolidBrush(Color.Transparent);
            graphicsCanvas.FillRectangle(brush, new Rectangle(0, 0, panelSizeX, panelSizeY));

            graphicsCanvas = panelTextureCollector.CreateGraphics();
            graphicsCanvas.DrawImage(canvas, new Point(0, 0));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelCutTexture_MouseDown(object sender, MouseEventArgs e)
        {
            if (cuttingRectangle != null && drawImage != null)
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
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void DrawRectangle(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && mouseDrawRec == true)
            {
                Image image = null;
                image = panelCutTexture.BackgroundImage;

                if (image != null)
                {
                    this.Refresh();

                    int width = e.X - recPositionX;
                    int height = e.Y - recPositionY;

                    if (width <= 1)
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
            if (mouseDrawRec == true)
            {
                DrawRectangle(e);
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
                TextureRect nRect = new TextureRect(selectedImage, cuttingRectangle, new Point(0, 0));
                textureClips.Add(nRect);
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

            //    rectPos = Cursor.Position;
            //    rectPos = panelTextureCollector.PointToClient(rectPos);

            //    rectPos.X -= drawImage.Width / 2;
            //    rectPos.Y -= drawImage.Height / 2;
            if (drawImage != null)
            {
                Graphics graphics = panelTextureCollector.CreateGraphics();
                foreach (TextureRect tr in textureClips)
                {

                    graphics.DrawImage(
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
            cmb.SelectedIndex = 0;
        }

        /// <summary>
        /// Predefined Rectangles in the panelCutTexture.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            predefinedRectangle = ((CustomRectangleRule)cmb.SelectedItem).rec;


            Panelgraphics = panelCutTexture.CreateGraphics();
            Panelgraphics.DrawRectangle(pen, predefinedRectangle);
            cmb.Items.Remove();

        }
        //private void DraggingRec(MouseEventArgs e)
        //{
        //    if ((e.Button == MouseButtons.Left) && (predefinedRectangle.X == MousePosition.X) && (predefinedRectangle.Y == MousePosition.Y))
        //    {
        //        MousePosition.Equals(predefinedRectangle);
        //    }
        //}

    }
}

// Combobox show the title but no editing for user.
// Make the images stopp flashing and make them update correctly after the timer_tick.
// Reset so it´s not possible to draw rectangle in empty panels.
// Delete button, one key event in the form for everything or many key event for deleting objects ?
// Make it possible to dragging Rectangle with the DraggingRec method if it´s possible with only one method.

