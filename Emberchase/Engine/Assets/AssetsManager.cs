using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Emberchase.Assets
{
    public class AssetsManager : ContentManager
    {
        public AssetsManager(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public AssetsManager(IServiceProvider serviceProvider, string rootDirectory) : base(serviceProvider, rootDirectory) { }

        private GraphicsDevice GetGraphicsDevice()
        {
            var deviceService = ServiceProvider.GetService(typeof(IGraphicsDeviceService)) as IGraphicsDeviceService;
            return deviceService.GraphicsDevice;
        }

        private bool IsXnb(string name) => String.IsNullOrEmpty(Path.GetExtension(name));

        private Stream GetStream(string name)
        {
            return Path.IsPathRooted(name) ? File.OpenRead(name) : TitleContainer.OpenStream(name);
        }

        #region Strong type loaders
        public Texture2D LoadTexture(string name)
        {
            if (IsXnb(name))
                return this.Load<Texture2D>(name);

            if (LoadedAssets.TryGetValue(name, out var asset))
            {
                if (asset is Texture2D tex)
                    return tex;
            }

            using (var stream = GetStream(name))
            {

                var texture = Texture2D.FromStream(GetGraphicsDevice(), stream);
                texture.Name = name;
                LoadedAssets[name] = texture;

                return texture;
            }
        }

        public SoundEffect LoadSound(string name)
        {
            if (IsXnb(name))
                return this.Load<SoundEffect>(name);

            if (LoadedAssets.TryGetValue(name, out var asset))
            {
                if (asset is SoundEffect sound)
                    return sound;
            }

            using (var stream = GetStream(name))
            {
                var sound = SoundEffect.FromStream(stream);
                LoadedAssets[name] = sound;

                return sound;
            }
        }

        public SpriteFont LoadSpriteFont(string name) => Load<SpriteFont>(name);

        // Async loading
        // TODO: Make this work with stron typed loaders
        public void LoadAsync<T>(string name, Action<T> onLoaded)
        {
            var sync = SynchronizationContext.Current;

            Task.Run(() =>
            {
                var asset = this.Load<T>(name);

                if (onLoaded != null)
                {
                    sync.Post((d) =>
                    {
                        onLoaded(asset);
                    }, null);
                }
            });
        }

        public void LoadAsync<T>(IEnumerable<string> names, Action onLoad)
        {
            var sync = SynchronizationContext.Current;

            Task.Run(() =>
            {
                foreach (var name in names)
                    Load<T>(name);

                if (onLoad != null)
                {
                    sync.Post((d) =>
                    {
                        onLoad();
                    }, null);
                }
            });
        }

        #endregion
    }
}
