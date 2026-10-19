// Took help from the Example given by the proffesor in Class.

using System.Drawing.Drawing2D;

namespace GLA_4
{
    public partial class Form1 : Form
    {

        private Rectangle playfield = new Rectangle(120,120,800, 600);
        private Rectangle statsBox = new Rectangle(5,5,180,80);
        private Rectangle ship = new Rectangle(750,690,40,12);


        private const int ShipSpeed = 8;
        private const int AsteroidCount = 18;
        private List<Asteroid> asteroids = new List<Asteroid>();

        private bool leftPressed = false;
        private bool rightPressed = false;

        private int dodged = 0;
        private int hits = 0;
        

        private Random rng = new Random();


        public Form1()
        {
            InitializeComponent();
        }

      
        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < AsteroidCount; i++)
            {
                int diameter = rng.Next(13,30);

                int x = rng.Next(playfield.Left, playfield.Right);
                int y = rng.Next(playfield.Top, playfield.Bottom - 400);

                int vy = rng.Next(2, 6);

                Asteroid a = new Asteroid(x, y, vy, diameter);
                asteroids.Add(a);
            }


            tmrGame.Start();


        }

        private void tmrGame_Tick(object sender, EventArgs e)
        {

            UpdateDame();

            Invalidate();

        }

        private void UpdateDame()
        {
          

            if (leftPressed)
            {
                ship.X -= ShipSpeed;
            }
            if (rightPressed)
            {
                ship.X += ShipSpeed;
            }
            ship.X = Math.Clamp(ship.X, playfield.Left, playfield.Right - ship.Width);

            foreach(Asteroid asteroid in asteroids)
            {
                asteroid.Y += asteroid.VY;

                if(asteroid.Y > playfield.Bottom)
                {
                    dodged++;

                    // moveing the asteroids back to the top once they pass through bottom, so that game does not ends all 18
                    // asteroids are fallen.
                    asteroid.Diameter = rng.Next(16, 40);
                    asteroid.X = rng.Next(playfield.Left, playfield.Right);
                    asteroid.Y = rng.Next(playfield.Top);
                    asteroid.VY = rng.Next(2, 6);
                }
                if (asteroid.GetCollide().IntersectsWith(ship))
                {
                    hits++;

                    // moveing the asteroids back to the top once they hit the ship, so that game does not ends all 18
                    // asteroids are fallen.
                    asteroid.Diameter = rng.Next(13, 30);
                    asteroid.X = rng.Next(playfield.Left, playfield.Right);
                    asteroid.Y = rng.Next(playfield.Top);
                    asteroid.VY = rng.Next(2, 6);
                }

                
            }

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            DrawPlayfield(g);

            DrawStatusBox(g);
           

        }

        private void DrawPlayfield(Graphics g)
        {
            g.FillRectangle(Brushes.Black, playfield);
            g.DrawRectangle(Pens.White, playfield);

            g.SetClip(playfield);

            foreach(Asteroid asteroid in asteroids)
            {
               // g.TranslateTransform(asteroid.X, asteroid.Y);

                g.FillEllipse(Brushes.Red, asteroid.GetCollide());
                g.DrawEllipse(Pens.White, asteroid.GetCollide());

               // g.ResetTransform();

            }
           
            // Designing Ship 
            // Came up with the numbers by try and error.

            g.FillRectangle(Brushes.Red, ship.Left, ship.Top - 13, 60, 12);
            g.DrawRectangle(Pens.White, ship.Left, ship.Top - 13, 60, 12);

            g.FillEllipse(Brushes.Yellow, ship.Left + 2, ship.Top -10, 6, 6);
            g.FillEllipse(Brushes.Yellow, ship.Left + 52, ship.Top -10, 6, 6);

            g.FillRectangle(Brushes.Silver, ship.Left + 20, ship.Top - 35, 20, 40);
            g.DrawRectangle(Pens.White, ship.Left + 20, ship.Top - 35, 20, 40);

            g.FillEllipse(Brushes.DeepSkyBlue, ship.Left + 25, ship.Top - 27, 10, 16);
            g.DrawEllipse(Pens.White, ship.Left + 25, ship.Top - 27, 10, 16);

            g.FillRectangle(Brushes.DimGray, ship.Left + 4, ship.Top, 6, 6);
            g.FillRectangle(Brushes.DimGray, ship.Left + 50, ship.Top, 6, 6);




            g.ResetClip();

        }


        private void DrawStatusBox(Graphics g)
        {
            g.FillRectangle(Brushes.Black, statsBox);
            g.DrawRectangle(Pens.White, statsBox);

            using (Font font = new Font("Arial", 10f))
            {
                g.DrawString($"Dodged : {dodged} \nHits : {hits}", font, Brushes.White, statsBox.Left + 8, statsBox.Top + 8);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                leftPressed = true;
            }
            if (e.KeyCode == Keys.Right) 
            { 

                 rightPressed = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                leftPressed = false;
            }
            if(e.KeyCode == Keys.Right)
            {
                rightPressed= false;
            }
        }
    }
}
