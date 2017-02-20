using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A02_Sprite
{
    public class ImageSprite : Sprite
    {
        Image image;

        public Image Image
        {
            get { return image; }
            set { image = value; }
        }

        public ImageSprite(Image imageData)
        {
            this.image = imageData;
        }

        public override void paint(Graphics g)
        {
            g.DrawImage(image, X, Y, image.Width, image.Height);
        }

        public override void act()
        {
            base.act();
            X+=2;
            if (X > 150)
                X = 0;
            Y += .5f;
            if (Y > 200)
                Y /= 8;
        }
    }
}