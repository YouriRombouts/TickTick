using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class StaticGameObjectList : StaticGameObject
{
    protected List<StaticGameObject> children;

    public StaticGameObjectList(int layer = 0, string id = "") : base(layer, id)
    {
        children = new List<StaticGameObject>();
    }

    public List<StaticGameObject> Children
    {
        get { return children; }
    }

    public void Add(StaticGameObject obj)
    {
        obj.Parent = this;
        for (int i = 0; i < children.Count; i++)
        {
            if (children[i].Layer > obj.Layer)
            {
                children.Insert(i, obj);
                return;
            }
        }
        children.Add(obj);
    }

    public void Remove(StaticGameObject obj)
    {
        children.Remove(obj);
        obj.Parent = null;
    }

    public StaticGameObject Find(string id)
    {
        foreach (StaticGameObject obj in children)
        {
            if (obj.Id == id)
            {
                return obj;
            }
            if (obj is StaticGameObjectList)
            {
                StaticGameObjectList objList = obj as StaticGameObjectList;
                StaticGameObject subObj = objList.Find(id);
                if (subObj != null)
                {
                    return subObj;
                }
            }
        }
        return null;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        for (int i = children.Count - 1; i >= 0; i--)
        {
            children[i].HandleInput(inputHelper);
        }
    }

    public override void Update(GameTime gameTime)
    {
        foreach (StaticGameObject obj in children)
        {
            obj.Update(gameTime);
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (!visible)
        {
            return;
        }
        List<StaticGameObject>.Enumerator e = children.GetEnumerator();
        while (e.MoveNext())
        {
            e.Current.Draw(gameTime, spriteBatch);
        }
    }

    public override void Reset()
    {
        base.Reset();
        foreach (StaticGameObject obj in children)
        {
            obj.Reset();
        }
    }
}
