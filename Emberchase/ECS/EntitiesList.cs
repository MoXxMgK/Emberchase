using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emberchase.ECS
{
    public class EntitiesList
    {
        private World _world;

        private List<Entity> _entities = new List<Entity>();
        private List<Entity> _toAdd = new List<Entity>();
        private List<Entity> _toRemove = new List<Entity>();

        public EntitiesList(World world)
        {
            _world = world;
        }

        public void Add(Entity entity)
        {
            entity.Initialize();
            _toAdd.Add(entity);
        }

        public void Remove(Entity entity)
        {
            if (_toAdd.Contains(entity))
            {
                _toAdd.Remove(entity);
                return;
            }

            _toRemove.Remove(entity);
        }

        public T FindEntity<T>(string name) where T : Entity
        {
            return _entities.First(e => e is T && e.Name == name) as T;
        }

        private void UpdateLists()
        {
            if (_toRemove.Count > 0)
            {
                foreach (Entity entity in _toRemove)
                {
                    entity.OnRemoveFromWorld();
                    entity.World = null;
                    _entities.Remove(entity);
                }

                _toRemove.Clear();
            }

            if (_toAdd.Count > 0)
            {
                foreach (Entity entity in _toAdd)
                {
                    entity.World = _world;
                    entity.OnAddToWorld();
                    _entities.Add(entity);
                }

                _toAdd.Clear();
            }
        }

        public void Update()
        {
            UpdateLists();

            foreach (Entity entity in _entities)
            {
                if (entity.IsActive)
                    entity.Update();
            }
        }
    }
}
