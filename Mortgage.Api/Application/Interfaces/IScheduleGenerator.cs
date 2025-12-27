public interface IScheduleGenerator
{
    public ScheduleDto Generate(Mortgagee mortgage);
    public ScheduleDto Generate(Guid mortgage_Id);
}