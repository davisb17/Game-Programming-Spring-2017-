using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Bullet : PhysicsSprite
    {
        private float bulletSpeed = 20f;
        public float BulletSpeed
        {
            get { return BulletSpeed; }
            set { bulletSpeed = value; }
        }

        //Bullets should always be square
        public Bullet(Image image, Character origin, float targetX, float targetY) : base(image)
        {
            Scale = 0.25f;

            float diffX = targetX - origin.X;
            float diffY = targetY - origin.Y;

            //radius of box in x and y axes
            float rx = Width / 2.0f * Scale;
            float ry = Height / 2.0f * Scale;

            //radius of origin in x and y axes
            float rox = origin.Width / 2.0f * origin.Scale;
            float roy = origin.Height / 2.0f * origin.Scale;

            //if targetX or targetY inside the shape
            if (Math.Abs(diffX) <= rx + rox && Math.Abs(diffY) <= ry + roy)
            {
                this.Image = null;
                Mask = 0;
            }

            double theta = Math.Atan2(diffY, diffX);


            //if on the bottom or top of origin
            if (Math.Abs(theta) > Math.Atan2(roy, rox) && Math.Abs(theta) < Math.Atan2(roy, -rox))
            {
                if (diffY > 0) // target is below
                {
                    Y = origin.Y + roy + ry;
                    X = origin.X + (float)((roy + ry) / Math.Tan(theta));
                }
                else //target is above
                {
                    Y = origin.Y - roy - ry;
                    X = origin.X - (float)((roy + ry) / Math.Tan(theta));
                }
            }
            else //if on side of the origin
            {
                if (diffX > 0) //target is right
                {
                    X = origin.X + rox + rx;
                    Y = origin.Y + (float)((rox + rx) * Math.Tan(theta));
                }
                else //target is left
                {
                    X = origin.X - rox - rx;
                    Y = origin.Y - (float)((rox + rx) * Math.Tan(theta));
                }

            }

            VX = 50f * (float)Math.Cos(theta);
            VY = 50f * (float)Math.Sin(theta);


            Model = MotionModel.Kinematic;
        }

        public override void Act()
        {
            base.Act();
        }
    }
}
