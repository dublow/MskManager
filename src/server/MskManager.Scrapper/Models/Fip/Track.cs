using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MskManager.Scrapper.Models.Fip
{
    public class Track
    {
        public string Uuid { get; set; }
        public string StepId { get; set; }
        public string Title { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public string FatherStepId { get; set; }
        public int StationId { get; set; }
        public string EmbedId { get; set; }
        public string EmbedType { get; set; }
        public int Depth { get; set; }
        public string DiscJockey { get; set; }
        public string Authors { get; set; }
        public string Performers { get; set; }
        public string TitleSlug { get; set; }
        public string Path { get; set; }
        public string LienYoutube { get; set; }
        public string VisuelYoutube { get; set; }
        public string AnneeEditionMusique { get; set; }
        public string SongId { get; set; }
        public string Visual { get; set; }
        public string TitreAlbum { get; set; }
        public string Label { get; set; }
        public string ReleaseId { get; set; }
        public string CoverUuid { get; set; }
    }
}
