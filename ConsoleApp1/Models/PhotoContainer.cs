using System.Drawing;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace ConsoleApp1.Models
{
    public class PhotoContainer
    {
        public int Id { get; set; }
        public Image Picture { get; set; }
        public string FileName { get; set; }
    }
}