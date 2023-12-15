namespace Day15
{
    internal class Lens
    {
        public string Label { get; }
        public int FocalLength { get; set; }

        public Lens(string label, int focalLength)
        {
            this.Label = label;
            this.FocalLength = focalLength;
        }
    }
}
