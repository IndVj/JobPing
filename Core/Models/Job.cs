namespace Core.Models
{
    public class Job
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Url { get; set; } = default!;

        public override bool Equals(object? obj)
        {
            if (obj is not Job other) return false;
            return Url == other.Url;
        }

        public override int GetHashCode()
        {
            return Url.GetHashCode();
        }
    }
}
