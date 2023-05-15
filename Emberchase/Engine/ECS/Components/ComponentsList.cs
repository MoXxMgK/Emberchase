using Emberchase.ECS.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emberchase.ECS.Components
{
    public class ComponentsList
    {
        private Entity _owner;

        private List<Component> _components = new List<Component>();
        private List<IUpdateComponent> _updateComponents = new List<IUpdateComponent>();

        private List<Component> _toAdd = new List<Component>();
        private List<Component> _toRemove = new List<Component>();

        private Dictionary<Type, Component> _cache = new Dictionary<Type, Component>();

        public ComponentsList(Entity owner)
        {
            _owner = owner;
        }

        public void Add(Component component)
        {
            component.Owner = _owner;
            component.Initialize();
            _toAdd.Add(component);
        }

        public void Remove(Component component)
        {
            if (_cache.ContainsKey(component.GetType()))
            {
                _cache.Remove(component.GetType());
            }

            if (_toAdd.Contains(component))
            {
                _toAdd.Remove(component);
                return;
            }

            _toRemove.Add(component);
        }

        public T GetComponent<T>() where T : Component
        {
            T component;

            if (_cache.ContainsKey(typeof(T)))
            {
                component = (T)_cache[typeof(T)];
            }
            else
            {
                component = _components.Find(c => c is T) as T;
                _cache.Add(typeof(T), component);
            }

            return component; 
        }

        public List<T> GetComponents<T>() where T : Component
        {
            return _components.Where(c => c is T).Cast<T>().ToList();
        }

        private void UpdateLists()
        {
            if (_toRemove.Count > 0)
            {
                foreach (var component in _toRemove)
                {
                    component.OnRemoveFromEntity();
                    component.Owner = null;

                    if (component is IUpdateComponent comp)
                    {
                        _updateComponents.Remove(comp);
                    }

                    if (component is IDrawComponent drawable)
                    {
                        _owner.World.RemoveDrawable(drawable);
                    }

                    _components.Remove(component);
                }

                _toRemove.Clear();
            }

            if (_toAdd.Count > 0)
            {
                foreach (var component in _toAdd)
                {
                    if (component is IUpdateComponent comp)
                    {
                        _updateComponents.Add(comp);
                    }

                    if (component is IDrawComponent drawable)
                    {
                        _owner.World.AddDrawable(drawable);
                    }

                    _components.Add(component);
                }

                // Loop one more time to call OnAddToEntity, after all components is set
                foreach (var c in _toAdd)
                    c.OnAddToEntity();

                _toAdd.Clear();
            }
        }

        public void Update()
        {
            UpdateLists();

            foreach(var component in _updateComponents)
            {
                if (component.IsEnabled && (component as Component).IsEnabled)
                    component.Update();
            }
        }
    }
}
