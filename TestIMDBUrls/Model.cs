using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIMDBUrls
{
    public class Model
    {
        public string Id { get; set; }
        public string ImdbId { get; set; }
        public string OriginalTitle { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TrailerLink { get; set; }
        public string TrailerEmbedCode { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string Genre { get; set; }
        public int RatingCount { get; set; }
        public double Rating { get; set; }
        public string CensorRating { get; set; }
        public string ReleaseDate { get; set; }
        //public int Runtime { get; set; }
        //public int Budget { get; set; }
        //public int Revenue { get; set; }
        public string PosterPath { get; set; }
    }
}
