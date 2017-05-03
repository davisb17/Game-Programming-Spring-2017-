using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class ImageSprite : Sprite
    {
        private float width;
        public float Width
        {
            get { return width; }
            set { width = value; }
        }

        private float height;
        public float Height
        {
            get { return height; }
            set { height = value; }
        }

        private Image image;
        public Image Image
        {
            get { return image; }
            set { image = value; }
        }

        public ImageSprite(Image image)
        {
            this.image = image;
            this.width = image.Width;
            this.height = image.Height;
        }

        public ImageSprite(Image image, float x, float y)
        {
            this.image = image;
            width = image.Width;
            height = image.Height;
            X = x;
            Y = y;
        }

        //distorts imagedata
        public ImageSprite(Image image, float x, float y, float width, float height)
        {
            this.image = image;
            this.width = width;
            this.height = height;
            X = x;
            Y = y;
        }

        public override void Paint(Graphics g)
        {
            if (Image == null) return;
            g.DrawImage(Image, -width / 2, -height / 2, width, height);
        }
        
    }

}
