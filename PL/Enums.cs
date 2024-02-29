using System.Collections;

namespace PL;
internal class ChefExperiencesCollection : IEnumerable
{
    static readonly IEnumerable<BO.ChefExperience> s_enums =
    (Enum.GetValues(typeof(BO.ChefExperience)) as IEnumerable<BO.ChefExperience>)!;
    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}

internal class ChefExperiencesCollectionFilter : IEnumerable
{
    static readonly IEnumerable<BO.ChefExperienceFilter> s_enums =
    (Enum.GetValues(typeof(BO.ChefExperienceFilter)) as IEnumerable<BO.ChefExperienceFilter>)!;
    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}