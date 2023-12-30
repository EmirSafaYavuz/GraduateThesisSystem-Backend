using NpgsqlTypes;

namespace DataAccess.Entities.Enums;

public enum ThesisType
{
    [PgName("Master")]
    Master,
    [PgName("Doctorate")]
    Doctorate,
    [PgName("Specialization in Medicine")]
    SpecializationInMedicine,
    [PgName("Proficiency in Art")]
    ProficiencyInArt
}

public static class ThesisTypeExtensions
{
    public static string GetThesisTypeAsString(this ThesisType thesisType)
    {
        switch (thesisType)
        {
            case ThesisType.Master:
                return "Master";
            case ThesisType.Doctorate:
                return "Doctorate";
            case ThesisType.SpecializationInMedicine:
                return "Specialization in Medicine";
            case ThesisType.ProficiencyInArt:
                return "Proficiency in Art";
            default:
                throw new ArgumentOutOfRangeException(nameof(thesisType), thesisType, null);
        }
    }
}