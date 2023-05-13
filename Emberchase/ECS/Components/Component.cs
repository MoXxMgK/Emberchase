using Emberchase.ECS.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emberchase.ECS.Components
{
    public abstract class Component
    {
        public Entity Owner { get; set; }

        public Transform Transform => Owner.Transform;

        private bool _isEnabled = true;
        public bool IsEnabled
        {
            get => Owner != null ? Owner.IsActive && _isEnabled : _isEnabled;
            set => SetEnabled(value);
        }

        public virtual void Initialize() { }

        public virtual void OnAddToEntity() { }

        public virtual void OnRemoveFromEntity() { }

        public virtual void Enabled() { }

        public virtual void Disabled() { }

        public Component SetEnabled(bool value)
        {
            if (_isEnabled != value)
            {
                _isEnabled = value;

                if (_isEnabled)
                    Enabled();
                else
                    Disabled();
            }

            return this;
        }
    }
}
