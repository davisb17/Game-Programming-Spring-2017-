using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

public class Sprite
{
    public Sprite parent = null;
    private Queue<Sprite> killQueue = new Queue<Sprite>();
    private Queue<Sprite> addQueue = new Queue<Sprite>();

    private List<Sprite> children = new List<Sprite>();
    public List<Sprite> Children
    {
        get { return children; }
    }

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
    
    //Calls Paint on itself then Render on children
    public void Render(Graphics g)
    {
        Matrix original = g.Transform.Clone();
        g.TranslateTransform(x, y);
        g.ScaleTransform(scale, scale);
        Paint(g);
        for (int i = 0; i < children.Count; i++)
        {
            Sprite s = children.ElementAt(i);
            if (s == null) continue;
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
        for (int i = 0; i < children.Count; i++)
        {
            Sprite s = children.ElementAt(i);
            if (s == null) continue;
            s.Update();
        }
    }

    //Update Sprite
    public virtual void Act()
    {
        

        while (killQueue.Count > 0) KillSprite(killQueue.Dequeue());
        while (addQueue.Count > 0) AddSprite(addQueue.Dequeue());

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

    private void AddSprite(Sprite s)
    {
        s.parent = this;
        children.Add(s);
        //Physics Sprites add to list at construction
    }

    private void KillSprite(Sprite s)
    {
        children.Remove(s);
        s.parent = null;
        if (s.GetType() == typeof(Engine.PhysicsSprite) || s.GetType() == typeof(Engine.Bullet))
            Engine.PhysicsSprite.sprites.Remove((Engine.PhysicsSprite)s);
    }
}
