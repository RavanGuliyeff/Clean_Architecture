using HR.LeaveManagement.Domain.Common;

namespace HR.LeaveManagement.Application.Exceptions
{
    public class NotFoundException<T> : Exception where T : BaseEntity, new()
    {
        public NotFoundException(object key) : base($"{typeof(T).Name} with key ({key}) was not found!") { }
    }
}
