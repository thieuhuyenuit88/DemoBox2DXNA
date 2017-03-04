using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace DemoBox2DXNA
{
    class MainMenu : IState
    {
        KeyboardState newState;
        KeyboardState oldState;
        SpriteFont spriteFont;

        public MainMenu(IPlay iPlay, Game game)
            : base(iPlay, game)
        {
            ID = STATEGAME.MAINMENU;
        }

        public override void Init()
        {
            spriteFont = Game.Content.Load<SpriteFont>("font");
            base.Init();
        }

        public override void Update(GameTime gameTime)
        {
            oldState = newState;
            newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.S) && oldState.IsKeyUp(Keys.S))
            {
                Play.NextState = new SplitScreen(Play, Play.Game);
            }

            if (newState.IsKeyDown(Keys.B) && oldState.IsKeyUp(Keys.B))
            {
                Play.NextState = new TestScreen(Play, Play.Game);
            }
            base.Update(gameTime);
        }

        public override void Render(GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch _SpriteBatch)
        {
            SpriteBatch spriteBatch = Play.Spritebatch;
            Game.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            String msg = "Press S to come to Split Demo";
            Vector2 textSize = spriteFont.MeasureString(msg);
            spriteBatch.DrawString(spriteFont, msg, new Vector2(400 - textSize.X / 2, 40), Color.Red);

            msg = "Press B to come to Basic Demo";
            textSize = spriteFont.MeasureString(msg);
            spriteBatch.DrawString(spriteFont, msg, new Vector2(400 - textSize.X / 2, 20), Color.Red);
            spriteBatch.End();
            base.Render(gameTime, _SpriteBatch);
        }

        public override void Destroy()
        {
            base.Destroy();
        }
    }
}
