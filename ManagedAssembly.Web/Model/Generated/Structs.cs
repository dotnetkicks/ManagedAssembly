


using System;
using SubSonic.Schema;
using System.Collections.Generic;
using SubSonic.DataProviders;
using System.Data;

namespace ManagedAssembly.Data {
	
        /// <summary>
        /// Table: Tag
        /// Primary Key: TagId
        /// </summary>

        public class TagTable: DatabaseTable {
            
            public TagTable(IDataProvider provider):base("Tag",provider){
                ClassName = "Tag";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("TagId", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Label", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("CorrectTagId", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UserId", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });
                    
                
                
            }
            
            public IColumn TagId{
                get{
                    return this.GetColumn("TagId");
                }
            }
            				
   			public static string TagIdColumn{
			      get{
        			return "TagId";
      			}
		    }
           
            public IColumn Label{
                get{
                    return this.GetColumn("Label");
                }
            }
            				
   			public static string LabelColumn{
			      get{
        			return "Label";
      			}
		    }
           
            public IColumn CorrectTagId{
                get{
                    return this.GetColumn("CorrectTagId");
                }
            }
            				
   			public static string CorrectTagIdColumn{
			      get{
        			return "CorrectTagId";
      			}
		    }
           
            public IColumn UserId{
                get{
                    return this.GetColumn("UserId");
                }
            }
            				
   			public static string UserIdColumn{
			      get{
        			return "UserId";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: PostTag
        /// Primary Key: PostTagId
        /// </summary>

        public class PostTagTable: DatabaseTable {
            
            public PostTagTable(IDataProvider provider):base("PostTag",provider){
                ClassName = "PostTag";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("PostTagId", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("PostId", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TagId", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UserId", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });
                    
                
                
            }
            
            public IColumn PostTagId{
                get{
                    return this.GetColumn("PostTagId");
                }
            }
            				
   			public static string PostTagIdColumn{
			      get{
        			return "PostTagId";
      			}
		    }
           
            public IColumn PostId{
                get{
                    return this.GetColumn("PostId");
                }
            }
            				
   			public static string PostIdColumn{
			      get{
        			return "PostId";
      			}
		    }
           
            public IColumn TagId{
                get{
                    return this.GetColumn("TagId");
                }
            }
            				
   			public static string TagIdColumn{
			      get{
        			return "TagId";
      			}
		    }
           
            public IColumn UserId{
                get{
                    return this.GetColumn("UserId");
                }
            }
            				
   			public static string UserIdColumn{
			      get{
        			return "UserId";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: Vote
        /// Primary Key: VoteId
        /// </summary>

        public class VoteTable: DatabaseTable {
            
            public VoteTable(IDataProvider provider):base("Vote",provider){
                ClassName = "Vote";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("VoteId", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UserId", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("PostId", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("IsDeleted", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("VoteDirectionId", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }
            
            public IColumn VoteId{
                get{
                    return this.GetColumn("VoteId");
                }
            }
            				
   			public static string VoteIdColumn{
			      get{
        			return "VoteId";
      			}
		    }
           
            public IColumn UserId{
                get{
                    return this.GetColumn("UserId");
                }
            }
            				
   			public static string UserIdColumn{
			      get{
        			return "UserId";
      			}
		    }
           
            public IColumn PostId{
                get{
                    return this.GetColumn("PostId");
                }
            }
            				
   			public static string PostIdColumn{
			      get{
        			return "PostId";
      			}
		    }
           
            public IColumn IsDeleted{
                get{
                    return this.GetColumn("IsDeleted");
                }
            }
            				
   			public static string IsDeletedColumn{
			      get{
        			return "IsDeleted";
      			}
		    }
           
            public IColumn VoteDirectionId{
                get{
                    return this.GetColumn("VoteDirectionId");
                }
            }
            				
   			public static string VoteDirectionIdColumn{
			      get{
        			return "VoteDirectionId";
      			}
		    }
           
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
            				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: VoteDirection
        /// Primary Key: VoteDirectionId
        /// </summary>

        public class VoteDirectionTable: DatabaseTable {
            
            public VoteDirectionTable(IDataProvider provider):base("VoteDirection",provider){
                ClassName = "VoteDirection";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("VoteDirectionId", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("PointValue", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }
            
            public IColumn VoteDirectionId{
                get{
                    return this.GetColumn("VoteDirectionId");
                }
            }
            				
   			public static string VoteDirectionIdColumn{
			      get{
        			return "VoteDirectionId";
      			}
		    }
           
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
            				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
           
            public IColumn PointValue{
                get{
                    return this.GetColumn("PointValue");
                }
            }
            				
   			public static string PointValueColumn{
			      get{
        			return "PointValue";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: User
        /// Primary Key: UserId
        /// </summary>

        public class UserTable: DatabaseTable {
            
            public UserTable(IDataProvider provider):base("User",provider){
                ClassName = "User";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("UserId", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ExternalKey", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100
                });

                Columns.Add(new DatabaseColumn("IsModerator", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("IsAdmin", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("DisplayName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });

                Columns.Add(new DatabaseColumn("Email", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("IsBanned", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UpVotes", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("DownVotes", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Points", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("PostUpVotes", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("PostDownVotes", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Bio", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = -1
                });

                Columns.Add(new DatabaseColumn("BioRaw", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = -1
                });

                Columns.Add(new DatabaseColumn("Url", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });

                Columns.Add(new DatabaseColumn("Twitter", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50
                });

                Columns.Add(new DatabaseColumn("JoinedOn", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("StackOverflowID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }
            
            public IColumn UserId{
                get{
                    return this.GetColumn("UserId");
                }
            }
            				
   			public static string UserIdColumn{
			      get{
        			return "UserId";
      			}
		    }
           
            public IColumn ExternalKey{
                get{
                    return this.GetColumn("ExternalKey");
                }
            }
            				
   			public static string ExternalKeyColumn{
			      get{
        			return "ExternalKey";
      			}
		    }
           
            public IColumn IsModerator{
                get{
                    return this.GetColumn("IsModerator");
                }
            }
            				
   			public static string IsModeratorColumn{
			      get{
        			return "IsModerator";
      			}
		    }
           
            public IColumn IsAdmin{
                get{
                    return this.GetColumn("IsAdmin");
                }
            }
            				
   			public static string IsAdminColumn{
			      get{
        			return "IsAdmin";
      			}
		    }
           
            public IColumn DisplayName{
                get{
                    return this.GetColumn("DisplayName");
                }
            }
            				
   			public static string DisplayNameColumn{
			      get{
        			return "DisplayName";
      			}
		    }
           
            public IColumn Email{
                get{
                    return this.GetColumn("Email");
                }
            }
            				
   			public static string EmailColumn{
			      get{
        			return "Email";
      			}
		    }
           
            public IColumn IsBanned{
                get{
                    return this.GetColumn("IsBanned");
                }
            }
            				
   			public static string IsBannedColumn{
			      get{
        			return "IsBanned";
      			}
		    }
           
            public IColumn UpVotes{
                get{
                    return this.GetColumn("UpVotes");
                }
            }
            				
   			public static string UpVotesColumn{
			      get{
        			return "UpVotes";
      			}
		    }
           
            public IColumn DownVotes{
                get{
                    return this.GetColumn("DownVotes");
                }
            }
            				
   			public static string DownVotesColumn{
			      get{
        			return "DownVotes";
      			}
		    }
           
            public IColumn Points{
                get{
                    return this.GetColumn("Points");
                }
            }
            				
   			public static string PointsColumn{
			      get{
        			return "Points";
      			}
		    }
           
            public IColumn PostUpVotes{
                get{
                    return this.GetColumn("PostUpVotes");
                }
            }
            				
   			public static string PostUpVotesColumn{
			      get{
        			return "PostUpVotes";
      			}
		    }
           
            public IColumn PostDownVotes{
                get{
                    return this.GetColumn("PostDownVotes");
                }
            }
            				
   			public static string PostDownVotesColumn{
			      get{
        			return "PostDownVotes";
      			}
		    }
           
            public IColumn Bio{
                get{
                    return this.GetColumn("Bio");
                }
            }
            				
   			public static string BioColumn{
			      get{
        			return "Bio";
      			}
		    }
           
            public IColumn BioRaw{
                get{
                    return this.GetColumn("BioRaw");
                }
            }
            				
   			public static string BioRawColumn{
			      get{
        			return "BioRaw";
      			}
		    }
           
            public IColumn Url{
                get{
                    return this.GetColumn("Url");
                }
            }
            				
   			public static string UrlColumn{
			      get{
        			return "Url";
      			}
		    }
           
            public IColumn Twitter{
                get{
                    return this.GetColumn("Twitter");
                }
            }
            				
   			public static string TwitterColumn{
			      get{
        			return "Twitter";
      			}
		    }
           
            public IColumn JoinedOn{
                get{
                    return this.GetColumn("JoinedOn");
                }
            }
            				
   			public static string JoinedOnColumn{
			      get{
        			return "JoinedOn";
      			}
		    }
           
            public IColumn StackOverflowID{
                get{
                    return this.GetColumn("StackOverflowID");
                }
            }
            				
   			public static string StackOverflowIDColumn{
			      get{
        			return "StackOverflowID";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: Post
        /// Primary Key: PostId
        /// </summary>

        public class PostTable: DatabaseTable {
            
            public PostTable(IDataProvider provider):base("Post",provider){
                ClassName = "Post";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("PostId", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ParentId", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CreateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UserId", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Url", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 2000
                });

                Columns.Add(new DatabaseColumn("Title", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });

                Columns.Add(new DatabaseColumn("Contents", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = -1
                });

                Columns.Add(new DatabaseColumn("ContentsRaw", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = -1
                });

                Columns.Add(new DatabaseColumn("Weight", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Points", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UpVotes", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("DownVotes", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("IsDeleted", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("IsSpam", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CommentCount", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("TopMostId", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("IsApproved", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Slug", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });

                Columns.Add(new DatabaseColumn("DomainName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });

                Columns.Add(new DatabaseColumn("IsDestroyed", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }
            
            public IColumn PostId{
                get{
                    return this.GetColumn("PostId");
                }
            }
            				
   			public static string PostIdColumn{
			      get{
        			return "PostId";
      			}
		    }
           
            public IColumn ParentId{
                get{
                    return this.GetColumn("ParentId");
                }
            }
            				
   			public static string ParentIdColumn{
			      get{
        			return "ParentId";
      			}
		    }
           
            public IColumn CreateDate{
                get{
                    return this.GetColumn("CreateDate");
                }
            }
            				
   			public static string CreateDateColumn{
			      get{
        			return "CreateDate";
      			}
		    }
           
            public IColumn UserId{
                get{
                    return this.GetColumn("UserId");
                }
            }
            				
   			public static string UserIdColumn{
			      get{
        			return "UserId";
      			}
		    }
           
            public IColumn Url{
                get{
                    return this.GetColumn("Url");
                }
            }
            				
   			public static string UrlColumn{
			      get{
        			return "Url";
      			}
		    }
           
            public IColumn Title{
                get{
                    return this.GetColumn("Title");
                }
            }
            				
   			public static string TitleColumn{
			      get{
        			return "Title";
      			}
		    }
           
            public IColumn Contents{
                get{
                    return this.GetColumn("Contents");
                }
            }
            				
   			public static string ContentsColumn{
			      get{
        			return "Contents";
      			}
		    }
           
            public IColumn ContentsRaw{
                get{
                    return this.GetColumn("ContentsRaw");
                }
            }
            				
   			public static string ContentsRawColumn{
			      get{
        			return "ContentsRaw";
      			}
		    }
           
            public IColumn Weight{
                get{
                    return this.GetColumn("Weight");
                }
            }
            				
   			public static string WeightColumn{
			      get{
        			return "Weight";
      			}
		    }
           
            public IColumn Points{
                get{
                    return this.GetColumn("Points");
                }
            }
            				
   			public static string PointsColumn{
			      get{
        			return "Points";
      			}
		    }
           
            public IColumn UpVotes{
                get{
                    return this.GetColumn("UpVotes");
                }
            }
            				
   			public static string UpVotesColumn{
			      get{
        			return "UpVotes";
      			}
		    }
           
            public IColumn DownVotes{
                get{
                    return this.GetColumn("DownVotes");
                }
            }
            				
   			public static string DownVotesColumn{
			      get{
        			return "DownVotes";
      			}
		    }
           
            public IColumn IsDeleted{
                get{
                    return this.GetColumn("IsDeleted");
                }
            }
            				
   			public static string IsDeletedColumn{
			      get{
        			return "IsDeleted";
      			}
		    }
           
            public IColumn IsSpam{
                get{
                    return this.GetColumn("IsSpam");
                }
            }
            				
   			public static string IsSpamColumn{
			      get{
        			return "IsSpam";
      			}
		    }
           
            public IColumn CommentCount{
                get{
                    return this.GetColumn("CommentCount");
                }
            }
            				
   			public static string CommentCountColumn{
			      get{
        			return "CommentCount";
      			}
		    }
           
            public IColumn TopMostId{
                get{
                    return this.GetColumn("TopMostId");
                }
            }
            				
   			public static string TopMostIdColumn{
			      get{
        			return "TopMostId";
      			}
		    }
           
            public IColumn IsApproved{
                get{
                    return this.GetColumn("IsApproved");
                }
            }
            				
   			public static string IsApprovedColumn{
			      get{
        			return "IsApproved";
      			}
		    }
           
            public IColumn Slug{
                get{
                    return this.GetColumn("Slug");
                }
            }
            				
   			public static string SlugColumn{
			      get{
        			return "Slug";
      			}
		    }
           
            public IColumn DomainName{
                get{
                    return this.GetColumn("DomainName");
                }
            }
            				
   			public static string DomainNameColumn{
			      get{
        			return "DomainName";
      			}
		    }
           
            public IColumn IsDestroyed{
                get{
                    return this.GetColumn("IsDestroyed");
                }
            }
            				
   			public static string IsDestroyedColumn{
			      get{
        			return "IsDestroyed";
      			}
		    }
           
                    
        }
        
}