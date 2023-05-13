using Emberchase.ECS.Base;
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
        public static class Renderlayer
        {
            public const int BG3 = -120;
            public const int BG2 = -110;
            public const int BG1 = -100;
            public const int Floor2 = -40;
            public const int Floor1 = -30;
            public const int Walls = -20;
            public const int Main = 10;
        }

        public readonly EntitiesList Entities;

        protected readonly List<IDrawComponent> _drawable = new List<IDrawComponent>();
        protected Dictionary<int, List<IDrawComponent>> _drawableByLayer = new Dictionary<int, List<IDrawComponent>>();

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

        public void AddDrawable(IDrawComponent drawable)
        {
            _drawable.Add(drawable);
            
            if (!_drawableByLayer.ContainsKey(drawable.DrawLayer))
                _drawableByLayer.Add(drawable.DrawLayer, new List<IDrawComponent>());

            _drawableByLayer[drawable.DrawLayer].Add(drawable);
            drawable.OnDrawLayerChanged += OnDrawableLayerChanged;
        }

        public void RemoveDrawable(IDrawComponent drawable)
        {
            _drawable.Remove(drawable);
            _drawableByLayer[drawable.DrawLayer].Remove(drawable);
            drawable.OnDrawLayerChanged -= OnDrawableLayerChanged;
        }

        private void OnDrawableLayerChanged(IDrawComponent drawable, int oldLayer)
        {
            _drawableByLayer[oldLayer].Remove(drawable);

            if (_drawableByLayer[oldLayer].Count == 0)
                _drawableByLayer.Remove(oldLayer);

            AddDrawable(drawable);
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
            foreach (var pair in _drawableByLayer.OrderBy(pair => pair.Key))
            {
                foreach (IDrawComponent drawable in pair.Value)
                {
                    drawable.Draw(batch);
                }
            }
        }
    }
}
