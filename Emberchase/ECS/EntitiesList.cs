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

        // May be replace with id or something
        private Dictionary<Type, Entity> _cache = new Dictionary<Type, Entity>();

        public EntitiesList(World world)
        {
            _world = world;
        }

        public void Add(Entity entity)
        {
            entity.World = _world;
            entity.Initialize();
            _toAdd.Add(entity);
        }

        public void Remove(Entity entity)
        {
            if (_cache.ContainsKey(entity.GetType()))
            {
                _cache.Remove(entity.GetType());
            }

            if (_toAdd.Contains(entity))
            {
                _toAdd.Remove(entity);
                return;
            }

            _toRemove.Remove(entity);
        }

        public Entity FindEntityByName(string name)
        {
            return _entities.First(e => e.Name == name);
        }

        public Entity FindEntityById(int id)
        {
            return _entities.First(e => e.Id == id);
        }

        public T GetEntity<T>() where T : Entity
        {
            T entity;

            if (_cache.ContainsKey(typeof(T)))
            {
                entity = (T)_cache[typeof(T)];
            }
            else
            {
                entity = _entities.First(e => e is T) as T;
                if (entity is not Entity)
                {
                    _cache.Add(typeof(T), entity);
                }
            }

            return entity;
        }

        public List<T> GetAllEntities<T>() where T : Entity
        {
            return _entities.Where(e => e is T).Cast<T>().ToList();
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
