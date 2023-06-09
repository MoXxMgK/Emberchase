﻿using Emberchase.ECS.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Immutable;

namespace Emberchase.ECS
{
    public class World
    {
        public readonly EntitiesList Entities;

        protected readonly List<IDrawComponent> _drawable = new List<IDrawComponent>();

        public World()
        {
            Entities = new EntitiesList(this);
        }

        public Entity CreateEntity()
        {
            Entity entity = new Entity();
            Entities.Add(entity);
            return entity;
        }

        public Entity CreateEntity(Vector2 position)
        {
            Entity entity = new Entity();
            entity.Position = position;
            Entities.Add(entity);   
            return entity;
        }

        public Entity CreateEntity(Vector2 position, string name)
        {
            Entity entity = new Entity(name);
            entity.Position = position;
            Entities.Add(entity);
            return entity;
        }

        public Entity CreateEntity(string name) => CreateEntity(Vector2.Zero, name);

        public void AddDrawable(IDrawComponent drawable)
        {
            _drawable.Add(drawable);
            _drawable.Sort();
        }

        public void RemoveDrawable(IDrawComponent drawable)
        {
            _drawable.Remove(drawable);
        }

        public void AddEntity(Entity entity)
        {
            Entities.Add(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            Entities.Remove(entity);
        }

        public virtual void Update()
        {
            Entities.Update();
        }

        public virtual void Draw(SpriteBatch batch)
        {
            // May be i should sort dictioary every time i add a new drawable???
            foreach (var d in _drawable)
            {
                d.Draw(batch);
            }
        }
    }
}
