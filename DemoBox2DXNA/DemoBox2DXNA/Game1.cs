using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Box2D.XNA;

namespace DemoBox2DXNA
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        float _fps, _eLapsedTime;

        public Random rnd { get; private set; }
        
        BasicEffect basicEffect;
        MouseState mouseState;
        MouseState prevMouseState;
        KeyboardState newState;
        KeyboardState oldState;
        World physicsWorld;
        Texture2D blank;
        bool isDrawing = false;
        Vector2 pointStart;
        Vector2 pointEnd;
        List<Vector2> pointCollideCenter = new List<Vector2>();
        bool isFired = false;
        DebugDraw debugDraw = new DebugDraw();

        SpriteFont spriteFont;
        int width = 800;
        int height = 480;
        float viewZoom = 1.0f;
        Vector2 viewCenter = new Vector2(0.0f, 27.0f);
        int tw, th;

        List<Body> listAfterLaser;
        List<Vector2> listEntryPoint;
        List<Vector2> listExitPoint;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;

            rnd = new Random();
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
            this.IsMouseVisible = true;
            Components.Add(new GamePlay(this));
            _fps = 0; _eLapsedTime = 0;
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
            spriteFont = Content.Load<SpriteFont>("font");

            //physicsWorld = new World(new Vector2(0, 100), true);
            
            //basicEffect = new BasicEffect(GraphicsDevice);
            //basicEffect.VertexColorEnabled = true;

            //DrawRenderer();
            ////addStuff();

            //basicEffect.Projection = Matrix.CreateOrthographicOffCenter
            //            (0, GraphicsDevice.Viewport.Width,     // left, right
            //            GraphicsDevice.Viewport.Height, 0,    // bottom, top
            //            0, 1);   
            ////Resize(GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight);

            //blank = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            //blank.SetData(new[] { Color.White });
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            //    this.Exit();
            //prevMouseState = mouseState;
            //oldState = newState;
            //mouseState = Mouse.GetState();
            //newState = Keyboard.GetState();

            //if (newState.IsKeyDown(Keys.Q) && oldState.IsKeyUp(Keys.Q))
            //{
            //    addBoxW(new Vector2(mouseState.X, mouseState.Y));
            //}

            //if (newState.IsKeyDown(Keys.W) && oldState.IsKeyUp(Keys.W))
            //{
            //    addBoxH(new Vector2(mouseState.X, mouseState.Y));
            //}

            //if (newState.IsKeyDown(Keys.E) && oldState.IsKeyUp(Keys.E))
            //{
            //    addPolygon(new Vector2(mouseState.X, mouseState.Y), 70, 3);
            //}

            //if (newState.IsKeyDown(Keys.R) && oldState.IsKeyUp(Keys.R))
            //{
            //    addPolygon(new Vector2(mouseState.X, mouseState.Y), 40, 6);
            //}

            //if (newState.IsKeyDown(Keys.P) && oldState.IsKeyUp(Keys.P))
            //{
            //    for (Body mBody = physicsWorld.GetBodyList(); mBody!=null; mBody = mBody.GetNext())
            //    {
            //        physicsWorld.DestroyBody(mBody);
            //    }
            //}

            //if (newState.IsKeyDown(Keys.F) && oldState.IsKeyUp(Keys.F))
            //{
            //    addFloor();
            //}

            //if (newState.IsKeyDown(Keys.T) && oldState.IsKeyUp(Keys.T))
            //{
            //    addTri(new Vector2(mouseState.X, mouseState.Y));
            //}

            //if (newState.IsKeyDown(Keys.Y) && oldState.IsKeyUp(Keys.Y))
            //{
            //    addTra(new Vector2(mouseState.X, mouseState.Y));
            //}

            //if (isPressed()){
            //    isDrawing = true;
            //    pointStart = new Vector2(mouseState.X, mouseState.Y);
            //}
            //else if(isPressedMove())
            //{
            //    if (isDrawing)
            //    {
            //        pointEnd = new Vector2(mouseState.X, mouseState.Y);
            //    }
            //}
            //else if (isRelease())
            //{
            //    isDrawing = false;
            //    pointEnd = new Vector2(mouseState.X, mouseState.Y);
            //}

            //physicsWorld.Step((float)(1/30f), 20, 20);
            //physicsWorld.ClearForces();
            //Vector2 r = pointEnd - pointStart;
            //if (!isDrawing && (r.LengthSquared() > 0.0f))
            //{
            //    listAfterLaser = new List<Body>();
            //    listEntryPoint = new List<Vector2>();
            //    listExitPoint = new List<Vector2>();
            //    physicsWorld.RayCast(fired, pointStart, pointEnd);
            //    physicsWorld.RayCast(fired, pointEnd, pointStart);
            //    pointEnd = pointStart = new Vector2();
            //}
            //else
            //{
            //    isFired = false;
            //    pointCollideCenter.Clear();
            //}
            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        //float fired(Fixture fixture, Vector2 point, Vector2 normal, float fraction)
        //{
        //    isFired = true;
        //    Body affectedBody = fixture.GetBody();
        //    PolygonShape affectedPolygon = (PolygonShape)fixture.GetShape();
        //    int fixtureIndex = listAfterLaser.IndexOf(affectedBody);
        //    if (fixtureIndex == -1)
        //    {
        //        listAfterLaser.Add(affectedBody);
        //        listEntryPoint.Add(point);
        //    }
        //    else
        //    {
        //        listExitPoint.Add(point);
        //        Vector2 pointCenter = new Vector2((point.X + listEntryPoint[fixtureIndex].X) / 2, (point.Y + listEntryPoint[fixtureIndex].Y) / 2);
        //        pointCollideCenter.Add(pointCenter);
        //        float rayAngle = (float)Math.Atan2(listEntryPoint[fixtureIndex].Y - point.Y,
        //            listEntryPoint[fixtureIndex].X - point.X);

        //        int numVt = affectedPolygon.GetVertexCount();
        //        Vector2[] polyVertices = new Vector2[numVt];
        //        for (int i = 0; i < numVt; i++)
        //        {
        //            polyVertices[i] = affectedPolygon.GetVertex(i);
        //        }


        //        List<Vector2> newPolyVertices1 = new List<Vector2>();
        //        List<Vector2> newPolyVertices2 = new List<Vector2>();
        //        int currentPoly = 0;
        //        bool cutPlace1 = false;
        //        bool cutPlace2 = false;

        //        for (int i = 0; i < polyVertices.Length; i++)
        //        {
        //            Vector2 worldPoint = affectedBody.GetWorldPoint(polyVertices[i]);
        //            float cutAngel = (float)Math.Atan2(worldPoint.Y - pointCenter.Y, worldPoint.X - pointCenter.X) - rayAngle;
        //            //if (cutAngel < Math.PI*-1)
        //            //{
        //            //    cutAngel += (float)(2 * Math.PI);
        //            //}
        //            //if (cutAngel > 0 && cutAngel <= Math.PI)
        //            //{
        //            //    listPoint1.Add(worldPoint);
        //            //}
        //            //else
        //            //{
        //            //    listPoint2.Add(worldPoint);
        //            //}
        //            if (cutAngel < Math.PI * -1)
        //            {
        //                cutAngel += (float)(2 * Math.PI);
        //            }

        //            if (cutAngel > 0 && cutAngel <= Math.PI)
        //            {
        //                if (currentPoly == 2)
        //                {
        //                    cutPlace1 = true;
        //                    newPolyVertices1.Add(point);
        //                    newPolyVertices1.Add(listEntryPoint[fixtureIndex]);
        //                }
        //                newPolyVertices1.Add(worldPoint);
        //                currentPoly = 1;
        //            }
        //            else
        //            {
        //                if (currentPoly == 1)
        //                {
        //                    cutPlace2 = true;
        //                    newPolyVertices2.Add(listEntryPoint[fixtureIndex]);
        //                    newPolyVertices2.Add(point);
        //                }
        //                newPolyVertices2.Add(worldPoint);
        //                currentPoly = 2;
        //            }
        //        }
        //        if (!cutPlace1)
        //        {
        //            newPolyVertices1.Add(point);
        //            newPolyVertices1.Add(listEntryPoint[fixtureIndex]);
        //        }
        //        if (!cutPlace2)
        //        {
        //            newPolyVertices2.Add(listEntryPoint[fixtureIndex]);
        //            newPolyVertices2.Add(point);
        //        }

        //        createSlice(convertListToArray(newPolyVertices1), newPolyVertices1.Count);
        //        createSlice(convertListToArray(newPolyVertices2), newPolyVertices2.Count);
        //        physicsWorld.DestroyBody(affectedBody);
        //    }
        //    return 1.0f;
        //}
        
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //// TODO: Add your drawing code here
            //basicEffect.Techniques[0].Passes[0].Apply();

            //spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            //physicsWorld.DrawDebugData();
            //debugDraw.FinishDrawShapes();

            //if (isDrawing)
            //{
            //    if (pointStart != Vector2.Zero && pointEnd != Vector2.Zero)
            //    {
            //        DrawLine(spriteBatch, blank, 1, Color.Red, pointStart, pointEnd);
            //    }
            //}

            //if (isFired)
            //{
            //    if (pointCollideCenter.Count > 0)
            //    {
            //        for (int i = 0; i < pointCollideCenter.Count; i++)
            //        {
            //            DrawLine(spriteBatch, blank, 3, Color.Orange, pointCollideCenter.ElementAt(i) + new Vector2(1, 1), pointCollideCenter.ElementAt(i) - new Vector2(1, 1));
            //        }
            //    }

            //    for (int i = 0; i < listEntryPoint.Count; i++)
            //    {
            //        DrawLine(spriteBatch, blank, 3, Color.Purple, listEntryPoint.ElementAt(i) + new Vector2(1, 1), listEntryPoint.ElementAt(i) - new Vector2(1, 1));
            //    }

            //    for (int i = 0; i < listExitPoint.Count; i++)
            //    {
            //        DrawLine(spriteBatch, blank, 3, Color.Red, listExitPoint.ElementAt(i) + new Vector2(1, 1), listExitPoint.ElementAt(i) - new Vector2(1, 1));
            //    }
            //}
            //spriteBatch.End();

            base.Draw(gameTime);
        }

        //private void DrawRenderer()
        //{
        //    DebugDraw._batch = spriteBatch;
        //    DebugDraw._device = GraphicsDevice;
        //    DebugDraw._font = spriteFont;

        //    this.debugDraw.Flags = DebugDrawFlags.AABB |
        //                      DebugDrawFlags.CenterOfMass |
        //                      DebugDrawFlags.Joint |
        //                      DebugDrawFlags.Pair |
        //                      DebugDrawFlags.Shape;

        //    physicsWorld.DebugDraw = debugDraw;
        //}

//        void DrawLine(SpriteBatch batch, Texture2D blank,
//              float width, Color color, Vector2 point1, Vector2 point2)
//        {
//            float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
//            float length = Vector2.Distance(point1, point2);

//            batch.Draw(blank, point1, null, color,
//                       angle, Vector2.Zero, new Vector2(length, width),
//                       SpriteEffects.None, 0);
//        }

//        private bool isPressed()
//        {
//            if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
//            {
//                return true;
//            }
//            else { return false; }
//        }

//        private bool isPressedMove()
//        {
//            if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Pressed)
//            {
//                return true;
//            }
//            else { return false; }
//        }

//        private bool isRelease()
//        {
//            if (mouseState.LeftButton == ButtonState.Released && prevMouseState.LeftButton == ButtonState.Pressed)
//            {
//                return true;
//            }
//            else { return false; }
//        }

//        void Resize(int w, int h)
//        {
//            width = w;
//            height = h;

//            tw = GraphicsDevice.Viewport.Width;
//            th = GraphicsDevice.Viewport.Height;
//            int x = GraphicsDevice.Viewport.X;
//            int y = GraphicsDevice.Viewport.Y;

//            float ratio = (float)tw / (float)th;

//            Vector2 extents = new Vector2(ratio * 25.0f, 25.0f);
//            extents *= viewZoom;

//            Vector2 lower = viewCenter - extents;
//            Vector2 upper = viewCenter + extents;

//            // L/R/B/T
//            basicEffect.Projection = Matrix.CreateOrthographicOffCenter(lower.X, upper.X, lower.Y, upper.Y, -1, 1);
//        }

//        void addFloor()
//        {
//            var floorBody = new BodyDef();
//            floorBody.position = new Vector2(0, 470);
//            var floorShape = new PolygonShape();
//            floorShape.SetAsBox((float)800, 10);
//            var floorFixture = new FixtureDef();
//            floorFixture.shape = floorShape;
//            Body worldFloor = physicsWorld.CreateBody(floorBody);
//            worldFloor.CreateFixture(floorFixture);
//        }

//        void addTri(Vector2 pos)
//        {
//            var tempBody = new BodyDef();
//            tempBody.position = pos - new Vector2(100, 40);
//            tempBody.type = BodyType.Dynamic;
//            var tempShape = new PolygonShape();
//            Vector2[] triVertices = new Vector2[3];
//            triVertices[0] = new Vector2(0, 80);
//            triVertices[1] = new Vector2(100, 0);
//            triVertices[2] = new Vector2(200, 80);
//            tempShape.Set(triVertices, 3);
//            var tempFixture = new FixtureDef();
//            tempFixture.shape = tempShape;
//            tempFixture.density = 1;
//            Body tempFloor = physicsWorld.CreateBody(tempBody);
//            tempFloor.CreateFixture(tempFixture);
//        }

//        void addTra(Vector2 pos)
//        {
//            var tempBody = new BodyDef();
//            tempBody.position = pos - new Vector2(55, 25);
//            tempBody.type = BodyType.Dynamic;
//            var tempShape = new PolygonShape();
//            Vector2[] triVertices = new Vector2[4];
//            triVertices[0] = new Vector2(30, 0);
//            triVertices[3] = new Vector2(0, 50);
//            triVertices[2] = new Vector2(110, 50);
//            triVertices[1] = new Vector2(80, 0);
//            tempShape.Set(triVertices, 4);
//            var tempFixture = new FixtureDef();
//            tempFixture.shape = tempShape;
//            tempFixture.density = 1;
//            Body tempFloor = physicsWorld.CreateBody(tempBody);
//            tempFloor.CreateFixture(tempFixture);
//        }

//        void addBoxH(Vector2 pos)
//        {
//            var tempBody = new BodyDef();
//            tempBody.position = pos;
//            tempBody.type = BodyType.Dynamic;
//            var tempShape = new PolygonShape();
//            tempShape.SetAsBox((float)60, 20);
//            var tempFixture = new FixtureDef();
//            tempFixture.shape = tempShape;
//            tempFixture.density = 1;
//            Body tempFloor = physicsWorld.CreateBody(tempBody);
//            tempFloor.CreateFixture(tempFixture);
//        }

//        void addBoxW(Vector2 pos)
//        {
//            var tempBody = new BodyDef();
//            tempBody.position = pos;
//            tempBody.type = BodyType.Dynamic;
//            var tempShape = new PolygonShape();
//            tempShape.SetAsBox((float)20, 60);
//            var tempFixture = new FixtureDef();
//            tempFixture.shape = tempShape;
//            tempFixture.density = 1;
//            Body tempFloor = physicsWorld.CreateBody(tempBody);
//            tempFloor.CreateFixture(tempFixture);
//        }

//        void addPolygon(Vector2 pos, int radius, int count)
//        {
//            int circleSteps = count;
//            Vector2[] circleList = new Vector2[circleSteps];
//            var circleRadius = radius;
//            for (int i = 0; i < circleSteps; i++)
//            {
//                circleList[i] = new Vector2((float)(circleRadius * Math.Cos(2 * Math.PI / circleSteps * i)), (float)(circleRadius * Math.Sin(2 * Math.PI / circleSteps * i)));
//            }

//            var circleBody = new BodyDef();
//            circleBody.type = BodyType.Dynamic;
//            circleBody.position = pos;
//            var circleShape = new PolygonShape();
//            circleShape.Set(circleList, circleSteps);
//            var circleFixture = new FixtureDef();
//            circleFixture.shape = circleShape;
//            circleFixture.density = 1;
//            Body worldCircle = physicsWorld.CreateBody(circleBody);
//            worldCircle.CreateFixture(circleFixture);
//        }

//        void addStuff()
//        {
//            var floorBody = new BodyDef();
//            floorBody.position = new Vector2(0, 450);
//            var floorShape = new PolygonShape();
//            floorShape.SetAsBox((float)800, 30);
//            var floorFixture = new FixtureDef();
//            floorFixture.shape = floorShape;
//            Body worldFloor = physicsWorld.CreateBody(floorBody);
//            worldFloor.CreateFixture(floorFixture);
//            //
//            var squareBody= new BodyDef();
//            squareBody.type = BodyType.Dynamic;
//            squareBody.position = new Vector2(400, 100);
//            var squareShape = new PolygonShape();
//            squareShape.SetAsBox(100, 80);
//            var squareFixture = new FixtureDef();
//            squareFixture.shape=squareShape;
//            squareFixture.density = 1;
//            Body worldSquare = physicsWorld.CreateBody(squareBody);
//            worldSquare.CreateFixture(squareFixture);
//            //
//            int circleSteps = 8;
//            Vector2[] circleList = new Vector2[circleSteps];
//            var circleRadius = 50;
//            for (int i = 0; i < circleSteps; i++) {
//                circleList[i] = new Vector2((float)(circleRadius * Math.Cos(2 * Math.PI / circleSteps * i)), (float)(circleRadius * Math.Sin(2 * Math.PI / circleSteps * i)));
//            }

//            var circleBody = new BodyDef();
//            circleBody.type = BodyType.Dynamic;
//            circleBody.position = new Vector2(200, 120);
//            var circleShape = new PolygonShape();
//            circleShape.Set(circleList, circleSteps);
//            var circleFixture = new FixtureDef();
//            circleFixture.shape=circleShape;
//            Body worldCircle = physicsWorld.CreateBody(circleBody);
//            worldCircle.CreateFixture(circleFixture);
//            //
//            var triAngleBody = new BodyDef();
//            triAngleBody.position = new Vector2(600, 80);
//            triAngleBody.type = BodyType.Dynamic;
//            var triAngleShape = new PolygonShape();
//            Vector2[] triVertices = new Vector2[3];
//            triVertices[0] = new Vector2(0, 0);
//            triVertices[1] = new Vector2(120, 0);
//            triVertices[2] = new Vector2(120, 120);
//            triAngleShape.Set(triVertices, 3);
//            var triAngleFixture = new FixtureDef();
//            triAngleFixture.shape = triAngleShape;
//            triAngleFixture.density = 1;
//            Body worldTriAngle = physicsWorld.CreateBody(triAngleBody);
//            worldTriAngle.CreateFixture(triAngleFixture);
//            //var circleBody1 = new BodyDef();
//            //circleBody1.position = new Vector2(600, 180);
//            //var circleShape1 = new CircleShape();
//            ////circleShape1._p = new Vector2(600, 180);
//            //circleShape1._radius = 40;
//            //var circleFixture1 = new FixtureDef();
//            //circleFixture1.shape = circleShape1;
//            //Body worldCircle1 = physicsWorld.CreateBody(circleBody1);
//            //worldCircle1.CreateFixture(circleFixture1);
//        }

//        private Vector2 findCentroid(Vector2[] listSegment, int count)
//        {
//            Vector2 c = new Vector2(0.0f, 0.0f);
//            float area = 0.0f;

//            if (count == 2)
//            {
//                c = 0.5f * (listSegment[0] + listSegment[1]);
//                return c;
//            }

//            // pRef is the reference point for forming triangles.
//            // It's location doesn't change the result (except for rounding error).
//            Vector2 pRef = new Vector2(0.0f, 0.0f);
//#if false
//            // This code would put the reference point inside the polygon.
//            for (int i = 0; i < count; ++i)
//            {
//                pRef += vs[i];
//            }
//            pRef *= 1.0f / count;
//#endif

//            const float inv3 = 1.0f / 3.0f;

//            for (int i = 0; i < count; ++i)
//            {
//                // Triangle vertices.
//                Vector2 p1 = pRef;
//                Vector2 p2 = listSegment[i];
//                Vector2 p3 = i + 1 < count ? listSegment[i + 1] : listSegment[0];

//                Vector2 e1 = p2 - p1;
//                Vector2 e2 = p3 - p1;

//                float D = MathUtils.Cross(e1, e2);

//                float triangleArea = 0.5f * D;
//                area += triangleArea;

//                // Area weighted centroid
//                c += triangleArea * inv3 * (p1 + p2 + p3);
//            }

//            // Centroid
//            c *= 1.0f / area;
//            return c;
//        }

//        private void createSlice(Vector2[] vertices, int numVertices) {
//            Vector2 centre = findCentroid(vertices, vertices.Length);
//            for (int i = 0; i < numVertices; i++)
//            {
//                vertices[i] -= centre;
//            }

//            var sliceBody = new BodyDef();
//            sliceBody.position = centre;
//            sliceBody.type = BodyType.Dynamic;
//            var slicePoly = new PolygonShape();
//            if (2 <= numVertices && numVertices <= Settings.b2_maxPolygonVertices)
//            {
//                slicePoly.Set(vertices, numVertices);
//            }
//            var sliceFixture = new FixtureDef();
//            sliceFixture.shape = slicePoly;
//            sliceFixture.density = 1;
//            Body worldSlice = physicsWorld.CreateBody(sliceBody);
//            worldSlice.CreateFixture(sliceFixture);

//            for (int i = 0; i < numVertices; i++)
//            {
//                vertices[i] += centre;
//            }
//        }

//        private Vector2[] convertListToArray(List<Vector2> listVt)
//        {
//            int countLVT = listVt.Count;
//            Vector2[] arrayVT = new Vector2[countLVT];
//            for (int i = 0; i < countLVT; i++)
//            {
//                arrayVT[i] = listVt.ElementAt(i);
//            }

//            return arrayVT;
//        }
    }
}
