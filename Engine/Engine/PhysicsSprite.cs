using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public enum MotionModel { Dynamic, Static, Kinematic };

    public class PhysicsSprite: ImageSprite
    {
        public static List<PhysicsSprite> sprites = new List<PhysicsSprite>();
        
        private MotionModel model = MotionModel.Dynamic;
        public MotionModel Model
        {
            get { return model; }
            set { model = value; }
        }

        private float gx = 0;
        private float gy = 1f;
        public float GX
        {
            get { return gx; }
            set { gx = value; }
        }
        public float GY
        {
            get { return gy; }
            set { gy = value; }
        }

        private float ax = 0;
        private float ay = 0;
        public float AX
        {
            get { return ax; }
            set { ax = value; }
        }
        public float AY
        {
            get { return ay; }
            set { ay = value; }
        }

        private float vx = 0;
        private float vy = 0;
        public float VX
        {
            get { return vx; }
            set { vx = value; }
        }
        public float VY
        {
            get { return vy; }
            set { vy = value; }
        }

        //X,Y already implemented

        private float mass = 1;
        public float Mass
        {
            get { return mass; }
            set { mass = value; }
        }

        private int mask = 1;
        public int Mask
        {
            get { return mask; }
            set { mask = value; }
        }
        
        public PhysicsSprite(Image image) : base(image)
        {
            sprites.Add(this);
        }

        public PhysicsSprite(Image image, float x, float y) : base(image, x, y)
        {
            sprites.Add(this);
        }

        public PhysicsSprite(Image image, float x, float y, float width, float height) : base(image, x, y, width, height)
        {
            sprites.Add(this);
        }

        public override void Act()
        {
            base.Act();

            if (X > 2000 || X < -2000 || Y > 2000 || Y < -2000)
            {
                parent.QueueKill(this);
                sprites.Remove(this);
            }

            if (Model.Equals(MotionModel.Static)) return;

            //Update position
            X += VX;
            Y += VY;

            List<PhysicsSprite> collisions = GetCollisions();
            foreach (PhysicsSprite s in collisions)
                Collide(this, s);
            
            if (Model.Equals(MotionModel.Kinematic)) return;
            //Update velocity from acceleration and gravity
            VX += AX + GX;
            VY += AY + GY;
        }
        
        public List<PhysicsSprite> GetCollisions()
        {
            List<PhysicsSprite> output = new List<PhysicsSprite>();

            //radii of this Sprite
            float rx = Scale * Width / 2;
            float ry = Scale * Height / 2;

            for (int i = 0; i < sprites.Count; i++)
            {
                PhysicsSprite sprite = sprites.ElementAt(i);
                if (sprite == this) continue;
                if (sprite == null) continue;
                if ((mask & sprite.Mask) == 0) continue;

                //radii of other sprite
                float r2x = sprite.Scale * sprite.Width / 2;
                float r2y = sprite.Scale * sprite.Height / 2;

                if (Math.Abs(X - sprite.X) >= rx + r2x) continue;
                if (Math.Abs(Y - sprite.Y) >= ry + r2y) continue;
                output.Add(sprite);
            }

            return output;
        }

        public static void Collide(PhysicsSprite s1, PhysicsSprite s2)
        {
            //If there is a collision, bullets destroys everything but blocks
            if (s2.GetType() == typeof(Bullet))
            {
                try
                {
                    s2.parent.QueueKill(s2);
                    sprites.Remove(s2);
                    if (s1.Model == MotionModel.Static) ;
                    else
                    {
                        s1.parent.QueueKill(s1);
                        sprites.Remove(s1);
                    }
                }
                catch (Exception) { }
                
            }
            
            else if (s1.GetType() == typeof(Bullet))
            {
                try
                {
                    s1.parent.QueueKill(s1);
                    sprites.Remove(s1);
                }
                catch (Exception) { }
                
                if (s2.Model == MotionModel.Static) ;
                else
                {
                    try
                    {
                        s2.parent.QueueKill(s2);
                        sprites.Remove(s2);
                    }
                    catch (Exception) { }
                }
            }
            
            //Blocks block everything else
            else if (s2.Model == MotionModel.Static)
            {
                float r1x = s1.Scale * s1.Width / 2;
                float r1y = s1.Scale * s1.Height / 2;
                float r2x = s2.Scale * s2.Width / 2;
                float r2y = s2.Scale * s2.Height / 2;

                float diffX = s2.X - s1.X;
                float diffY = s2.Y - s1.Y;

                float intersectX, intersectY;
                bool left, above;

                if (diffX > 0) //s1 is left of s2
                {
                    intersectX = (r2x + r1x) - diffX;
                    left = true;
                }
                else
                {
                    intersectX = (r2x + r1x) + diffX;
                    left = false;
                }

                if (diffY > 0) //s1 is above s2
                {
                    intersectY = (r2y + r1y) - diffY;
                    above = true;
                }
                else
                {
                    intersectY = (r2y + r1y) + diffY;
                    above = false;
                }

                if (intersectX > intersectY)
                {
                    if (above)
                        s1.Y -= (intersectY + 0.01f);
                    else
                        s1.Y += (intersectY + 0.01f);

                    s1.VY = 0;
                    s1.AY = 0;
                }
                else
                {
                    if (left)
                        s1.X -= (intersectX + 0.01f);
                    else
                        s1.X += (intersectX + 0.01f);
                    s1.VX = 0;
                    s1.AX = 0;
                }
            }
            
            //if one character is the player and hits an enemy
            else if (s1.GetType() == typeof(Character) && s2.GetType() == typeof(Character))
            {
                Character c1 = (Character)s1;
                Character c2 = (Character)s2;
                if (c1.Player)
                    s1.parent.QueueKill(s1);
                else if (c2.Player)
                    s2.parent.QueueKill(s2);
            }

            //Otherwise, elastic collsion
            else
            {
                float v1xi = s1.VX;
                float v1yi = s1.VY;
                float v2xi = s2.VX;
                float v2yi = s2.VY;

                float diffRatio = (s1.Mass - s2.Mass) / (s1.Mass + s2.Mass);
                float twoRatio = (2 * s2.Mass / (s1.Mass + s2.Mass));

                s1.VX = diffRatio * v1xi + twoRatio * v2xi;
                s1.VY = diffRatio * v1yi + twoRatio * v2yi;

                s2.VX = twoRatio * v1xi - twoRatio * v2xi;
                s2.VY = twoRatio * v1yi - twoRatio * v2yi;
            }
        }
    }

}
