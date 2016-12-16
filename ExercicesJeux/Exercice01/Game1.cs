using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
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
        GameObject balle;
        GameObject[] tabEnnemis= new GameObject [10];
        GameObject fond;
        bool balleSortie = false;
        SoundEffect son;
        SoundEffectInstance bombe;
        SpriteFont font;
        Random r = new Random();
        int nombreEnnemies = 0;

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

            for (int i = 0; i <tabEnnemis.Length; i++)
            {
                tabEnnemis[i] = new GameObject();
                tabEnnemis[i].estVivant = true;
                tabEnnemis[i].vitesse = 3;
                tabEnnemis[i].sprite = Content.Load<Texture2D>("Bowser.png");
                tabEnnemis[i].position = tabEnnemis[i].sprite.Bounds;
                tabEnnemis[i].position.Offset(350, 200);
                tabEnnemis[i].direction.X = r.Next(-4, 5);
                tabEnnemis[i].direction.Y = r.Next(-4, 5);

            }

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

            fond = new GameObject();
            fond.sprite = Content.Load<Texture2D>("city.png");
            fond.position = fond.sprite.Bounds;
            fond.position.Offset(0, 0);

            font = Content.Load<SpriteFont>("Font");
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
            //bouger
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
            //ennemies
      
            for (int i = 0; i < nombreEnnemies; i++)
            {
                tabEnnemis[i].position.X += (int)tabEnnemis[i].direction.X;
                tabEnnemis[i].position.Y += (int)tabEnnemis[i].direction.Y;

                if (tabEnnemis[i].position.X > fenetre.Right - heros.sprite.Width)
                {
                    tabEnnemis[i].direction.X = -(int)tabEnnemis[i].direction.X;                            
                }
                if (tabEnnemis[i].position.X < fenetre.Left)
                {
                    tabEnnemis[i].direction.X = -(int)tabEnnemis[i].direction.X;            
                }
                if (tabEnnemis[i].position.Y > fenetre.Top)
                {
                    tabEnnemis[i].direction.Y = -(int)tabEnnemis[i].direction.Y ;            
                }
                if (tabEnnemis[i].position.Y < fenetre.Bottom - heros.sprite.Height)
                {
                    tabEnnemis[i].direction.Y = -(int)tabEnnemis[i].direction.Y ;                 
                }

                if (tabEnnemis[i].position.Intersects(heros.position))
                {
                    heros.estVivant = false;
                    bombe.Play();
                }
                else if (balle.position.Intersects(heros.position))
                {
                    heros.estVivant = false;
                    bombe.Play();
                }
                if (balleSortie == true)
                {
                    balle.position.X = tabEnnemis[i].position.X;
                    balle.position.Y = tabEnnemis[i].position.Y + 60;
                    balleSortie = false;
                }
            }
   
            // paramètre de la balle
            if (balle.position.Intersects(heros.position))
            {
                heros.estVivant = false;
                bombe.Play();
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

            if (balle.position.Y > fenetre.Bottom - balle.sprite.Bounds.Height)
            {
                balleSortie = true;
            }
            if (nombreEnnemies * 5 < gameTime.TotalGameTime.Seconds)
            {
                nombreEnnemies++;
            }

            base.Update(gameTime);
        }
        protected void UpdateHeros()
        {
            //protection de sortie
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

            for (int i = 0; i < nombreEnnemies; i++)
            {
                if (tabEnnemis[i].position.X < fenetre.Left)
                {
                    tabEnnemis[i].position.X = fenetre.Left;
                }
                if (tabEnnemis[i].position.Y < fenetre.Top)
                {
                    tabEnnemis[i].position.Y = fenetre.Top;
                }
                if (tabEnnemis[i].position.X > fenetre.Right - tabEnnemis[i].sprite.Width)
                {
                    tabEnnemis[i].position.X = fenetre.Right - tabEnnemis[i].sprite.Width;
                }
                if (tabEnnemis[i].position.Y > fenetre.Bottom - tabEnnemis[i].sprite.Height)
                {
                    tabEnnemis[i].position.Y = fenetre.Bottom - tabEnnemis[i].sprite.Height;
                }
            }
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(fond.sprite, fond.position, Color.White);
            // TODO: Add your drawing code here

            // draw heros
            if (heros.estVivant == true)
            {
                spriteBatch.Draw(heros.sprite, heros.position, Color.White);
            }
            //game over
            if (heros.estVivant == false)
            {
                spriteBatch.DrawString(font, "Game Over!", new Vector2(300, 200), Color.Black);
               // spriteBatch.DrawString(font, GameTime, new Vector2(300, 200), Color.Black);
            }
            //draw ennemis
            for (int i = 0; i < nombreEnnemies; i++)
            {
                spriteBatch.Draw(tabEnnemis[i].sprite, tabEnnemis[i].position, Color.White);
            }

            spriteBatch.Draw(balle.sprite, balle.position, Color.White);

            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
