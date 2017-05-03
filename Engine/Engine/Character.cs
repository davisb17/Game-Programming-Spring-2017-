using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Character : PhysicsSprite
    {
        private bool player = false;
        public bool Player
        {
            get { return player; }
            set { player = value; }
        }
        
        private float lastY;

        private bool inAir = true;
        public bool InAir
        {
            get { return Y != lastY; }
        }

        private int bulletMask;
        public int BulletMask
        {
            get { return bulletMask; }
            set { bulletMask = value; }
        }

        public Character(Image image) : base(image)
        {
            lastY = Y;
        }

        public Character(Image image, float x, float y) : base(image, x, y)
        {
            lastY = Y;
        }

        public Character(Image image, float x, float y, float width, float height) : base(image, x, y, width, height)
        {
            lastY = Y;
        }

        public override void Act()
        {
            lastY = Y;
            base.Act();
        }

        public Bullet Shoot(Image image, float x, float y, float speed)
        {
            return new Bullet(image, this, x, y) { BulletSpeed = speed, Mask = BulletMask };
        }
    }
}
