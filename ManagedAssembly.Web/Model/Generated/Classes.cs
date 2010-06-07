


using System;
using System.ComponentModel;
using System.Linq;

namespace ManagedAssembly.Data
{
    
    
    
    
    /// <summary>
    /// A class which represents the Tag table in the ManagedAssembly Database.
    /// This class is queryable through ManagedAssemblyDB.Tag 
    /// </summary>

	public partial class Tag: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public Tag(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnTagIdChanging(int value);
        partial void OnTagIdChanged();
		
		private int _TagId;
		public int TagId { 
		    get{
		        return _TagId;
		    } 
		    set{
		        this.OnTagIdChanging(value);
                this.SendPropertyChanging();
                this._TagId = value;
                this.SendPropertyChanged("TagId");
                this.OnTagIdChanged();
		    }
		}
		
        partial void OnLabelChanging(string value);
        partial void OnLabelChanged();
		
		private string _Label;
		public string Label { 
		    get{
		        return _Label;
		    } 
		    set{
		        this.OnLabelChanging(value);
                this.SendPropertyChanging();
                this._Label = value;
                this.SendPropertyChanged("Label");
                this.OnLabelChanged();
		    }
		}
		
        partial void OnCorrectTagIdChanging(int? value);
        partial void OnCorrectTagIdChanged();
		
		private int? _CorrectTagId;
		public int? CorrectTagId { 
		    get{
		        return _CorrectTagId;
		    } 
		    set{
		        this.OnCorrectTagIdChanging(value);
                this.SendPropertyChanging();
                this._CorrectTagId = value;
                this.SendPropertyChanged("CorrectTagId");
                this.OnCorrectTagIdChanged();
		    }
		}
		
        partial void OnUserIdChanging(int value);
        partial void OnUserIdChanged();
		
		private int _UserId;
		public int UserId { 
		    get{
		        return _UserId;
		    } 
		    set{
		        this.OnUserIdChanging(value);
                this.SendPropertyChanging();
                this._UserId = value;
                this.SendPropertyChanged("UserId");
                this.OnUserIdChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<PostTag> PostTags
        {
            get
            {
                  var db=new ManagedAssembly.Data.ManagedAssemblyDB();
                  return from items in db.PostTags
                       where items.TagId == _TagId
                       select items;
            }
        }

        public IQueryable<Tag> Tags
        {
            get
            {
                  var db=new ManagedAssembly.Data.ManagedAssemblyDB();
                  return from items in db.Tags
                       where items.TagId == _CorrectTagId
                       select items;
            }
        }

        public IQueryable<User> Users
        {
            get
            {
                  var db=new ManagedAssembly.Data.ManagedAssemblyDB();
                  return from items in db.Users
                       where items.UserId == _UserId
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the PostTag table in the ManagedAssembly Database.
    /// This class is queryable through ManagedAssemblyDB.PostTag 
    /// </summary>

	public partial class PostTag: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public PostTag(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnPostTagIdChanging(int value);
        partial void OnPostTagIdChanged();
		
		private int _PostTagId;
		public int PostTagId { 
		    get{
		        return _PostTagId;
		    } 
		    set{
		        this.OnPostTagIdChanging(value);
                this.SendPropertyChanging();
                this._PostTagId = value;
                this.SendPropertyChanged("PostTagId");
                this.OnPostTagIdChanged();
		    }
		}
		
        partial void OnPostIdChanging(int value);
        partial void OnPostIdChanged();
		
		private int _PostId;
		public int PostId { 
		    get{
		        return _PostId;
		    } 
		    set{
		        this.OnPostIdChanging(value);
                this.SendPropertyChanging();
                this._PostId = value;
                this.SendPropertyChanged("PostId");
                this.OnPostIdChanged();
		    }
		}
		
        partial void OnTagIdChanging(int value);
        partial void OnTagIdChanged();
		
		private int _TagId;
		public int TagId { 
		    get{
		        return _TagId;
		    } 
		    set{
		        this.OnTagIdChanging(value);
                this.SendPropertyChanging();
                this._TagId = value;
                this.SendPropertyChanged("TagId");
                this.OnTagIdChanged();
		    }
		}
		
        partial void OnUserIdChanging(int value);
        partial void OnUserIdChanged();
		
		private int _UserId;
		public int UserId { 
		    get{
		        return _UserId;
		    } 
		    set{
		        this.OnUserIdChanging(value);
                this.SendPropertyChanging();
                this._UserId = value;
                this.SendPropertyChanged("UserId");
                this.OnUserIdChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<Post> Posts
        {
            get
            {
                  var db=new ManagedAssembly.Data.ManagedAssemblyDB();
                  return from items in db.Posts
                       where items.PostId == _PostId
                       select items;
            }
        }

        public IQueryable<Tag> Tags
        {
            get
            {
                  var db=new ManagedAssembly.Data.ManagedAssemblyDB();
                  return from items in db.Tags
                       where items.TagId == _TagId
                       select items;
            }
        }

        public IQueryable<User> Users
        {
            get
            {
                  var db=new ManagedAssembly.Data.ManagedAssemblyDB();
                  return from items in db.Users
                       where items.UserId == _UserId
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the Vote table in the ManagedAssembly Database.
    /// This class is queryable through ManagedAssemblyDB.Vote 
    /// </summary>

	public partial class Vote: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public Vote(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnVoteIdChanging(int value);
        partial void OnVoteIdChanged();
		
		private int _VoteId;
		public int VoteId { 
		    get{
		        return _VoteId;
		    } 
		    set{
		        this.OnVoteIdChanging(value);
                this.SendPropertyChanging();
                this._VoteId = value;
                this.SendPropertyChanged("VoteId");
                this.OnVoteIdChanged();
		    }
		}
		
        partial void OnUserIdChanging(int value);
        partial void OnUserIdChanged();
		
		private int _UserId;
		public int UserId { 
		    get{
		        return _UserId;
		    } 
		    set{
		        this.OnUserIdChanging(value);
                this.SendPropertyChanging();
                this._UserId = value;
                this.SendPropertyChanged("UserId");
                this.OnUserIdChanged();
		    }
		}
		
        partial void OnPostIdChanging(int value);
        partial void OnPostIdChanged();
		
		private int _PostId;
		public int PostId { 
		    get{
		        return _PostId;
		    } 
		    set{
		        this.OnPostIdChanging(value);
                this.SendPropertyChanging();
                this._PostId = value;
                this.SendPropertyChanged("PostId");
                this.OnPostIdChanged();
		    }
		}
		
        partial void OnIsDeletedChanging(bool value);
        partial void OnIsDeletedChanged();
		
		private bool _IsDeleted;
		public bool IsDeleted { 
		    get{
		        return _IsDeleted;
		    } 
		    set{
		        this.OnIsDeletedChanging(value);
                this.SendPropertyChanging();
                this._IsDeleted = value;
                this.SendPropertyChanged("IsDeleted");
                this.OnIsDeletedChanged();
		    }
		}
		
        partial void OnVoteDirectionIdChanging(int value);
        partial void OnVoteDirectionIdChanged();
		
		private int _VoteDirectionId;
		public int VoteDirectionId { 
		    get{
		        return _VoteDirectionId;
		    } 
		    set{
		        this.OnVoteDirectionIdChanging(value);
                this.SendPropertyChanging();
                this._VoteDirectionId = value;
                this.SendPropertyChanged("VoteDirectionId");
                this.OnVoteDirectionIdChanged();
		    }
		}
		
        partial void OnCreateDateChanging(DateTime value);
        partial void OnCreateDateChanged();
		
		private DateTime _CreateDate;
		public DateTime CreateDate { 
		    get{
		        return _CreateDate;
		    } 
		    set{
		        this.OnCreateDateChanging(value);
                this.SendPropertyChanging();
                this._CreateDate = value;
                this.SendPropertyChanged("CreateDate");
                this.OnCreateDateChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<Post> Posts
        {
            get
            {
                  var db=new ManagedAssembly.Data.ManagedAssemblyDB();
                  return from items in db.Posts
                       where items.PostId == _PostId
                       select items;
            }
        }

        public IQueryable<User> Users
        {
            get
            {
                  var db=new ManagedAssembly.Data.ManagedAssemblyDB();
                  return from items in db.Users
                       where items.UserId == _UserId
                       select items;
            }
        }

        public IQueryable<VoteDirection> VoteDirections
        {
            get
            {
                  var db=new ManagedAssembly.Data.ManagedAssemblyDB();
                  return from items in db.VoteDirections
                       where items.VoteDirectionId == _VoteDirectionId
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the VoteDirection table in the ManagedAssembly Database.
    /// This class is queryable through ManagedAssemblyDB.VoteDirection 
    /// </summary>

	public partial class VoteDirection: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public VoteDirection(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnVoteDirectionIdChanging(int value);
        partial void OnVoteDirectionIdChanged();
		
		private int _VoteDirectionId;
		public int VoteDirectionId { 
		    get{
		        return _VoteDirectionId;
		    } 
		    set{
		        this.OnVoteDirectionIdChanging(value);
                this.SendPropertyChanging();
                this._VoteDirectionId = value;
                this.SendPropertyChanged("VoteDirectionId");
                this.OnVoteDirectionIdChanged();
		    }
		}
		
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
		
		private string _Name;
		public string Name { 
		    get{
		        return _Name;
		    } 
		    set{
		        this.OnNameChanging(value);
                this.SendPropertyChanging();
                this._Name = value;
                this.SendPropertyChanged("Name");
                this.OnNameChanged();
		    }
		}
		
        partial void OnPointValueChanging(int value);
        partial void OnPointValueChanged();
		
		private int _PointValue;
		public int PointValue { 
		    get{
		        return _PointValue;
		    } 
		    set{
		        this.OnPointValueChanging(value);
                this.SendPropertyChanging();
                this._PointValue = value;
                this.SendPropertyChanged("PointValue");
                this.OnPointValueChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<Vote> Votes
        {
            get
            {
                  var db=new ManagedAssembly.Data.ManagedAssemblyDB();
                  return from items in db.Votes
                       where items.VoteDirectionId == _VoteDirectionId
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the User table in the ManagedAssembly Database.
    /// This class is queryable through ManagedAssemblyDB.User 
    /// </summary>

	public partial class User: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public User(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnUserIdChanging(int value);
        partial void OnUserIdChanged();
		
		private int _UserId;
		public int UserId { 
		    get{
		        return _UserId;
		    } 
		    set{
		        this.OnUserIdChanging(value);
                this.SendPropertyChanging();
                this._UserId = value;
                this.SendPropertyChanged("UserId");
                this.OnUserIdChanged();
		    }
		}
		
        partial void OnExternalKeyChanging(string value);
        partial void OnExternalKeyChanged();
		
		private string _ExternalKey;
		public string ExternalKey { 
		    get{
		        return _ExternalKey;
		    } 
		    set{
		        this.OnExternalKeyChanging(value);
                this.SendPropertyChanging();
                this._ExternalKey = value;
                this.SendPropertyChanged("ExternalKey");
                this.OnExternalKeyChanged();
		    }
		}
		
        partial void OnIsModeratorChanging(bool value);
        partial void OnIsModeratorChanged();
		
		private bool _IsModerator;
		public bool IsModerator { 
		    get{
		        return _IsModerator;
		    } 
		    set{
		        this.OnIsModeratorChanging(value);
                this.SendPropertyChanging();
                this._IsModerator = value;
                this.SendPropertyChanged("IsModerator");
                this.OnIsModeratorChanged();
		    }
		}
		
        partial void OnIsAdminChanging(bool value);
        partial void OnIsAdminChanged();
		
		private bool _IsAdmin;
		public bool IsAdmin { 
		    get{
		        return _IsAdmin;
		    } 
		    set{
		        this.OnIsAdminChanging(value);
                this.SendPropertyChanging();
                this._IsAdmin = value;
                this.SendPropertyChanged("IsAdmin");
                this.OnIsAdminChanged();
		    }
		}
		
        partial void OnDisplayNameChanging(string value);
        partial void OnDisplayNameChanged();
		
		private string _DisplayName;
		public string DisplayName { 
		    get{
		        return _DisplayName;
		    } 
		    set{
		        this.OnDisplayNameChanging(value);
                this.SendPropertyChanging();
                this._DisplayName = value;
                this.SendPropertyChanged("DisplayName");
                this.OnDisplayNameChanged();
		    }
		}
		
        partial void OnEmailChanging(string value);
        partial void OnEmailChanged();
		
		private string _Email;
		public string Email { 
		    get{
		        return _Email;
		    } 
		    set{
		        this.OnEmailChanging(value);
                this.SendPropertyChanging();
                this._Email = value;
                this.SendPropertyChanged("Email");
                this.OnEmailChanged();
		    }
		}
		
        partial void OnIsBannedChanging(bool value);
        partial void OnIsBannedChanged();
		
		private bool _IsBanned;
		public bool IsBanned { 
		    get{
		        return _IsBanned;
		    } 
		    set{
		        this.OnIsBannedChanging(value);
                this.SendPropertyChanging();
                this._IsBanned = value;
                this.SendPropertyChanged("IsBanned");
                this.OnIsBannedChanged();
		    }
		}
		
        partial void OnUpVotesChanging(int value);
        partial void OnUpVotesChanged();
		
		private int _UpVotes;
		public int UpVotes { 
		    get{
		        return _UpVotes;
		    } 
		    set{
		        this.OnUpVotesChanging(value);
                this.SendPropertyChanging();
                this._UpVotes = value;
                this.SendPropertyChanged("UpVotes");
                this.OnUpVotesChanged();
		    }
		}
		
        partial void OnDownVotesChanging(int value);
        partial void OnDownVotesChanged();
		
		private int _DownVotes;
		public int DownVotes { 
		    get{
		        return _DownVotes;
		    } 
		    set{
		        this.OnDownVotesChanging(value);
                this.SendPropertyChanging();
                this._DownVotes = value;
                this.SendPropertyChanged("DownVotes");
                this.OnDownVotesChanged();
		    }
		}
		
        partial void OnPointsChanging(int value);
        partial void OnPointsChanged();
		
		private int _Points;
		public int Points { 
		    get{
		        return _Points;
		    } 
		    set{
		        this.OnPointsChanging(value);
                this.SendPropertyChanging();
                this._Points = value;
                this.SendPropertyChanged("Points");
                this.OnPointsChanged();
		    }
		}
		
        partial void OnPostUpVotesChanging(int value);
        partial void OnPostUpVotesChanged();
		
		private int _PostUpVotes;
		public int PostUpVotes { 
		    get{
		        return _PostUpVotes;
		    } 
		    set{
		        this.OnPostUpVotesChanging(value);
                this.SendPropertyChanging();
                this._PostUpVotes = value;
                this.SendPropertyChanged("PostUpVotes");
                this.OnPostUpVotesChanged();
		    }
		}
		
        partial void OnPostDownVotesChanging(int value);
        partial void OnPostDownVotesChanged();
		
		private int _PostDownVotes;
		public int PostDownVotes { 
		    get{
		        return _PostDownVotes;
		    } 
		    set{
		        this.OnPostDownVotesChanging(value);
                this.SendPropertyChanging();
                this._PostDownVotes = value;
                this.SendPropertyChanged("PostDownVotes");
                this.OnPostDownVotesChanged();
		    }
		}
		
        partial void OnBioChanging(string value);
        partial void OnBioChanged();
		
		private string _Bio;
		public string Bio { 
		    get{
		        return _Bio;
		    } 
		    set{
		        this.OnBioChanging(value);
                this.SendPropertyChanging();
                this._Bio = value;
                this.SendPropertyChanged("Bio");
                this.OnBioChanged();
		    }
		}
		
        partial void OnBioRawChanging(string value);
        partial void OnBioRawChanged();
		
		private string _BioRaw;
		public string BioRaw { 
		    get{
		        return _BioRaw;
		    } 
		    set{
		        this.OnBioRawChanging(value);
                this.SendPropertyChanging();
                this._BioRaw = value;
                this.SendPropertyChanged("BioRaw");
                this.OnBioRawChanged();
		    }
		}
		
        partial void OnUrlChanging(string value);
        partial void OnUrlChanged();
		
		private string _Url;
		public string Url { 
		    get{
		        return _Url;
		    } 
		    set{
		        this.OnUrlChanging(value);
                this.SendPropertyChanging();
                this._Url = value;
                this.SendPropertyChanged("Url");
                this.OnUrlChanged();
		    }
		}
		
        partial void OnTwitterChanging(string value);
        partial void OnTwitterChanged();
		
		private string _Twitter;
		public string Twitter { 
		    get{
		        return _Twitter;
		    } 
		    set{
		        this.OnTwitterChanging(value);
                this.SendPropertyChanging();
                this._Twitter = value;
                this.SendPropertyChanged("Twitter");
                this.OnTwitterChanged();
		    }
		}
		
        partial void OnJoinedOnChanging(DateTime value);
        partial void OnJoinedOnChanged();
		
		private DateTime _JoinedOn;
		public DateTime JoinedOn { 
		    get{
		        return _JoinedOn;
		    } 
		    set{
		        this.OnJoinedOnChanging(value);
                this.SendPropertyChanging();
                this._JoinedOn = value;
                this.SendPropertyChanged("JoinedOn");
                this.OnJoinedOnChanged();
		    }
		}
		
        partial void OnStackOverflowIDChanging(int? value);
        partial void OnStackOverflowIDChanged();
		
		private int? _StackOverflowID;
		public int? StackOverflowID { 
		    get{
		        return _StackOverflowID;
		    } 
		    set{
		        this.OnStackOverflowIDChanging(value);
                this.SendPropertyChanging();
                this._StackOverflowID = value;
                this.SendPropertyChanged("StackOverflowID");
                this.OnStackOverflowIDChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<Post> Posts
        {
            get
            {
                  var db=new ManagedAssembly.Data.ManagedAssemblyDB();
                  return from items in db.Posts
                       where items.UserId == _UserId
                       select items;
            }
        }

        public IQueryable<PostTag> PostTags
        {
            get
            {
                  var db=new ManagedAssembly.Data.ManagedAssemblyDB();
                  return from items in db.PostTags
                       where items.UserId == _UserId
                       select items;
            }
        }

        public IQueryable<Tag> Tags
        {
            get
            {
                  var db=new ManagedAssembly.Data.ManagedAssemblyDB();
                  return from items in db.Tags
                       where items.UserId == _UserId
                       select items;
            }
        }

        public IQueryable<Vote> Votes
        {
            get
            {
                  var db=new ManagedAssembly.Data.ManagedAssemblyDB();
                  return from items in db.Votes
                       where items.UserId == _UserId
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the Post table in the ManagedAssembly Database.
    /// This class is queryable through ManagedAssemblyDB.Post 
    /// </summary>

	public partial class Post: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public Post(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnPostIdChanging(int value);
        partial void OnPostIdChanged();
		
		private int _PostId;
		public int PostId { 
		    get{
		        return _PostId;
		    } 
		    set{
		        this.OnPostIdChanging(value);
                this.SendPropertyChanging();
                this._PostId = value;
                this.SendPropertyChanged("PostId");
                this.OnPostIdChanged();
		    }
		}
		
        partial void OnParentIdChanging(int? value);
        partial void OnParentIdChanged();
		
		private int? _ParentId;
		public int? ParentId { 
		    get{
		        return _ParentId;
		    } 
		    set{
		        this.OnParentIdChanging(value);
                this.SendPropertyChanging();
                this._ParentId = value;
                this.SendPropertyChanged("ParentId");
                this.OnParentIdChanged();
		    }
		}
		
        partial void OnCreateDateChanging(DateTime value);
        partial void OnCreateDateChanged();
		
		private DateTime _CreateDate;
		public DateTime CreateDate { 
		    get{
		        return _CreateDate;
		    } 
		    set{
		        this.OnCreateDateChanging(value);
                this.SendPropertyChanging();
                this._CreateDate = value;
                this.SendPropertyChanged("CreateDate");
                this.OnCreateDateChanged();
		    }
		}
		
        partial void OnUserIdChanging(int value);
        partial void OnUserIdChanged();
		
		private int _UserId;
		public int UserId { 
		    get{
		        return _UserId;
		    } 
		    set{
		        this.OnUserIdChanging(value);
                this.SendPropertyChanging();
                this._UserId = value;
                this.SendPropertyChanged("UserId");
                this.OnUserIdChanged();
		    }
		}
		
        partial void OnUrlChanging(string value);
        partial void OnUrlChanged();
		
		private string _Url;
		public string Url { 
		    get{
		        return _Url;
		    } 
		    set{
		        this.OnUrlChanging(value);
                this.SendPropertyChanging();
                this._Url = value;
                this.SendPropertyChanged("Url");
                this.OnUrlChanged();
		    }
		}
		
        partial void OnTitleChanging(string value);
        partial void OnTitleChanged();
		
		private string _Title;
		public string Title { 
		    get{
		        return _Title;
		    } 
		    set{
		        this.OnTitleChanging(value);
                this.SendPropertyChanging();
                this._Title = value;
                this.SendPropertyChanged("Title");
                this.OnTitleChanged();
		    }
		}
		
        partial void OnContentsChanging(string value);
        partial void OnContentsChanged();
		
		private string _Contents;
		public string Contents { 
		    get{
		        return _Contents;
		    } 
		    set{
		        this.OnContentsChanging(value);
                this.SendPropertyChanging();
                this._Contents = value;
                this.SendPropertyChanged("Contents");
                this.OnContentsChanged();
		    }
		}
		
        partial void OnContentsRawChanging(string value);
        partial void OnContentsRawChanged();
		
		private string _ContentsRaw;
		public string ContentsRaw { 
		    get{
		        return _ContentsRaw;
		    } 
		    set{
		        this.OnContentsRawChanging(value);
                this.SendPropertyChanging();
                this._ContentsRaw = value;
                this.SendPropertyChanged("ContentsRaw");
                this.OnContentsRawChanged();
		    }
		}
		
        partial void OnWeightChanging(decimal value);
        partial void OnWeightChanged();
		
		private decimal _Weight;
		public decimal Weight { 
		    get{
		        return _Weight;
		    } 
		    set{
		        this.OnWeightChanging(value);
                this.SendPropertyChanging();
                this._Weight = value;
                this.SendPropertyChanged("Weight");
                this.OnWeightChanged();
		    }
		}
		
        partial void OnPointsChanging(int value);
        partial void OnPointsChanged();
		
		private int _Points;
		public int Points { 
		    get{
		        return _Points;
		    } 
		    set{
		        this.OnPointsChanging(value);
                this.SendPropertyChanging();
                this._Points = value;
                this.SendPropertyChanged("Points");
                this.OnPointsChanged();
		    }
		}
		
        partial void OnUpVotesChanging(int value);
        partial void OnUpVotesChanged();
		
		private int _UpVotes;
		public int UpVotes { 
		    get{
		        return _UpVotes;
		    } 
		    set{
		        this.OnUpVotesChanging(value);
                this.SendPropertyChanging();
                this._UpVotes = value;
                this.SendPropertyChanged("UpVotes");
                this.OnUpVotesChanged();
		    }
		}
		
        partial void OnDownVotesChanging(int value);
        partial void OnDownVotesChanged();
		
		private int _DownVotes;
		public int DownVotes { 
		    get{
		        return _DownVotes;
		    } 
		    set{
		        this.OnDownVotesChanging(value);
                this.SendPropertyChanging();
                this._DownVotes = value;
                this.SendPropertyChanged("DownVotes");
                this.OnDownVotesChanged();
		    }
		}
		
        partial void OnIsDeletedChanging(bool value);
        partial void OnIsDeletedChanged();
		
		private bool _IsDeleted;
		public bool IsDeleted { 
		    get{
		        return _IsDeleted;
		    } 
		    set{
		        this.OnIsDeletedChanging(value);
                this.SendPropertyChanging();
                this._IsDeleted = value;
                this.SendPropertyChanged("IsDeleted");
                this.OnIsDeletedChanged();
		    }
		}
		
        partial void OnIsSpamChanging(bool value);
        partial void OnIsSpamChanged();
		
		private bool _IsSpam;
		public bool IsSpam { 
		    get{
		        return _IsSpam;
		    } 
		    set{
		        this.OnIsSpamChanging(value);
                this.SendPropertyChanging();
                this._IsSpam = value;
                this.SendPropertyChanged("IsSpam");
                this.OnIsSpamChanged();
		    }
		}
		
        partial void OnCommentCountChanging(int value);
        partial void OnCommentCountChanged();
		
		private int _CommentCount;
		public int CommentCount { 
		    get{
		        return _CommentCount;
		    } 
		    set{
		        this.OnCommentCountChanging(value);
                this.SendPropertyChanging();
                this._CommentCount = value;
                this.SendPropertyChanged("CommentCount");
                this.OnCommentCountChanged();
		    }
		}
		
        partial void OnTopMostIdChanging(int? value);
        partial void OnTopMostIdChanged();
		
		private int? _TopMostId;
		public int? TopMostId { 
		    get{
		        return _TopMostId;
		    } 
		    set{
		        this.OnTopMostIdChanging(value);
                this.SendPropertyChanging();
                this._TopMostId = value;
                this.SendPropertyChanged("TopMostId");
                this.OnTopMostIdChanged();
		    }
		}
		
        partial void OnIsApprovedChanging(bool value);
        partial void OnIsApprovedChanged();
		
		private bool _IsApproved;
		public bool IsApproved { 
		    get{
		        return _IsApproved;
		    } 
		    set{
		        this.OnIsApprovedChanging(value);
                this.SendPropertyChanging();
                this._IsApproved = value;
                this.SendPropertyChanged("IsApproved");
                this.OnIsApprovedChanged();
		    }
		}
		
        partial void OnSlugChanging(string value);
        partial void OnSlugChanged();
		
		private string _Slug;
		public string Slug { 
		    get{
		        return _Slug;
		    } 
		    set{
		        this.OnSlugChanging(value);
                this.SendPropertyChanging();
                this._Slug = value;
                this.SendPropertyChanged("Slug");
                this.OnSlugChanged();
		    }
		}
		
        partial void OnDomainNameChanging(string value);
        partial void OnDomainNameChanged();
		
		private string _DomainName;
		public string DomainName { 
		    get{
		        return _DomainName;
		    } 
		    set{
		        this.OnDomainNameChanging(value);
                this.SendPropertyChanging();
                this._DomainName = value;
                this.SendPropertyChanged("DomainName");
                this.OnDomainNameChanged();
		    }
		}
		
        partial void OnIsDestroyedChanging(bool value);
        partial void OnIsDestroyedChanged();
		
		private bool _IsDestroyed;
		public bool IsDestroyed { 
		    get{
		        return _IsDestroyed;
		    } 
		    set{
		        this.OnIsDestroyedChanging(value);
                this.SendPropertyChanging();
                this._IsDestroyed = value;
                this.SendPropertyChanged("IsDestroyed");
                this.OnIsDestroyedChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<Post> Posts
        {
            get
            {
                  var db=new ManagedAssembly.Data.ManagedAssemblyDB();
                  return from items in db.Posts
                       where items.PostId == _TopMostId
                       select items;
            }
        }

        public IQueryable<Post> Posts1
        {
            get
            {
                  var db=new ManagedAssembly.Data.ManagedAssemblyDB();
                  return from items in db.Posts
                       where items.PostId == _ParentId
                       select items;
            }
        }

        public IQueryable<PostTag> PostTags
        {
            get
            {
                  var db=new ManagedAssembly.Data.ManagedAssemblyDB();
                  return from items in db.PostTags
                       where items.PostId == _PostId
                       select items;
            }
        }

        public IQueryable<Vote> Votes
        {
            get
            {
                  var db=new ManagedAssembly.Data.ManagedAssemblyDB();
                  return from items in db.Votes
                       where items.PostId == _PostId
                       select items;
            }
        }

        public IQueryable<User> Users
        {
            get
            {
                  var db=new ManagedAssembly.Data.ManagedAssemblyDB();
                  return from items in db.Users
                       where items.UserId == _UserId
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
}