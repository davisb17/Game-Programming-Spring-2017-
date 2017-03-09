using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A03_BasicEngine
{
    public class SlideSprite : Sprite
    {

        //Insteance vars
        Image image;
        public Image Image
        {
            get { return image; }
            set { image = value; }
        }

        private float targetX;
        public float TargetX
        {
            get { return targetX; }
            set { targetX = value; }
        }

        private float targetY;
        public float TargetY
        {
            get { return targetY; }
            set { targetY = value; }
        }

        private float velocity = 1;
        public float Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public SlideSprite(Image imageData)
        {
            this.image = imageData;
            this.targetX = X;
            this.TargetY = Y;
        }

        public override void Paint(Graphics g)
        {
            g.DrawImage(Image, X, Y);
        }

        public override void Act()
        {
            float xDiff = X - targetX;
            if (Math.Abs(xDiff) <= velocity) X = TargetX;
            else if (xDiff < 0) X += velocity;
            else X -= velocity;

            float yDiff = Y - targetY;
            if (Math.Abs(yDiff) <= velocity) Y = TargetY;
            else if (yDiff < 0) Y += velocity;
            else Y -= velocity;
        }
    }
}
