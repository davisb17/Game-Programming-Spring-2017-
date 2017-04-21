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
        
        private static List<PhysicsSprite> sprites = new List<PhysicsSprite>();
        
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

        public override void Act()
        {
            
            if (Model.Equals(MotionModel.Static)) return;

            //List<PhysicsSprite> collisions = GetCollisions();
            foreach (PhysicsSprite s in GetCollisions())
                Collide(this, s);

            //Update position
            X += VX;
            Y += VY;
            
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

            foreach (PhysicsSprite sprite in sprites)
            {
                if (sprite == this) continue;
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
            

            if (s2.Model == MotionModel.Static)
            {
                float r1x = s1.Scale * s1.Width / 2;
                float r1y = s1.Scale * s1.Height / 2;
                float r2x = s2.Scale * s2.Width / 2;
                float r2y = s2.Scale * s2.Height / 2;

                float diffX = Math.Abs(s1.X - s2.X);
                float diffY = Math.Abs(s1.Y - s2.Y);
                
                if (diffX < r1x + r2x)
                {
                    if (r1y + r2y - diffY < 2.0f)
                    {
                        if (s1.Y > s2.Y) s1.Y = s2.Y + r1y + r2y - 0.01f;
                        else s1.Y = s2.Y - r1y - r2y + 0.01f;
                        s1.VY = 0;
                    }
                    else s1.VY *= -.80f; //lose energy and reverse direction
                }

                if (diffY < r1y + r2y)
                {
                    if (r1x + r2x - diffX < 2.0f)
                    {
                        if (s1.X > s2.X) s1.X = s2.X + r1x + r2x - 0.01f;
                        else s1.X = s2.X - r1x - r2x + 0.01f;
                        s1.VX = 0;
                    }
                    s1.VX *= -.80f; //lose energy and reverse direction
                }
            }

            //Else: totally elastic collision
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
