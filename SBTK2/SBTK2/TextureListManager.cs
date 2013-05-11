using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace SBTK2
{
    class TextureListManager
    {
        // uses to validate format.
        private readonly string[] ALLOWED_IMAGE_EXTENSIONS = { ".jpg", ".jpeg", ".png", ".bmp" };

        private bool draggingItemOverAtlas = false; 

        List<Image> image = new List<Image>();

        List<Bitmap> cuttedBitmap = new List<Bitmap>();

        /// <summary>
        ///  Gets or sets the cuttedBitmaps stored in the Bitmap list
        /// </summary>
        public List<Bitmap> Bitmaps
        {
            get { return this.cuttedBitmap; }
            set { this.cuttedBitmap = value; }
        }

        /// <summary>
        /// Gets the number of stored bitmaps.
        /// </summary>
        public int NrOfBitmaps
        {
            get { return cuttedBitmap.Count; }
        }

        /// <summary>
        /// Adds an bitmap object to the list<Bitmap>cuttedBitmaps
        /// </summary>
        /// <param name="imageInput"></param>
        public void AddBitmap(Bitmap bitmapInput)
        {
            if (cuttedBitmap != null) return;
            {
                this.cuttedBitmap.Add(bitmapInput);
            }
        }

        /// <summary>
        /// Gets the cuttedBitmaps at the specified index
        /// </summary>
        /// <param name="index">specified index</param>
        /// <returns>The image at the index</returns>
        public Bitmap GetBitmapAtIndex(int index)
        {
            return cuttedBitmap[index];
        }

        /// <summary>
        ///  Delete selected Bitmap in cuttedBitmap
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool DeleteBitmap(int index)
        {
            if (NrOfBitmaps >= 0)
            {
                cuttedBitmap.RemoveAt(index);
                return true;
            }
            return false;
        }

        /// <summary>
        ///  Clear the hole list of cuttedBitmaps
        /// </summary>
        public void Clear()
        {
            cuttedBitmap.Clear();
        }

        /// <summary>
        /// Gets or sets the images stored in the Image list
        /// </summary>
        public List<Image> Images
        {
            get { return this.image; }
            set { this.image = value; }
        }

        /// <summary>
        /// Gets the number of stored images.
        /// </summary>
        public int NrOfImages
        {
            get { return image.Count; }
        }

        /// <summary>
        /// Gets the image at the specified index
        /// </summary>
        /// <param name="index">specified index</param>
        /// <returns>The image at the index</returns>
        public Image GetImageAtIndex(int index)
        {
                return image[index];
        }
        string imageExtension = "";

        /// <summary>
        /// Adds an image object to the list<Image>image
        /// </summary>
        /// <param name="imageInput"></param>
        public void AddImage(Image imageInput)
        {
            if (image != null) return;
            {
                this.image.Add(imageInput);
            }
        }

        // Open a image using OpenFileDialog.
        public void OpenImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            //Enables multiselect
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.bmp; *.png;) | *.png; *.jpg; *. jpeg; *.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < openFileDialog.FileNames.Length; i++)
                {
                    this.image.Add(Image.FromFile(openFileDialog.FileNames[i]));
                }
            }

            //Check the file type
            int dotIndex = openFileDialog.FileName.LastIndexOf('.');
            imageExtension = openFileDialog.FileName.Substring(
                    openFileDialog.FileName.Length - dotIndex
                    );
        }

        /// <summary>
        ///  DragEnter checks the item and validate if it´s a file or bitmap. If true, copy the item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DragEnterGeneric(object sender, DragEventArgs e)
        {
            draggingItemOverAtlas = true;

            // if the data item is a file or a bitmap
            if (e.Data.GetDataPresent(typeof(ListViewItem)) ||
                e.Data.GetDataPresent(DataFormats.FileDrop) ||
                e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        ///  Check to se if the choosen object exist and uses the method IsFileCorrectType to verify that the format is ok.
        ///  If ok add image to listView via  AddFile and AddImage.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DragDropFromDesktop(object sender, DragEventArgs e)
        {
            string[] handles = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (handles != null)
            {
                foreach (string s in handles)
                {

                    if (File.Exists(s))
                    {

                        if (IsFileCorrectType(s, ALLOWED_IMAGE_EXTENSIONS))
                        {
                            AddFile(s);
                        }
                        else
                        {
                            MessageBox.Show("The system only support the formats .jpg .jpeg .png .bmp");
                        }

                    }
                    else
                    {
                        MessageBox.Show("File format is not valid");
                    }
                }
            }
        }

        /// <summary>
        /// Leaving the DragDrop events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DragDropLeave(object sender, EventArgs e)
        {
            draggingItemOverAtlas = false;
        }

        /// <summary>
        ///  Determines if the file has one of the accepted extensions.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="validExtensions"></param>
        /// <returns></returns>
        private bool IsFileCorrectType(string filePath, string[] validExtensions)
        {
            bool isCorrect = false;
            foreach (string extension in validExtensions)
            {
                if (string.Compare(Path.GetExtension(filePath), extension, true) == 0)
                {
                    isCorrect = true;
                    break;
                }
            }

            return isCorrect;
        }

        /// <summary>
        /// Adds the files to the list<Image>image in TextureListManager
        /// </summary>
        /// <param name="fullFilePath"></param>
        private void AddFile(string fullFilePath)
        {
            Image picture = Image.FromFile(fullFilePath);
            AddImage(picture);
        }
    }
}
