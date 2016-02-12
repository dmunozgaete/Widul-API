﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace API.Endpoints.KnowledgeRequests.Models
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Widul")]
	public partial class KnowledgeRequestsDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public KnowledgeRequestsDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["WidulConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public KnowledgeRequestsDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public KnowledgeRequestsDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public KnowledgeRequestsDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public KnowledgeRequestsDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<NewKnowledgeRequest> NewKnowledgeRequests
		{
			get
			{
				return this.GetTable<NewKnowledgeRequest>();
			}
		}
		
		public System.Data.Linq.Table<KnowledgeRequestDetail> KnowledgeRequestDetails
		{
			get
			{
				return this.GetTable<KnowledgeRequestDetail>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.TB_WID_KnowledgeRequest")]
	public partial class NewKnowledgeRequest
	{
		
		private string _excerpt;
		
		private string _knowledge;
		
		public NewKnowledgeRequest()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="REQU_Excerpt", Storage="_excerpt", DbType="VarChar(2000) NOT NULL", CanBeNull=false)]
		public string excerpt
		{
			get
			{
				return this._excerpt;
			}
			set
			{
				if ((this._excerpt != value))
				{
					this._excerpt = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="REQU_KNOW_Id", Storage="_knowledge", CanBeNull=false)]
		public string knowledge
		{
			get
			{
				return this._knowledge;
			}
			set
			{
				if ((this._knowledge != value))
				{
					this._knowledge = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.VW_KnowledgeRequests")]
	public partial class KnowledgeRequestDetail
	{
		
		private System.DateTime _createdAt;
		
		private System.Guid _token;
		
		private string _state_name;
		
		private string _state_identifier;
		
		private System.Guid _state_token;
		
		private string _excerpt;
		
		private string _creator_name;
		
		private System.Guid _creator_token;
		
		private System.Nullable<System.Guid> _creator_photo;
		
		private string _knowledge_name;
		
		private System.Guid _knowledge_token;
		
		public KnowledgeRequestDetail()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="DOCU_CreatedAt", Storage="_createdAt", DbType="DateTime NOT NULL")]
		public System.DateTime createdAt
		{
			get
			{
				return this._createdAt;
			}
			set
			{
				if ((this._createdAt != value))
				{
					this._createdAt = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="DOCU_Token", Storage="_token", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid token
		{
			get
			{
				return this._token;
			}
			set
			{
				if ((this._token != value))
				{
					this._token = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="STAT_Name", Storage="_state_name", DbType="VarChar(200) NOT NULL", CanBeNull=false)]
		public string state_name
		{
			get
			{
				return this._state_name;
			}
			set
			{
				if ((this._state_name != value))
				{
					this._state_name = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="STAT_Identifier", Storage="_state_identifier", DbType="Char(4) NOT NULL", CanBeNull=false)]
		public string state_identifier
		{
			get
			{
				return this._state_identifier;
			}
			set
			{
				if ((this._state_identifier != value))
				{
					this._state_identifier = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="STAT_Token", Storage="_state_token", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid state_token
		{
			get
			{
				return this._state_token;
			}
			set
			{
				if ((this._state_token != value))
				{
					this._state_token = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="REQU_Excerpt", Storage="_excerpt", DbType="VarChar(2000) NOT NULL", CanBeNull=false)]
		public string excerpt
		{
			get
			{
				return this._excerpt;
			}
			set
			{
				if ((this._excerpt != value))
				{
					this._excerpt = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="USER_FullName", Storage="_creator_name", DbType="VarChar(250) NOT NULL", CanBeNull=false)]
		public string creator_name
		{
			get
			{
				return this._creator_name;
			}
			set
			{
				if ((this._creator_name != value))
				{
					this._creator_name = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="USER_Token", Storage="_creator_token", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid creator_token
		{
			get
			{
				return this._creator_token;
			}
			set
			{
				if ((this._creator_token != value))
				{
					this._creator_token = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="USER_Photo", Storage="_creator_photo", DbType="UniqueIdentifier")]
		public System.Nullable<System.Guid> creator_photo
		{
			get
			{
				return this._creator_photo;
			}
			set
			{
				if ((this._creator_photo != value))
				{
					this._creator_photo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="KNOW_Name", Storage="_knowledge_name", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string knowledge_name
		{
			get
			{
				return this._knowledge_name;
			}
			set
			{
				if ((this._knowledge_name != value))
				{
					this._knowledge_name = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="KNOW_Token", Storage="_knowledge_token", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid knowledge_token
		{
			get
			{
				return this._knowledge_token;
			}
			set
			{
				if ((this._knowledge_token != value))
				{
					this._knowledge_token = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
