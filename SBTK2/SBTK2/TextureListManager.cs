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

        List<Image> image = new List<Image>();

        /// <summary>
        /// Gets or sets the images stored for the thumbnail list.
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
            this.image.Add(imageInput);
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
        ///  Check to se if the choosen object exist and uses the method IsFileCorrectType to verify that the format is ok.
        ///  If ok add image to listView via  AddFile and AddImage.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FileFormats(object sender, DragEventArgs e)
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
                            //AddFile(s);
                        }
                        else
                        {
                            MessageBox.Show("The system only support the formats .jpg .jpeg .png .bmp");
                        }

                    }
                    else
                    {
                        MessageBox.Show("File not found");
                    }
                }
            }
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
    }
}
