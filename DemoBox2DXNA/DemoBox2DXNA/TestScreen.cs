using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Box2D.XNA;

namespace DemoBox2DXNA
{
    class TestScreen: IState
    {
        BasicEffect basicEffect;
        MouseState mouseState;
        MouseState prevMouseState;
        KeyboardState newState;
        KeyboardState oldState;
        SpriteFont spriteFont;
        Texture2D blank;
        World physicsWorld;
        DebugDraw debugDraw = new DebugDraw();

        private void DrawRenderer()
        {
            DebugDraw._batch = Play.Spritebatch;
            DebugDraw._device = Game.GraphicsDevice;
            DebugDraw._font = spriteFont;

            this.debugDraw.Flags = DebugDrawFlags.AABB |
                              DebugDrawFlags.CenterOfMass |
                              DebugDrawFlags.Joint |
                              DebugDrawFlags.Pair |
                              DebugDrawFlags.Shape;

            physicsWorld.DebugDraw = debugDraw;
        }

        public TestScreen(IPlay iPlay, Game game)
            : base(iPlay, game)
        {
            ID = STATEGAME.TESTSCREEN;
        }

        public override void Init()
        {
            spriteFont = Game.Content.Load<SpriteFont>("font");

            physicsWorld = new World(new Vector2(0, 100), true);

            basicEffect = new BasicEffect(Game.GraphicsDevice);
            basicEffect.VertexColorEnabled = true;
            DrawRenderer();
            basicEffect.Projection = Matrix.CreateOrthographicOffCenter
                        (0, Game.GraphicsDevice.Viewport.Width,     // left, right
                        Game.GraphicsDevice.Viewport.Height, 0,    // bottom, top
                        0, 1);
            //Resize(GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight);

            blank = new Texture2D(Game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            blank.SetData(new[] { Color.White });

            base.Init();
        }

        public override void Update(GameTime gameTime)
        {
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();
            oldState = newState;
            newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.Tab) && oldState.IsKeyUp(Keys.Tab))
            {
                Play.NextState = new MainMenu(Play, Play.Game);
            }

            if (newState.IsKeyDown(Keys.F) && oldState.IsKeyUp(Keys.F))
            {
                addFloor();
            }

            if (newState.IsKeyDown(Keys.S) && oldState.IsKeyUp(Keys.S))
            {
                addStaticSquare(new Vector2(mouseState.X, mouseState.Y));
            }

            if (newState.IsKeyDown(Keys.D) && oldState.IsKeyUp(Keys.D))
            {
                addDynamicSquare(new Vector2(mouseState.X, mouseState.Y));
            }

            if (newState.IsKeyDown(Keys.M) && oldState.IsKeyUp(Keys.M))
            {
                addKinematicSquareCanMove(new Vector2(mouseState.X, mouseState.Y), new Vector2(5, 10));
            }

            if (newState.IsKeyDown(Keys.K) && oldState.IsKeyUp(Keys.K))
            {
                addKinematicSquare(new Vector2(mouseState.X, mouseState.Y));
            }

            if (newState.IsKeyDown(Keys.C) && oldState.IsKeyUp(Keys.C))
            {
                addStaticCircle(new Vector2(mouseState.X, mouseState.Y), 10);
            }

            if (newState.IsKeyDown(Keys.A) && oldState.IsKeyUp(Keys.A))
            {
                addMutiFixture(new Vector2(mouseState.X, mouseState.Y));
            }

            if (newState.IsKeyDown(Keys.W) && oldState.IsKeyUp(Keys.W))
            {
                addDynamicCircle(new Vector2(mouseState.X, mouseState.Y), 30);
            }

            if (newState.IsKeyDown(Keys.P) && oldState.IsKeyUp(Keys.P))
            {
                for (Body mBody = physicsWorld.GetBodyList(); mBody != null; mBody = mBody.GetNext())
                {
                    physicsWorld.DestroyBody(mBody);
                }
            }
            physicsWorld.Step((float)(gameTime.ElapsedGameTime.TotalSeconds), 10, 3);
            //physicsWorld.ClearForces();
            base.Update(gameTime);
        }

        public override void Render(GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch _SpriteBatch)
        {
            SpriteBatch spriteBatch = Play.Spritebatch;
            Game.GraphicsDevice.Clear(Color.Black);

            basicEffect.Techniques[0].Passes[0].Apply();

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            physicsWorld.DrawDebugData();
            debugDraw.FinishDrawShapes();

            String msg = "Press S: Static, D: Dynamic, K: Kinematic, M: Kinematic Move";
            Vector2 textSize = spriteFont.MeasureString(msg);
            spriteBatch.DrawString(spriteFont, msg, new Vector2(400 - textSize.X / 2, 20), Color.Red);

            spriteBatch.End();
            base.Render(gameTime, _SpriteBatch);
        }

        void addFloor()
        {
            var floorBody = new BodyDef();
            floorBody.type = BodyType.Static;
            floorBody.position = new Vector2(0, 470);
            var floorShape = new PolygonShape();
            floorShape.SetAsBox((float)800, 10);
            var floorFixture = new FixtureDef();
            floorFixture.shape = floorShape;
            Body worldFloor = physicsWorld.CreateBody(floorBody);
            worldFloor.CreateFixture(floorFixture); 
        }

        void addStaticSquare(Vector2 pos)
        {
            var tempbody = new BodyDef();
            tempbody.type = BodyType.Static;
            tempbody.position = new Vector2(pos.X, pos.Y);
            var floorShape = new PolygonShape();
            floorShape.SetAsBox((float)10, 10);
            var floorFixture = new FixtureDef();
            floorFixture.shape = floorShape;
            Body worldFloor = physicsWorld.CreateBody(tempbody);
            worldFloor.CreateFixture(floorFixture);
        }

        void addDynamicSquare(Vector2 pos)
        {
            var tempbody = new BodyDef();
            tempbody.type = BodyType.Dynamic;
            tempbody.position = new Vector2(pos.X, pos.Y);
            var floorShape = new PolygonShape();
            floorShape.SetAsBox((float)10, 10);
            var floorFixture = new FixtureDef();
            floorFixture.shape = floorShape;
            Body worldFloor = physicsWorld.CreateBody(tempbody);
            worldFloor.CreateFixture(floorFixture);
        }

        void addKinematicSquare(Vector2 pos)
        {
            var tempbody = new BodyDef();
            tempbody.type = BodyType.Kinematic;
            tempbody.position = new Vector2(pos.X, pos.Y);
            var floorShape = new PolygonShape();
            floorShape.SetAsBox((float)10, 10);
            var floorFixture = new FixtureDef();
            floorFixture.shape = floorShape;
            Body worldFloor = physicsWorld.CreateBody(tempbody);
            worldFloor.CreateFixture(floorFixture);
            worldFloor.SetAngularVelocity(0.2f);
        }

        void addKinematicSquareCanMove(Vector2 pos, Vector2 veloc)
        {
            var tempbody = new BodyDef();
            tempbody.type = BodyType.Kinematic;
            tempbody.position = new Vector2(pos.X, pos.Y);
            var floorShape = new PolygonShape();
            floorShape.SetAsBox((float)10, 10);
            var floorFixture = new FixtureDef();
            floorFixture.shape = floorShape;
            Body worldFloor = physicsWorld.CreateBody(tempbody);
            worldFloor.CreateFixture(floorFixture);
            worldFloor.SetLinearVelocity(veloc);
        }

        void addStaticCircle(Vector2 pos, int radius)
        {
            var tempbody = new BodyDef();
            tempbody.type = BodyType.Static;
            tempbody.position = new Vector2(pos.X, pos.Y);
            var floorShape = new CircleShape();
            floorShape._radius = radius;
            floorShape._p = new Vector2(0, 0);
            var floorFixture = new FixtureDef();
            floorFixture.shape = floorShape;
            Body worldFloor = physicsWorld.CreateBody(tempbody);
            worldFloor.CreateFixture(floorFixture);
        }

        void addMutiFixture(Vector2 pos)
        {
            var tempbody = new BodyDef();
            tempbody.type = BodyType.Dynamic;
            tempbody.position = new Vector2(pos.X, pos.Y);

            var floorShape = new PolygonShape();
            floorShape.SetAsBox(20, 10);

            var floorShape1 = new PolygonShape();
            floorShape1.SetAsBox(10, 20);

            var floorShape2 = new CircleShape();
            floorShape2._radius = 20;

            var floorFixture = new FixtureDef();
            floorFixture.shape = floorShape;

            var floorFixture1 = new FixtureDef();
            floorFixture1.shape = floorShape1;

            var floorFixture2 = new FixtureDef();
            floorFixture2.shape = floorShape2;

            Body worldFloor = physicsWorld.CreateBody(tempbody);
            worldFloor.CreateFixture(floorFixture);
            worldFloor.CreateFixture(floorFixture1);
            worldFloor.CreateFixture(floorFixture2);
        }

        void addDynamicCircle(Vector2 pos, int radius)
        {
            var tempbody = new BodyDef();
            tempbody.type = BodyType.Dynamic;
            tempbody.position = new Vector2(pos.X, pos.Y);
            var floorShape = new CircleShape();
            floorShape._radius = radius;
            floorShape._p = new Vector2(0, 0);
            var floorFixture = new FixtureDef();
            floorFixture.shape = floorShape;
            floorFixture.restitution = 0.8f;
            Body worldFloor = physicsWorld.CreateBody(tempbody);
            worldFloor.CreateFixture(floorFixture);
        }

        public override void Destroy()
        {
            base.Destroy();
        }
    }
}
