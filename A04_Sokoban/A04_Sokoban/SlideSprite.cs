using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A04_Sokoban
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

		private float topSpeed = 50;
		public float TopSpeed
		{
			get { return topSpeed; }
			set { topSpeed = value; }
		}

		private float acceleration = 8.0f;
		public float Acceleration
		{
			get { return acceleration; }
			set { acceleration = value; }
		}

		private float speed = 0;
		//no get or set

		public SlideSprite(Image imageData)
		{
			this.image = imageData;
			this.targetX = X;
			this.TargetY = Y;
		}

        public  SlideSprite(Image imageData, float x, float y)
        {
            this.image = imageData;
            this.X = x;
            this.targetX = X;
            this.Y = y;
            this.targetY = y;
        }

		public override void Paint(Graphics g)
		{
			g.DrawImage(Image, 0, 0);
		}

		public override void Act()
		{

			speed += acceleration;
			if (speed > topSpeed) speed = topSpeed;
			
			double angle = Math.Atan2((targetY - Y), (targetX - X));
            
			float dx = (float)(speed * Math.Cos(angle));
			float dy = (float)(speed * Math.Sin(angle));


            if ((targetX - X) <= 0 && dx <= (targetX - X)) X = targetX;
            else if ((targetX - X) >= 0 && dx >= (targetX - X)) X = targetX;
            else X += dx;

            if ((targetY - Y) <= 0 && dy <= (targetY - Y)) Y = targetY;
            else if ((targetY - Y) >= 0 && dy >= (targetY - Y)) Y = targetY;
            else Y += dy;

            if (targetY == Y && targetX == X) speed = 0;
		}
	}
}