using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpriteStuff
{
    class ImageSprite:Sprite
    {
        Image image;
        float cornerX;
        float cornerY;

        public Image Image
        {
            get { return image; }
            set { image = value; }
        }
        public float CornerX
        {
            get { return cornerX; }
            set { cornerX = value; }
        }
        public float CornerY
        {
            get { return cornerY; }
            set { cornerY = value; }
        }

        public ImageSprite(Image imageData)
        {
            this.image = imageData;
            this.cornerX = .5f;
            this.cornerY = .5f;
        }

        public override void Paint(Graphics g)
        {
            g.DrawImage(image, -image.Width/2, -image.Height/2, image.Width, image.Height);
        }
    }
}
