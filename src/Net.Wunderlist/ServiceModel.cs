using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;

namespace System.Net.Wunderlist
{
    internal sealed class ResourcePart
    {
        internal ResourcePart(JToken jtoken)
        {
            Id = jtoken.Value<uint>("id");
            UserId = jtoken.Value<uint?>("user_id");
            State = jtoken.Value<string>("state");
            ExpiresAt = jtoken.Value<DateTime>("expires_at");

            var part = jtoken.Value<JObject>("part");

            Url = part.Value<string>("url");
            Authorization = part.Value<string>("authorization");
            Date = part.Value<string>("date");
        }

        public uint Id { get; internal set; }

        public uint? UserId { get; internal set; }

        public string State { get; internal set; }

        public DateTime ExpiresAt { get; internal set; }

        public string Url { get; internal set; }

        public string Authorization { get; internal set; }

        public string Date { get; internal set; }

    }

    public abstract class Resource
    {
        internal Resource(JToken jtoken)
        {
            Id = jtoken.Value<uint>("id");
            CreatedAt = jtoken.Value<DateTime?>("created_at");
            UpdatedAt = jtoken.Value<DateTime?>("updated_at");
        }

        public uint Id { get; internal set; }

        public DateTime? CreatedAt { get; internal set; }

        public DateTime? UpdatedAt { get; internal set; }
    }

    public abstract class VersionedResource : Resource
    {
        internal VersionedResource(JToken jtoken) : base(jtoken)
        {            
            Revision = jtoken.Value<int>("revision");
        }

        public int Revision { get; internal set; }
    }

    public class ResourceRevision
    {
        internal ResourceRevision(JToken jtoken)
        {
            Id = jtoken.Value<uint>("id");
            Revision = jtoken.Value<int>("revision");
        }

        public uint Id { get; internal set; }

        public int Revision { get; internal set; }
    }

    public abstract class CollectionResource<T> : VersionedResource, ICollection<T>
    {
        internal CollectionResource(JToken jtoken) : base(jtoken)
        {
        }

        private ICollection<T> innerCollection = new List<T>();

        public int Count
        {
            get { return innerCollection.Count; }
        }

        public bool IsReadOnly
        {
            get { return innerCollection.IsReadOnly; }
        }

        public void Add(T item)
        {
            innerCollection.Add(item);
        }

        public void Clear()
        {
            innerCollection.Clear();
        }

        public bool Contains(T item)
        {
            return innerCollection.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            innerCollection.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return innerCollection.GetEnumerator();
        }

        public bool Remove(T item)
        {
            return innerCollection.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class Comment : VersionedResource
    {
        internal Comment(JToken jtoken) : base(jtoken)
        {
            TaskId = jtoken.Value<uint>("task_id");
            Value = jtoken.Value<string>("text");
        }

        public uint TaskId { get; internal set; }

        public string Value { get; set; }
    }

    public class File : VersionedResource
    {
        internal File(JToken jtoken) : base(jtoken)
        {
            TaskId = jtoken.Value<uint>("task_id");
            ListId = jtoken.Value<uint>("list_id");
            UserId = jtoken.Value<uint>("user_id");
            Name = jtoken.Value<string>("file_name");
            ContentType = jtoken.Value<string>("content_type");
            Size = jtoken.Value<int>("file_size");
            LocalCreatedAt = jtoken.Value<DateTime>("local_created_at");
        }

        public uint TaskId { get; internal set; }

        public uint ListId { get; internal set; }

        public uint UserId { get; internal set; }

        public string Name { get; internal set; }

        public string ContentType { get; internal set; }

        public int Size { get; internal set; }

        public DateTime LocalCreatedAt { get; internal set; }
    }

    public class Folder : CollectionResource<int>
    {
        internal Folder(JToken jtoken) : base(jtoken)
        {
            Title = jtoken.Value<string>("title");
            foreach (var item in jtoken.Values("list_ids"))
                base.Add(item.Value<int>());
        }

        public string Title { get; internal set; }
    }

    public class List : VersionedResource
    {
        internal List(JToken jtoken) : base(jtoken)
        {
            OwnerId = jtoken.Value<uint>("owner_id");
            OwnerType = jtoken.Value<string>("owner_type");
            Title = jtoken.Value<string>("title");
            Type = jtoken.Value<string>("type");
        }

        public uint OwnerId { get; set; }

        public string OwnerType { get; set; }

        public string Title { get; set; } // max lenght 255

        public string Type { get; set; }
    }

    public class Membership : Resource
    {
        internal Membership(JToken jtoken) : base(jtoken)
        {
            UserId = jtoken.Value<uint>("user_id");
            ListId = jtoken.Value<uint>("list_id");
            Owner = jtoken.Value<bool>("owner");
            Muted = jtoken.Value<bool>("muted");
			
			MembershipState state;
			State = (Enum.TryParse(jtoken.Value<string>("state"), true, out state)) ? 
				state : MembershipState.Pending;
        }

        public uint UserId { get; internal set; }

        public uint ListId { get; internal set; }

        public MembershipState State { get; internal set; }

        public bool Owner { get; set; }

        public bool Muted { get; set; }
    }


    public enum MembershipState { Pending, Accepted }

    public class Note : VersionedResource
    {
        internal Note(JToken jtoken) : base(jtoken)
        {
            TaskId = jtoken.Value<uint>("task_id");
            Content = jtoken.Value<string>("content");
        }

        public uint TaskId { get; internal set; }

        public string Content { get; set; }
    }

    public class Positions : CollectionResource<int>
    {
        internal Positions(JToken jtoken) : base(jtoken)
        {
            foreach (var item in jtoken.Values("values"))
                base.Add(item.Value<int>());
        }
    }

    public class Reminder : VersionedResource
    {
        internal Reminder(JToken jtoken) : base(jtoken)
        {
            TaskId = jtoken.Value<uint>("task_id");
            Date = jtoken.Value<DateTime>("date");
        }

        public uint TaskId { get; internal set; }

        public DateTime Date { get; set; }
    }

    //public class Root : VersionedResource
    //{
    //    internal Root(JToken jtoken) : base(jtoken)
    //    {
    //        UserId = jtoken.Value<int>("user_id");
    //    }

    //    public int UserId { get; internal set; }
    //}

    
    public abstract class TaskBase : VersionedResource
    {
        internal TaskBase(JToken jtoken) : base(jtoken)
        {
            Title = jtoken.Value<string>("title");
            ListId = jtoken.Value<uint>("list_id");
            DueDate = jtoken.Value<DateTime?>("due_date");
            CreatedBy = jtoken.Value<int>("created_by_id");
            CompletedBy = jtoken.Value<int?>("completed_by_id");
            CompletedAt = jtoken.Value<DateTime?>("completed_at");
        }
        
        public string Title { get; set; } // max lenght 255

        public uint ListId { get; internal set; }

        public DateTime? DueDate { get; set; }

        public int CreatedBy { get; internal set; }

        public int? CompletedBy { get; internal set; }

        public DateTime? CompletedAt { get; internal set; }
    }

    public class MainTask : TaskBase
    {
        internal MainTask(JToken jtoken) : base(jtoken)
        {
            AssigneeId = jtoken.Value<uint>("assignee_id");
            AssignerId = jtoken.Value<uint?>("assigner_id");
            Starred = jtoken.Value<bool>("starred");
        }

        public uint AssigneeId { get; set; }

        public uint? AssignerId { get; set; }

        public bool Starred { get; internal set; }
    }

    public class SubTask : TaskBase
    {
        internal SubTask(JToken jtoken) : base(jtoken)
        {
        }
    }

    public class User : VersionedResource
    {
        internal User(JToken jtoken) : base(jtoken)
        {
            Name = jtoken.Value<string>("name");
            Email = jtoken.Value<string>("email");
        }

        public string Name { get; set; }

        public string Email { get; set; }
    }

    public class Webhook : VersionedResource
    {
        internal Webhook(JToken jtoken) : base(jtoken)
        {
            ListId = jtoken.Value<uint>("list_id");
            MembershipId = jtoken.Value<uint>("membership_id");
            Endpoint = jtoken.Value<Uri>("url");
            ProcessorType = jtoken.Value<string>("processor_type");
            Configuration = jtoken.Value<string>("configuration");
        }

        public uint ListId { get; set; }

        public uint MembershipId { get; set; }

        public Uri Endpoint { get; set; }

        public string ProcessorType { get; set; }

        public string Configuration { get; set; }
    }
}

