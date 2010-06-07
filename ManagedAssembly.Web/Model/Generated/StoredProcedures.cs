


  
using System;
using SubSonic;
using SubSonic.Schema;
using SubSonic.DataProviders;
using System.Data;

namespace ManagedAssembly.Data{
	public partial class ManagedAssemblyDB{

        public StoredProcedure CalculatePostStats(int? PostId){
            StoredProcedure sp=new StoredProcedure("CalculatePostStats",this.Provider);
            sp.Command.AddParameter("PostId",PostId,DbType.Int32);
            return sp;
        }
        public StoredProcedure CalculateUserStats(int? UserId){
            StoredProcedure sp=new StoredProcedure("CalculateUserStats",this.Provider);
            sp.Command.AddParameter("UserId",UserId,DbType.Int32);
            return sp;
        }
        public StoredProcedure CommentList(int? ParentID,int? PageNum,int? PageSize){
            StoredProcedure sp=new StoredProcedure("CommentList",this.Provider);
            sp.Command.AddParameter("ParentID",ParentID,DbType.Int32);
            sp.Command.AddParameter("PageNum",PageNum,DbType.Int32);
            sp.Command.AddParameter("PageSize",PageSize,DbType.Int32);
            return sp;
        }
        public StoredProcedure PostList(int? ParentID,int? PageNum,int? PageSize){
            StoredProcedure sp=new StoredProcedure("PostList",this.Provider);
            sp.Command.AddParameter("ParentID",ParentID,DbType.Int32);
            sp.Command.AddParameter("PageNum",PageNum,DbType.Int32);
            sp.Command.AddParameter("PageSize",PageSize,DbType.Int32);
            return sp;
        }
	
	}
	
}
 