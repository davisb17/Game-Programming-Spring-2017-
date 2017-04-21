using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

public class Sprite
{
    private Sprite parent = null;
    private List<Sprite> children = new List<Sprite>();
    private Queue<Sprite> killQueue = new Queue<Sprite>();
    private Queue<Sprite> addQueue = new Queue<Sprite>();

    public float x = 0;
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

    //Update Sprite
    public virtual void Act()
    {
        while (killQueue.Count > 0) Kill(killQueue.Dequeue());
        while (addQueue.Count > 0) Add(addQueue.Dequeue());

        //then extending Sprite extends this function
    }

    public void QueueAdd(Sprite s)
    {
        addQueue.Enqueue(s);
    }

    public void QueueKill(Sprite s)
    {
        killQueue.Enqueue(s);
    }

    private void Add(Sprite s)
    {
        s.parent = this;
        children.Add(s);
    }

    private void Kill(Sprite s)
    {
        s.parent = null;
        children.Remove(s);
    }
}
