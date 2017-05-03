using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Engine
{ 
    public class Enemy : Character
    {
        public static Random r = new Random();

        private Character target;
        public Character Target
        {
            get { return target; }
            set { target = value; }
        }

        private int shootTicks;
        public int ShootTicks
        {
            get { return shootTicks; }
            set { shootTicks = value; }
        }

        private Image bulletImage;
        public Image BulletImage
        {
            get { return bulletImage; }
            set { bulletImage = value; }
        }

        private float bulletSpeed;
        public float BulletSpeed
        {
            get { return bulletSpeed; }
            set { bulletSpeed = value; }
        }

        private int currentTicks = r.Next(15);
        
        public Enemy(Image image) : base(image)
        {

        }

        public Enemy(Image image, float x, float y) : base(image, x, y)
        {

        }

        public Enemy(Image image, float x, float y, float width, float height) : base(image, x, y, width, height)
        {

        }

        public override void Act()
        {
            base.Act();
            currentTicks++;

            if (currentTicks == shootTicks && target != null)
            {
                Bullet b = Shoot(BulletImage, target.X, target.Y, bulletSpeed);
                currentTicks = 0;
                parent.QueueAdd(b);
            }
        }

    }
}
