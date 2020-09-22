using System.Collections.Generic;
using JetBrains.Annotations;
using NpgsqlWithOwnedEntities.Lib;

namespace NpgsqlWithOwnedEntities.StockItems
{
        public class Substance : ValueObject
    {
        private Substance()
        { }


        public Substance(int anicorsRegistryNumber, decimal? averageMolWeight, string casRegistryNumber, string description,  string formula,
                         string hazardPictograms, string hazardStatements, bool? isNarcotic, bool? isRadioactive, string iupacName, string molFile)
        {
            AnicorsRegistryNumber = anicorsRegistryNumber;
            AverageMolWeight = averageMolWeight;
            CasRegistryNumber = casRegistryNumber;
            Description = description;
            Formula = formula;
            HazardPictograms = hazardPictograms;
            HazardStatements = hazardStatements;
            IsNarcotic = isNarcotic;
            IsRadioactive = isRadioactive;
            IupacName = iupacName;
            MolFile = molFile;
        }

        public Substance(int anicorsRegistryNumber, Substance substance)
        {
            AnicorsRegistryNumber = anicorsRegistryNumber;
            AverageMolWeight = substance.AverageMolWeight;
            CasRegistryNumber = substance.CasRegistryNumber;
            Description = substance.Description;
            Formula = substance.Formula;
            HazardPictograms = substance.HazardPictograms;
            HazardStatements = substance.HazardStatements;
            IsNarcotic = substance.IsNarcotic;
            IsRadioactive = substance.IsRadioactive;
            IupacName = substance.IupacName;
            MolFile = substance.MolFile;
        }

        public Substance(Substance substance)
        {
            AnicorsRegistryNumber = substance.AnicorsRegistryNumber;
            AverageMolWeight = substance.AverageMolWeight;
            CasRegistryNumber = substance.CasRegistryNumber;
            Description = substance.Description;
            Formula = substance.Formula;
            HazardPictograms = substance.HazardPictograms;
            HazardStatements = substance.HazardStatements;
            IsNarcotic = substance.IsNarcotic;
            IsRadioactive = substance.IsRadioactive;
            IupacName = substance.IupacName;
            MolFile = substance.MolFile;
        }

        public Substance(int anicorsRegistryNumber, string molFile)
        {
            AnicorsRegistryNumber = anicorsRegistryNumber;
            MolFile = molFile;
        }

        public int AnicorsRegistryNumber { get; set; }
        public decimal? AverageMolWeight { get; set; }
        public string CasRegistryNumber { get; set; }

        public decimal? DensityBaseUnitValue { get; set; }
        public string DensityBaseUnit { get; set; }
        public string DensityDisplayUnit { get; set; }
        public string Description { get; set; }
        public string Formula { get; set; }
        public string HazardPictograms { get; set; }
        public string HazardStatements { get; set; }
        public bool? IsNarcotic { get; set; }
        public bool? IsRadioactive { get; set; }
        public string IupacName { get; set; }
        public string MolFile { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            if (AnicorsRegistryNumber != default(int))
            {
                yield return AnicorsRegistryNumber;
            }
            else
            {
                yield return AverageMolWeight;
                yield return CasRegistryNumber;
                yield return Description;
                yield return Formula;
                yield return HazardPictograms;
                yield return HazardStatements;
                yield return IsNarcotic;
                yield return IsRadioactive;
                yield return IupacName;
                yield return MolFile;
            }
        }
    }

}