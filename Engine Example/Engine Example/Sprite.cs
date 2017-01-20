using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine_Example
{
    public class Sprite
    {
        private double x = 0;

        public double X
        {
            get {   return x;   }
            set {   x = value;  }
        }


        private double y = 0;

        public double Y
        {
            get { return y; }
            set { y = value; }
        }


        protected List<Sprite> children = new List<Sprite>();


        private double scale = 1;

        public double Scale
        {
            get { return scale; }
            set { scale = value; }
        }


        public void Render(Graphics g)
        {
            Matrix original = g.Transform.Clone();
            g.ScaleTransform((float)scale,(float)scale);
            g.TranslateTransform((float)x, (float)y);
            Paint(g);
            foreach (Sprite s in children)
                s.Render(g);
            g.Transform = original;

        }

        public virtual void Paint(Graphics g)
        {

        }

        public void Add(Sprite s)
        {
            children.Add(s);
        }
    }
}
