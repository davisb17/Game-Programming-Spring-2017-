using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpriteStuff
{
    public class Sprite
    {
        private Sprite parent;
        private float alpha = 1;
        private float x = 0.0f;
        private float y = 0.0f;
        private float scale = 1.0f;
        private float rotation = 0.0f;

        protected List<Sprite> children = new List<Sprite>();

        public Sprite Parent
        {
            get { return parent; }
            set { parent = value; }
        }
        public float Alpha
        {
            get { return alpha; }
            set { alpha = value; }
        }
        public float X
        {
            get { return x; }
            set { x = value; }
        }
        public float Y
        {
            get { return y; }
            set { y = value; }
        }
        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        } 
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public Sprite()
        {
            Engine.sprites.Add(this);
        }

        public void Destroy()
        {
            Parent.children.Remove(this);
            Engine.sprites.Remove(this);
        }

        public void Render(Graphics g)
        {
            Matrix original = g.Transform.Clone();
            g.TranslateTransform(scale, scale);
            g.ScaleTransform(scale, scale);
            g.RotateTransform(rotation);
            Paint(g);
            foreach (Sprite s in children)
            {
                s.Render(g);
            }
            g.Transform = original;
        }

        public virtual void Paint(Graphics g)
        {

        }
        
        public void Add(Sprite s)
        {
            s.parent = this;
            children.Add(s);
        }

    }
}
