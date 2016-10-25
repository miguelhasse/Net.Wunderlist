using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Wunderlist
{
    public interface IFileInfo
    {
        Task<File> GetAsync(uint id, CancellationToken cancellationToken);

        Task<IEnumerable<File>> GetByListAsync(uint listId, CancellationToken cancellationToken);

        Task<IEnumerable<File>> GetByTaskAsync(uint taskId, CancellationToken cancellationToken);

        Task<Uri> GetPreviewAsync(uint id, PreviewPlatform? platform, bool retina, CancellationToken cancellationToken);

        Task<File> CreateAsync(uint taskId, string filename, CancellationToken cancellationToken);

        Task DeleteAsync(uint id, int revision, CancellationToken cancellationToken);
    }

    public enum PreviewPlatform { Web, Windows, Mac, iPhone, iPad, Android }

    public interface IFolderInfo
    {
        Task<Folder> GetAsync(uint id, CancellationToken cancellationToken);

        Task<IEnumerable<Folder>> GetAsync(CancellationToken cancellationToken);

        Task<Folder> CreateAsync(string name, IEnumerable<int> ids, CancellationToken cancellationToken);

        Task<Folder> UpdateAsync(uint id, int revision, string name, IEnumerable<int> ids, CancellationToken cancellationToken);

        Task DeleteAsync(uint id, int revision, CancellationToken cancellationToken);
    }

    public interface IListInfo
    {
        Task<List> GetAsync(uint id, CancellationToken cancellationToken);

        Task<IEnumerable<List>> GetAsync(CancellationToken cancellationToken);

        Task<Positions> GetPositionAsync(uint id, CancellationToken cancellationToken);

        Task<IEnumerable<Positions>> GetPositionsAsync(CancellationToken cancellationToken);

        Task<bool> ChangeStateAsync(uint id, int revision, bool makePublic, CancellationToken cancellationToken);

        Task<List> CreateAsync(string name, CancellationToken cancellationToken);

        Task<List> UpdateAsync(uint id, int revision, string name, CancellationToken cancellationToken);

        Task<Positions> UpdatePositionAsync(uint id, int revision, IEnumerable<int> positions, CancellationToken cancellationToken);

        Task DeleteAsync(uint id, int revision, CancellationToken cancellationToken);
    }

    public interface IMembershipInfo
    {
        Task<IEnumerable<Membership>> GetAsync(uint? listId, CancellationToken cancellationToken);

        Task<Membership> CreateAsync(uint listId, string email, bool muted, CancellationToken cancellationToken);

        Task<Membership> CreateAsync(uint listId, uint userId, bool muted, CancellationToken cancellationToken);

        Task<Membership> UpdateAsync(uint id, int revision, MembershipState state, bool muted, CancellationToken cancellationToken);

        Task DeleteAsync(uint id, int revision, CancellationToken cancellationToken);
    }

    public interface IReminderInfo
    {
        Task<Reminder> GetAsync(uint id, CancellationToken cancellationToken);

        Task<IEnumerable<Reminder>> GetByTaskAsync(uint taskId, CancellationToken cancellationToken);

        Task<IEnumerable<Reminder>> GetByListAsync(uint listId, CancellationToken cancellationToken);

        Task<Reminder> CreateAsync(uint taskId, DateTime date, string deviceUdid, CancellationToken cancellationToken);

        Task<Reminder> UpdateAsync(uint id, int revision, DateTime date, CancellationToken cancellationToken);

        Task DeleteAsync(uint id, int revision, CancellationToken cancellationToken);
    }

    public interface ICommentInfo
    {
        Task<Comment> GetAsync(uint id, CancellationToken cancellationToken);

        Task<IEnumerable<Comment>> GetByTaskAsync(uint taskId, CancellationToken cancellationToken);

        Task<IEnumerable<Comment>> GetByListAsync(uint listId, CancellationToken cancellationToken);

        Task<Comment> CreateAsync(uint taskId, string comment, CancellationToken cancellationToken);

        Task<Comment> UpdateAsync(uint id, int revision, string comment, CancellationToken cancellationToken);

        Task DeleteAsync(uint id, int revision, CancellationToken cancellationToken);
    }

    public interface ITaskInfo
    {
        Task<MainTask> GetAsync(uint id, CancellationToken cancellationToken);

        Task<IEnumerable<MainTask>> GetAsync(uint listId, bool? completed, CancellationToken cancellationToken);

        Task<Positions> GetPositionAsync(uint id, CancellationToken cancellationToken);

        Task<IEnumerable<Positions>> GetPositionsAsync(uint listId, CancellationToken cancellationToken);

        Task<MainTask> CreateAsync(uint listId, string name, uint? assigneeId, bool? completed, 
            RecurrenceType? recurrenceType, int? recurrenceCount, DateTime? dueDate, bool? starred,
            CancellationToken cancellationToken);

        Task<MainTask> UpdateAsync(uint id, int revision, string name, uint? assigneeId, bool? completed,
           RecurrenceType? recurrenceType, int? recurrenceCount, DateTime? dueDate, bool? starred,
           CancellationToken cancellationToken);

        Task<Positions> UpdatePositionAsync(uint id, int revision, IEnumerable<int> positions, CancellationToken cancellationToken);

        Task DeleteAsync(uint id, int revision, CancellationToken cancellationToken);
    }

    public enum RecurrenceType { Day, Week, Month, Year }

    public interface ISubtaskInfo
    {
        Task<SubTask> GetAsync(uint id, CancellationToken cancellationToken);

        Task<IEnumerable<SubTask>> GetByListAsync(uint listId, bool? completed, CancellationToken cancellationToken);

        Task<IEnumerable<SubTask>> GetByTaskAsync(uint taskId, bool? completed, CancellationToken cancellationToken);

        Task<Positions> GetPositionAsync(uint id, CancellationToken cancellationToken);

        Task<IEnumerable<Positions>> GetPositionsByListAsync(uint listId, CancellationToken cancellationToken);

        Task<IEnumerable<Positions>> GetPositionsByTaskAsync(uint taskId, CancellationToken cancellationToken);

        Task<SubTask> CreateAsync(uint taskId, string name, bool? completed, CancellationToken cancellationToken);

        Task<SubTask> UpdateAsync(uint id, int revision, string name, bool? completed, CancellationToken cancellationToken);

        Task<Positions> UpdatePositionAsync(uint id, int revision, IEnumerable<int> positions, CancellationToken cancellationToken);

        Task DeleteAsync(uint id, int revision, CancellationToken cancellationToken);
    }

    public interface IUserInfo
    {
        /// <summary>
        /// Fetch the currently logged in user
        /// </summary>
        Task<User> GetAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Fetch the users this user can access
        /// </summary>
        Task<IEnumerable<User>> GetAsync(uint? listId, CancellationToken cancellationToken);

        Task<int> GetRootAsync(CancellationToken cancellationToken);

        Task<Uri> GetAvatarAsync(uint userId, int? size, bool fallback, CancellationToken cancellationToken);
    }

    public interface IWebhookInfo
    {
        Task<IEnumerable<Webhook>> GetByListAsync(uint listId, CancellationToken cancellationToken);

        Task<Webhook> CreateAsync(uint listId, Uri endpoint, string processorType, string configuration, CancellationToken cancellationToken);

        Task DeleteAsync(uint id, int revision, CancellationToken cancellationToken);
    }
}
