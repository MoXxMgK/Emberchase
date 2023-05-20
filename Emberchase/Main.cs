using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


using Emberchase.ECS;
using Emberchase.ECS.Components;
using Emberchase.Graphics;
using Emberchase.Components;
using Emberchase.Assets;

namespace Emberchase
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private World _testWorld;

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _testWorld = new World();

            base.Initialize();

            var dummy = _testWorld.CreateEntity(new Vector2(0, 0), "Player");
            Texture2D tex = Content.Load<Texture2D>("DummyBall");
            dummy.AddComponent(new SpriteRenderer(tex).AsSize())
                .AddComponent(new SpriteRenderer(new Sprite(tex))
                {
                    LocalOffset = new Vector2(32, 32)
                })
                .AddComponent(new PlayerMovementComponent())
                .AddComponent(new KeepInBounds(GraphicsDevice.Viewport.Bounds))
                .AddComponent(new BoxCollider());
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            Time.Update(gameTime);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic 

            _testWorld.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            _testWorld.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}