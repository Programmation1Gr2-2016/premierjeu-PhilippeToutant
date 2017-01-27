using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace SpriteAnime
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState keys = new KeyboardState();
        KeyboardState previousKeys = new KeyboardState();
        GameObjectAnimé rambo;
        GameObjectTile fond;
        int Nombredevie=3;
        int Nombredereussite = 0;

        int gameTimeBest = 99;
        int gameTimeMoyenne=0;
        int gameTimeTotal=0;
        int Wannabe=0;
        SpriteFont font;
        SpriteFont font1;
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
            //this.graphics.PreferredBackBufferWidth=graphics.GraphicsDevice.dis

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            rambo = new GameObjectAnimé();
            rambo.direction = Vector2.Zero;
            rambo.vitesse.X = 2;
            rambo.vitesse.Y = 2;
            rambo.objetState = GameObjectAnimé.etats.attenteDroite;
            rambo.position = new Rectangle(50, 50, 65, 65);   //Position initiale de Rambo
            rambo.sprite = Content.Load<Texture2D>("straight_outta_undertale_chara_frisk_spritesheet_by_toreodere-d9ko1g9.png");

            spriteBatch = new SpriteBatch(GraphicsDevice);
	        fond = new GameObjectTile();
            fond.texture = Content.Load<Texture2D>("hyptosis_tile-art-batch-1.png");

            font = Content.Load<SpriteFont>("Font");
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

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

            keys = Keyboard.GetState();
            rambo.position.X += (int)(rambo.vitesse.X * rambo.direction.X);
            rambo.position.Y += (int)(rambo.vitesse.Y * rambo.direction.Y);

            if (keys.IsKeyDown(Keys.Right))
            {
                rambo.direction.X = rambo.vitesse.X;
                rambo.objetState = GameObjectAnimé.etats.runDroite;
            }
            if (keys.IsKeyUp(Keys.Right) && previousKeys.IsKeyDown(Keys.Right))
            {
                rambo.direction.X = 0;
                rambo.objetState = GameObjectAnimé.etats.attenteDroite;
            }
            if (keys.IsKeyDown(Keys.Left))
            {
                rambo.direction.X =- rambo.vitesse.X;
                rambo.objetState = GameObjectAnimé.etats.runGauche;
            }
            if (keys.IsKeyUp(Keys.Left) && previousKeys.IsKeyDown(Keys.Left))
            {
                rambo.direction.X = 0;
                rambo.objetState = GameObjectAnimé.etats.attenteGauche;
            }
            if (keys.IsKeyDown(Keys.Up))
            {
                rambo.direction.Y = -rambo.vitesse.Y;
                rambo.objetState = GameObjectAnimé.etats.runHaut;
            }
            if (keys.IsKeyUp(Keys.Up) && previousKeys.IsKeyDown(Keys.Up))
            {
                rambo.direction.Y = 0;
                rambo.objetState = GameObjectAnimé.etats.attenteHaut;
            }
            if (keys.IsKeyDown(Keys.Down))
            {
                rambo.direction.Y = rambo.vitesse.Y;
                rambo.objetState = GameObjectAnimé.etats.runBas;
            }
            if (keys.IsKeyUp(Keys.Down) && previousKeys.IsKeyDown(Keys.Down))
            {
                rambo.direction.Y = 0;
                rambo.objetState = GameObjectAnimé.etats.attenteBas;
            }

            if (keys.IsKeyDown(Keys.R))
            {
                Nombredevie = 3;
                Nombredereussite = 0;
                rambo.position = new Rectangle(50, 50, 65, 65);
            }

                        
            // TODO: Add your update logic here
            rambo.Update(gameTime);
            previousKeys = keys;

            for (int ligne = 0; ligne < fond.map.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < fond.map.GetLength(1); colonne++)
                {
                    Rectangle tuile = new Rectangle();
                    tuile.X = colonne * GameObjectTile.LARGEUR_TUILE - (int)(rambo.vitesse.X * rambo.direction.X);
                    tuile.Y = ligne * GameObjectTile.HAUTEUR_TUILE - (int)(rambo.vitesse.Y * rambo.direction.Y);
                    tuile.Width = GameObjectTile.LARGEUR_TUILE;
                    tuile.Height = GameObjectTile.HAUTEUR_TUILE;
                    if (tuile.Intersects(rambo.position))
                    {
                        switch (fond.map[ligne, colonne])
                        {
                            case 1:
                                rambo.direction.X = 0;
                                rambo.direction.Y = 0;
                                break;
                            case 2:// rien faire...

                                break;
                            case 3:
                                rambo.position = new Rectangle(50, 50, 65, 65);
                                Nombredevie = Nombredevie - 1;
                                break;
                            case 4:
                                rambo.position = new Rectangle(50, 50, 65, 65);
                                Nombredereussite ++;

                                gameTimeBest = gameTimeTotal;

                                Wannabe = gameTimeTotal - gameTimeBest;
                                if (gameTimeBest > Wannabe)
                                {
                                    gameTimeBest = Wannabe;
                                }
                                break;
                        }
                    }
                }
            }
            gameTimeTotal = gameTime.TotalGameTime.Seconds;

            if (Nombredevie==0)
            {
                gameTimeMoyenne = gameTimeTotal / Nombredereussite;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
        	fond.Draw(spriteBatch);

            spriteBatch.DrawString(font, "Vie  Reussite bestTime Moyenne\n "+Nombredevie+"       "+Nombredereussite+"       "+ gameTimeBest + "          "+ gameTimeMoyenne , new Vector2(210, 10), Color.White);

            if (Nombredevie>0)
            {
                spriteBatch.Draw(rambo.sprite, rambo.position, rambo.spriteAfficher, Color.White);
            }

            if (Nombredevie == 0)
            {
                spriteBatch.DrawString(font, "GAME OVER!! \n Press R to restart", new Vector2(350,200), Color.Black);
            }
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
