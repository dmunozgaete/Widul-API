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

namespace API.Endpoints.Bpm.Models
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="AX_Inspecciones")]
	public partial class DocumentDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Definiciones de métodos de extensibilidad
    partial void OnCreated();
    partial void InsertV_Document(V_Document instance);
    partial void UpdateV_Document(V_Document instance);
    partial void DeleteV_Document(V_Document instance);
    partial void InsertV_DocumentTypes(V_DocumentTypes instance);
    partial void UpdateV_DocumentTypes(V_DocumentTypes instance);
    partial void DeleteV_DocumentTypes(V_DocumentTypes instance);
    #endregion
		
		public DocumentDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["WidulConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public DocumentDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DocumentDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DocumentDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DocumentDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<TransitionData> TransitionDatas
		{
			get
			{
				return this.GetTable<TransitionData>();
			}
		}
		
		public System.Data.Linq.Table<History> Histories
		{
			get
			{
				return this.GetTable<History>();
			}
		}
		
		public System.Data.Linq.Table<V_Document> V_Documents
		{
			get
			{
				return this.GetTable<V_Document>();
			}
		}
		
		public System.Data.Linq.Table<V_DocumentTypes> V_DocumentTypes
		{
			get
			{
				return this.GetTable<V_DocumentTypes>();
			}
		}
		
		public System.Data.Linq.Table<NotificationRecipient> NotificationRecipients
		{
			get
			{
				return this.GetTable<NotificationRecipient>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="")]
	public partial class TransitionData
	{
		
		private string _observation;
		
		public TransitionData()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_observation", CanBeNull=false)]
		public string observation
		{
			get
			{
				return this._observation;
			}
			set
			{
				if ((this._observation != value))
				{
					this._observation = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.TB_BPM_Bitacora")]
	public partial class History
	{
		
		private System.Nullable<System.DateTime> _updatedAt;
		
		private string _observation;
		
		private string _state_name;
		
		private System.Guid _state_token;
		
		private string _state_identifier;
		
		private string _entity_type;
		
		private string _entity_name;
		
		private System.Guid _entity_token;
		
		public History()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="BITA_Fecha", Storage="_updatedAt", DbType="DateTime NOT NULL")]
		public System.Nullable<System.DateTime> updatedAt
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="BITA_Observacion", Storage="_observation", DbType="Text NOT NULL", CanBeNull=false, UpdateCheck=UpdateCheck.Never)]
		public string observation
		{
			get
			{
				return this._observation;
			}
			set
			{
				if ((this._observation != value))
				{
					this._observation = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="ESTA_Nombre", Storage="_state_name", DbType="VarChar(200) NOT NULL", CanBeNull=false)]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="ESTA_Token", Storage="_state_token", DbType="UniqueIdentifier NOT NULL")]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="ESTA_Identificador", Storage="_state_identifier", DbType="Char(4) NOT NULL", CanBeNull=false)]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="TIEN_Identificador", Storage="_entity_type", DbType="Char(4)")]
		public string updatedBy_type
		{
			get
			{
				return this._entity_type;
			}
			set
			{
				if ((this._entity_type != value))
				{
					this._entity_type = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="ENTI_Identificador", Storage="_entity_name", DbType="VarChar(200) NOT NULL", CanBeNull=false)]
		public string updatedBy_name
		{
			get
			{
				return this._entity_name;
			}
			set
			{
				if ((this._entity_name != value))
				{
					this._entity_name = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="ENTI_Token", Storage="_entity_token", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid updatedBy_token
		{
			get
			{
				return this._entity_token;
			}
			set
			{
				if ((this._entity_token != value))
				{
					this._entity_token = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.VT_Documentos")]
	public partial class V_Document : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _token;
		
		private string _identifier;
		
		private System.DateTime _createdAt;
		
		private System.Guid _type_token;
		
		private string _type_name;
		
		private string _type_identifier;
		
		private System.Guid _state_token;
		
		private string _state_name;
		
		private string _state_identifier;
		
		private System.Guid _entity_token;
		
		private string _entity_identifier;
		
		private System.Guid _entityType_token;
		
		private string _entityType_name;
		
		private string _entityType_identifier;
		
		private string _lastObservation;
		
    #region Definiciones de métodos de extensibilidad
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OntokenChanging(System.Guid value);
    partial void OntokenChanged();
    partial void OnidentifierChanging(string value);
    partial void OnidentifierChanged();
    partial void OncreatedAtChanging(System.DateTime value);
    partial void OncreatedAtChanged();
    partial void Ontype_tokenChanging(System.Guid value);
    partial void Ontype_tokenChanged();
    partial void Ontype_nameChanging(string value);
    partial void Ontype_nameChanged();
    partial void Ontype_identifierChanging(string value);
    partial void Ontype_identifierChanged();
    partial void Onstate_tokenChanging(System.Guid value);
    partial void Onstate_tokenChanged();
    partial void Onstate_nameChanging(string value);
    partial void Onstate_nameChanged();
    partial void Onstate_identifierChanging(string value);
    partial void Onstate_identifierChanged();
    partial void Onentity_tokenChanging(System.Guid value);
    partial void Onentity_tokenChanged();
    partial void Onentity_identifierChanging(string value);
    partial void Onentity_identifierChanged();
    partial void OnentityType_tokenChanging(System.Guid value);
    partial void OnentityType_tokenChanged();
    partial void OnentityType_nameChanging(string value);
    partial void OnentityType_nameChanged();
    partial void OnentityType_identifierChanging(string value);
    partial void OnentityType_identifierChanged();
    partial void OnlastObservationChanging(string value);
    partial void OnlastObservationChanged();
    #endregion
		
		public V_Document()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="DOCU_Token", Storage="_token", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="DOCU_Identificador", Storage="_identifier", DbType="Char(9) NOT NULL", CanBeNull=false)]
		public string identifier
		{
			get
			{
				return this._identifier;
			}
			set
			{
				if ((this._identifier != value))
				{
					this.OnidentifierChanging(value);
					this.SendPropertyChanging();
					this._identifier = value;
					this.SendPropertyChanged("identifier");
					this.OnidentifierChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="DOCU_Fecha", Storage="_createdAt", DbType="DateTime NOT NULL")]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="TIDO_Token", Storage="_type_token", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid type_token
		{
			get
			{
				return this._type_token;
			}
			set
			{
				if ((this._type_token != value))
				{
					this.Ontype_tokenChanging(value);
					this.SendPropertyChanging();
					this._type_token = value;
					this.SendPropertyChanged("type_token");
					this.Ontype_tokenChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="TIDO_Nombre", Storage="_type_name", DbType="VarChar(200) NOT NULL", CanBeNull=false)]
		public string type_name
		{
			get
			{
				return this._type_name;
			}
			set
			{
				if ((this._type_name != value))
				{
					this.Ontype_nameChanging(value);
					this.SendPropertyChanging();
					this._type_name = value;
					this.SendPropertyChanged("type_name");
					this.Ontype_nameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="TIDO_Identificador", Storage="_type_identifier", DbType="Char(4) NOT NULL", CanBeNull=false)]
		public string type_identifier
		{
			get
			{
				return this._type_identifier;
			}
			set
			{
				if ((this._type_identifier != value))
				{
					this.Ontype_identifierChanging(value);
					this.SendPropertyChanging();
					this._type_identifier = value;
					this.SendPropertyChanged("type_identifier");
					this.Ontype_identifierChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="ESTA_Token", Storage="_state_token", DbType="UniqueIdentifier NOT NULL")]
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
					this.Onstate_tokenChanging(value);
					this.SendPropertyChanging();
					this._state_token = value;
					this.SendPropertyChanged("state_token");
					this.Onstate_tokenChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="ESTA_Nombre", Storage="_state_name", DbType="VarChar(200) NOT NULL", CanBeNull=false)]
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
					this.Onstate_nameChanging(value);
					this.SendPropertyChanging();
					this._state_name = value;
					this.SendPropertyChanged("state_name");
					this.Onstate_nameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="ESTA_Identificador", Storage="_state_identifier", DbType="Char(4) NOT NULL", CanBeNull=false)]
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
					this.Onstate_identifierChanging(value);
					this.SendPropertyChanging();
					this._state_identifier = value;
					this.SendPropertyChanged("state_identifier");
					this.Onstate_identifierChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="ENTI_Token", Storage="_entity_token", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid entity_token
		{
			get
			{
				return this._entity_token;
			}
			set
			{
				if ((this._entity_token != value))
				{
					this.Onentity_tokenChanging(value);
					this.SendPropertyChanging();
					this._entity_token = value;
					this.SendPropertyChanged("entity_token");
					this.Onentity_tokenChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="ENTI_Identificador", Storage="_entity_identifier", DbType="VarChar(200) NOT NULL", CanBeNull=false)]
		public string entity_identifier
		{
			get
			{
				return this._entity_identifier;
			}
			set
			{
				if ((this._entity_identifier != value))
				{
					this.Onentity_identifierChanging(value);
					this.SendPropertyChanging();
					this._entity_identifier = value;
					this.SendPropertyChanged("entity_identifier");
					this.Onentity_identifierChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="TIEN_Token", Storage="_entityType_token", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid entityType_token
		{
			get
			{
				return this._entityType_token;
			}
			set
			{
				if ((this._entityType_token != value))
				{
					this.OnentityType_tokenChanging(value);
					this.SendPropertyChanging();
					this._entityType_token = value;
					this.SendPropertyChanged("entityType_token");
					this.OnentityType_tokenChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="TIEN_Nombre", Storage="_entityType_name", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string entityType_name
		{
			get
			{
				return this._entityType_name;
			}
			set
			{
				if ((this._entityType_name != value))
				{
					this.OnentityType_nameChanging(value);
					this.SendPropertyChanging();
					this._entityType_name = value;
					this.SendPropertyChanged("entityType_name");
					this.OnentityType_nameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="TIEN_Identificador", Storage="_entityType_identifier", DbType="Char(4)")]
		public string entityType_identifier
		{
			get
			{
				return this._entityType_identifier;
			}
			set
			{
				if ((this._entityType_identifier != value))
				{
					this.OnentityType_identifierChanging(value);
					this.SendPropertyChanging();
					this._entityType_identifier = value;
					this.SendPropertyChanged("entityType_identifier");
					this.OnentityType_identifierChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="BITA_Observacion", Storage="_lastObservation", DbType="Text", UpdateCheck=UpdateCheck.Never)]
		public string lastObservation
		{
			get
			{
				return this._lastObservation;
			}
			set
			{
				if ((this._lastObservation != value))
				{
					this.OnlastObservationChanging(value);
					this.SendPropertyChanging();
					this._lastObservation = value;
					this.SendPropertyChanged("lastObservation");
					this.OnlastObservationChanged();
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.VT_TipoDocumentos")]
	public partial class V_DocumentTypes : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _name;
		
		private string _description;
		
		private string _identifier;
		
		private System.Guid _token;
		
    #region Definiciones de métodos de extensibilidad
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnnameChanging(string value);
    partial void OnnameChanged();
    partial void OndescriptionChanging(string value);
    partial void OndescriptionChanged();
    partial void OnidentifierChanging(string value);
    partial void OnidentifierChanged();
    partial void OntokenChanging(System.Guid value);
    partial void OntokenChanged();
    #endregion
		
		public V_DocumentTypes()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="TIDO_Nombre", Storage="_name", DbType="VarChar(200) NOT NULL", CanBeNull=false)]
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
					this.OnnameChanging(value);
					this.SendPropertyChanging();
					this._name = value;
					this.SendPropertyChanged("name");
					this.OnnameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="TIDO_Descripcion", Storage="_description", DbType="VarChar(500) NOT NULL", CanBeNull=false)]
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
					this.OndescriptionChanging(value);
					this.SendPropertyChanging();
					this._description = value;
					this.SendPropertyChanged("description");
					this.OndescriptionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="TIDO_Identificador", Storage="_identifier", DbType="Char(4) NOT NULL", CanBeNull=false)]
		public string identifier
		{
			get
			{
				return this._identifier;
			}
			set
			{
				if ((this._identifier != value))
				{
					this.OnidentifierChanging(value);
					this.SendPropertyChanging();
					this._identifier = value;
					this.SendPropertyChanged("identifier");
					this.OnidentifierChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="TIDO_Token", Storage="_token", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="")]
	public partial class NotificationRecipient
	{
		
		private string _notification;
		
		private string _documentType;
		
		private string _state;
		
		private string _recipient;
		
		private string _type;
		
		public NotificationRecipient()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="Notificacion", Storage="_notification", CanBeNull=false)]
		public string notification
		{
			get
			{
				return this._notification;
			}
			set
			{
				if ((this._notification != value))
				{
					this._notification = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="TIDO_Nombre", Storage="_documentType", CanBeNull=false)]
		public string documentType
		{
			get
			{
				return this._documentType;
			}
			set
			{
				if ((this._documentType != value))
				{
					this._documentType = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_state", CanBeNull=false)]
		public string state
		{
			get
			{
				return this._state;
			}
			set
			{
				if ((this._state != value))
				{
					this._state = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_recipient", CanBeNull=false)]
		public string recipient
		{
			get
			{
				return this._recipient;
			}
			set
			{
				if ((this._recipient != value))
				{
					this._recipient = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_type", CanBeNull=false)]
		public string type
		{
			get
			{
				return this._type;
			}
			set
			{
				if ((this._type != value))
				{
					this._type = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
