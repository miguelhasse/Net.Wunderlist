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

    public interface INoteInfo
    {
        Task<Note> GetAsync(uint id, CancellationToken cancellationToken);

        Task<IEnumerable<Note>> GetByListAsync(uint listId, CancellationToken cancellationToken);

        Task<IEnumerable<Note>> GetByTaskAsync(uint taskId, CancellationToken cancellationToken);

        Task<Note> CreateAsync(uint taskId, string content, CancellationToken cancellationToken);

        Task<Note> UpdateAsync(uint id, int revision, string content, CancellationToken cancellationToken);

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

        Task<MainTask> UpdateAsync(uint id, int revision, uint? listId, string name, uint? assigneeId, bool? completed,
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
        /// Fetch the currently logged in user.
        /// </summary>
        /// <returns>All info related to the currently signed in user.</returns>
        Task<User> GetAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Fetch the users this user can access.
        /// </summary>
        /// <param name="listId">Restricts the list of returned users to only those who have access to a particular list.</param>
        /// <returns>All info related to the users who have access to a particular list.</returns>
        Task<IEnumerable<User>> GetAsync(uint? listId, CancellationToken cancellationToken);

        /// <summary>
        /// Fetch the Root for the current User.
        /// </summary>
        /// <returns>The top-level entity in the sync hierarchy.</returns>
        Task<int> GetRootAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Fetch user avatars of different sizes.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="size">Values: 25, 28, 30, 32, 50, 54, 56, 60, 64, 108, 128, 135, 256, 270, 512 and original.</param>
        /// <param name="fallback">If there is no custom avatar uploaded for the given user, true will return fallback avatars, and false will throw an exception (204 No Content).</param>
        /// <returns>Url for the avatar of the given user.</returns>
        Task<Uri> GetAvatarAsync(uint userId, int? size, bool fallback, CancellationToken cancellationToken);
    }

    public interface IWebhookInfo
    {
        /// <summary>
        /// Get all webhooks for a list.
        /// </summary>
        /// <param name="listId">List identifier.</param>
        /// <returns>All info related to webhook processors for events occurring on a given list.</returns>
        Task<IEnumerable<Webhook>> GetByListAsync(uint listId, CancellationToken cancellationToken);

        /// <summary>
        /// Create a Webhook for events occurring on a given list.
        /// </summary>
        /// <param name="listId">List identifier.</param>
        /// <param name="endpoint">Webhook endpoint Url (maximum length is 255 characters).</param>
        /// <param name="processorType">Values: generic</param>
        /// <returns></returns>
        Task<Webhook> CreateAsync(uint listId, Uri endpoint, string processorType, string configuration, CancellationToken cancellationToken);

        /// <summary>
        /// Delete a webhook permanently.
        /// </summary>
        /// <param name="id">Webhook identifier.</param>
        /// <param name="revision">Revision number.</param>
        /// <returns></returns>
        Task DeleteAsync(uint id, int revision, CancellationToken cancellationToken);
    }
}
