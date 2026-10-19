
namespace GLA_4
{
    public class Asteroid
    {
        public int X {  get; set; }
        public int Y { get; set; }

        //public int VX { get; set; }
        public int VY { get; set; }

        public int Diameter { get; set; }

        public Asteroid(int x, int y, int vy, int diameter)
        {
            X = x;
            Y = y;
            //VX = vx;
            VY = vy;
            Diameter = diameter;
        }

        public Rectangle GetCollide()
        {
            return new Rectangle(X - (Diameter / 2), Y - (Diameter / 2), Diameter, Diameter);
        }




    }
}
