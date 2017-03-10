using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShackApi
{
    public class UploadResult
    {
        public class RatingDetails
        {
            public int Ratings { get; set; }
            public double Average { get; set; }
        }

        public class ResolutionDetails
        {
            public int Width { get; set; }
            public int Height { get; set; }
        }

        public class LinkDetails
        {
            public string ImageLink { get; set; }
            public string ThumbLink { get; set; }
        }

        public RatingDetails Rating { get; set; }
        public ResolutionDetails Resolution { get; set; }
        public LinkDetails Links { get; set; }
    }
}
