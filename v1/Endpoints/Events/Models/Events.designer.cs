﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
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
	public partial class EventsDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Definiciones de métodos de extensibilidad
    partial void OnCreated();
    #endregion
		
		public EventsDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["WidulConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public EventsDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public EventsDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public EventsDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public EventsDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<NewEvent> NewEvents
		{
			get
			{
				return this.GetTable<NewEvent>();
			}
		}
		
		public System.Data.Linq.Table<Pagination> Pagination
		{
			get
			{
				return this.GetTable<Pagination>();
			}
		}
		
		public System.Data.Linq.Table<EventTag> EventTag
		{
			get
			{
				return this.GetTable<EventTag>();
			}
		}
		
		public System.Data.Linq.Table<Place> Places
		{
			get
			{
				return this.GetTable<Place>();
			}
		}
		
		public System.Data.Linq.Table<EventDetails> EventDetails
		{
			get
			{
				return this.GetTable<EventDetails>();
			}
		}
		
		public System.Data.Linq.Table<FindedEvent> FindedEvent
		{
			get
			{
				return this.GetTable<FindedEvent>();
			}
		}
		
		public System.Data.Linq.Table<FindedTag> FindedTag
		{
			get
			{
				return this.GetTable<FindedTag>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.TB_Event")]
	public partial class NewEvent
	{
		
		private string _name;
		
		private string _description;
		
		private System.DateTime _date;
		
		private string _location;
		
		private string _knowledge;
		
		private List<String> _tags;
		
		private string _isRestricted;
		
		private string _isPrivate;
		
		public NewEvent()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="EVN_Name", Storage="_name", DbType="VarChar(200) NOT NULL", CanBeNull=false)]
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				if ((this._name != value))
				{
					this._name = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="EVN_Description", Storage="_description", DbType="VarChar(500) NOT NULL", CanBeNull=false)]
		public string description
		{
			get
			{
				return this._description;
			}
			set
			{
				if ((this._description != value))
				{
					this._description = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="EVN_Date", Storage="_date", DbType="DateTime")]
		public System.DateTime date
		{
			get
			{
				return this._date;
			}
			set
			{
				if ((this._date != value))
				{
					this._date = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="PLAC_Token", Storage="_location", CanBeNull=false)]
		public string place
		{
			get
			{
				return this._location;
			}
			set
			{
				if ((this._location != value))
				{
					this._location = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="KNW_Token", Storage="_knowledge", CanBeNull=false)]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_tags", CanBeNull=false)]
		public List<String> tags
		{
			get
			{
				return this._tags;
			}
			set
			{
				if ((this._tags != value))
				{
					this._tags = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_isRestricted", CanBeNull=false)]
		public string isRestricted
		{
			get
			{
				return this._isRestricted;
			}
			set
			{
				if ((this._isRestricted != value))
				{
					this._isRestricted = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_isPrivate", CanBeNull=false)]
		public string isPrivate
		{
			get
			{
				return this._isPrivate;
			}
			set
			{
				if ((this._isPrivate != value))
				{
					this._isPrivate = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="")]
	public partial class Pagination
	{
		
		private int _limit;
		
		private int _offset;
		
		private int _total;
		
		public Pagination()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="Limit", Storage="_limit")]
		public int limit
		{
			get
			{
				return this._limit;
			}
			set
			{
				if ((this._limit != value))
				{
					this._limit = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="Offset", Storage="_offset")]
		public int offset
		{
			get
			{
				return this._offset;
			}
			set
			{
				if ((this._offset != value))
				{
					this._offset = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="Total", Storage="_total")]
		public int total
		{
			get
			{
				return this._total;
			}
			set
			{
				if ((this._total != value))
				{
					this._total = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.TB_WID_EventTag")]
	public partial class EventTag
	{
		
		private string _name;
		
		private System.Guid _token;
		
		public EventTag()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="ETAG_Name", Storage="_name", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				if ((this._name != value))
				{
					this._name = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="ETAG_Token", Storage="_token", DbType="UniqueIdentifier NOT NULL")]
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
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.VW_Places")]
	public partial class Place
	{
		
		private string _name;
		
		private string _address;
		
		private double _latitude;
		
		private double _longitude;
		
		private int _capacity;
		
		private string _tip;
		
		private System.Guid _token;
		
		public Place()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="PLAC_Name", Storage="_name", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				if ((this._name != value))
				{
					this._name = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="PLAC_Address", Storage="_address", DbType="VarChar(400) NOT NULL", CanBeNull=false)]
		public string address
		{
			get
			{
				return this._address;
			}
			set
			{
				if ((this._address != value))
				{
					this._address = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="PLAC_Latitude", Storage="_latitude", DbType="Float NOT NULL")]
		public double lat
		{
			get
			{
				return this._latitude;
			}
			set
			{
				if ((this._latitude != value))
				{
					this._latitude = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="PLAC_Longitude", Storage="_longitude", DbType="Float NOT NULL")]
		public double lng
		{
			get
			{
				return this._longitude;
			}
			set
			{
				if ((this._longitude != value))
				{
					this._longitude = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="PLAC_Capacity", Storage="_capacity", DbType="Int NOT NULL")]
		public int capacity
		{
			get
			{
				return this._capacity;
			}
			set
			{
				if ((this._capacity != value))
				{
					this._capacity = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="PLAC_Tip", Storage="_tip", DbType="VarChar(400) NOT NULL", CanBeNull=false)]
		public string tip
		{
			get
			{
				return this._tip;
			}
			set
			{
				if ((this._tip != value))
				{
					this._tip = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="PLAC_Token", Storage="_token", DbType="UniqueIdentifier NOT NULL")]
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
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.VW_Events")]
	public partial class EventDetails
	{
		
		private System.DateTime _createdAt;
		
		private System.Guid _token;
		
		private string _state_name;
		
		private string _state_identifier;
		
		private System.Guid _state_token;
		
		private System.DateTime _date;
		
		private string _description;
		
		private string _name;
		
		private System.DateTime _updatedAt;
		
		private string _creator_name;
		
		private System.Guid _creator_token;
		
		private System.Nullable<System.Guid> _creator_photo;
		
		private string _knowledge_name;
		
		private System.Guid _knowledge_token;
		
		private Models.Place _place;
		
		private List<Models.VW_EventComment> _lastComments;
		
		private List<Models.EventTag> _tags;
		
		public EventDetails()
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="EVNT_Date", Storage="_date", DbType="DateTime NOT NULL")]
		public System.DateTime date
		{
			get
			{
				return this._date;
			}
			set
			{
				if ((this._date != value))
				{
					this._date = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="EVNT_Description", Storage="_description", DbType="VarChar(500) NOT NULL", CanBeNull=false)]
		public string description
		{
			get
			{
				return this._description;
			}
			set
			{
				if ((this._description != value))
				{
					this._description = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="EVNT_Name", Storage="_name", DbType="VarChar(200) NOT NULL", CanBeNull=false)]
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				if ((this._name != value))
				{
					this._name = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="EVNT_UpdatedAt", Storage="_updatedAt", DbType="DateTime NOT NULL")]
		public System.DateTime updatedAt
		{
			get
			{
				return this._updatedAt;
			}
			set
			{
				if ((this._updatedAt != value))
				{
					this._updatedAt = value;
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_place", CanBeNull=false)]
		public Models.Place place
		{
			get
			{
				return this._place;
			}
			set
			{
				if ((this._place != value))
				{
					this._place = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_lastComments", CanBeNull=false)]
		public List<Models.VW_EventComment> lastComments
		{
			get
			{
				return this._lastComments;
			}
			set
			{
				if ((this._lastComments != value))
				{
					this._lastComments = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_tags", CanBeNull=false)]
		public List<Models.EventTag> tags
		{
			get
			{
				return this._tags;
			}
			set
			{
				if ((this._tags != value))
				{
					this._tags = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.VW_Events")]
	public partial class FindedEvent
	{
		
		private int _id;
		
		private System.Guid _token;
		
		private System.DateTime _date;
		
		private string _description;
		
		private string _name;
		
		private string _creator_name;
		
		private System.Guid _creator_token;
		
		private System.Nullable<System.Guid> _creator_photo;
		
		private string _knowledge_name;
		
		private System.Guid _knowledge_token;
		
		public FindedEvent()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="DOCU_Id", Storage="_id", DbType="Int NOT NULL")]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="EVNT_Date", Storage="_date", DbType="DateTime NOT NULL")]
		public System.DateTime date
		{
			get
			{
				return this._date;
			}
			set
			{
				if ((this._date != value))
				{
					this._date = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="EVNT_Description", Storage="_description", DbType="VarChar(500) NOT NULL", CanBeNull=false)]
		public string description
		{
			get
			{
				return this._description;
			}
			set
			{
				if ((this._description != value))
				{
					this._description = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="EVNT_Name", Storage="_name", DbType="VarChar(200) NOT NULL", CanBeNull=false)]
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				if ((this._name != value))
				{
					this._name = value;
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.TB_WID_EventTag")]
	public partial class FindedTag
	{
		
		private string _name;
		
		private int _event_id;
		
		public FindedTag()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="ETAG_Name", Storage="_name", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				if ((this._name != value))
				{
					this._name = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="ETAG_EVNT_id", Storage="_event_id")]
		public int event_id
		{
			get
			{
				return this._event_id;
			}
			set
			{
				if ((this._event_id != value))
				{
					this._event_id = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
