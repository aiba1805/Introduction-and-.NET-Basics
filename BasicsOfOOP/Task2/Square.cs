namespace BasicsOfOOP.Task2
{
    public class Square : IShape
    {
        public double SideA { get; set; }

        public double Area()
        {
            return SideA * SideA;
        }

        public double Perimeter()
        {
            return SideA * 4;
        }
    }
}