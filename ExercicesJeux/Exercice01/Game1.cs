using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Exercice01
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle fenetre;
        GameObject heros;
        GameObject bowser;
        GameObject balle;
        GameObject nombreEnnemis;
        GameObject front;
        bool balleSortie = false;
        SoundEffect son;
        SoundEffectInstance bombe;
        SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            fenetre = graphics.GraphicsDevice.Viewport.Bounds;

            heros = new GameObject();
            heros.estVivant = true;
            heros.vitesse = 10;
            heros.sprite = Content.Load<Texture2D>("Mario.png");
            heros.position = heros.sprite.Bounds;
            heros.position.Offset(0, fenetre.Height - heros.position.Height);

            bowser = new GameObject();
            bowser.estVivant = true;
            bowser.vitesse = 3;
            bowser.sprite = Content.Load<Texture2D>("Bowser.png");
            bowser.position = bowser.sprite.Bounds;
            bowser.position.Offset(100, 0);         

            balle = new GameObject();
            balle.estVivant = true;
            balle.vitesse = 3;
            balle.sprite = Content.Load<Texture2D>("120px-Spiny_Egg_PMttyd.png");
            balle.position = balle.sprite.Bounds;
            balle.position.Offset(100, 0);

            son = Content.Load<SoundEffect>("bombe");
            bombe = son.CreateInstance();
            Song song = Content.Load<Song>("DARK SOULS SONG - YOU DIED!");
            MediaPlayer.Play(song);

            front = new GameObject();
            front.sprite = Content.Load<Texture2D>("city.png");
            front.position = front.sprite.Bounds;
            front.position.Offset(0, 0);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {

            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                heros.position.Y += heros.vitesse;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                heros.position.X += heros.vitesse;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                heros.position.Y -= heros.vitesse;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                heros.position.X -= heros.vitesse;
            }

            UpdateHeros();
            for (int nombreEnnemis = 0; nombreEnnemis < 10; nombreEnnemis++)
            {
                if (bowser.position.X == 0)
                {
                    bowser.vitesse = 5;
                }
                if (bowser.position.X == fenetre.Right - bowser.sprite.Width)
                {
                    bowser.vitesse = -5;
                }
                bowser.position.X += bowser.vitesse;

                if (bowser.position.Intersects(heros.position))
                {
                    heros.estVivant = false;
                    bombe.Play();
                }
                else if (balle.position.Intersects(heros.position))
                {
                    heros.estVivant = false;
                    bombe.Play();
                }
            }

            if (balle.position.Y > fenetre.Bottom)
            {
                for (int i = 1; i < 11; i++)
                {
                    balle.position.Y = fenetre.Top - balle.sprite.Height;
                    balle.vitesse *= -1;
                    balle.vitesse++;
                }
            }
            else
            {
                balle.position.Y += balle.vitesse;
            }
            if (balleSortie == false)
            {
                balle.vitesse = 5;
                balle.position.Y += balle.vitesse;
            }
            if (balleSortie == true)
            {
                balle.position.X = bowser.position.X;
                balle.position.Y = bowser.position.Y + 60;
                balleSortie = false;
            }
            if (balle.position.Y > fenetre.Bottom - balle.sprite.Bounds.Height)
            {
                balleSortie = true;
            }

            base.Update(gameTime);
        }
        protected void UpdateHeros()
        {
            if (heros.position.X < fenetre.Left)
            {
                heros.position.X = fenetre.Left;
            }
            if (heros.position.Y < fenetre.Top)
            {
                heros.position.Y = fenetre.Top;
            }
            if (heros.position.X > fenetre.Right - heros.sprite.Width)
            {
                heros.position.X = fenetre.Right - heros.sprite.Width;
            }
            if (heros.position.Y > fenetre.Bottom - heros.sprite.Height)
            {
                heros.position.Y = fenetre.Bottom - heros.sprite.Height;
            }

            if (bowser.position.X < fenetre.Left)
            {
                bowser.position.X = fenetre.Left;
            }
            if (bowser.position.Y < fenetre.Top)
            {
                bowser.position.Y = fenetre.Top;
            }
            if (bowser.position.X > fenetre.Right - bowser.sprite.Width)
            {
                bowser.position.X = fenetre.Right - bowser.sprite.Width;
            }
            if (bowser.position.Y > fenetre.Bottom - bowser.sprite.Height)
            {
                bowser.position.Y = fenetre.Bottom - bowser.sprite.Height;
            }

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(front.sprite, front.position, Color.White);

            // TODO: Add your drawing code here
            if (heros.estVivant == true)
            {
                spriteBatch.Draw(heros.sprite, heros.position, Color.White);
            }

            for (int nombreEnnemis = 0; nombreEnnemis < 10; nombreEnnemis++)
            {
                spriteBatch.Draw(bowser.sprite, bowser.position, Color.White);
            }
            spriteBatch.Draw(balle.sprite, balle.position, Color.White);


            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
