namespace BlApi;

/// <summary>
/// Chef interface
/// </summary>
public interface IChef
{
    public int Create(BO.Chef item);
    public BO.Chef? Read(int id);
    public IEnumerable<BO.Chef> ReadAll();
    public void Update(BO.Chef item);
    public void Delete(int id);
    //public BO.StudentInCourse GetDetailedCourseForStudent(int StudentId, int CourseId);
}
