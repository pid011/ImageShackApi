using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShackApi
{
    /// <summary>
    /// Uploaded result is gathered here.
    /// </summary>
    public class UploadResult
    {
        /// <summary>
        /// Details of Rating.
        /// </summary>
        public class RatingDetails
        {
            /// <summary>
            /// Image rating.
            /// </summary>
            public int Ratings { get; set; }
            
            /// <summary>
            /// Image average.
            /// </summary>
            public double Average { get; set; }
        }

        /// <summary>
        /// Details of Resolution.
        /// </summary>
        public class ResolutionDetails
        {
            /// <summary>
            /// Image width.
            /// </summary>
            public int Width { get; set; }

            /// <summary>
            /// Image height.
            /// </summary>
            public int Height { get; set; }
        }

        /// <summary>
        /// Details of Link.
        /// </summary>
        public class LinkDetails
        {
            /// <summary>
            /// Image link.
            /// </summary>
            public string ImageLink { get; set; }

            /// <summary>
            /// Image thumbnail link.
            /// </summary>
            public string ThumbLink { get; set; }
        }

        /// <summary>
        /// Image rating.
        /// </summary>
        public RatingDetails Rating { get; set; }

        /// <summary>
        /// Image resolution.
        /// </summary>
        public ResolutionDetails Resolution { get; set; }

        /// <summary>
        /// Links to uploaded images.
        /// </summary>
        public LinkDetails Links { get; set; }
    }
}
