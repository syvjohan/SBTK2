using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SBTK2
{
    // For the Rectangle, Image and Point in the panelTextureCollector.
  class TextureRect
  {
    public Image SourceImage
    {
      get { return sourceImage; }
      set { sourceImage = value; }
    }

    public Rectangle Clip
    {
      get { return clip; }
      set { clip = value; }
    }

    public Point Position
    {
      get { return position; }
      set { position = value; }
    }

    // The image the rectangle should clip from.
    Image sourceImage;
    Point position;
    Rectangle clip;

    public TextureRect(Image source, Rectangle clip, Point position)
    {
      sourceImage = source;
      this.clip = clip;
      this.position = position;
    }
     

  }
}
