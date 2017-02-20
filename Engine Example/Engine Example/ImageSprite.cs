using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine_Example
{
    class ImageSprite:Sprite
    {
        Image image;

        public Image Image
        {
            get { return image; }
            set { image = value; }
        }


        public ImageSprite(Image image)
        {
            this.image = image;

        }

        public override void Paint(Graphics g)
        {
            g.DrawImage(image, new Rectangle();
        }


    }
}
