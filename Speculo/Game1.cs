﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Speculo.Screens;
using System;
using System.Collections.Generic;

namespace Speculo
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteBatch hudSpriteBatch;



        Utility.SharedVariables sharedVariables = Utility.SharedVariables.Instance;

        //screens

        List<BaseScreen> Screens = new List<BaseScreen>();
        public MenuScreen MenuScreen;
        public GameScreen GameScreen;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.SynchronizeWithVerticalRetrace = false;
            TargetElapsedTime = TimeSpan.FromTicks(66666);//fps
            IsMouseVisible = true;
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

            graphics.PreferredBackBufferWidth = 1680;
            graphics.PreferredBackBufferHeight = 950;
            graphics.ApplyChanges();


            sharedVariables.Content = Content;
            sharedVariables.Graphics = GraphicsDevice;
            sharedVariables.GraphicsManager = graphics;

            
            sharedVariables.initVariables();

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

            hudSpriteBatch = new SpriteBatch(GraphicsDevice);

            MenuScreen = new MenuScreen(this);
            MenuScreen.IsActive = true;

            GameScreen = new GameScreen(this);

            Screens.Add(GameScreen);
            Screens.Add(MenuScreen);
            

            //sharedVariables.Hud.LoadContent(Content);
            sharedVariables.Hud.ShowHud = true;

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
        /// 
        void UpdateScreen(int width, int height)
        {
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;
            graphics.ApplyChanges();
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            Screens.ForEach(s =>
            {
                if (s.IsActive)
                {
                    s.Update(gameTime);
                }     
            });

            //sharedVariables.CharacterClass.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);


            // TODO: Add your drawing code here
            //spriteBatch.Begin();
            //sharedVariables.CharacterClass.Draw(spriteBatch);
            //spriteBatch.End();

            Screens.ForEach(s =>
            {
                if (s.IsActive)
                {
                    s.Draw(spriteBatch);
                }
            });

            hudSpriteBatch.Begin();
            sharedVariables.Hud.Draw(hudSpriteBatch);
            hudSpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
