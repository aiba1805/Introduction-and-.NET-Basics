namespace BasicsOfOOP.Task2
{
    public class Rectangle : Square, IShape
    {
        public double SideB { get; set; }
        public new double Area()
        {
            return SideB * SideA;
        }

        public new double Perimeter()
        {
            return 2*(SideB + SideA);
        }
    }
}