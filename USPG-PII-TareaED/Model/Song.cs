using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USPG_PII_TareaED.Model
{
    internal class Song
    {
        public Guid Id { get; private set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public int DurationInSeconds { get; set; }

        public Song(string title, string artist, int durationInSeconds)
        {
            Id = Guid.NewGuid();
            Title = title;
            Artist = artist;
            DurationInSeconds = durationInSeconds;
        }

        public override string ToString()
        {
            return $"'{Title}' by {Artist}";
        }
    }
}
