//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Drawing;

//namespace SBTK2
//{
//    class Sprite
//    {
//        private int mX;
//        private int mY;
//        private int mWidth;
//        private int mHeight;
//        private Image mTexture;
//        private Point mOrigin;
//        private Rectangle mPictureEdge;

//        public int X { get { return mX; } set { this.mX = value; } }
//        public int Y { get { return mY; } set { this.mY = value; } }
//        public int Width { get { return mWidth; } set { this.mWidth = value; } }
//        public int Height { get { return mHeight; } set { this.mHeight = value; } }
//        public Image Texture { get { return mTexture; } set { this.mTexture = value; } }
//        public Point Origin { get { return mOrigin; } set { this.mOrigin = value; } }
//        public Rectangle PictureEdge { get { return mPictureEdge; } set { this.mPictureEdge = value; } }

//        public Sprite(Image Texture, int X, int Y, int Width, int Height, Point Origin, Rectangle PictureEdge)
//        {
//            this.mTexture = Texture;
//            this.mX = X;
//            this.mY = Y;
//            this.mWidth = Width;
//            this.mHeight = Height;
//            this.mOrigin = Origin;
//            this.mPictureEdge = PictureEdge;
//        }
//        public void UpdateRectanglePosition()
//        {
//            this.mPictureEdge.X = this.mX;
//            this.mPictureEdge.Y = this.mY;
//        }
//    }
//}
