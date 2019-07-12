using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Omnipath
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        #region Fields

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Rectangle screen;
        Texture2D playerTexture;

        Dictionary<Keys, PlayerAction> controlMapping;

        KeyboardState currentkbState;
        KeyboardState previouskbState;

        MouseState currentMouseState;
        MouseState previousMouseState;


        #region GameObjects
        Player player;
        string settingsDataFilePath;
        string playerDataFilePath;
        #endregion

        #region Enums
        GameState gameState;
        #endregion

        #region Managers
        GameObjectManager<NPC> npcManager;
        #endregion

        #endregion

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

            // Locate files from which to read data
            // playerDataFilePath = ;




            // Initalize the screen width and height from the settings file
            // screen = new Rectangle();

            // Create new player and initialize their data from a file
            player = new Player(playerTexture);
            //  If data can't be found and read, prompt the player to make a new character
            if (playerDataFilePath == null || !player.Initialize(/* Pass file to read data from here */))
            {
                // Prompt player to make new character
            }

            // Initialize manager objects
            // npcManager = new GameObjectManager<NPC>();

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

            // Update keyboard and mouse states
            previouskbState = currentkbState;
            previousMouseState = currentMouseState;
            currentkbState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();

            switch(gameState)
            {
                case GameState.Gameplay:
                    // Update the player
                    player.Update(currentkbState, previouskbState, currentMouseState, previousMouseState);
                    // Update managers
                    npcManager.Update();
                    break;

                case GameState.LoadScreen:
                    break;

                case GameState.Menu:
                    break;
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
            
            // TODO: Add your drawing code here

            switch (gameState)
            {
                case GameState.Gameplay:
                    // Draw player
                    player.Draw(spriteBatch);
                    // Draw managers
                    npcManager.Draw(spriteBatch);
                    break;

                case GameState.LoadScreen:
                    break;

                case GameState.Menu:
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
