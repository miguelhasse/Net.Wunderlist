using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Wunderlist
{
    public interface IFileInfo
    {
        Task<File> GetAsync(int id, CancellationToken cancellationToken);

        Task<IEnumerable<File>> GetByListAsync(int listId, CancellationToken cancellationToken);

        Task<IEnumerable<File>> GetByTaskAsync(int taskId, CancellationToken cancellationToken);

        Task<Uri> GetPreviewAsync(int id, PreviewPlatform? platform, bool retina, CancellationToken cancellationToken);

        Task<File> CreateAsync(int taskId, string filename, CancellationToken cancellationToken);

        Task DeleteAsync(int id, int revision, CancellationToken cancellationToken);
    }

    public enum PreviewPlatform { Web, Windows, Mac, iPhone, iPad, Android }

    public interface IFolderInfo
    {
        Task<Folder> GetAsync(int id, CancellationToken cancellationToken);

        Task<IEnumerable<Folder>> GetAsync(CancellationToken cancellationToken);

        Task<Folder> CreateAsync(string name, IEnumerable<int> ids, CancellationToken cancellationToken);

        Task<Folder> UpdateAsync(int id, int revision, string name, IEnumerable<int> ids, CancellationToken cancellationToken);

        Task DeleteAsync(int id, int revision, CancellationToken cancellationToken);
    }

    public interface IListInfo
    {
        Task<List> GetAsync(int id, CancellationToken cancellationToken);

        Task<IEnumerable<List>> GetAsync(CancellationToken cancellationToken);

        Task<Positions> GetPositionAsync(int id, CancellationToken cancellationToken);

        Task<IEnumerable<Positions>> GetPositionsAsync(CancellationToken cancellationToken);

        Task<bool> ChangeStateAsync(int id, int revision, bool makePublic, CancellationToken cancellationToken);

        Task<List> CreateAsync(string name, CancellationToken cancellationToken);

        Task<List> UpdateAsync(int id, int revision, string name, CancellationToken cancellationToken);

        Task<Positions> UpdatePositionAsync(int id, int revision, IEnumerable<int> positions, CancellationToken cancellationToken);

        Task DeleteAsync(int id, int revision, CancellationToken cancellationToken);
    }

    public interface IMembershipInfo
    {
        Task<IEnumerable<Membership>> GetAsync(int? listId, CancellationToken cancellationToken);

        Task<Membership> CreateAsync(int listId, string email, bool muted, CancellationToken cancellationToken);

        Task<Membership> CreateAsync(int listId, int userId, bool muted, CancellationToken cancellationToken);

        Task<Membership> UpdateAsync(int id, int revision, MembershipState state, bool muted, CancellationToken cancellationToken);

        Task DeleteAsync(int id, int revision, CancellationToken cancellationToken);
    }

    public interface IReminderInfo
    {
        Task<Reminder> GetAsync(int id, CancellationToken cancellationToken);

        Task<IEnumerable<Reminder>> GetByTaskAsync(int taskId, CancellationToken cancellationToken);

        Task<IEnumerable<Reminder>> GetByListAsync(int listId, CancellationToken cancellationToken);

        Task<Reminder> CreateAsync(int taskId, DateTime date, string deviceUdid, CancellationToken cancellationToken);

        Task<Reminder> UpdateAsync(int id, int revision, DateTime date, CancellationToken cancellationToken);

        Task DeleteAsync(int id, int revision, CancellationToken cancellationToken);
    }

    public interface ICommentInfo
    {
        Task<Comment> GetAsync(int id, CancellationToken cancellationToken);

        Task<IEnumerable<Comment>> GetByTaskAsync(int taskId, CancellationToken cancellationToken);

        Task<IEnumerable<Comment>> GetByListAsync(int listId, CancellationToken cancellationToken);

        Task<Comment> CreateAsync(int taskId, string comment, CancellationToken cancellationToken);

        Task<Comment> UpdateAsync(int id, int revision, string comment, CancellationToken cancellationToken);

        Task DeleteAsync(int id, int revision, CancellationToken cancellationToken);
    }

    public interface ITaskInfo
    {
        Task<MainTask> GetAsync(int id, CancellationToken cancellationToken);

        Task<IEnumerable<MainTask>> GetAsync(int listId, bool? completed, CancellationToken cancellationToken);

        Task<Positions> GetPositionAsync(int id, CancellationToken cancellationToken);

        Task<IEnumerable<Positions>> GetPositionsAsync(int listId, CancellationToken cancellationToken);

        Task<MainTask> CreateAsync(int listId, string name, int? assigneeId, bool? completed, 
            RecurrenceType? recurrenceType, int? recurrenceCount, DateTime? dueDate, bool? starred,
            CancellationToken cancellationToken);

        Task<MainTask> UpdateAsync(int id, int revision, string name, int? assigneeId, bool? completed,
           RecurrenceType? recurrenceType, int? recurrenceCount, DateTime? dueDate, bool? starred,
           CancellationToken cancellationToken);

        Task<Positions> UpdatePositionAsync(int id, int revision, IEnumerable<int> positions, CancellationToken cancellationToken);

        Task DeleteAsync(int id, int revision, CancellationToken cancellationToken);
    }

    public enum RecurrenceType { Day, Week, Month, Year }

    public interface ISubtaskInfo
    {
        Task<SubTask> GetAsync(int id, CancellationToken cancellationToken);

        Task<IEnumerable<SubTask>> GetByListAsync(int listId, bool? completed, CancellationToken cancellationToken);

        Task<IEnumerable<SubTask>> GetByTaskAsync(int taskId, bool? completed, CancellationToken cancellationToken);

        Task<Positions> GetPositionAsync(int id, CancellationToken cancellationToken);

        Task<IEnumerable<Positions>> GetPositionsByListAsync(int listId, CancellationToken cancellationToken);

        Task<IEnumerable<Positions>> GetPositionsByTaskAsync(int taskId, CancellationToken cancellationToken);

        Task<SubTask> CreateAsync(int taskId, string name, bool? completed, CancellationToken cancellationToken);

        Task<SubTask> UpdateAsync(int id, int revision, string name, bool? completed, CancellationToken cancellationToken);

        Task<Positions> UpdatePositionAsync(int id, int revision, IEnumerable<int> positions, CancellationToken cancellationToken);

        Task DeleteAsync(int id, int revision, CancellationToken cancellationToken);
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
        Task<IEnumerable<User>> GetAsync(int? listId, CancellationToken cancellationToken);

        Task<int> GetRootAsync(CancellationToken cancellationToken);

        Task<Uri> GetAvatarAsync(int userId, int? size, bool fallback, CancellationToken cancellationToken);
    }

    public interface IWebhookInfo
    {
        Task<IEnumerable<Webhook>> GetByListAsync(int listId, CancellationToken cancellationToken);

        Task<Webhook> CreateAsync(int listId, Uri endpoint, string processorType, string configuration, CancellationToken cancellationToken);

        Task DeleteAsync(int id, int revision, CancellationToken cancellationToken);
    }
}
