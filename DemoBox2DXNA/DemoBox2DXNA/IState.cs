using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public enum STATEGAME
{
    MAINMENU,
    SLITSCREEN,
    TESTSCREEN,
}

namespace DemoBox2DXNA
{
    class IState
    {
        private STATEGAME m_ID;
        private IPlay m_IPlay;
        private Game m_Game;
        public STATEGAME ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }
        public IPlay Play
        {
            get { return m_IPlay; }
            set { m_IPlay = value; }
        }
        public Game Game
        {
            get { return m_Game; }
            set { m_Game = value; }
        }
        public IState(IPlay _IPlay, Game _game)
        {
            m_IPlay = _IPlay;
            Game = _game;
        }
        public virtual void Init() { }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Render(GameTime gameTime,SpriteBatch _SpriteBatch) { }
        public virtual void Destroy() { }
    }
}
