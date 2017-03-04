using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DemoBox2DXNA
{
    class IPlay : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private IState m_CurState;
        private IState m_NextState;
        private SpriteBatch m_SpriteBatch;
        public IState CurrentState
        {
            get { return m_CurState; }
            set { m_CurState = value; }
        }
        public IState NextState
        {
            get { return m_NextState; }
            set { m_NextState = value; }
        }
        public SpriteBatch Spritebatch
        {
            get { return m_SpriteBatch; }
            set { m_SpriteBatch = value;}
        }
        public IPlay(Game game)
            : base(game)
        {
            Spritebatch = new SpriteBatch(game.GraphicsDevice);
            // TODO: Construct any child components here
        }
        
        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
