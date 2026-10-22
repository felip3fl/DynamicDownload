namespace Core.Model
{
    public class Video
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime RealizeDate { get; set; }

        public override string ToString()
        {
            return $"Name: {Name} - Url: {Url} - RealizeDate: {RealizeDate}";
        }

    }
}