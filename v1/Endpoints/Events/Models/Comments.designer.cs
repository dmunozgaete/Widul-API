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

namespace API.Endpoints.Events.Models
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
	public partial class CommentsDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertVW_EventComment(VW_EventComment instance);
    partial void UpdateVW_EventComment(VW_EventComment instance);
    partial void DeleteVW_EventComment(VW_EventComment instance);
    #endregion
		
		public CommentsDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["WidulConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public CommentsDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CommentsDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CommentsDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CommentsDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<NewComment> NewComments
		{
			get
			{
				return this.GetTable<NewComment>();
			}
		}
		
		public System.Data.Linq.Table<VW_EventComment> VW_EventComment
		{
			get
			{
				return this.GetTable<VW_EventComment>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="")]
	public partial class NewComment
	{
		
		private string _comment;
		
		public NewComment()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_comment", CanBeNull=false)]
		public string comment
		{
			get
			{
				return this._comment;
			}
			set
			{
				if ((this._comment != value))
				{
					this._comment = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.VW_EventComments")]
	public partial class VW_EventComment : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _creator_token;
		
		private string _creator_name;
		
		private System.Nullable<System.Guid> _creator_photo;
		
		private string _comment;
		
		private System.DateTime _createdAt;
		
		private System.Guid _token;
		
		private byte[] _creator_photoBinary;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void Oncreator_tokenChanging(System.Guid value);
    partial void Oncreator_tokenChanged();
    partial void Oncreator_nameChanging(string value);
    partial void Oncreator_nameChanged();
    partial void Oncreator_photoChanging(System.Nullable<System.Guid> value);
    partial void Oncreator_photoChanged();
    partial void OncommentChanging(string value);
    partial void OncommentChanged();
    partial void OncreatedAtChanging(System.DateTime value);
    partial void OncreatedAtChanged();
    partial void OntokenChanging(System.Guid value);
    partial void OntokenChanged();
    partial void Oncreator_photoBinaryChanging(byte[] value);
    partial void Oncreator_photoBinaryChanged();
    #endregion
		
		public VW_EventComment()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="ENTI_Token", Storage="_creator_token", DbType="UniqueIdentifier NOT NULL")]
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
					this.Oncreator_tokenChanging(value);
					this.SendPropertyChanging();
					this._creator_token = value;
					this.SendPropertyChanged("creator_token");
					this.Oncreator_tokenChanged();
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
					this.Oncreator_nameChanging(value);
					this.SendPropertyChanging();
					this._creator_name = value;
					this.SendPropertyChanged("creator_name");
					this.Oncreator_nameChanged();
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
					this.Oncreator_photoChanging(value);
					this.SendPropertyChanging();
					this._creator_photo = value;
					this.SendPropertyChanged("creator_photo");
					this.Oncreator_photoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="COMM_Comment", Storage="_comment", DbType="VarChar(500) NOT NULL", CanBeNull=false)]
		public string comment
		{
			get
			{
				return this._comment;
			}
			set
			{
				if ((this._comment != value))
				{
					this.OncommentChanging(value);
					this.SendPropertyChanging();
					this._comment = value;
					this.SendPropertyChanged("comment");
					this.OncommentChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="COMM_createdAt", Storage="_createdAt", DbType="DateTime NOT NULL")]
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
					this.OncreatedAtChanging(value);
					this.SendPropertyChanging();
					this._createdAt = value;
					this.SendPropertyChanged("createdAt");
					this.OncreatedAtChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="COMM_Token", Storage="_token", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
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
					this.OntokenChanging(value);
					this.SendPropertyChanging();
					this._token = value;
					this.SendPropertyChanged("token");
					this.OntokenChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="USER_PhotoBinary", Storage="_creator_photoBinary", CanBeNull=false)]
		public byte[] creator_photoBinary
		{
			get
			{
				return this._creator_photoBinary;
			}
			set
			{
				if ((this._creator_photoBinary != value))
				{
					this.Oncreator_photoBinaryChanging(value);
					this.SendPropertyChanging();
					this._creator_photoBinary = value;
					this.SendPropertyChanged("creator_photoBinary");
					this.Oncreator_photoBinaryChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
