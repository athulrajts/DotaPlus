namespace DotaPlus.Core.Models
{
    public class TalentTree
    {
        public (TalentOption Left, TalentOption Right) Level10 { get; set; }
        public (TalentOption Left, TalentOption Right) Level15 { get; set; }
        public (TalentOption Left, TalentOption Right) Level20 { get; set; }
        public (TalentOption Left, TalentOption Right) Level25 { get; set; }
    }
}
