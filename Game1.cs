using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Time_and_Sound
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D bombTexture;
        Texture2D explosionTexture;
        SpriteFont timeFont;
        float seconds;
        float startTime;
        MouseState mouseState;
        SoundEffectInstance explodeInstance;
        bool exploded;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.ApplyChanges();
            this.Window.Title = "The Bomb";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            exploded = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            bombTexture = Content.Load<Texture2D>("bomb");
            explosionTexture = Content.Load<Texture2D>("ExplosionBG");
            timeFont = Content.Load<SpriteFont>("Time");
            var explode = Content.Load<SoundEffect>("explosion");
            explodeInstance = explode.CreateInstance();
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                startTime = (float)gameTime.TotalGameTime.TotalSeconds;
            }
            
            if (seconds >= 15 && !exploded)
            {
                explodeInstance.Play();
                exploded =  true;   
           
            }
            
            if (explodeInstance.State == SoundState.Stopped && exploded)
            {
                this.Exit();
            }
              
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(bombTexture, new Rectangle(50, 50, 700, 400), Color.White);
            _spriteBatch.DrawString(timeFont, (15-seconds).ToString("00.0"), new Vector2(270, 200), Color.Black);
            if (exploded)
            {
                _spriteBatch.Draw(explosionTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}