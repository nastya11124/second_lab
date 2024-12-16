using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2.CoreSpace.DataBase
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Track> Tracks { get; set; } = new List<Track>();
    }

    public class Track
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ArtistId { get; set; }
    }
}
