using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A04_Sokoban
{
    public class Sprite
    {
        private Sprite parent = null;

        private float x = 0;
        public float X
        {
            get { return x; }
            set { x = value; }
        }

        private float y = 0;
        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        private float scale = 1;
        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        private float rotation = 0;
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public List<Sprite> children = new List<Sprite>();
        
        public void Kill()
        {
            parent.children.Remove(this);
        }

        public void KillChildren()
        {
            try
            {
                foreach (Sprite s in children)
                {
                    s.KillChildren();
                    s.Kill();
                }
                children = new List<Sprite>();
            }
            catch { }
            
        }

        //Calls Paint on itself then Render on children
        public void Render(Graphics g)
        {
            Matrix original = g.Transform.Clone();
            g.TranslateTransform(x, y);
            g.ScaleTransform(scale, scale);
            g.RotateTransform(rotation);
            Paint(g);
            foreach (Sprite s in children)
            {
                s.Render(g);
            }
            g.Transform = original;
        }

        //Paints actual Sprite
        public virtual void Paint(Graphics g)
        {

        }

        //Cals Act on itself then Update on chilren
        public void Update()
        {
            Act();
            foreach (Sprite s in children)
            {
                s.Update();
            }

        }

        //Updates position or image of Sprite
        public virtual void Act()
        {

        }

        public void Add(Sprite s)
        {
            s.parent = this;
            children.Add(s);
        }

    }
}
